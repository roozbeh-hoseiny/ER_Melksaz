# Command Design Rules

Version: 1.0

This document defines how Commands are designed and implemented throughout the repository.

Commands represent business intent.

A Command requests the system to change state.

Commands are not business logic.

Commands orchestrate business logic.

---

# Definition

A Command

- expresses an intention
- changes state
- belongs to a single use case
- has exactly one handler
- executes once

Examples

Good

CreateInvoice

CancelInvoice

ApproveOrder

RegisterCustomer

AssignRole

PayInvoice

Bad

InvoiceCommand

Process

Execute

Update

Save

---

# Philosophy

Commands describe

WHAT

the caller wants.

Never

HOW

it will happen.

---

# Command Rules

## CMD-001

Every Command represents one business use case.

Never combine unrelated operations.

---

## CMD-002

Commands should be immutable.

Use init-only properties or constructor parameters.

---

## CMD-003

Command names start with a verb.

Examples

CreateInvoiceCommand

CancelInvoiceCommand

ApproveOrderCommand

IssuePolicyCommand

---

## CMD-004

Commands never contain business logic.

---

## CMD-005

Commands never access repositories.

---

## CMD-006

Commands never access DbContext.

---

## CMD-007

Commands never call HTTP services.

---

## CMD-008

Commands never perform validation.

Validation belongs to Validators.

---

## CMD-009

Commands should contain only the data required for the use case.

Avoid dumping entire DTOs.

---

## CMD-010

Prefer Value Objects over primitive types.

Good

CustomerId

Money

InvoiceNumber

Bad

Guid

string

decimal

---

# Command Handler

Each Command has exactly one Handler.

---

## HANDLER-001

One handler per command.

---

## HANDLER-002

Handler contains orchestration only.

---

## HANDLER-003

Business rules belong in Domain.

---

## HANDLER-004

Infrastructure is accessed through interfaces.

---

## HANDLER-005

Handlers should be small.

Target

20–80 lines.

---

## HANDLER-006

Handlers should read like application workflows.

---

## HANDLER-007

Load Aggregate.

↓

Execute business method.

↓

Persist.

↓

Return Result.

---

## HANDLER-008

Do not manipulate Aggregate state directly.

Bad

```csharp
invoice.Status = InvoiceStatus.Paid;
```

Good

```csharp
invoice.Pay(payment);
```

---

## HANDLER-009

Handlers should never contain SQL.

---

## HANDLER-010

Handlers should never know EF implementation details.

---

# Validation

## VALID-001

Every Command has a Validator.

---

## VALID-002

Validators validate request structure.

Examples

Required fields

Maximum length

Minimum length

Format

Ranges

Null

---

## VALID-003

Validators do NOT validate business rules.

---

## VALID-004

Business validation belongs in Domain.

---

# Result

## RESULT-001

Handlers return the repository Result abstraction.

---

## RESULT-002

Avoid throwing exceptions for expected business failures.

---

## RESULT-003

Validation failures become Result failures.

---

# Transactions

## TX-001

Each Command executes inside one transaction.

---

## TX-002

Do not manually create transactions unless required.

---

## TX-003

Transaction management belongs to pipelines or Unit of Work.

---

# Domain Events

## EVENT-001

Handlers do not create Domain Events.

Aggregates do.

---

## EVENT-002

Handlers persist Aggregates.

Infrastructure dispatches Domain Events.

---

# Authorization

## AUTH-001

Authorization happens before Handler execution.

---

## AUTH-002

Handlers assume authorization already succeeded.

---

# Logging

## LOG-001

Handlers should not contain excessive logging.

Cross-cutting logging belongs in pipelines.

---

# Mapping

## MAP-001

Map DTOs into Domain concepts.

---

## MAP-002

Never expose infrastructure models.

---

# Dependencies

Allowed

✓ Repository interfaces

✓ Domain services

✓ Time provider abstraction

✓ User abstraction

✓ Unit of Work abstraction

Forbidden

✗ DbContext

✗ ILogger (unless repository convention requires it)

✗ IConfiguration

✗ HttpContext

✗ IServiceProvider

---

# Performance

## PERF-001

Load only required Aggregates.

---

## PERF-002

Avoid unnecessary queries.

---

## PERF-003

Avoid loading unrelated navigation properties.

---

# Anti-Patterns

Never

- put business logic inside handlers
- update Entity properties directly
- call SaveChanges multiple times
- bypass Aggregates
- use DbContext directly (unless repository convention explicitly allows it)
- return EF entities
- expose infrastructure types
- mix multiple use cases
- create "God Handlers"

---

# AI Generation Rules

When generating a Command, automatically generate:

✓ Command

✓ Validator

✓ Handler

✓ Unit Tests

✓ Integration Tests

✓ Endpoint

✓ Mapping

✓ Dependency Injection registration (if not automatic)

✓ Documentation (if repository convention requires it)

Never generate a Command without its complete vertical slice.

---

# AI Verification Checklist

Before presenting generated code, verify:

✓ Command is immutable

✓ One handler exists

✓ One validator exists

✓ Business logic is in Domain

✓ Handler is thin

✓ Uses repository interfaces

✓ Returns Result

✓ No DbContext dependency

✓ No HTTP dependency

✓ Uses Value Objects

✓ Supports CancellationToken

✓ Follows repository naming conventions

The generated Command should represent a single business capability and orchestrate, rather than implement, business behaviour.