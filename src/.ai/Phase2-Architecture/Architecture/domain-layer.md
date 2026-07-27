# Domain Layer

Version: 1.0

---

# Purpose

This document defines the responsibilities, boundaries, and design principles of the Domain Layer.

The Domain Layer represents the business itself.

It contains the business language, business rules, and business behaviour that are independent of any technology.

The Domain must remain stable even if every framework, database, or transport technology changes.

---

# Primary Principle

The Domain answers one question:

> **How does the business work?**

Everything else exists to support the Domain.

---

# Responsibilities

The Domain Layer is responsible for:

* Business rules.
* Business behaviour.
* Business invariants.
* Business concepts.
* State transitions.
* Business policies.
* Domain Events.
* Domain language.

The Domain owns every business decision.

---

# Contains

The Domain Layer may contain:

* Aggregates
* Aggregate Roots
* Entities
* Value Objects
* Domain Events
* Domain Services
* Specifications
* Repository Interfaces
* Business Exceptions
* Domain Policies
* Strongly Typed Identifiers
* Enumerations
* Business Constants

Everything inside the Domain should represent business concepts.

---

# Does Not Contain

The Domain Layer must never contain:

* HTTP
* gRPC
* EF Core
* SQL
* ASP.NET Core
* Controllers
* Endpoints
* Middleware
* Authentication
* Authorization implementations
* Logging
* Configuration
* Dependency Injection
* Caching
* Messaging
* Serialization
* DTOs
* Request Models
* Response Models

Technology belongs outside the Domain.

---

# Business Behaviour

Business behaviour belongs inside Domain objects.

Examples:

```text id="h7v2qn"
Approve()

Reject()

ReceivePayment()

Cancel()

AddLine()

RemoveLine()
```

Avoid exposing CRUD-style methods.

Business methods should communicate business intent.

---

# Business Invariants

The Domain is responsible for protecting invariants.

Examples:

* An Invoice cannot be paid twice.
* An Order must contain at least one item.
* A Payment cannot exceed the remaining balance.
* A Customer cannot exceed their credit limit.

Invalid business state must never exist.

---

# Rich Domain Model

The Domain should be behaviour-rich.

Objects should encapsulate:

* Rules
* Decisions
* Calculations
* State transitions

Avoid anemic models that contain only properties.

---

# Encapsulation

The Domain owns its internal state.

Avoid:

* Public setters.
* Mutable public collections.
* Exposing implementation details.

External code interacts through business behaviour.

---

# Persistence Ignorance

The Domain must remain completely independent of persistence.

The Domain must never know:

* Tables
* Columns
* SQL
* ORM configuration
* Lazy loading
* Tracking
* Transactions

Persistence is an implementation detail.

---

# Framework Independence

The Domain must compile independently of any framework.

Changing:

* ASP.NET Core
* EF Core
* gRPC
* RabbitMQ
* Redis
* MassTransit

must not require changes to business behaviour.

---

# Dependency Rule

The Domain depends on nothing.

No project reference should point outward from the Domain.

The Domain is the architectural centre.

---

# Domain Language

Every class, method, property, and event should use business terminology.

Avoid technical names.

Prefer:

```text id="r8k4wx"
Invoice

Settlement

CreditLimit

ReceivePayment
```

Avoid:

```text id="m5z1ya"
DataObject

Processor

Manager

Execute
```

Business experts should recognise the language.

---

# Collaboration

Domain objects collaborate with:

* Other Domain objects.
* Domain Services.
* Specifications.

Infrastructure collaboration occurs through abstractions only.

---

# Validation

Business validation belongs inside the Domain.

Examples:

* Business policies.
* State transitions.
* Business invariants.

Input validation belongs outside the Domain.

---

# Domain Events

Important business facts should be represented as Domain Events.

Events should:

* Be immutable.
* Use business language.
* Be named in the past tense.

The Domain raises events.

Outer layers publish them.

---

# Exceptions

Business exceptions should represent business failures.

Avoid technical exceptions inside the Domain.

Business errors should communicate business meaning.

---

# Testing

The Domain should be testable without:

* Databases
* HTTP
* Message Brokers
* External Services
* Dependency Injection

Most Domain tests should be fast unit tests.

---

# Evolution

As the business evolves:

* Extend the Domain.
* Preserve business language.
* Protect invariants.
* Keep behaviour close to the data it governs.

The Domain should become richer—not more coupled.

---

# Anti-Patterns

Avoid:

* Anemic Domain Models.
* Public setters.
* Infrastructure dependencies.
* ORM attributes.
* Business logic inside Application.
* Business logic inside API.
* Business logic inside Infrastructure.
* Primitive Obsession.
* Service-heavy Domain models.

---

# Domain Layer Checklist

Before completing any Domain implementation, verify:

* Business terminology is used consistently.
* Business rules remain inside the Domain.
* Invariants are protected.
* Framework dependencies do not exist.
* Persistence ignorance is preserved.
* Encapsulation is maintained.
* Domain Events represent business facts.
* The Domain compiles independently.

---

# Guiding Principle

The Domain Layer is the heart of the software.

If every external technology disappeared tomorrow, the Domain should still accurately describe how the business operates.
