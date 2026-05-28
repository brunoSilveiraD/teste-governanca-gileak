# Dependabot Public Test Runbook

## Goal

Exercise `Dependabot version updates` in this public repository using the most predictable path:

- `npm` only
- valid manifest and lockfile
- green CI on Dependabot pull requests

## Current test baseline

- `.github/dependabot.yml` monitors only `npm`
- checks run `daily`
- no `target-branch` override, so updates target the default branch
- `package.json` pins `axios` to `1.6.0`
- `package-lock.json` is committed
- CI validates only the `npm` flow with `npm ci` and `npm test`

## Why this should create a Dependabot PR

As of 2026-05-28, the npm package page shows `axios` latest as `1.12.2`, while this repository pins `1.6.0`. That keeps the repository installable while leaving a clear version update for Dependabot to propose.

Source:
- https://www.npmjs.com/package/axios?activeTab=versions

## Expected outcome

After these changes are merged to `main`:

1. GitHub populates the Dependabot status tab in the dependency graph.
2. Dependabot opens an automated PR for `axios`.
3. The PR triggers CI.
4. CI passes.

## Evidence checklist

- Screenshot of the repository `Dependency graph` with the `Dependabot` tab populated
- Link to the Dependabot PR
- PR source branch and target branch
- CI run result on the PR
- PR diff showing `package.json` and `package-lock.json` updates

## Fallback if no PR appears

1. Check the repository Dependabot status/log page first.
2. Confirm the default branch is `main`.
3. Confirm the repository is still public.
4. If needed, pin `axios` to an older valid release and retry in a separate commit.
