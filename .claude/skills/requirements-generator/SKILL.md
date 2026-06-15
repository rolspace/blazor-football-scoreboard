---
name: requirements-generator
description: Generates a formal requirements document from a user story using the project's requirements template. Use this skill whenever the user asks to generate a requirements document, create requirements from a user story, write up requirements, or turn a user story into a requirements doc. Trigger even if the user just pastes a user story and asks to "document this" or "make requirements for this".
allowed-tools: Read, Glob, Write, Bash
---

# Requirements Generator Skill

## Purpose
Transform a user story into a formal requirements document using the project template, then save it to `docs/requirements/` with the correct sequential filename.

## Steps

### 1. Read the template

Read `docs/requirements-template.md` from the workspace root. This is the canonical structure for all requirements documents in this project.

### 2. Determine the next document number

List existing files in `docs/requirements/` using Glob with pattern `docs/requirements/*.md`. Files follow the naming pattern `NNN-description.md` (e.g., `001-hub-connection-failure-notification.md`). Find the highest existing number and increment by 1. Zero-pad to 3 digits (001, 002, 003...). If no files exist, start at 001.

### 3. Derive the filename description

Create a short, lowercase, hyphen-separated description (3-6 words) that captures the essence of the user story. This becomes the `[requirement description]` portion of the filename.

Good examples:
- User story about reconnection retry → `hub-reconnection-retry`
- User story about user login → `user-login-authentication`
- User story about score display → `live-score-display`

### 4. Fill in the template

Use the user story to populate the template. Apply careful judgment — the goal is a document a developer can implement from, not a mechanical fill-in.

**Introduction**: Describe the feature in 2-4 sentences. Explain the current gap or problem, what the feature does, and the value it delivers to users.

**Requirements section**: Break the user story down into individual, implementable requirements. Each requirement needs:
- A clear user story in the standard format (`As a [role], I want [feature], so that [benefit]`)
- Acceptance criteria written as WHEN/IF/THEN statements using the SHALL pattern

Write as many requirements as make sense — a single user story often decomposes into 2-5 requirements covering the happy path, edge cases, and error states.

**Non-Functional Requirements**: Fill in relevant sections based on what the feature touches. If a section genuinely does not apply, write "N/A" rather than leaving placeholders. Be specific — avoid generic statements. Name concrete thresholds where possible.

For testing sections, think about:
- Unit tests: what can be tested in isolation?
- Integration tests: what component interactions need end-to-end coverage?
- E2E tests: what user-visible flows need browser or system-level testing?

### 5. Create the folder if needed

Before writing, ensure `docs/requirements/` exists. If not, create it using:
- PowerShell: `New-Item -ItemType Directory -Force docs/requirements`
- Bash: `mkdir -p docs/requirements`

### 6. Write the file

Write the completed document to `docs/requirements/[NNN]-[description].md`.

Confirm to the user: the file path, the document number, and a one-sentence summary of what was generated.

## Naming convention

```
docs/requirements/001-first-feature-name.md
docs/requirements/002-second-feature-name.md
docs/requirements/003-your-new-requirement-here.md
```

## Quality bar

A good requirements document produced by this skill should:
- Let a developer implement the feature without asking clarifying questions
- Have acceptance criteria specific enough to write tests against
- Identify non-obvious edge cases (error states, empty states, concurrent users, invalid input)
- Be concise and direct — no filler phrases or redundant explanations
