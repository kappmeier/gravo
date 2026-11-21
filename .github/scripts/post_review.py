import argparse
import json
import os
import re
import subprocess
import sys


def gh(args, dry_run=False):
    if dry_run:
        print("DRY-RUN: gh " + " ".join(args))
        return 0, ""
    proc = subprocess.run(["gh"] + args, capture_output=True, text=True)
    if proc.returncode != 0:
        print(f"::warning::gh {' '.join(args[:3])} failed: {proc.stderr.strip()}")
    return proc.returncode, proc.stdout


def parse_diff_lines(diff_text):
    valid = set()
    path = None
    new_line = 0
    in_hunk = False
    for line in diff_text.splitlines():
        if line.startswith("diff --git "):
            in_hunk = False
            path = None
            continue
        if line.startswith("+++ b/"):
            path = line[len("+++ b/"):]
            continue
        m = re.match(r"@@ -\d+(?:,\d+)? \+(\d+)(?:,\d+)? @@", line)
        if m:
            in_hunk = True
            new_line = int(m.group(1))
            continue
        if not in_hunk or path is None or line.startswith("\\"):
            continue
        if not line.startswith("-"):
            valid.add((path, new_line))
            new_line += 1
    return valid


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--pr", required=True)
    ap.add_argument("--commit", required=True)
    ap.add_argument("--artifact", required=True)
    ap.add_argument("--dry-run", action="store_true")
    args = ap.parse_args()

    if not os.path.exists(args.artifact):
        print(f"::notice::review artifact '{args.artifact}' not found; nothing to post")
        return 0
    try:
        with open(args.artifact) as f:
            review = json.load(f)
        summary = review["summary"]
        comments = review["comments"]
    except Exception as e:
        print(f"::warning::could not read review artifact '{args.artifact}': {e}")
        return 0

    repo = os.environ.get("GITHUB_REPOSITORY", "")
    rc, diff = gh(["pr", "diff", args.pr])
    if rc == 0:
        valid = parse_diff_lines(diff)
    else:
        print("::warning::could not fetch the PR diff; all findings go to the summary")
        valid = set()

    skipped = []
    for c in comments:
        path, line, body = c.get("path"), c.get("line"), c.get("body")
        if (path, line) in valid:
            rc, _ = gh(["api", f"repos/{repo}/pulls/{args.pr}/comments",
                        "-f", f"commit_id={args.commit}",
                        "-f", f"path={path}",
                        "-f", f"line={line}",
                        "-f", "side=RIGHT",
                        "-f", f"body={body}"], dry_run=args.dry_run)
            if rc != 0:
                print(f"::warning::failed to post inline comment on {path}:{line}")
        else:
            skipped.append(f"- `{path}:{line}`: {body}")

    if skipped:
        summary += "\n\nNot placed inline (line not in the diff):\n" + "\n".join(skipped)
    rc, _ = gh(["pr", "comment", args.pr, "--body", summary], dry_run=args.dry_run)
    if rc != 0:
        print("::warning::failed to post the summary comment")
    return 0


if __name__ == "__main__":
    sys.exit(main())
