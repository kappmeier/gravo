# AI review workflow scaffold — implementation plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add `.github/workflows/ai-review.yml`, the opt-in scaffold for the AI pull request review pilot (issue #16, sub-issue of #15).

**Architecture:** One job `ai-review` on `ubuntu-latest`, gated by a job-level `if` on the event payload: the `ai-review` label for `pull_request` events, a `/review` comment on a PR for `issue_comment` events. Steps: idempotent best-effort creation of the labels `ai-review`/`skip-ai-review`, head-SHA resolve via `gh pr view` (only for `issue_comment`, whose payload has no `pull_request.head.sha`), checkout at the PR head SHA, placeholder run step.

**Tech Stack:** GitHub Actions YAML (actions/checkout@v4, matching `dotnetcore.yml`); verification via a python3 + PyYAML script kept in the plan workspace only, never committed.

**Spec:** `docs/superpowers/specs/2026-11-19-ai-review-workflow-design.md`

## Global Constraints

- The `permissions:` block is exactly `contents: read` and `pull-requests: write`; never add more.
- Triggers: `pull_request` with types `opened`, `synchronize`, `labeled`; `issue_comment` with type `created`.
- The checkout ref must reference `github.event.pull_request.head.sha`.
- Label creation is idempotent and must never fail the job (warn on 403).
- Branch `vibe/ai-review-scaffold-1aa97e`; never commit on master.
- Never commit `.vibe/` or `.superpowers/`; add paths explicitly instead of `git add -A`.

## Review Focus

- Non-opt-in PR: the job gate must evaluate false — the `if` must cover both event types and both opt-in conditions (label, `/review`).
- `issue_comment` on a plain issue (not a PR): gate false via the `github.event.issue.pull_request` term.
- Label step under the restricted token: must exit 0, so every `gh` call sits in an `if` condition or behind `||`.
- YAML 1.1 quirk: PyYAML parses the `on:` key as boolean `True`; the validation script must accept both `on` and `True`.

## Task 1: Scaffold `.github/workflows/ai-review.yml`

**Files:**
- Create: `.github/workflows/ai-review.yml`
- Workspace only, not committed: `<sdd-workspace>/validate_workflow.py`

**Interfaces:**
- Consumes: the spec's exact trigger types, permission values, gate terms, label names.
- Produces: the workflow file, proven by `validate_workflow.py`.

- [ ] **Step 1: Write the validation script** `<sdd-workspace>/validate_workflow.py` (python3 + PyYAML) asserting: file parses; `pull_request` types are exactly `opened`, `synchronize`, `labeled`; `issue_comment` types exactly `created`; `permissions` equals `{contents: read, pull-requests: write}`; the single job's `if` contains both opt-in conditions (`ai-review` label for `pull_request`, `/review` for `issue_comment` on a PR); a checkout step's `ref` contains `github.event.pull_request.head.sha`; a step runs `gh label create` covering both `ai-review` and `skip-ai-review`.
- [ ] **Step 2: Run the script to verify it fails**

  Run: `python3 <sdd-workspace>/validate_workflow.py`
  Expected: FAIL, "workflow file not found".

- [ ] **Step 3: Create `.github/workflows/ai-review.yml`** per the design doc: name `AI review`; trigger block; permission block; job `ai-review` with the gate `if`; steps in order — ensure labels, resolve PR head SHA (`if: github.event_name == 'issue_comment'`), checkout `ref: ${{ github.event.pull_request.head.sha || steps.pr.outputs.sha }}`, placeholder review step.
- [ ] **Step 4: Run the script to verify it passes**

  Run: `python3 <sdd-workspace>/validate_workflow.py`
  Expected: PASS with every check listed green.

- [ ] **Step 5: Commit**

  ```bash
  git add docs/superpowers
  git commit -m "docs: add design and plan for the ai-review workflow scaffold"
  git add .github/workflows/ai-review.yml
  git commit -m "ci: scaffold ai-review workflow with opt-in trigger"
  ```

## Task 2: Verify against the issue #16 acceptance criteria

**Files:**
- Verify: `.github/workflows/ai-review.yml`, `git status`.

**Interfaces:**
- Consumes: Task 1's workflow file and validation script.

- [ ] **Step 1: Re-run the validation script**

  Run: `python3 <sdd-workspace>/validate_workflow.py`
  Expected: PASS.

- [ ] **Step 2: Read the workflow file once against the issue bullets** — triggers, pilot gate, permissions, head-SHA checkout, label creation, opt-in-only execution.
- [ ] **Step 3: Check the working tree**

  Run: `git status --short`
  Expected: only intended files changed; `.vibe/` and `.superpowers/` never staged.

- [ ] **Step 4: Commit any fix the review finds** (none expected), then close the task with `task-done` using the validation command.
