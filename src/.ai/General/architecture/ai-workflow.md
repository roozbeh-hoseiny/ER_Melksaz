# AI Workflow

Version: 1.0

---

# Purpose

This document defines the mandatory workflow every AI agent must follow before generating, modifying, reviewing, or refactoring code.

The goal is to ensure every AI-generated change is consistent with the repository architecture and indistinguishable from code written by the engineering team.

This workflow is mandatory for every request.

---

# Guiding Principle

Never generate code immediately.

Always understand the repository before making changes.

The AI's first responsibility is preserving the architecture.

---

# Phase 1 — Understand the Request

Before writing code, identify:

* Business objective
* Functional requirements
* Non-functional requirements
* Expected behaviour
* Constraints
* Requested deliverables

If requirements are ambiguous, ask questions before implementation.

Never guess business behaviour.

---

# Phase 2 — Inspect the Repository

Search the repository for:

* Similar features
* Existing modules
* Existing patterns
* Existing abstractions
* Existing tests
* Existing conventions

Always reuse repository patterns before introducing new ones.

---

# Phase 3 — Identify the Architecture

Determine:

* Responsible module
* Responsible architectural layer
* Required dependencies
* Existing contracts
* Required infrastructure

Never implement functionality in the wrong layer.

---

# Phase 4 — Determine Required Artefacts

A feature may require multiple artefacts.

Typical artefacts include:

* Aggregate
* Entity
* Value Object
* Domain Event
* Command
* Query
* Validator
* Handler
* Repository
* Repository implementation
* EF Configuration
* Endpoint
* Request model
* Response model
* Mapping
* Dependency registration
* Unit tests
* Integration tests
* Documentation

Do not generate only the first file that comes to mind.

---

# Phase 5 — Review Existing Code

Before generating new code, verify whether equivalent functionality already exists.

Never duplicate:

* Business logic
* Validation
* Mapping
* Infrastructure
* Utilities

Reuse existing implementations whenever possible.

---

# Phase 6 — Generate Code

Generate code that:

* Compiles.
* Follows repository conventions.
* Uses repository libraries.
* Uses repository architecture.
* Matches repository naming.
* Matches repository formatting.
* Matches repository folder structure.

Generated code should not appear distinguishable from handwritten code.

---

# Phase 7 — Verify Architecture

After generation, verify:

* Dependency direction.
* Layer responsibilities.
* Module ownership.
* Business rule placement.
* Repository conventions.

Architecture takes precedence over implementation convenience.

---

# Phase 8 — Verify Behaviour

Verify:

* Business requirements.
* Edge cases.
* Validation.
* Failure scenarios.
* Success scenarios.

The implementation should satisfy the complete business requirement.

---

# Phase 9 — Generate Tests

Generate all required tests.

Typical test types include:

* Unit Tests
* Integration Tests
* Functional Tests

Tests are considered part of the implementation.

---

# Phase 10 — Perform Self Review

Review the implementation against:

* AGENTS.md
* Architecture documents
* Naming conventions
* Coding style
* Review checklist

Do not consider the task complete until all reviews pass.

---

# Code Generation Rules

Generated code must:

* Compile successfully.
* Follow repository architecture.
* Avoid placeholders.
* Avoid TODO comments.
* Avoid dead code.
* Avoid commented-out code.
* Avoid speculative abstractions.

---

# Refactoring Workflow

When refactoring:

1. Understand existing behaviour.
2. Preserve behaviour.
3. Improve readability.
4. Reduce complexity.
5. Remove duplication.
6. Preserve public contracts unless instructed otherwise.

---

# Bug Fix Workflow

When fixing defects:

1. Understand the root cause.
2. Avoid superficial fixes.
3. Preserve existing behaviour.
4. Add regression tests.
5. Verify surrounding code.

Never fix symptoms without understanding the cause.

---

# Review Workflow

Before submitting changes, verify:

* Architecture
* Naming
* Dependencies
* Performance
* Security
* Testing
* Documentation

Every implementation must pass the repository review checklist.

---

# AI Restrictions

The AI must never:

* Invent repository conventions.
* Ignore existing patterns.
* Introduce unrelated refactoring.
* Introduce unnecessary libraries.
* Generate placeholder implementations.
* Bypass architectural boundaries.
* Duplicate existing functionality.

---

# Completion Criteria

A task is complete only when:

* Requirements are satisfied.
* Architecture is preserved.
* Code compiles.
* Tests are generated.
* Existing tests continue to pass.
* Documentation is updated where required.
* Self-review has been completed.

---

# AI Self-Assessment

Before producing the final answer, verify:

* Did I understand the requirement?
* Did I inspect the repository?
* Did I reuse existing patterns?
* Did I preserve the architecture?
* Did I generate every required artefact?
* Did I include appropriate tests?
* Would this pass a senior architectural review?

If any answer is "No", continue improving the implementation.

---

# Guiding Principle

The AI is not measured by the amount of code it generates.

It is measured by how well it preserves the architecture, follows the repository standards, and produces production-ready software.
