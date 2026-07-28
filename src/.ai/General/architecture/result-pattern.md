# Result Pattern

Version: 1.0

---

# Purpose

This document defines the principles behind the Result Pattern used throughout the repository.

The Result Pattern provides a consistent mechanism for representing expected outcomes without relying on exceptions.

Its purpose is to make business behaviour explicit, predictable, and testable.

---

# Objectives

The Result Pattern exists to:

* Represent expected business outcomes.
* Eliminate exception-driven business logic.
* Improve readability.
* Improve testability.
* Make failures explicit.
* Separate business failures from system failures.

---

# Core Principle

Expected business outcomes should be represented by a Result.

Unexpected failures should be represented by exceptions.

Never use exceptions for normal business flow.

---

# Success

A successful Result represents the successful completion of a business operation.

Success should clearly communicate:

* The operation completed.
* Any produced value.
* Any relevant metadata.

---

# Failure

A failed Result represents an expected business outcome that prevented the requested operation from completing.

Examples include:

* Duplicate entity.
* Invalid business rule.
* Missing aggregate.
* Invalid state transition.
* Domain validation failure.

Failures should be explicit.

---

# Business Failures

Business failures are not exceptional.

Examples:

* Customer already exists.
* Invoice is already paid.
* Product is unavailable.
* Credit limit exceeded.

These outcomes are part of normal business behaviour.

---

# Exceptions

Exceptions represent situations that should not normally occur.

Examples:

* Database unavailable.
* Network failure.
* Serialization failure.
* Programming defect.
* Invalid configuration.

Exceptions should never be converted into business Results unless explicitly required by repository policy.

---

# Explicit Outcomes

Every operation should communicate its outcome explicitly.

Consumers should never need to inspect:

* Exception messages.
* Null values.
* Magic return values.
* Boolean flags.

The outcome should be obvious.

---

# No Null Results

A Result should never be null.

Operations should always return a valid outcome.

Null creates ambiguity.

---

# Rich Failures

Failures should contain enough information to allow the caller to make an appropriate decision.

Failure information should be:

* Predictable.
* Structured.
* Consistent.
* Meaningful.

---

# Business Language

Failure information should use business terminology.

Avoid infrastructure-specific wording.

Good:

* CustomerAlreadyExists
* InvoiceAlreadyPaid
* InvalidInvoiceStatus

Avoid:

* SqlException
* DbUpdateException
* InvalidOperationException

outside infrastructure boundaries.

---

# Layer Responsibilities

## Domain

The Domain produces business outcomes.

Business rules should communicate expected failures explicitly.

---

## Application

The Application coordinates Results.

It should:

* Propagate business failures.
* Translate failures when appropriate.
* Avoid hiding business intent.

---

## Infrastructure

Infrastructure translates technical failures.

Infrastructure should never expose provider-specific failure details outside its boundary.

---

## API

The API translates Results into transport-specific responses.

Transport protocols must not influence business behaviour.

---

# Composition

Operations should compose naturally.

The Result Pattern should support building larger workflows from smaller operations without relying on exceptions for expected outcomes.

---

# Readability

Code using the Result Pattern should clearly communicate:

* What happened.
* Why it happened.
* What the next decision should be.

Business workflows should remain easy to follow.

---

# Validation

Validation failures should produce explicit business outcomes.

Validation should not rely on exceptions.

---

# Logging

Expected business failures should generally not be logged as application errors.

Unexpected exceptions should be logged according to the repository logging strategy.

---

# Testing

Every business outcome should be testable.

Tests should verify:

* Successful outcomes.
* Business failures.
* Error propagation.
* State consistency.

Avoid tests that depend on exception messages for expected behaviour.

---

# Consistency

All business operations should communicate outcomes consistently.

Developers should not need to learn different failure mechanisms for different parts of the repository.

---

# Anti-Patterns

Avoid:

* Returning null.
* Returning boolean success flags.
* Returning magic values.
* Throwing exceptions for expected business outcomes.
* Mixing exceptions and business failures for the same scenario.
* Hiding failure reasons.

---

# Review Checklist

Before completing an implementation, verify:

* Are expected outcomes represented explicitly?
* Are exceptions reserved for unexpected failures?
* Are business failures easy to understand?
* Is failure information consistent?
* Is business intent preserved?
* Are outcomes fully testable?

---

# Guiding Principle

Business behaviour should be communicated through explicit Results.

Exceptions exist to report unexpected failures—not expected business decisions.
