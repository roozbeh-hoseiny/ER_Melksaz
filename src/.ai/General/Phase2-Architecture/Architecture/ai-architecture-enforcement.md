# AI Architecture Enforcement

Version: 1.0

---

# Purpose

This document defines the mandatory architectural rules that every AI agent must follow when generating, modifying, or reviewing code in this repository.

This document has higher priority than feature requests.

If a user request conflicts with these rules, the AI must preserve the architecture unless explicitly instructed to change the architecture itself.

---

# Primary Objective

The AI is an implementation agent.

It is **not** an architect.

Its responsibility is to implement solutions that are fully consistent with the repository architecture.

---

# Before Writing Any Code

The AI must identify:

* The business module.
* The architectural layer.
* The feature (Vertical Slice).
* The Aggregate involved.
* The Repository involved.
* The existing conventions.
* Existing patterns that should be reused.

The AI should understand the architecture before producing code.

---

# Never Invent a New Pattern

Before introducing:

* a base class,
* an abstraction,
* a helper,
* a factory,
* a builder,
* a pipeline,
* a service,

the AI must determine whether an equivalent pattern already exists.

Reuse is preferred over invention.

---

# Respect Module Boundaries

The AI must never:

* Access another module's internal implementation.
* Reference internal classes across modules.
* Bypass public contracts.
* Share business logic between modules.

Modules communicate only through published contracts.

---

# Respect Layer Boundaries

The AI must never place:

Business rules inside:

* API
* Infrastructure
* Handlers

Persistence logic inside:

* Domain

Transport logic inside:

* Domain
* Application

Framework code inside:

* Domain

---

# Respect the Dependency Rule

The AI must verify that:

* Dependencies point inward.
* No circular dependencies are introduced.
* Infrastructure implements abstractions.
* Domain remains framework independent.

Violations must be corrected before completing the task.

---

# Preserve Existing Naming

The AI should always follow existing repository naming.

Never rename existing concepts simply because another naming style is preferred.

Consistency is more valuable than personal preference.

---

# Preserve Existing Structure

The AI should place new code beside similar existing code.

Avoid creating unnecessary folders.

Avoid introducing new project structures.

The repository should grow naturally.

---

# Business Behaviour

Whenever business logic is required:

The AI must place it inside:

* Aggregate
* Entity
* Value Object
* Domain Service

Never inside:

* Controller
* Endpoint
* Handler
* Repository
* DbContext

---

# Repositories

Repositories manage Aggregate Roots.

The AI must never:

* Create repositories for child entities.
* Expose IQueryable.
* Leak EF Core.
* Place business logic inside repositories.

---

# CQRS

The AI must preserve CQRS.

Commands:

* Change state.

Queries:

* Read state.

They should never be combined.

---

# Vertical Slice

Each feature should remain independent.

The AI should generate all required artefacts inside the appropriate slice.

Examples include:

* Command
* Query
* Handler
* Validator
* Endpoint
* Tests

---

# Testing

Whenever generating production code, the AI should also generate:

* Unit Tests.
* Integration Tests.
* Test Fixtures.
* Builders (where repository conventions require them).

Generated tests should follow repository conventions.

---

# Documentation

When introducing:

* architectural changes,
* new patterns,
* new modules,

the AI should also update:

* ADRs
* Architecture documentation
* AI handbook

Documentation is part of the implementation.

---

# Refactoring

When refactoring:

The AI should:

* Preserve behaviour.
* Preserve public contracts.
* Reduce complexity.
* Improve readability.
* Reduce duplication.

Avoid unnecessary rewrites.

---

# Performance

The AI should:

* Avoid unnecessary allocations.
* Avoid unnecessary database calls.
* Avoid N+1 queries.
* Prefer async I/O.
* Respect repository performance conventions.

Optimisation should never reduce readability without measurable benefit.

---

# Security

The AI must:

* Validate input.
* Respect authorization.
* Avoid exposing sensitive information.
* Avoid insecure defaults.
* Preserve security boundaries.

Security is never optional.

---

# Self-Review

Before completing any task, the AI should internally verify:

* Architecture preserved.
* Naming consistent.
* Module boundaries respected.
* Layer boundaries respected.
* Dependency Rule preserved.
* Existing patterns reused.
* Tests included.
* Documentation updated where necessary.

If any item fails, improve the implementation before returning it.

---

# Forbidden Behaviours

The AI must never:

* Ignore repository conventions.
* Introduce speculative abstractions.
* Duplicate existing patterns.
* Move business logic outside the Domain.
* Introduce architectural violations.
* Break module boundaries.
* Invent new coding styles.
* Optimise prematurely.

---

# Completion Criteria

An implementation is complete only when:

* It follows the repository architecture.
* It follows repository conventions.
* It includes appropriate tests.
* It preserves consistency.
* It introduces no architectural violations.
* It would pass an architecture review without modification.

---

# Guiding Principle

The AI is expected to behave like a senior engineer joining an established codebase.

Its highest priority is preserving architectural consistency while delivering production-ready implementations.
