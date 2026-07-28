# AI Code Generation Rules

Version: 1.0

---

# Purpose

This document defines the mandatory rules every AI agent must follow when generating code for this repository.

Its goal is to ensure that AI-generated code is indistinguishable from code written by the engineering team.

These rules apply to all generated code regardless of the requested feature.

---

# Primary Rule

The AI must never optimise for speed.

The AI must optimise for architectural correctness.

Correct architecture always takes priority over generating code quickly.

---

# Repository First

The repository is the source of truth.

If the repository conventions differ from general best practices, the repository conventions always win.

---

# Never Invent Conventions

Never introduce:

* New architectural styles
* New naming conventions
* New folder structures
* New patterns
* New helper classes
* New abstractions

unless explicitly requested.

---

# Search Before Generate

Before generating any code, search for:

* Similar features
* Existing abstractions
* Existing interfaces
* Existing repositories
* Existing utilities
* Existing tests

Reuse existing implementations whenever possible.

---

# Generate Complete Features

When implementing a business capability, generate every required artefact.

A complete feature may require:

* Domain Model
* Value Objects
* Domain Events
* Commands
* Queries
* Validators
* Handlers
* Repository Contracts
* Repository Implementations
* Persistence Configuration
* Dependency Registration
* API Endpoints
* Unit Tests
* Integration Tests
* Documentation

Never generate only one file when multiple artefacts are required.

---

# Preserve Architecture

The AI must preserve:

* Clean Architecture
* Dependency direction
* Module boundaries
* Repository conventions
* Folder structure
* Namespace structure

Architecture must never be violated.

---

# Preserve Existing Behaviour

When modifying existing code:

* Preserve behaviour.
* Preserve public contracts.
* Preserve compatibility unless instructed otherwise.

Avoid unnecessary refactoring.

---

# Produce Production Code

Generated code must:

* Compile successfully.
* Be production-ready.
* Be fully implemented.
* Contain no placeholders.
* Contain no TODO comments.
* Contain no mock implementations.

---

# Generate Readable Code

Generated code should prioritise:

* Readability
* Maintainability
* Simplicity
* Explicitness

Avoid clever implementations.

---

# Avoid Duplication

Before writing new code, determine whether equivalent behaviour already exists.

Duplicate code increases long-term maintenance cost.

---

# Explicit Dependencies

Dependencies must be explicit.

Never use:

* Service Locator
* Static service resolution
* Hidden dependencies
* Manual dependency construction

---

# Preserve Business Logic

Business rules belong only inside the Domain layer.

The AI must never place business logic inside:

* Controllers
* Endpoints
* Repositories
* DbContext
* Infrastructure Services

---

# Error Handling

Expected business failures must use the repository's Result Pattern.

Unexpected failures should use exceptions.

Do not introduce alternative error handling mechanisms.

---

# Testing

Generated features must include appropriate tests.

Tests should verify:

* Success scenarios
* Failure scenarios
* Edge cases
* Business rules

Testing is part of implementation.

---

# Documentation

If a generated feature introduces:

* New architecture
* New conventions
* Public APIs
* Important business behaviour

the AI should also update the corresponding documentation.

---

# Naming

Generated names must:

* Use repository terminology.
* Follow business language.
* Match existing conventions.

Never invent inconsistent naming.

---

# Formatting

Generated code should match the formatting style already used within the repository.

Formatting should not reveal that code was AI-generated.

---

# Performance

Avoid:

* Reflection
* Unnecessary allocations
* Multiple enumeration
* Blocking asynchronous code
* N+1 queries

Optimise only when necessary.

---

# Security

Generated code must never:

* Expose secrets.
* Disable validation.
* Ignore authorization.
* Leak sensitive information.
* Bypass repository security mechanisms.

---

# Refactoring

During refactoring:

* Preserve behaviour.
* Reduce complexity.
* Improve readability.
* Reduce duplication.

Do not introduce unrelated improvements.

---

# Self Review

Before producing the final answer, verify:

* Architecture is preserved.
* Repository conventions are followed.
* Code compiles.
* Dependencies are correct.
* Business rules are correctly located.
* Tests are complete.
* Documentation is updated where required.

---

# Completion Criteria

Generated code is complete only when:

* Every required artefact exists.
* Architecture is preserved.
* Tests are included.
* Documentation is updated where required.
* No repository rule has been violated.

---

# AI Promise

The AI should behave like a senior software architect working on a long-lived enterprise system.

Every generated line of code should improve the repository rather than merely satisfy the immediate request.

---

# Guiding Principle

The success of AI-generated code is measured by how well it integrates into the existing repository—not by how quickly it was produced.
