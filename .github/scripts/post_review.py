import argparse
import json
import os
import re
import subprocess
import sys
from dataclasses import dataclass


@dataclass
class ReviewFinding:
    path: str
    line: int
    body: str


@dataclass
class Review:
    summary: str
    findings: list[ReviewFinding]


def parse_arguments():
    parser = argparse.ArgumentParser()
    parser.add_argument("--pr", required=True)
    parser.add_argument("--commit", required=True)
    parser.add_argument("--artifact", required=True)
    parser.add_argument("--dry-run", action="store_true")
    return parser.parse_args()


def load_review(artifact_path):
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
        if not isinstance(finding.path, str) or type(finding.line) is not int or not isinstance(finding.body, str):
            raise TypeError("each comment needs string 'path', int 'line', string 'body'")
        findings.append(finding)
    return Review(summary=summary, findings=findings)


def run_gh(arguments, dry_run):
    if dry_run:
        print("DRY-RUN: gh " + " ".join(arguments))
        return 0, ""
    process = subprocess.run(["gh"] + arguments, capture_output=True, text=True)
    if process.returncode != 0:
        print(f"::warning::gh {' '.join(arguments[:3])} failed: {process.stderr.strip()}")
    return process.returncode, process.stdout


def parse_diff_lines(diff_text):
    diff_lines = set()
    current_path = None
    new_line_number = 0
    inside_hunk = False
    for line in diff_text.splitlines():
        if line.startswith("diff --git "):
            inside_hunk = False
            current_path = None
            continue
        if line.startswith("+++ b/"):
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
    return_code, diff_text = run_gh(["pr", "diff", pr_number], dry_run=False)
    if return_code != 0:
        print("::warning::could not fetch the PR diff; all findings go to the summary")
        return set()
    return parse_diff_lines(diff_text)


def split_findings(findings, diff_lines):
    inline_findings = []
    off_diff_findings = []
    for finding in findings:
        if (finding.path, finding.line) in diff_lines:
            inline_findings.append(finding)
        else:
            off_diff_findings.append(finding)
    return inline_findings, off_diff_findings


def build_summary(summary, off_diff_findings):
    if not off_diff_findings:
        return summary
    skipped_lines = "\n".join(
        f"- `{finding.path}:{finding.line}`: {finding.body}" for finding in off_diff_findings
    )
    return summary + "\n\nNot placed inline (line not in the diff):\n" + skipped_lines


def post_inline_comment(args, repository, finding):
    return_code, _ = run_gh(
        ["api", f"repos/{repository}/pulls/{args.pr}/comments",
         "-f", f"commit_id={args.commit}",
         "-f", f"path={finding.path}",
         "-f", f"line={finding.line}",
         "-f", "side=RIGHT",
         "-f", f"body={finding.body}"],
        dry_run=args.dry_run,
    )
    if return_code != 0:
        print(f"::warning::failed to post inline comment on {finding.path}:{finding.line}")


def post_summary_comment(args, summary):
    return_code, _ = run_gh(["pr", "comment", args.pr, "--body", summary], dry_run=args.dry_run)
    if return_code != 0:
        print("::warning::failed to post the summary comment")


def main():
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
    post_summary_comment(args, build_summary(review.summary, off_diff_findings))
    return 0


if __name__ == "__main__":
    sys.exit(main())
