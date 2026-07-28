# Implementation Strategy

Version: 1.0

---

# Purpose

This document defines how an AI agent should approach implementing new features, bug fixes, refactorings, and enhancements within this repository.

A good implementation strategy produces software that is correct, maintainable, consistent, and aligned with the repository's architecture.

---

# Primary Principle

Understand before implementing.

Implementation begins with analysis, not coding.

---

# Understand the Problem

Before writing code, the AI must understand:

* The business objective.
* The technical requirements.
* The architectural context.
* Existing repository conventions.
* Expected behaviour.
* Constraints and assumptions.

Do not begin implementation while critical requirements remain unclear.

---

# Search Before Creating

Before creating any new type, the AI should search for existing:

* Services
* Interfaces
* Base classes
* Utilities
* Extension methods
* Validators
* Mappers
* Helpers

Reuse existing implementations whenever possible.

---

# Identify the Correct Layer

Every implementation belongs to a specific architectural layer.

Determine whether the change belongs in:

* API
* Application
* Domain
* Infrastructure
* Shared Kernel

Never place code in a layer simply because it is convenient.

---

# Preserve Existing Behaviour

Unless explicitly requested otherwise:

* Preserve public contracts.
* Preserve observable behaviour.
* Preserve compatibility.
* Minimise breaking changes.

Refactoring should improve the implementation without changing behaviour.

---

# Small, Incremental Changes

Prefer multiple small changes over one large change.

Each modification should represent one logical improvement.

Large unrelated changes should be avoided.

---

# Follow Repository Standards

Every generated implementation must follow repository standards for:

* Architecture
* Naming
* Dependency Injection
* Validation
* Exception handling
* Logging
* Testing
* Documentation

Repository conventions always take precedence.

---

# Prefer Explicit Implementations

Prefer:

* Explicit dependencies.
* Explicit mapping.
* Explicit validation.
* Explicit control flow.

Avoid hidden or implicit behaviour.

---

# Keep Responsibilities Focused

Every generated type should have one clear responsibility.

If a class grows beyond a single responsibility, consider decomposition.

Avoid introducing "God Classes."

---

# Write Readable Code

Optimise for future maintainers.

Prefer:

* Clear names.
* Straightforward control flow.
* Small methods.
* Minimal nesting.

Readable code is easier to review and maintain.

---

# Minimise Dependencies

Introduce the smallest set of dependencies required to solve the problem.

Avoid adding new libraries or frameworks unless they provide clear, demonstrable value.

---

# Handle Errors Deliberately

The AI should:

* Validate inputs.
* Preserve stack traces.
* Distinguish expected and unexpected failures.
* Follow repository exception handling conventions.

Do not suppress failures.

---

# Preserve Determinism

Generated implementations should behave deterministically.

Avoid:

* Hidden randomness.
* Unstable ordering.
* Environment-dependent behaviour unless explicitly required.

Deterministic code is easier to test and debug.

---

# Consider Testability

Before completing an implementation, consider:

* How the behaviour will be tested.
* Whether dependencies are explicit.
* Whether the code is deterministic.
* Whether observable behaviour is clear.

Implementation decisions should support testing naturally.

---

# Update Supporting Artifacts

When appropriate, implementation should also update:

* Tests
* Documentation
* Configuration
* Dependency registration
* Generated code
* Architecture documentation

Supporting artifacts should remain consistent with the implementation.

---

# Avoid Premature Optimisation

Optimise only when:

* Profiling indicates a bottleneck.
* Measurements justify optimisation.
* Repository conventions require it.

Readability and correctness take precedence.

---

# Respect Module Boundaries

Implementation must not bypass established module boundaries.

Cross-module communication should occur through approved contracts and abstractions.

---

# Explain Non-Obvious Decisions

When a solution involves meaningful trade-offs, explain:

* Why the approach was selected.
* Alternatives that were considered.
* Why they were rejected.

Do not explain obvious implementation details.

---

# AI Responsibilities

When implementing code, the AI must:

* Understand the problem first.
* Reuse existing repository patterns.
* Preserve architecture.
* Generate production-quality code.
* Keep implementations simple.
* Maintain deterministic behaviour.
* Update related artifacts when necessary.

---

# Anti-Patterns

Avoid:

* Coding before understanding the problem.
* Introducing unnecessary abstractions.
* Duplicating existing functionality.
* Mixing architectural layers.
* Hidden dependencies.
* Placeholder implementations.
* Unrelated refactoring within the same change.
* Premature optimisation.

---

# Implementation Checklist

Before completing an implementation, verify:

* Requirements are understood.
* Existing patterns have been reused.
* The correct architectural layer is used.
* Responsibilities remain focused.
* Behaviour is preserved.
* Dependencies are explicit.
* Tests and documentation are updated where required.
* Repository standards are fully respected.

---

# Guiding Principle

A successful implementation is not the one with the least code.

It is the one that integrates naturally into the existing architecture, solves the problem correctly, remains easy to maintain, and leaves the repository in a better state than before.
