# AI review workflow scaffold — design (2026-11-19)

## Intent

Issue #16 (sub-issue of #15): create `.github/workflows/ai-review.yml`, the scaffold
for the opt-in AI pull request review pilot. The actual review run (Mistral Vibe,
inline comments) lands in later sub-issues; this file only wires triggers, the
opt-in gate, permissions, checkout, and label creation.

## Goals

- Workflow triggers on `pull_request` (`opened`, `synchronize`, `labeled`) and
  `issue_comment` (`created`).
- The job runs only when the PR carries the `ai-review` label or a PR comment
  contains `/review`. Without opt-in, nothing happens and the run stays green.
- Permissions are exactly `contents: read` and `pull-requests: write`.
- The job checks out the PR head SHA (`github.event.pull_request.head.sha`).
- The repo labels `ai-review` and `skip-ai-review` are created if missing.

## Non-goals

- Running the review itself, posting comments, choosing a model, bot account.
- Any permission beyond the two listed above.

## Constraints

- File path: `.github/workflows/ai-review.yml`.
- Acceptance criteria in issue #16 are the binding spec, in particular the
  permission set, which must not grow.

## Approaches

1. **Single job with a job-level `if` gate on the event payload.** For
   `pull_request` events the gate checks `github.event.pull_request.labels.*.name`
   for `ai-review`; for `issue_comment` it checks that the comment is on a PR
   (`github.event.issue.pull_request`) and contains `/review`. Chosen: it is the
   idiomatic reading of "the job runs only when...", and a skipped job is a green,
   do-nothing outcome.
2. Gate in a shell step writing an output that later steps consume. Same
   permissions, more moving parts, no benefit for a scaffold. Rejected.
3. Separate job for label creation. Rejected: extra run noise, and it gains
   nothing because of the permission conflict below.

## Key finding and ruling

Creating repo labels through the API needs `issues: write`, but the mandated
permission set excludes it. Ruling: the label step creates both labels
idempotently (`gh label create`, skipped when the label already exists) and never
fails the job — on a 403 it emits a `::warning::` and continues. The
`permissions:` block stays exactly `contents: read` + `pull-requests: write`,
because that is the acceptance criterion.

For `issue_comment` events the payload has no `pull_request.head.sha`, so a
resolve step gets the head SHA via `gh pr view` (covered by
`pull-requests: write`) and checkout uses
`github.event.pull_request.head.sha || steps.pr.outputs.sha`.

## Left to the builder

The exact YAML: workflow name, trigger block, permission block, one job
`ai-review` on `ubuntu-latest` with the gate, the label step, the SHA resolve
step (only for `issue_comment`), checkout at the head SHA, and a placeholder
step marking where the review run goes later.
