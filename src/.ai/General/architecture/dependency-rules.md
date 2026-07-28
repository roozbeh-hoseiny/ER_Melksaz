# Dependency Rules

Version: 1.0

---

# Purpose

This document defines the dependency rules that govern every project, namespace, module, and class within the repository.

The objective is to create a system with low coupling, high cohesion, clear responsibilities, and maintainable boundaries.

Every dependency introduced into the repository must comply with these rules.

---

# Fundamental Rule

Dependencies always point toward business knowledge.

The closer a component is to the Domain, the fewer dependencies it should have.

---

# Dependency Direction

The allowed dependency direction is:

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

Dependencies must never point in the opposite direction.

---

# Dependency Matrix

| From | Domain | Application | Infrastructure | API | Presentation |
|------|--------|-------------|----------------|-----|--------------|
| Domain | ✓ | ✗ | ✗ | ✗ | ✗ |
| Application | ✓ | ✓ | ✗ | ✗ | ✗ |
| Infrastructure | ✓ | ✓ | ✓ | ✗ | ✗ |
| API | ✗ | ✓ | ✗ | ✓ | ✗ |
| Presentation | ✗ | ✗ | ✗ | ✓ | ✓ |

Any dependency outside this matrix requires explicit architectural approval.

---

# Domain Dependencies

The Domain layer must remain completely independent.

The Domain must never reference:

- ASP.NET Core
- EF Core
- SQL Server
- RabbitMQ
- Redis
- Docker
- gRPC
- Serilog
- OpenTelemetry
- IConfiguration
- IServiceProvider
- ILogger
- HttpContext
- Dependency Injection frameworks

The Domain may depend only on:

- .NET Base Class Library
- Other Domain components

---

# Application Dependencies

The Application layer may depend on:

- Domain
- Application contracts
- Abstractions defined by the repository

The Application layer must never depend on:

- EF Core
- ASP.NET Core
- Repository implementations
- Database providers
- Message brokers
- Cache implementations

---

# Infrastructure Dependencies

Infrastructure may depend on:

- Domain
- Application
- External libraries
- Databases
- Messaging frameworks
- Logging frameworks

Infrastructure must never introduce dependencies into the Domain.

---

# API Dependencies

The API layer may depend on:

- Application
- ASP.NET Core
- Authentication libraries
- Serialization libraries

The API layer must never depend directly on Infrastructure implementations.

Always communicate through Application contracts.

---

# Presentation Dependencies

Presentation communicates through the public API exposed by the solution.

Presentation must not bypass architectural layers.

---

# Class-Level Dependencies

A class should depend only on the services required to fulfil its responsibility.

Avoid unnecessary constructor dependencies.

A constructor with excessive dependencies usually indicates multiple responsibilities.

---

# Constructor Injection

Use constructor injection exclusively.

Do not use:

- Service Locator
- Static service access
- Manual service resolution
- Property injection
- Method injection unless explicitly required

Dependencies must be explicit.

---

# Dependency Inversion

Depend on abstractions.

Do not depend on implementations.

Example:

Good

```
IInvoiceRepository
```

Bad

```
SqlInvoiceRepository
```

---

# Circular Dependencies

Circular dependencies are prohibited.

Examples:

Module A

↓

Module B

↓

Module A

This applies to:

- Projects
- Assemblies
- Namespaces
- Classes

---

# Namespace Dependencies

Namespaces should reflect architectural boundaries.

Avoid referencing unrelated namespaces.

Namespace dependencies should remain predictable.

---

# Module Dependencies

Modules should communicate through well-defined contracts.

Avoid direct knowledge of another module's internal implementation.

Modules should be independently maintainable.

---

# Interface Ownership

Interfaces belong to the layer that consumes them.

Implementations belong to outer layers.

Example

Application defines:

```
IInvoiceRepository
```

Infrastructure implements:

```
InvoiceRepository
```

---

# External Libraries

Every external dependency must satisfy the following requirements:

- Provides measurable value.
- Is actively maintained.
- Is compatible with the architecture.
- Does not increase unnecessary coupling.
- Does not duplicate existing functionality.

Prefer existing repository libraries over introducing new ones.

---

# Static Dependencies

Avoid static state.

Static dependencies reduce testability and increase coupling.

Use dependency injection instead.

---

# Configuration Dependencies

Configuration should enter the system only at the application's composition root.

Business objects should never read configuration directly.

---

# Time Dependencies

Do not depend directly on system time.

Depend on an abstraction when business behaviour depends on time.

---

# User Context

Do not depend directly on transport-specific user objects.

Depend on abstractions representing the current user.

---

# Persistence Dependencies

Business logic must never depend on persistence technology.

Repositories abstract persistence.

Persistence remains replaceable.

---

# Communication Dependencies

External communication should occur through abstractions.

Examples:

- Email
- SMS
- File Storage
- Payment Providers
- Message Brokers

The Domain must never know implementation details.

---

# Forbidden Dependencies

The following dependencies are prohibited:

- Domain → Infrastructure
- Domain → API
- Domain → EF Core
- Domain → ASP.NET Core
- Application → DbContext
- Application → SQL Server
- Application → RabbitMQ
- API → Repository Implementations
- API → DbContext
- Presentation → Database

---

# Dependency Review Checklist

Before introducing a dependency, verify:

- Does it respect dependency direction?
- Does it increase coupling?
- Is an abstraction available?
- Does it belong in this layer?
- Can it be replaced?
- Does it improve maintainability?
- Is it required?

Only introduce the dependency if every answer supports the architectural goals.

---

# Guiding Principle

Dependencies should make the system easier to understand, easier to test, easier to maintain, and easier to evolve.

Every new dependency increases architectural cost.

Treat dependencies as long-term design decisions, not implementation details.