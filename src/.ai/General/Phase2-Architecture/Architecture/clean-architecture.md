# Clean Architecture

Version: 1.0

---

# Purpose

This document defines the Clean Architecture principles used throughout the repository.

Clean Architecture separates business logic from implementation details, allowing the system to evolve without coupling business rules to frameworks, databases, or transport technologies.

Every architectural decision must preserve these boundaries.

---

# Primary Principle

Business rules are the centre of the system.

Everything else exists to support the business.

Dependencies always point toward the business.

---

# Architectural Layers

The repository is organised into the following layers:

```text
API

↓

Application

↓

Domain

Infrastructure

↓

Application

↓

Domain
```

The Domain has no dependency on any other project.

---

# Domain Layer

The Domain represents the business.

The Domain contains:

* Aggregates
* Entities
* Value Objects
* Domain Events
* Domain Services
* Specifications
* Repository Contracts
* Business Rules

The Domain must never depend on:

* Databases
* EF Core
* ASP.NET
* HTTP
* gRPC
* Logging
* Messaging
* Dependency Injection
* Configuration

The Domain must remain a pure business model.

---

# Application Layer

The Application layer coordinates business use cases.

Typical responsibilities include:

* Commands
* Queries
* Handlers
* Validators
* Authorization
* Transactions
* Application Services
* Mapping
* Interfaces

The Application layer:

* Orchestrates work.
* Coordinates dependencies.
* Invokes Domain behaviour.

The Application layer must not contain business rules.

---

# Infrastructure Layer

Infrastructure contains technical implementations.

Examples include:

* EF Core
* SQL Server
* PostgreSQL
* Redis
* RabbitMQ
* Email
* Blob Storage
* File System
* Identity Providers
* Logging

Infrastructure implements abstractions defined by inner layers.

Infrastructure is replaceable.

---

# API Layer

The API layer exposes application functionality.

Typical responsibilities include:

* Endpoints
* Request Models
* Response Models
* Authentication
* Middleware
* Dependency Registration
* OpenAPI

The API layer must remain thin.

Business decisions do not belong here.

---

# Dependency Rule

Dependencies always point inward.

Allowed dependencies:

```text
API

↓

Application

↓

Domain

Infrastructure

↓

Application

↓

Domain
```

Forbidden dependencies include:

```text
Domain → Application

Domain → Infrastructure

Domain → API

Application → API

Application → Infrastructure (implementation)

Infrastructure → API
```

---

# Dependency Inversion

Inner layers define contracts.

Outer layers implement those contracts.

Example:

Application defines:

```text
IInvoiceRepository
```

Infrastructure implements:

```text
InvoiceRepository
```

---

# Business Rules

Business rules belong only inside the Domain.

Never implement business rules inside:

* Controllers
* Endpoints
* Handlers
* Repositories
* DbContext
* Infrastructure Services

---

# Framework Independence

Frameworks are implementation details.

Business code must not depend on:

* ASP.NET Core
* EF Core
* AutoMapper
* MediatR
* gRPC
* MassTransit

Frameworks may change.

Business rules should not.

---

# Database Independence

The database is an implementation detail.

Business behaviour must not depend on:

* SQL
* ORM behaviour
* Database constraints

The Domain should remain valid without a database.

---

# UI Independence

The business should behave identically regardless of whether requests originate from:

* HTTP
* gRPC
* CLI
* Messaging
* Background Jobs

Transport protocols must not influence business behaviour.

---

# Cross-Cutting Concerns

Cross-cutting concerns belong outside the Domain.

Examples include:

* Logging
* Caching
* Validation
* Transactions
* Metrics
* Tracing
* Authorization

These concerns should be applied through appropriate application mechanisms.

---

# Composition Root

Object composition occurs only within the application's composition root.

The Domain and Application layers must remain independent of the DI container.

---

# Testing

Each layer should be independently testable.

Typical tests include:

* Domain Unit Tests
* Application Unit Tests
* Infrastructure Integration Tests
* API Integration Tests

---

# Evolution

As the repository evolves:

* Preserve dependency direction.
* Preserve layer responsibilities.
* Avoid shortcut dependencies.
* Keep the Domain isolated.

Architecture should become stronger over time.

---

# Anti-Patterns

Avoid:

* Business Logic in API.
* Business Logic in Infrastructure.
* EF Core inside Domain.
* HTTP types inside Application.
* Transport-specific behaviour inside Domain.
* Infrastructure dependencies inside Domain.
* Circular project references.

---

# Architecture Checklist

Before completing any implementation, verify:

* Correct layer identified.
* Dependencies point inward.
* Domain remains framework independent.
* Business rules remain inside Domain.
* Infrastructure implements abstractions.
* API remains thin.
* Cross-cutting concerns remain isolated.

---

# Guiding Principle

Clean Architecture is not about projects or folders.

It is about protecting the business from implementation details and ensuring that the core of the system remains stable as technology evolves.
