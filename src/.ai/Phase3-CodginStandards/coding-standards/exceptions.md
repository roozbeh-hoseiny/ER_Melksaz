# Exception Handling

Version: 1.0

---

# Purpose

This document defines the mandatory exception handling rules for the repository.

Exceptions communicate unexpected failures.

They must never be used to control normal business flow.

---

# Primary Principle

Exceptions represent exceptional situations.

Expected business outcomes should be represented explicitly.

---

# Expected vs Unexpected

Distinguish between:

Expected:

* Validation failures
* Business rule violations
* Entity not found (when expected)
* Duplicate requests
* Authorization failures

Unexpected:

* Database failures
* Network failures
* Serialization failures
* Infrastructure failures
* Programming errors

Expected failures should not normally rely on exceptions.

---

# Business Failures

Business rules should preferably return a repository-approved Result type when applicable.

Avoid throwing exceptions for ordinary business decisions.

Example:

```text id="v8g4mh"
InsufficientCredit

DuplicateInvoice

OrderAlreadyCancelled
```

These are expected outcomes.

---

# Infrastructure Failures

Infrastructure failures may throw exceptions.

Examples:

* SQL Server unavailable
* Redis unavailable
* HTTP timeout
* Message broker unavailable

These represent unexpected system failures.

---

# Fail Fast

Validate arguments immediately.

Example:

```csharp id="t6r2wa"
ArgumentNullException.ThrowIfNull(customer);
```

Invalid input should fail as early as possible.

---

# Never Swallow Exceptions

Avoid:

```csharp id="w9d3nf"
try
{
    ...
}
catch
{
}
```

Every exception must be:

* handled,
* translated,
* logged,
* or allowed to propagate.

---

# Preserve Stack Trace

Never write:

```csharp id="r4z8lm"
throw ex;
```

Always use:

```csharp id="j7x2qh"
throw;
```

This preserves the original stack trace.

---

# Catch Specific Exceptions

Catch only the exceptions you can meaningfully handle.

Prefer:

```text id="b3m7ke"
SqlException

TimeoutException

OperationCanceledException
```

Avoid:

```text id="z5h1cr"
Exception
```

unless implementing a top-level exception boundary.

---

# Top-Level Handling

Unhandled exceptions should be caught at application boundaries.

Examples:

* API middleware
* Background workers
* Message consumers
* CLI entry points

Lower layers should not attempt global exception handling.

---

# Logging

Log exceptions exactly once.

Prefer logging at architectural boundaries.

Avoid duplicate logging as exceptions propagate.

---

# Domain Layer

The Domain may throw exceptions only for truly exceptional invariant violations.

Business rules should generally communicate expected outcomes without exceptions when repository conventions provide an alternative.

The Domain must never throw infrastructure-specific exceptions.

---

# Application Layer

The Application Layer may:

* Translate infrastructure exceptions.
* Coordinate retries when appropriate.
* Convert failures into application results.

It should not hide unexpected failures.

---

# Infrastructure Layer

Infrastructure may throw implementation-specific exceptions internally.

Where appropriate, translate them into repository-approved abstractions before crossing architectural boundaries.

---

# API Layer

The API converts exceptions into appropriate HTTP responses.

Examples:

* 400 Bad Request
* 401 Unauthorized
* 403 Forbidden
* 404 Not Found
* 409 Conflict
* 500 Internal Server Error

Clients should never receive raw exception details.

---

# Custom Exceptions

Create custom exception types only when they communicate meaningful intent.

Examples:

```text id="q4k9vd"
BusinessRuleViolationException

ConcurrencyException

ExternalServiceUnavailableException
```

Avoid unnecessary exception hierarchies.

---

# Exception Messages

Messages should:

* Clearly explain the failure.
* Avoid implementation details.
* Avoid exposing sensitive information.

Messages should help engineers diagnose problems.

---

# Inner Exceptions

When wrapping exceptions, preserve the original exception.

Example:

```csharp id="f8n5py"
throw new ExternalServiceUnavailableException(
    "Payment service is unavailable.",
    ex);
```

---

# Cancellation

Never treat:

```text id="e2m7qs"
OperationCanceledException
```

as an error.

Cancellation is expected behaviour.

---

# Retry Logic

Retry only transient failures.

Retry policies belong in:

* Infrastructure
* Resilience pipelines

Business logic should not implement retry loops.

---

# Finally Blocks

Use `finally` only for deterministic cleanup.

Avoid placing business logic inside `finally`.

---

# AI Responsibilities

When generating code, the AI must:

* Distinguish expected and unexpected failures.
* Avoid exceptions for normal business flow.
* Preserve stack traces.
* Log exceptions once.
* Catch only meaningful exceptions.
* Follow repository conventions for Result types.

---

# Anti-Patterns

Avoid:

* Swallowed exceptions.
* `throw ex`.
* Catching `Exception` everywhere.
* Exceptions for validation.
* Exceptions for ordinary business decisions.
* Logging the same exception multiple times.
* Exposing internal exception details to clients.

---

# Exception Checklist

Before completing an implementation, verify:

* Exceptions represent exceptional situations.
* Expected business outcomes are explicit.
* Stack traces are preserved.
* Logging occurs once.
* Sensitive information is protected.
* Cancellation is handled correctly.
* Repository conventions are followed.

---

# Guiding Principle

Exceptions communicate unexpected failures.

Business behaviour should be explicit, predictable, and should not depend on exceptions for normal execution flow.
