# Architectural Decision Making

Version: 1.0

---

# Purpose

This document defines how AI agents and developers should make engineering decisions when implementing or modifying the repository.

Its purpose is to ensure that every technical decision is consistent, predictable, and aligned with the repository architecture.

---

# Primary Objective

The goal of every engineering decision is to maximise the long-term quality of the repository.

Short-term convenience must never compromise long-term maintainability.

---

# Decision Hierarchy

When multiple solutions are possible, always make decisions using the following order of precedence.

1. Business Requirements
2. Repository Architecture
3. Repository Handbook
4. Existing Repository Conventions
5. Existing Repository Patterns
6. Performance Requirements
7. Technology Best Practices
8. Personal Preference

A higher priority always overrides a lower priority.

---

# Business First

Every engineering decision must support the business.

Technology exists to implement business capabilities.

Technology must never dictate the business model.

---

# Preserve Existing Patterns

Before introducing a new pattern, search the repository.

If an equivalent pattern already exists, reuse it.

Consistency is more valuable than novelty.

---

# Architecture Before Code

Before writing code, determine:

* Which architectural layer owns the responsibility.
* Which module owns the business capability.
* Which existing abstractions already exist.
* Which dependencies are required.

Only then should implementation begin.

---

# Avoid Reinvention

Never introduce:

* New patterns
* New abstractions
* New libraries
* New helper classes

if an existing solution already satisfies the requirement.

---

# Prefer Existing Conventions

If multiple valid implementations exist, prefer the implementation that most closely resembles the surrounding code.

Developers should not be able to distinguish AI-generated code from handwritten code.

---

# Simplicity

Choose the simplest solution that correctly solves the problem.

Avoid unnecessary:

* Generic abstractions
* Design patterns
* Indirection
* Configuration
* Flexibility

Complexity must always be justified.

---

# Readability

When choosing between two equivalent implementations, prefer the one that is easier to understand.

Code is maintained for years.

Readability has long-term value.

---

# Explicit Behaviour

Prefer explicit behaviour over implicit behaviour.

Examples include:

* Explicit dependencies
* Explicit state transitions
* Explicit mappings
* Explicit validation

Avoid hidden behaviour.

---

# Future Evolution

Implementations should be easy to extend without requiring significant redesign.

Avoid premature optimisation for hypothetical future requirements.

---

# Performance

Performance is important.

However, optimisation should occur only after:

* Correctness
* Readability
* Maintainability

Measure before optimising.

---

# Security

Security requirements always override convenience.

Never bypass:

* Validation
* Authentication
* Authorization
* Input sanitisation

---

# Testability

When evaluating multiple implementations, prefer the one that is easier to test.

Testability usually indicates good design.

---

# Error Handling

Expected business failures should follow the repository Result Pattern.

Unexpected failures should use exceptions.

Do not invent new error handling mechanisms.

---

# Dependencies

Before introducing a dependency, verify:

* Does an equivalent dependency already exist?
* Does it belong in this layer?
* Does it increase coupling?
* Can an abstraction be reused?

Every dependency has a long-term maintenance cost.

---

# Refactoring Decisions

When refactoring:

* Preserve behaviour.
* Improve readability.
* Reduce complexity.
* Reduce duplication.
* Preserve architecture.

Never perform unrelated refactoring during feature implementation.

---

# Breaking Changes

Avoid breaking public contracts.

When unavoidable:

* Document the change.
* Minimise impact.
* Preserve compatibility where practical.

---

# AI Self-Review

Before producing the final implementation, ask:

* Did I preserve the architecture?
* Did I reuse existing patterns?
* Did I avoid unnecessary abstractions?
* Did I introduce unnecessary dependencies?
* Is the code consistent with the repository?
* Would this pass an architectural review?

If any answer is "No", continue refining the implementation.

---

# Anti-Patterns

Avoid making decisions based on:

* Personal preference
* Framework popularity
* Internet trends
* Convenience
* Excessive flexibility
* Premature optimisation

Repository consistency always has higher priority.

---

# Decision Checklist

Before implementing any solution, verify:

* Is the business requirement understood?
* Is the responsible layer identified?
* Is the responsible module identified?
* Does an existing implementation already exist?
* Is the simplest solution being used?
* Does the solution preserve architectural boundaries?
* Is the solution easy to test?
* Is the solution easy to maintain?
* Is the solution consistent with the repository?

---

# Guiding Principle

Every engineering decision should make the repository more consistent, more maintainable, and easier to evolve than it was before.
