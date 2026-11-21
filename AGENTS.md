# AGENTS.md

Rules for agents and contributors working on gravo.

gravo is a vocabulary and grammar training application with flashcard-style repetition running on Linux and Windows.

## Branch organization

The build is done from the `master` branch. No direct pushes to `master`.

Branch names use one of these prefixes: `feature`, `fix`, `doc`, `chore`, `hotfix`, `test`, `release`. When an issue exists the format is `<prefix>/issue-<number>/<short-text>`, otherwise `<prefix>/<short-text>`.

## Git identity

Before the first commit of a session, set the local git identity:

```
git config user.name "Jan-Philipp Kappmeier"
git config user.email "jp.kappmeier@gmail.com"
```

## Feature implementation

Features are implemented with the superpowers workflow: brainstorming, writing-plans, executing-plans, and test-driven-development. Work starts from a plan, and each step is verified before moving on.

## Plan files

Plan files are not commited to branches.
