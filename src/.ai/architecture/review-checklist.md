# Code Review Checklist

Version: 1.0

---

# Purpose

This document defines the mandatory review checklist that every AI agent and developer must execute before considering any implementation complete.

A feature is not complete when the code compiles.

A feature is complete only after it passes this review.

---

# Review Philosophy

The purpose of a review is to improve:

* Correctness
* Maintainability
* Consistency
* Architecture
* Readability
* Performance
* Security

A reviewer is protecting the repository, not judging the author.

---

# Review Order

Every review must follow this order.

1. Requirements
2. Architecture
3. Business Logic
4. Design
5. Correctness
6. Maintainability
7. Performance
8. Security
9. Testing
10. Documentation

Never skip steps.

---

# Requirements

Verify:

* The requested feature has been fully implemented.
* Nothing requested has been omitted.
* No unrelated behaviour has changed.
* No unnecessary functionality has been introduced.

---

# Architecture

Verify:

* Clean Architecture is preserved.
* Dependency direction is correct.
* Responsibilities remain separated.
* No architectural boundaries are violated.
* The Domain remains framework independent.

---

# Business Rules

Verify:

* Business rules exist only inside the Domain.
* Business terminology is used consistently.
* Domain invariants are protected.
* No business logic exists in the API or Infrastructure.

---

# Design

Verify:

* Responsibilities are clear.
* SOLID principles are respected where appropriate.
* Classes remain cohesive.
* Methods have a single responsibility.
* Dependencies are explicit.

---

# Naming

Verify:

* Business terminology is used.
* Naming is consistent with the repository.
* Types, methods, and variables communicate intent.
* No unnecessary abbreviations exist.

---

# Folder Structure

Verify:

* Files are located correctly.
* Folder structure follows repository conventions.
* Namespaces match folders.
* No unnecessary folders have been introduced.

---

# Dependency Injection

Verify:

* Constructor injection is used.
* No Service Locator exists.
* Dependencies are explicit.
* No hidden dependencies exist.

---

# Error Handling

Verify:

* Business failures use the Result Pattern.
* Exceptions are reserved for unexpected failures.
* Sensitive information is not exposed.
* Logging follows repository conventions.

---

# Correctness

Verify:

* Behaviour matches requirements.
* Edge cases have been considered.
* Invalid states are prevented.
* Null handling follows repository rules.
* Code compiles successfully.

---

# Readability

Verify:

* Intent is obvious.
* Complexity is justified.
* Code is self-documenting.
* Comments explain why rather than what.
* Formatting is consistent.

---

# Maintainability

Verify:

* Duplication is avoided.
* Abstractions are justified.
* Code is easy to modify.
* Responsibilities are well separated.
* Technical debt has not increased.

---

# Performance

Verify:

* No unnecessary allocations exist.
* No multiple enumeration occurs.
* Async code remains asynchronous.
* No blocking calls exist.
* No obvious performance regressions have been introduced.

Optimise only where justified.

---

# Security

Verify:

* Authorization is enforced.
* Validation is complete.
* Secrets are protected.
* Sensitive data is not exposed.
* Input is properly validated.
* Security boundaries remain intact.

---

# Persistence

Verify:

* Transactions are handled correctly.
* Repository abstractions are respected.
* No direct database access exists outside Infrastructure.
* Persistence concerns remain isolated.

---

# API

Verify:

* Endpoints remain thin.
* Requests and responses are mapped correctly.
* Business logic is not implemented inside endpoints.
* HTTP behaviour is consistent.

---

# Testing

Verify:

* Appropriate unit tests exist.
* Integration tests exist where required.
* Important business behaviour is covered.
* Edge cases are tested.
* Existing tests continue to pass.
* Code coverage is greather than 90%.

---

# Documentation

Verify:

* Public APIs remain documented where required.
* Architectural changes are documented.
* New conventions are documented.
* Obsolete documentation has been updated.

---

# AI Verification

Before finishing a task, the AI agent must ask itself:

* Did I follow every handbook rule?
* Did I preserve the architecture?
* Did I introduce any unnecessary abstractions?
* Did I create production-ready code?
* Would I approve this in a senior code review?

If any answer is "No", the implementation is not complete.

---

# Completion Checklist

A task is complete only if:

* Requirements are satisfied.
* Architecture is preserved.
* Code compiles.
* Tests pass.
* Naming is consistent.
* Documentation is updated.
* No review item has failed.

---

# Guiding Principle

The repository should be in a better state after every change.

Every review exists to ensure that quality never regresses.
