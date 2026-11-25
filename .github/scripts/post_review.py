"""Reads a PR review artifact stored as review.json and post it on the PR.

Adds inline comments on changed lines and one single summary comment.
"""

import argparse
import json
import os
import re
import subprocess
import sys
from dataclasses import dataclass
from urllib.parse import quote


@dataclass
class ReviewFinding:
    """One inline finding from the review.
     
    Contains a file path, line on the new side of the diff, and the comment body.
    """

    path: str
    line: int
    body: str


@dataclass
class Review:
    """The parsed review artifact."""

    summary: str
    findings: list[ReviewFinding]


def parse_arguments():
    """CLI contract: --pr, --commit, --artifact, --dry-run."""
    parser = argparse.ArgumentParser()
    parser.add_argument("--pr", required=True)
    parser.add_argument("--commit", required=True)
    parser.add_argument("--artifact", required=True)
    parser.add_argument("--dry-run", action="store_true")
    return parser.parse_args()

def _is_str(value):
    """True when value is a string."""
    return isinstance(value, str)


def _is_int(value):
    """True when value is an int.
    
    Needs additional check as isinstance(True/False, int) returns True.
    """
    return isinstance(value, int) and not isinstance(value, bool)


def load_review(artifact_path):
    """Load and validate the artifact.
    
    Raises on unreadable file, bad JSON, missing keys, or wrong types.
    """
    with open(artifact_path, encoding="utf-8") as artifact_file:
        data = json.load(artifact_file)
    if not isinstance(data, dict):
        raise TypeError("artifact root must be a JSON object")
    summary = data["summary"]
    raw_comments = data["comments"]
    if not isinstance(summary, str) or not isinstance(raw_comments, list):
        raise TypeError("'summary' must be a string and 'comments' a list")
    findings = []
    for item in raw_comments:
        finding = ReviewFinding(path=item["path"], line=item["line"], body=item["body"])
        if not _is_str(finding.path) or not _is_int(finding.line) or not _is_str(finding.body):
            raise TypeError("each comment needs string 'path', int 'line', and string 'body'")
        findings.append(finding)
    return Review(summary=summary, findings=findings)


def run_gh(arguments, dry_run):
    """Run gh and return (returncode, stdout).
    
    The return code is always 0. Prints only the command in a dry-run.
    """
    if dry_run:
        print("DRY-RUN: gh " + " ".join(arguments))
        return 0, ""
    process = subprocess.run(["gh"] + arguments, capture_output=True, text=True, check=False)
    if process.returncode != 0:
        print(f"::warning::gh {' '.join(arguments[:3])} failed: {process.stderr.strip()}")
    return process.returncode, process.stdout


def parse_diff_lines(diff_text):
    """Collect the (path, line) pairs on the new side of the diff hunks."""
    diff_lines = set()
    current_path = None
    new_line_number = 0
    inside_hunk = False
    for line in diff_text.splitlines():
        if line.startswith("diff --git "):
            inside_hunk = False
            current_path = None
            continue
        if line.startswith("+++ b/") and not inside_hunk:
            current_path = line[len("+++ b/"):]
            continue
        hunk_header = re.match(r"@@ -\d+(?:,\d+)? \+(\d+)(?:,\d+)? @@", line)
        if hunk_header:
            inside_hunk = True
            new_line_number = int(hunk_header.group(1))
            continue
        if not inside_hunk or current_path is None or line.startswith("\\"):
            continue
        if not line.startswith("-"):
            diff_lines.add((current_path, new_line_number))
            new_line_number += 1
    return diff_lines


def fetch_diff_lines(pr_number):
    """Fetch the PR diff and return its new-side lines.
    
    Returns an empty set when the fetch fails.
    """
    return_code, diff_text = run_gh(["pr", "diff", pr_number], dry_run=False)
    if return_code != 0:
        print("::warning::could not fetch the PR diff; all findings go to the summary")
        return set()
    return parse_diff_lines(diff_text)


def split_findings(findings, diff_lines):
    """Partition findings by whether their (path, line) pair is part of the diff."""
    inline_findings = []
    off_diff_findings = []
    for finding in findings:
        if (finding.path, finding.line) in diff_lines:
            inline_findings.append(finding)
        else:
            off_diff_findings.append(finding)
    return inline_findings, off_diff_findings


def build_summary(summary, off_diff_findings):
    """Append findings that could not be placed inline to the summary text."""
    if not off_diff_findings:
        return summary
    skipped_lines = "\n".join(
        f"- `{finding.path}:{finding.line}`: {finding.body}" for finding in off_diff_findings
    )
    return summary + "\n\nNot placed inline (line not in the diff):\n" + skipped_lines


FILE_REF = re.compile(
    r"`(?P<bt_pl>[A-Za-z0-9_][A-Za-z0-9_.\-/~]*):(?P<bt_line>\d+)`"
    r"|`(?P<bt_path>[A-Za-z0-9_][A-Za-z0-9_.\-/~]*)`"
    r"|(?<![\w:/.])(?P<bare_path>[A-Za-z0-9_][A-Za-z0-9_.\-/~]*):(?P<bare_line>\d+)"
)


def file_url(path, line, repository, commit):
    """Blob URL of a file at the PR head SHA, with #L<line> when a line is given."""
    url = f"https://github.com/{repository}/blob/{commit}/{quote(path)}"
    if line:
        url += f"#L{line}"
    return url


def _looks_like_path(path):
    """True when the text plausibly names a repo file, not a word or host."""
    return "/" in path or "." in path


def _linkify_match(match, repository, commit):
    """Turn one path reference match into a markdown link pinned to the head SHA."""
    if match.group("bt_pl") is not None:
        path, line = match.group("bt_pl"), int(match.group("bt_line"))
    elif match.group("bt_path") is not None:
        path, line = match.group("bt_path"), None
    else:
        path, line = match.group("bare_path"), int(match.group("bare_line"))
    if not _looks_like_path(path):
        return match.group(0)
    label = f"{path}:{line}" if line else path
    return f"[{label}]({file_url(path, line, repository, commit)})"


def linkify_text(text, repository, commit):
    """Rewrite `path:line` refs and backticked paths into head-SHA blob links.

    One single substitution pass, so generated links are never re-processed.
    Returns the text unchanged when repository or commit is missing.
    """
    if not repository or not commit:
        return text
    return FILE_REF.sub(lambda match: _linkify_match(match, repository, commit), text)


def post_inline_comment(args, repository, finding):
    """Post one finding as an inline review comment on the PR."""
    return_code, _ = run_gh(
        ["api", f"repos/{repository}/pulls/{args.pr}/comments",
         "-f", f"commit_id={args.commit}",
         "-f", f"path={finding.path}",
         "-f", f"line={finding.line}",
         "-f", "side=RIGHT",
         "-f", f"body={linkify_text(finding.body, repository, args.commit)}"],
        dry_run=args.dry_run,
    )
    if return_code != 0:
        print(f"::warning::failed to post inline comment on {finding.path}:{finding.line}")


def post_summary_comment(args, summary):
    """Post the single summary comment on the PR."""
    return_code, _ = run_gh(["pr", "comment", args.pr, "--body", summary], dry_run=args.dry_run)
    if return_code != 0:
        print("::warning::failed to post the summary comment")


def main():
    """Load the review artifact and post it on the PR.
    
    Always exits 0 so a posting failure never fails CI.
    """
    args = parse_arguments()
    try:
        review = load_review(args.artifact)
    except FileNotFoundError:
        print(f"::notice::review artifact '{args.artifact}' not found; nothing to post")
        return 0
    except (OSError, json.JSONDecodeError, KeyError, TypeError, ValueError) as error:
        print(f"::warning::could not read review artifact '{args.artifact}': {error}")
        return 0

    repository = os.environ.get("GITHUB_REPOSITORY", "")
    diff_lines = fetch_diff_lines(args.pr)
    inline_findings, off_diff_findings = split_findings(review.findings, diff_lines)
    for finding in inline_findings:
        post_inline_comment(args, repository, finding)
    summary = linkify_text(build_summary(review.summary, off_diff_findings), repository, args.commit)
    post_summary_comment(args, summary)
    return 0


if __name__ == "__main__":
    sys.exit(main())
