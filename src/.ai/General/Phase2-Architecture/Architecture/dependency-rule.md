# Dependency Rule

Version: 1.0

---

# Purpose

This document defines the Dependency Rule used throughout the repository.

The Dependency Rule is the fundamental principle that protects the Domain from implementation details.

All architectural decisions must preserve this rule.

---

# Primary Principle

Source code dependencies always point toward the business.

Inner layers know nothing about outer layers.

Outer layers may depend on inner layers.

The reverse is never allowed.

---

# Dependency Direction

The allowed dependency flow is:

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

Every dependency must move toward the Domain.

---

# Domain Layer

The Domain depends on nothing.

The Domain must never reference:

* Application
* Infrastructure
* API
* Frameworks
* Databases
* Messaging
* Logging
* Configuration

The Domain is the architectural centre.

---

# Application Layer

The Application layer may depend on:

* Domain

The Application layer must not depend on:

* API
* Infrastructure implementations

Infrastructure is accessed only through abstractions.

---

# Infrastructure Layer

Infrastructure may depend on:

* Domain
* Application

Infrastructure implements contracts defined by inner layers.

Infrastructure must never own business rules.

---

# API Layer

The API layer may depend on:

* Application
* Domain (where repository conventions explicitly allow)

The API layer should never contain business logic.

---

# Dependency Inversion

Inner layers define abstractions.

Outer layers provide implementations.

Example:

Application:

```text
IEmailSender
```

Infrastructure:

```text
SmtpEmailSender
```

The abstraction belongs to the business.

The implementation belongs to infrastructure.

---

# Framework Independence

Frameworks belong to outer layers.

The Domain must never know about:

* ASP.NET Core
* EF Core
* gRPC
* RabbitMQ
* Redis
* MassTransit
* AutoMapper
* Serilog

Frameworks are replaceable.

Business rules are not.

---

# Database Independence

The database is an implementation detail.

Business code must not depend on:

* SQL
* ORM behaviour
* Table structure
* Database constraints

Persistence belongs to Infrastructure.

---

# Transport Independence

Business behaviour must not change based on transport.

Whether invoked through:

* HTTP
* gRPC
* CLI
* Background Jobs
* Messaging

the same Domain behaviour should execute.

---

# Configuration Independence

Configuration belongs to outer layers.

The Domain must not access:

* IConfiguration
* Environment Variables
* Connection Strings
* Options Pattern

Configuration should be injected through abstractions where required.

---

# Dependency Injection

The Dependency Injection container belongs only to the composition root.

The Domain and Application layers must not depend on any DI framework.

---

# Compile-Time Dependencies

Compile-time references must always follow architectural boundaries.

If Project A references Project B, then:

Project A depends on Project B.

This dependency must be architecturally valid.

---

# Runtime Dependencies

Runtime communication may occur through:

* Interfaces
* Events
* Messaging
* Contracts

Runtime interaction must not violate compile-time dependency rules.

---

# Shared Kernel

Shared Kernel projects should contain only:

* Shared abstractions
* Shared primitives
* Cross-cutting contracts

Business logic should remain inside business modules.

---

# Cross-Module Dependencies

One business module must never depend directly on another module's implementation.

Communication should occur through:

* Public contracts
* Integration Events
* Application interfaces

---

# Cyclic Dependencies

Circular dependencies are strictly prohibited.

If two projects require each other, the architecture is incorrect.

Extract the shared abstraction instead.

---

# Testing

Test projects may reference:

* The project under test.
* Supporting test infrastructure.

Production code must never depend on test projects.

---

# Dependency Checklist

Before completing an implementation, verify:

* Dependencies point inward.
* No outer layer dependency exists inside the Domain.
* Infrastructure implements abstractions.
* No circular references exist.
* Framework dependencies remain isolated.
* Configuration remains outside the Domain.
* Transport concerns remain outside business logic.

---

# Anti-Patterns

Avoid:

* Domain referencing Infrastructure.
* Domain referencing API.
* Business logic inside Infrastructure.
* Infrastructure abstractions defined in Infrastructure.
* Circular project references.
* Framework types inside the Domain.
* Repository implementations referenced by the Application.

---

# Guiding Principle

The Dependency Rule exists to protect the business from technology.

Technology changes.

Business rules endure.

The architecture should reflect that reality.
