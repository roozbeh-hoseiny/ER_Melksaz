# Command Query Responsibility Segregation (CQRS)

Version: 1.0

---

# Purpose

This document defines the CQRS principles used throughout the repository.

CQRS separates operations that modify business state from operations that read business data.

This separation simplifies business modelling, improves scalability, and produces clearer application architecture.

---

# Primary Principle

Commands change state.

Queries read state.

A single operation must never do both.

---

# Definition

CQRS divides application use cases into two categories:

* Commands
* Queries

Each has different responsibilities, validation rules, and performance requirements.

---

# Commands

Commands represent business intentions.

Examples:

* CreateInvoice
* CancelInvoice
* RegisterCustomer
* ApprovePayment

Commands request a state change.

They do not return business data.

---

# Query

Queries retrieve information.

Examples:

* GetInvoice
* GetCustomer
* SearchInvoices
* GetPaymentHistory

Queries must never modify business state.

---

# Separation

Commands and Queries should remain completely independent.

Avoid shared implementations that mix:

* State changes
* Data retrieval

The separation should remain explicit.

---

# Command Handler

A Command Handler coordinates one business use case.

Responsibilities include:

* Loading Aggregates.
* Invoking Domain behaviour.
* Persisting changes.
* Publishing Domain Events.
* Returning an appropriate result.

Command Handlers orchestrate work.

Business rules belong inside the Domain.

---

# Query Handler

A Query Handler retrieves data.

Responsibilities include:

* Reading data.
* Applying filtering.
* Applying sorting.
* Applying paging.
* Mapping to response models.

Query Handlers should not contain business rules.

---

# Read Model

Read Models exist only for querying.

Characteristics:

* Optimised for reading.
* Independent from Domain Aggregates.
* Designed for consumers.
* Free to denormalise data.

Read Models are not Domain Models.

---

# Write Model

The Write Model contains:

* Aggregates
* Entities
* Value Objects
* Domain Events
* Business Rules

The Write Model protects business consistency.

---

# Validation

Commands perform:

* Input validation.
* Business validation.

Queries perform:

* Input validation.

Queries never enforce business state transitions.

---

# Transactions

Commands execute inside business transactions.

Queries should avoid unnecessary transactions.

---

# Performance

Query implementations should optimise for:

* Read performance.
* Paging.
* Projection.
* Filtering.

Avoid loading Aggregates for reporting.

---

# Persistence

Commands interact with Aggregates through Repositories.

Queries may access:

* Read databases.
* Views.
* Projections.
* Optimised read stores.

Read-side persistence is implementation-specific.

---

# Return Types

Commands typically return:

* Success
* Failure
* Identifier
* Result Pattern

Queries return:

* DTOs
* Read Models
* Collections
* Paged Results

Commands should not return full Aggregate state.

---

# Idempotency

Where appropriate, Commands should support idempotent execution.

Repeated execution should not create inconsistent business state.

---

# Side Effects

Only Commands produce:

* State changes.
* Domain Events.
* Integration Events.

Queries must remain free of side effects.

---

# Testing

Command tests verify:

* Business rules.
* Aggregate behaviour.
* Domain Events.
* Persistence.

Query tests verify:

* Returned data.
* Filtering.
* Sorting.
* Paging.
* Projection.

---

# Anti-Patterns

Avoid:

* Commands returning large object graphs.
* Queries modifying state.
* Business logic inside Query Handlers.
* Loading Aggregates for reporting.
* Shared handlers for Commands and Queries.
* CRUD-style services replacing use cases.

---

# CQRS Checklist

Before completing a CQRS implementation, verify:

* Commands only modify state.
* Queries only read data.
* Handlers have a single responsibility.
* Business rules remain inside the Domain.
* Read Models are independent.
* Write Models protect consistency.
* Transactions exist only where required.
* Queries have no side effects.

---

# Guiding Principle

CQRS is not about creating more classes.

It is about making the intent of every use case explicit by clearly separating business state changes from data retrieval.
