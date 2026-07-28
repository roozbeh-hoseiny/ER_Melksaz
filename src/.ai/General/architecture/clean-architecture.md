# Clean Architecture

Version: 1.0

---

# Purpose

This document defines the Clean Architecture principles used throughout the repository.

Every component must belong to exactly one architectural layer.

Each layer has a single responsibility.

Dependencies always point toward the Domain.

---

# Goals

The architecture exists to achieve the following objectives:

- Separation of concerns
- Maintainability
- Testability
- Replaceable infrastructure
- Business-centric design
- Long-term evolution

---

# Architectural Layers

The repository is composed of the following layers.

```
Presentation

↓

API

↓

Application

↓

Domain

↓

Infrastructure
```

Although Infrastructure depends on Application and Domain, it is considered an outer layer and must never influence business design.

---

# Layer Responsibilities

## Domain

The Domain layer contains business knowledge.

It defines:

- Aggregates
- Entities
- Value Objects
- Domain Events
- Domain Services
- Specifications
- Business Rules

The Domain knows nothing about:

- Databases
- HTTP
- gRPC
- Serialization
- Logging
- Messaging
- Dependency Injection
- Configuration

---

## Application

The Application layer coordinates business use cases.

It is responsible for:

- Commands
- Queries
- Handlers
- Validation
- Authorization
- Transactions
- Result handling
- Orchestration

The Application layer does not contain business rules.

---

## Infrastructure

Infrastructure implements technical capabilities.

Examples include:

- EF Core
- SQL Server
- Redis
- RabbitMQ
- gRPC clients
- File Storage
- Email
- Logging
- Telemetry

Infrastructure exists only to support the Domain and Application layers.

---

## API

The API layer exposes application functionality.

Its responsibilities include:

- HTTP endpoints
- gRPC services
- Request binding
- Response mapping
- Authentication
- Authorization
- OpenAPI

The API layer contains no business logic.

---

## Presentation

Presentation includes any user-facing interface.

Examples include:

- Web applications
- Mobile applications
- Desktop applications
- Administrative portals

Presentation communicates exclusively with the API or Application layer according to the solution architecture.

---

# Dependency Rule

Dependencies always point inward.

```
Presentation

↓

API

↓

Infrastructure

↓

Application

↓

Domain
```

The Domain layer has no project dependencies.

Every outer layer depends on inner layers.

Never reverse this direction.

---

# Allowed Dependencies

| Layer | Allowed Dependencies |
|--------|----------------------|
| Domain | None |
| Application | Domain |
| Infrastructure | Application, Domain |
| API | Application |
| Presentation | API |

Any dependency outside these rules is prohibited unless explicitly documented.

---

# Domain Independence

The Domain must compile without requiring:

- EF Core
- ASP.NET Core
- SQL Server
- RabbitMQ
- Redis
- Docker
- Logging frameworks
- Serialization libraries

The Domain should remain usable as a pure business model.

---

# Business Rules

Business rules belong exclusively in the Domain layer.

Business rules must never be implemented in:

- Endpoints
- Controllers
- Handlers
- Validators
- Repositories
- DbContext
- Infrastructure services

---

# Orchestration

Application coordinates work.

Typical workflow:

```
Receive Request

↓

Load Aggregate

↓

Execute Business Behaviour

↓

Persist Changes

↓

Return Result
```

The Application layer should remain thin.

---

# Infrastructure Isolation

Infrastructure should never introduce business behaviour.

Infrastructure implements abstractions defined by inner layers.

Examples include:

- Repository implementations
- Messaging implementations
- Cache implementations
- External service adapters

---

# API Isolation

Endpoints translate transport requests into Application requests.

Endpoints should not:

- Execute business rules
- Access the database directly
- Contain workflow logic
- Manage transactions

Endpoints remain simple adapters.

---

# Cross-Cutting Concerns

Cross-cutting concerns should be implemented through dedicated mechanisms.

Examples:

- Logging
- Validation
- Authorization
- Transactions
- Retry policies
- Telemetry
- Metrics

Avoid duplicating cross-cutting behaviour throughout the codebase.

---

# Communication Between Layers

Communication should occur through explicit contracts.

Examples include:

- Interfaces
- Commands
- Queries
- DTOs
- Domain Events

Avoid exposing implementation details across layers.

---

# Layer Boundaries

Each layer owns its own responsibilities.

Do not allow one layer to assume responsibilities belonging to another.

When responsibility becomes unclear, move the behaviour closer to the Domain.

---

# Data Flow

Typical request flow:

```
HTTP Request

↓

API

↓

Command

↓

Handler

↓

Aggregate

↓

Repository

↓

Database

↓

Result

↓

API Response
```

Each step has a clearly defined responsibility.

---

# Error Handling

Errors should propagate through architectural boundaries in a consistent manner.

Business failures should use the repository's Result pattern.

Unexpected failures should be handled by the global exception strategy.

---

# Testing

Each layer should be testable independently.

Examples:

- Domain → Unit Tests
- Application → Unit Tests
- Infrastructure → Integration Tests
- API → Integration Tests

Avoid tests that unnecessarily span multiple layers.

---

# Architectural Integrity

Before introducing a dependency, verify:

- Is it necessary?
- Does it belong in this layer?
- Does it violate dependency direction?
- Can it be inverted?
- Does it increase coupling?

If the answer introduces architectural risk, redesign the solution.

---

# Common Violations

The following are prohibited:

- Domain referencing EF Core
- Domain referencing ASP.NET Core
- Handlers containing business rules
- Endpoints accessing DbContext
- Infrastructure referencing Presentation
- API referencing Infrastructure implementations directly
- Returning EF entities from API
- Exposing database models outside Infrastructure

---

# Architectural Review Checklist

Before completing any feature, verify:

- Business rules exist only in Domain.
- Application coordinates behaviour.
- Infrastructure implements abstractions.
- API acts as an adapter.
- Dependencies point inward.
- Domain is framework independent.
- No architectural boundaries are violated.
- Responsibilities are clearly separated.

---

# Guiding Principle

A technology can be replaced.

A framework can be upgraded.

A database can be migrated.

The Domain Model should remain unchanged.