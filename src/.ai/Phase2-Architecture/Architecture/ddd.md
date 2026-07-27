# Domain-Driven Design (DDD)

Version: 1.0

---

# Purpose

This document defines the Domain-Driven Design (DDD) principles used throughout the repository.

DDD exists to model complex business domains by placing the business language and business rules at the centre of the software.

The Domain is the heart of the application.

---

# Primary Principle

Software should model the business—not the database, framework, or transport protocol.

Every design decision begins with the business domain.

---

# Ubiquitous Language

All code must use the same language as the business.

The same terminology should appear consistently in:

* Conversations
* Documentation
* Source code
* Tests
* APIs

Avoid technical names when a business term exists.

Good:

```text id="kz5h7a"
Invoice

Customer

Payment

Settlement
```

Bad:

```text id="n1x4gf"
Record

Data

Object

Manager
```

---

# Bounded Context

A Bounded Context defines a clear business boundary.

Each business capability belongs to exactly one Bounded Context.

Examples:

```text id="x8qv3w"
Identity

Billing

Inventory

Accounting

Orders
```

Business models should not leak across contexts.

---

# Aggregate

An Aggregate represents a consistency boundary.

An Aggregate:

* Protects business invariants.
* Owns its internal state.
* Controls modifications.
* Exposes business behaviour.

External code should never modify internal state directly.

---

# Aggregate Root

Each Aggregate has exactly one Aggregate Root.

The Aggregate Root:

* Controls access.
* Enforces invariants.
* Coordinates child entities.
* Publishes Domain Events when appropriate.

Only Aggregate Roots should be loaded from repositories.

---

# Entity

An Entity has:

* Identity
* Lifecycle
* Mutable state

Equality is based on identity.

Entities encapsulate business behaviour.

Entities are not data containers.

---

# Value Object

A Value Object represents a business concept without identity.

Characteristics:

* Immutable
* Equality by value
* Self-validating
* Behaviour-rich

Examples:

* Money
* Email
* Address
* Quantity
* InvoiceNumber

Avoid using primitive types for meaningful business concepts.

---

# Domain Service

A Domain Service contains business behaviour that does not naturally belong to a single Aggregate or Value Object.

Domain Services should:

* Represent business concepts.
* Remain stateless.
* Avoid infrastructure dependencies.

Do not create Domain Services simply to move code out of Entities.

---

# Domain Event

A Domain Event represents something important that has already happened in the business.

Examples:

* InvoiceCreated
* PaymentReceived
* CustomerRegistered

Domain Events are immutable.

Their names should always be written in the past tense.

---

# Repository

Repositories provide access to Aggregate Roots.

Repositories:

* Load Aggregates.
* Persist Aggregates.

Repositories do not contain business logic.

Repositories do not expose persistence details.

---

# Factory

Factories create complex Aggregates when construction requires business rules.

Factories should not replace constructors without justification.

Prefer constructors for simple creation.

---

# Specification

Specifications encapsulate reusable business rules.

Specifications should:

* Express business intent.
* Be reusable.
* Be composable.

Avoid using Specifications for infrastructure filtering.

---

# Domain Invariants

Domain invariants define rules that must always remain true.

Aggregates are responsible for protecting their own invariants.

No external component should bypass Aggregate rules.

---

# Encapsulation

Domain objects own their state.

Avoid exposing mutable collections.

Avoid public setters.

Avoid allowing external code to manipulate internal state.

---

# Persistence Ignorance

The Domain must not know:

* EF Core
* SQL
* Database schema
* HTTP
* JSON
* Serialization

Persistence is an implementation detail.

---

# Rich Domain Model

Domain objects should contain behaviour.

Avoid models that contain only properties.

Business behaviour belongs inside the Domain.

---

# Transaction Boundary

An Aggregate is the default transactional consistency boundary.

Multiple Aggregates should only participate in the same business operation when explicitly required.

---

# Identity

Entity identity should represent business identity whenever practical.

Avoid exposing database-generated identifiers as business concepts.

---

# Validation

Business validation belongs inside the Domain.

Input validation belongs outside the Domain.

The Domain protects business correctness.

---

# Domain Independence

The Domain must remain independent of:

* Frameworks
* Databases
* Messaging
* Logging
* Configuration
* Dependency Injection

The Domain should compile independently.

---

# Testing

The Domain should be testable without:

* Databases
* HTTP
* Message brokers
* External services

Most Domain tests should be simple unit tests.

---

# Anti-Patterns

Avoid:

* Anemic Domain Models.
* Business Logic in Repositories.
* Business Logic in Handlers.
* Public setters.
* Primitive Obsession.
* Infrastructure dependencies.
* Transaction logic inside Entities.
* Persistence-specific behaviour.

---

# DDD Checklist

Before completing any Domain implementation, verify:

* Business language is used consistently.
* Aggregate boundaries are clear.
* Invariants are protected.
* Entities contain behaviour.
* Value Objects are immutable.
* Repositories expose Aggregate Roots only.
* Domain remains framework independent.
* Business rules exist only inside the Domain.

---

# Guiding Principle

The Domain model should describe how the business works—not how the application is implemented.

Every line of Domain code should make the business easier to understand.
