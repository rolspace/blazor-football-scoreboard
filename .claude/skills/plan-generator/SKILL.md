---
name: plan-generator
description: Generates an implementation plan from a requirements document using the project's plan template. Use this skill whenever the user asks to create an implementation plan, generate a plan from requirements, plan out a feature, or turn a requirements document into a dev plan. Trigger even if the user just names a requirements file and says "plan this" or "create a plan for this".
allowed-tools: Read, Glob, Grep, Write, PowerShell
---

# Plan Generator Skill

## Purpose

Transform a requirements document into a concrete implementation plan using the project's plan template, then save it to `docs/plans/` with the same number and name as the source requirements document.

## Steps

### 1. Locate the requirements document

If the user passed a path or number (e.g., `docs/requirements/002-api-integration-tests.md` or just `002`), resolve it to the exact file. If no argument was given, list `docs/requirements/*.md` and ask the user which one to plan — or, if there is only one without a matching plan, pick it automatically.

### 2. Read the template, requirements, and architecture

Read all three in parallel:
- `docs/plan-template.md` — canonical structure for all plans in this project
- The resolved requirements document
- `ARCHITECTURE.md` — the project's technical architecture, which the plan must not violate

### 3. Determine the output filename

Plans use the **same number and slug** as the requirements document they implement:

```
docs/requirements/002-api-integration-tests.md
                 → docs/plans/002-api-integration-tests.md
```

Extract the `NNN-description` portion from the requirements filename and reuse it verbatim.

### 4. Explore the codebase

Before writing the plan, read enough of the codebase to produce specific, accurate file paths and code references. Good plans name real files, not hypothetical ones.

Useful patterns:
- `Glob("src/**/*.cs")` to find existing source files relevant to the feature
- `Glob("tests/**/*.cs")` to understand what test projects and files already exist
- `Grep` for interfaces, base classes, or patterns the new code must follow

Focus your exploration on areas the requirements touch. You don't need a full audit — just enough to answer "where does this code live?" and "what already exists that the plan should build on or modify?"

### 5. Fill in the plan template

Populate each section with judgment, not mechanical fill-in. A good plan is something a developer can implement from without asking clarifying questions.

**Title**: `# Plan: [Feature Name]` — use the same name as the requirements document title.

**Context**: 3–5 sentences. Describe the current state of the code (what exists, what's missing, what's broken), then explain what the plan achieves and why. Reference the requirements document: `docs/requirements/NNN-description.md`.

**Files to Create / Modify / Delete**: Be specific. Use the real file paths you found during exploration. Omit tables whose rows would all be "N/A" — if nothing is deleted, drop that section entirely. For each file:
- *Create*: explain what the file is and why it's new
- *Modify*: explain what changes and why (not just "update this file")
- *Delete*: explain why removal is the right call, not just refactoring

**Implementation**: One numbered subsection per meaningful unit of work (a file, a class, a step). Include before/after code snippets when the change is non-obvious — especially for interface changes, new registrations, config additions, or subtle behavioral differences. C# snippets should compile against the project's actual types.

**Key Technical Decisions**: List the non-obvious choices. For each decision, explain why this approach was chosen over the obvious alternative. If there are no real decisions to make, omit the section rather than padding it.

**Tests to Add**: List concrete test method names in the `MethodName_Condition_ExpectedOutcome` format. For each test, describe the setup (what state or mocks) and the assertion (what is verified). Mirror the test naming and project conventions seen in the existing test files.

**Verification**: Always include the standard build + test commands. Add manual verification steps only if the change has UI or runtime behavior that tests cannot cover.

### 6. Architecture alignment check

Before finalizing the plan, verify it does not violate `ARCHITECTURE.md`. Check that:
- New files are placed in the correct layer (Domain, Application, host project, test project)
- Dependencies flow in the direction the architecture prescribes
- New abstractions or patterns are consistent with those already used in the codebase

If the requirements cannot be implemented without changing the architecture (e.g., a new cross-layer dependency, a new project, a new infrastructure concern), **do not silently deviate**. Instead, surface the conflict to the user:
- Describe exactly what would need to change in `ARCHITECTURE.md`
- Explain why the requirements drive that change
- Ask the user how they want to proceed before writing the plan

### 7. Write the plan

Ensure `docs/plans/` exists, then write the completed document to `docs/plans/NNN-description.md`.

Confirm to the user: the output file path and a one-sentence summary of what was planned.

---

## Naming convention

```
docs/requirements/001-hub-connection-failure-notification.md
               → docs/plans/001-hub-connection-failure-notification.md

docs/requirements/003-your-next-feature.md
               → docs/plans/003-your-next-feature.md
```

## Quality bar

A good plan produced by this skill should:
- Name real files at real paths, not placeholders like `path/to/file`
- Be implementable by a developer who has not read the requirements document
- Make every technical decision explicit, with the rationale stated
- Have test names specific enough that the developer knows exactly what to write
- Contain no filler — if a section has nothing to say, omit it
- Respect the architecture defined in `ARCHITECTURE.md` — if it can't, the conflict is surfaced to the user before writing anything
