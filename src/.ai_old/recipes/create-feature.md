# Recipe: Create a Complete Feature

Version: 1.0

## Purpose

This recipe instructs the AI how to generate an entire feature.

A feature is a complete vertical slice.

Never generate only a single class.

Always generate the entire implementation.

---

# Input

Example prompt

```
Create Customer feature
```

or

```
Create Invoice feature
```

---

# Step 1

Understand the business concept.

Determine

- Aggregate
- Aggregate Root
- Child Entities
- Value Objects
- Business Rules
- Commands
- Queries
- Events

Do not start coding before identifying the domain model.

---

# Step 2

Search the repository.

Locate

Existing Aggregates

Existing Commands

Existing Queries

Existing Validators

Existing Endpoints

Existing Repositories

Existing Tests

Imitate the closest implementation.

Never invent a new convention.

---

# Step 3

Determine the Aggregate.

Example

```
Invoice
```

Determine

```
InvoiceId

InvoiceNumber

InvoiceStatus

InvoiceLine

Money

CustomerId
```

Use Strongly Typed IDs.

Use Value Objects.

---

# Step 4

Generate Domain

Create

```
Domain/

    Invoice.cs

    InvoiceId.cs

    InvoiceNumber.cs

    InvoiceStatus.cs

    InvoiceLine.cs

    Events/

        InvoiceCreated.cs

        InvoiceCancelled.cs
```

Requirements

✓ Private constructor

✓ Factory

✓ Business methods

✓ Read-only collections

✓ Domain events

✓ Encapsulation

✓ Invariants

---

# Step 5

Generate Application Commands

Create

```
Application/

    Commands/

        CreateInvoice/

            Command.cs

            Validator.cs

            Handler.cs

        CancelInvoice/

            Command.cs

            Validator.cs

            Handler.cs
```

Each command

✓ One Handler

✓ One Validator

✓ Uses repository interfaces

✓ Returns Result

---

# Step 6

Generate Queries

```
Queries/

    GetInvoice/

    SearchInvoices/

    GetInvoiceLines/
```

Each Query contains

Query

Handler

Response

---

# Step 7

Generate Repository Interfaces

Example

```
IInvoiceRepository
```

Methods should express business intent.

Avoid

```
Get()

Find()

Update()
```

Prefer

```
LoadForPayment()

LoadDraft()

ExistsByNumber()

LoadForCancellation()
```

---

# Step 8

Generate Infrastructure

Create

Repository implementation

Entity Configuration

ValueConverters

Indexes

Relationships

Migration (when requested)

---

# Step 9

Generate API

Create endpoints

```
POST /invoices

POST /invoices/{id}/cancel

GET /invoices/{id}

GET /invoices
```

Endpoints

✓ Thin

✓ CancellationToken

✓ OpenAPI

✓ Authorization

✓ Result Mapping

---

# Step 10

Dependency Injection

Register

Repositories

Application Services

Domain Services

Pipeline Behaviors

Nothing should require manual registration after generation.

---

# Step 11

Generate Unit Tests

Every Aggregate

Every Value Object

Every Handler

Every Validator

Every Repository

where applicable.

---

# Step 12

Generate Integration Tests

Verify

Create

Update

Delete

Validation

Persistence

Endpoints

Authorization

Transactions

---

# Step 13

Verify Architecture

Ensure

Domain

↓

Application

↓

Infrastructure

↓

Api

No forbidden dependency exists.

---

# Step 14

Verify DDD

Aggregate protects invariants.

Value Objects immutable.

Entities encapsulated.

No anemic model.

---

# Step 15

Verify Clean Architecture

Business logic

↓

Domain

Workflow

↓

Application

Persistence

↓

Infrastructure

HTTP

↓

Api

---

# Step 16

Verify Code Style

Use

Repository naming

Repository folders

Repository namespaces

Repository formatting

Repository language features

---

# Step 17

Verify Performance

Avoid

Reflection

N+1

Multiple SaveChanges

Lazy Loading

Public mutable collections

Multiple allocations where unnecessary

---

# Step 18

Verify Tests

Ensure

Happy path

Validation failures

Business failures

Concurrency

Cancellation

Authorization

Persistence

---

# Step 19

Self Review

Before presenting code verify

✓ Compiles

✓ Uses existing abstractions

✓ Uses existing Result pattern

✓ Uses repository conventions

✓ No TODO

✓ No placeholder

✓ No dead code

✓ No duplicated code

---

# Output

The AI should produce

Domain

Application

Infrastructure

Api

Unit Tests

Integration Tests

DI

Configuration

OpenAPI

Documentation (if required)

The feature should be immediately usable.

---

# Forbidden

Never generate

Only Entity

Only Command

Only Endpoint

Only Repository

Only Handler

A feature is complete only when every architectural layer has been generated.

---

# Completion Checklist

□ Aggregate

□ Entities

□ Value Objects

□ Domain Events

□ Commands

□ Queries

□ Validators

□ Handlers

□ Repository Interface

□ Repository Implementation

□ EF Configuration

□ Endpoints

□ DI

□ Unit Tests

□ Integration Tests

□ Documentation

□ Architecture Validation

Only after every item is complete may the feature be considered finished.