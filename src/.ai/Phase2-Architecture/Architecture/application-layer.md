# Application Layer

Version: 1.0

---

# Purpose

This document defines the responsibilities, boundaries, and design rules of the Application Layer.

The Application Layer coordinates business use cases.

It does **not** contain business rules.

It orchestrates the Domain.

---

# Primary Principle

The Application Layer answers one question:

> **What business use case should be executed?**

It never answers:

> **How does the business work?**

That responsibility belongs exclusively to the Domain.

---

# Responsibilities

The Application Layer is responsible for:

* Coordinating use cases.
* Loading Aggregate Roots.
* Calling Domain methods.
* Persisting changes.
* Managing transactions.
* Publishing Domain Events.
* Calling Infrastructure abstractions.
* Returning application results.

---

# Contains

Typical Application components include:

* Commands
* Queries
* Command Handlers
* Query Handlers
* Validators
* DTOs
* Result Models
* Application Services
* Authorization Policies
* Interfaces
* Mapping
* Pipeline Behaviours

---

# Does Not Contain

The Application Layer must never contain:

* Business rules
* Aggregate logic
* EF Core
* SQL
* HTTP
* gRPC
* Controllers
* Middleware
* Infrastructure implementations

---

# Use Case

Each Handler implements exactly one business use case.

Examples:

```text id="u7x1pm"
CreateInvoice

ApproveInvoice

CancelInvoice

RegisterCustomer

ReceivePayment
```

A Handler represents a complete application workflow.

---

# Handler Responsibilities

A Handler should:

1. Validate input.
2. Load Aggregate(s).
3. Invoke Domain behaviour.
4. Persist changes.
5. Publish events.
6. Return a Result.

Handlers coordinate.

They do not make business decisions.

---

# Business Logic

Business rules belong inside:

* Aggregates
* Entities
* Value Objects
* Domain Services

Never inside:

* Handlers
* Validators
* DTOs
* Controllers

---

# Transactions

The Application Layer coordinates transactions.

A typical transaction includes:

* Load Aggregate
* Execute business behaviour
* Persist Aggregate
* Publish events

Transaction boundaries belong here.

---

# Validation

The Application Layer performs:

* Input validation
* Authorization validation
* Request consistency validation

Business validation remains inside the Domain.

---

# Authorization

Authorization policies belong to the Application Layer.

Business permissions should be evaluated before executing business operations.

Infrastructure performs authentication.

---

# Interfaces

Interfaces used by the Application Layer include:

* Repositories
* Unit of Work
* Email Sender
* Clock
* Identity Provider
* File Storage

Interfaces belong to inner layers.

Implementations belong to Infrastructure.

---

# DTOs

DTOs exist only for communication.

DTOs are not Domain Models.

DTOs should contain:

* Data
* No behaviour
* No business rules

---

# Mapping

Mapping should remain explicit.

Typical mappings include:

* Request → Command
* Query Result → Response
* Domain → DTO

Avoid hiding important mappings.

---

# Result Pattern

Handlers should return explicit results.

Typical results include:

* Success
* Failure
* Validation Error
* Not Found
* Unauthorized
* Conflict

Avoid using exceptions for expected business outcomes.

---

# Dependencies

The Application Layer may depend on:

* Domain
* Application abstractions

It must never depend directly on Infrastructure implementations.

---

# Async

All I/O operations should be asynchronous.

Avoid synchronous repository calls.

---

# Testing

Application tests should verify:

* Use case execution.
* Correct orchestration.
* Repository interaction.
* Transaction behaviour.
* Returned results.

Business rules should be tested separately in Domain tests.

---

# Anti-Patterns

Avoid:

* Business logic inside Handlers.
* Large Handlers.
* CRUD Handlers with no business behaviour.
* Infrastructure implementation references.
* EF Core usage.
* SQL queries.
* HTTP types.
* Static service locators.

---

# Application Layer Checklist

Before completing an implementation, verify:

* One Handler implements one use case.
* Business rules remain in the Domain.
* Transactions are coordinated correctly.
* Infrastructure is accessed through abstractions.
* DTOs contain no behaviour.
* Mapping is explicit.
* Validation is limited to input.
* Results are explicit.

---

# Guiding Principle

The Application Layer is the conductor of the orchestra.

It coordinates the work, but the music—the business behaviour—is performed entirely by the Domain.
