"""Runs Mistral Vibe on a PR diff and writes the review artifact (review.json).

Builds the review prompt from the PR diff (gh pr diff) plus repo context
(README.md, Gravo.sln, project layout), runs the vibe CLI read-only in
programmatic mode, and extracts the JSON review into the artifact consumed by
post_review.py. Always exits 0: a missing API key or a failed review must not
block the PR.
"""

import argparse
import json
import os
import subprocess
import sys

PROMPT_TEMPLATE = """You are the AI reviewer for a pull request in the gravo repository: a vocabulary
and grammar trainer for German speakers learning English, French, Italian, Latin or Spanish,
running on .NET 10 on Linux and Windows. The domain logic lives in the VB.NET GravoCore
library, the UI in the C# Avalonia GravoApp.

Review the pull request diff below against the repo context. Look at:
- correctness and possible bugs in the changed code
- code quality and style (VB.NET, C#, aligned SQL, line length)
- fit with the existing architecture (logic belongs in GravoCore, UI in GravoApp)
- missing or weakened tests (CI enforces passed-count floors TEST_FLOOR and UI_TEST_FLOOR)

Write in English, concise and advisory. This review never blocks merging; do not request changes.

Output ONLY a single JSON object, no markdown fences and no other text:
{{"summary": "<one paragraph: what changed, risks, open questions>",
  "comments": [{{"path": "<changed file>", "line": <line number on the new side of the diff>,
                "body": "<inline finding>"}}]}}
Only emit comments whose (path, line) is an added or context line of the diff, using the
new-side line number. If there is nothing to flag inline, emit an empty comments list.

## Repo context

### README.md
{readme}

### Gravo.sln
{solution}

### Project layout
{layout}

## Pull request diff (PR #{pr_number})
{diff}
"""


def parse_arguments(argv=None):
    """CLI contract: --pr (required), --output (default review.json)."""
    parser = argparse.ArgumentParser()
    parser.add_argument("--pr", required=True)
    parser.add_argument("--output", default="review.json")
    return parser.parse_args(argv)


def run_command(command, stdin_text=None):
    """Run a command and return (returncode, stdout, stderr)."""
    process = subprocess.run(command, input=stdin_text, capture_output=True, text=True, check=False)
    return process.returncode, process.stdout, process.stderr


def gh_pr_diff(pr_number):
    """Fetch the PR diff. Returns an empty string when the fetch fails."""
    return_code, diff_text, error = run_command(["gh", "pr", "diff", pr_number])
    if return_code != 0:
        print(f"::warning::could not fetch the PR diff: {error.strip()}")
        return ""
    return diff_text


def read_repo_file(path):
    """Read a repo file for the prompt. Returns an empty string when unreadable."""
    try:
        with open(path, encoding="utf-8") as repo_file:
            return repo_file.read()
    except OSError as error:
        print(f"::warning::could not read {path}: {error}")
        return ""


def project_layout():
    """List the tracked files (without binary data files) as the project layout."""
    return_code, files, _ = run_command(["git", "ls-files"])
    if return_code != 0:
        return ""
    excluded = (".s3db", ".png")
    return "\n".join(line for line in files.splitlines() if not line.endswith(excluded))


def build_prompt(pr_number):
    """Assemble the review prompt from the PR diff and the repo context."""
    return PROMPT_TEMPLATE.format(
        readme=read_repo_file("README.md"),
        solution=read_repo_file("Gravo.sln"),
        layout=project_layout(),
        pr_number=pr_number,
        diff=gh_pr_diff(pr_number) or "(diff unavailable)",
    )


def extract_review_json(text):
    """Extract and validate the review JSON object from the vibe output.

    Raises ValueError on non-JSON output or a schema mismatch.
    """
    start = text.find("{")
    end = text.rfind("}")
    if start == -1 or end <= start:
        raise ValueError("no JSON object found in the output")
    data = json.loads(text[start:end + 1])
    if not isinstance(data, dict) or "summary" not in data or "comments" not in data:
        raise ValueError("review root must be an object with 'summary' and 'comments'")
    summary = data["summary"]
    comments = data["comments"]
    if not isinstance(summary, str) or not isinstance(comments, list):
        raise ValueError("'summary' must be a string and 'comments' a list")
    for item in comments:
        if not isinstance(item, dict) or not all(key in item for key in ("path", "line", "body")):
            raise ValueError("each comment needs 'path', 'line', and 'body'")
        if not isinstance(item["path"], str) or not isinstance(item["body"], str):
            raise ValueError("each comment needs string 'path' and string 'body'")
        if not isinstance(item["line"], int) or isinstance(item["line"], bool):
            raise ValueError("each comment needs int 'line'")
    return data


def run_vibe(prompt):
    """Run vibe read-only in programmatic mode with hard cost caps.

    Returns (returncode, stdout, stderr). Tool calls that need approval are
    denied in programmatic mode; write/edit/bash are disabled explicitly.
    """
    command = [
        "vibe", "-p",
        "--trust",
        "--model", os.environ.get("VIBE_MODEL", "mistral-small-latest"),
        "--max-turns", os.environ.get("VIBE_MAX_TURNS", "25"),
        "--max-price", os.environ.get("VIBE_MAX_PRICE", "1.00"),
        "--output", "text",
        "--disabled-tools", "write_file,edit,bash",
    ]
    return run_command(command, stdin_text=prompt)


def main(argv=None):
    """Run the review and write the artifact. Always returns 0."""
    args = parse_arguments(argv)
    if not os.environ.get("MISTRAL_API_KEY"):
        print("::notice::MISTRAL_API_KEY is not set; skipping the AI review")
        return 0
    prompt = build_prompt(args.pr)
    return_code, output, error = run_vibe(prompt)
    if return_code != 0:
        print(f"::warning::vibe exited with {return_code}: {error.strip()}")
        return 0
    try:
        review = extract_review_json(output)
    except (ValueError, json.JSONDecodeError, KeyError, TypeError) as error:
        print(f"::warning::could not extract the review JSON: {error}")
        return 0
    with open(args.output, "w", encoding="utf-8") as artifact_file:
        json.dump(review, artifact_file, indent=2)
        artifact_file.write("\n")
    print(f"review written to {args.output}: {len(review['comments'])} inline finding(s)")
    return 0


if __name__ == "__main__":
    sys.exit(main())
