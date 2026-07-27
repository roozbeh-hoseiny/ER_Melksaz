# Application Handler Rules

Version: 1.0

This document defines how Application Handlers are designed and implemented.

A Handler represents a single application use case.

Handlers coordinate work.

They never implement business rules.

---

# Purpose

A Handler coordinates interactions between

- Domain
- Repositories
- Domain Services
- External Services
- Pipelines

Handlers should read like business workflows.

A reader should understand the entire use case from top to bottom.

---

# Responsibilities

A Handler is responsible for

✓ Loading Aggregates

✓ Calling business methods

✓ Persisting changes

✓ Returning Results

✓ Coordinating infrastructure

A Handler is NOT responsible for

✗ Business Rules

✗ Validation

✗ Authorization

✗ Logging

✗ Transaction management

✗ Retry policies

✗ Caching

Those belong to cross-cutting pipelines.

---

# Pipeline Architecture

The recommended execution flow is

```
HTTP/gRPC

↓

Authentication

↓

Authorization

↓

Validation

↓

Idempotency (optional)

↓

Logging

↓

Transaction

↓

Handler

↓

Persist

↓

Dispatch Domain Events

↓

Commit

↓

Return Result
```

Handlers should assume previous pipeline stages have already completed.

---

# Handler Structure

Every Handler should follow the same structure.

```
Validate (already performed)

↓

Load Aggregate(s)

↓

Execute Domain Behaviour

↓

Persist

↓

Return Result
```

Avoid mixing these stages.

---

# General Rules

## HANDLER-001

Each Handler executes one use case.

---

## HANDLER-002

Each Handler handles exactly one Command or one Query.

Never both.

---

## HANDLER-003

Handlers should remain small.

Recommended size

20–100 lines.

---

## HANDLER-004

Methods should read sequentially.

Avoid deeply nested logic.

---

## HANDLER-005

Return early whenever possible.

---

## HANDLER-006

Avoid multiple return types.

Use the repository's Result abstraction consistently.

---

# Dependency Rules

Handlers may depend on

✓ Repository Interfaces

✓ Domain Services

✓ Unit Of Work abstraction

✓ Time abstraction

✓ User abstraction

✓ External service abstractions

Handlers must NOT depend on

✗ DbContext

✗ IServiceProvider

✗ HttpContext

✗ IConfiguration

✗ ILogger (unless required by repository convention)

✗ EF Core implementation classes

---

# Aggregate Loading

## LOAD-001

Load only required Aggregates.

---

## LOAD-002

Never load unrelated Aggregates.

---

## LOAD-003

Prefer repository methods expressing intent.

Good

```
GetActiveInvoice(...)
```

Bad

```
Get(...)
```

when a more specific abstraction exists.

---

## LOAD-004

Repositories return Aggregates.

Not DTOs.

---

# Business Logic

## DOMAIN-001

Business rules belong in Domain.

---

## DOMAIN-002

Never duplicate Domain validation.

---

## DOMAIN-003

Never manipulate Aggregate state directly.

Bad

```csharp
invoice.Status = Paid;
```

Good

```csharp
invoice.MarkAsPaid(payment);
```

---

## DOMAIN-004

Application coordinates.

Domain decides.

---

# Persistence

## SAVE-001

Persist only after successful business execution.

---

## SAVE-002

Avoid multiple SaveChanges calls.

---

## SAVE-003

One use case.

One commit.

---

## SAVE-004

Repositories hide persistence implementation.

---

# Transactions

## TX-001

Transaction management belongs to the pipeline.

---

## TX-002

Handlers should not manually begin transactions unless explicitly required.

---

## TX-003

Every state-changing use case should execute atomically.

---

# Validation

## VALID-001

Request validation occurs before Handler execution.

---

## VALID-002

Business validation remains inside Domain.

---

## VALID-003

Handlers assume validated input.

---

# Authorization

## AUTH-001

Authorization occurs before the Handler.

---

## AUTH-002

Business permissions remain inside Domain when required.

---

# Domain Events

## EVENT-001

Aggregates raise Domain Events.

---

## EVENT-002

Handlers never instantiate Domain Events directly.

---

## EVENT-003

Infrastructure dispatches Domain Events after persistence.

---

# External Services

## EXT-001

Handlers communicate only through abstractions.

---

## EXT-002

Never reference SDKs directly.

---

## EXT-003

Infrastructure hides implementation details.

---

# Result Pattern

## RESULT-001

Return Result.

Avoid null.

---

## RESULT-002

Expected failures return Result failures.

---

## RESULT-003

Unexpected failures may throw exceptions.

---

# Logging

## LOG-001

Business logging belongs in pipelines.

---

## LOG-002

Avoid logging every line.

---

## LOG-003

Log business milestones only when meaningful.

---

# Idempotency

## IDEMP-001

Commands that may be retried should support idempotency.

---

## IDEMP-002

Idempotency belongs to infrastructure or pipeline behaviour.

---

# Cancellation

## CANCEL-001

Always accept CancellationToken.

---

## CANCEL-002

Pass CancellationToken to every async dependency.

---

# Performance

## PERF-001

Avoid unnecessary allocations.

---

## PERF-002

Avoid loading entire object graphs.

---

## PERF-003

Use async APIs consistently.

---

## PERF-004

Never block asynchronous code.

Forbidden

```
.Result

.Wait()

GetAwaiter().GetResult()
```

---

# Exception Rules

## EX-001

Do not catch exceptions merely to return Result.

---

## EX-002

Catch exceptions only when meaningful recovery exists.

---

## EX-003

Do not swallow exceptions.

---

# Handler Folder Structure

Recommended

```
Application

└── Invoices

    ├── Commands

    │     CreateInvoice

    │        Command.cs
    │        Validator.cs
    │        Handler.cs

    ├── Queries

    │     GetInvoice

    │        Query.cs
    │        Handler.cs
    │        Response.cs
```

One folder per feature.

Not one folder per type.

---

# Testing

Every Handler must have

✓ Unit Tests

✓ Integration Tests

Tests should verify

- success path

- business failures

- validation failures

- persistence

- domain events

- cancellation

---

# AI Generation Rules

Whenever generating a Handler, automatically generate

✓ Command or Query

✓ Handler

✓ Validator (when applicable)

✓ Endpoint

✓ Unit Tests

✓ Integration Tests

✓ Repository abstraction usage

✓ Domain method invocation

✓ CancellationToken support

✓ Result handling

Never generate only the Handler.

Generate the complete vertical slice.

---

# AI Verification Checklist

Before presenting a Handler, verify

✓ One responsibility

✓ One use case

✓ Thin orchestration

✓ Business logic inside Domain

✓ No DbContext dependency

✓ No HTTP dependency

✓ No infrastructure leakage

✓ Uses repository abstractions

✓ Returns Result

✓ Supports CancellationToken

✓ Uses asynchronous APIs

✓ Single commit

✓ Domain Events raised by Aggregates

✓ Follows repository folder structure

A Handler should read like an application workflow, with every business decision delegated to the Domain layer.