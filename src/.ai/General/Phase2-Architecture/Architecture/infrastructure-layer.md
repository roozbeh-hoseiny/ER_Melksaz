# Infrastructure Layer

Version: 1.0

---

# Purpose

This document defines the responsibilities, boundaries, and design principles of the Infrastructure Layer.

The Infrastructure Layer contains all technical implementations required by the application.

It provides concrete implementations of abstractions defined by the Domain and Application layers while keeping technical concerns isolated from business logic.

---

# Primary Principle

The Infrastructure Layer answers one question:

> **How is this technically implemented?**

It must never answer:

> **How does the business work?**

---

# Responsibilities

The Infrastructure Layer is responsible for:

* Data persistence.
* External system integration.
* Messaging.
* Authentication implementations.
* File storage.
* Email.
* Caching.
* Logging.
* Background processing.
* Third-party integrations.
* Repository implementations.

Everything here is an implementation detail.

---

# Contains

Typical Infrastructure components include:

* DbContext
* Repository Implementations
* Unit of Work Implementations
* EF Core Configurations
* SQL Scripts
* Redis
* RabbitMQ
* Kafka
* Blob Storage
* SMTP
* External API Clients
* Identity Providers
* Background Workers
* Cache Providers
* File Storage Providers

---

# Does Not Contain

The Infrastructure Layer must never contain:

* Business rules.
* Business decisions.
* Aggregate logic.
* Domain policies.
* Business validation.
* Transport logic.

Business behaviour belongs inside the Domain.

---

# Repository Implementations

Infrastructure implements Repository interfaces defined by the Domain.

Example:

```text id="h4j2rm"
Domain

    IInvoiceRepository

↓

Infrastructure

    InvoiceRepository
```

Infrastructure owns persistence.

The Domain owns the abstraction.

---

# Persistence

Persistence technologies are implementation details.

Examples:

* EF Core
* Dapper
* MongoDB
* Cosmos DB
* PostgreSQL
* SQL Server

Changing persistence technology should not affect business logic.

---

# EF Core

EF Core belongs exclusively to Infrastructure.

Examples include:

* DbContext
* EntityTypeConfiguration
* Value Converters
* Migrations
* Interceptors

No EF Core types should appear inside the Domain.

---

# External Services

Infrastructure integrates with external systems such as:

* Payment gateways
* Email providers
* SMS providers
* Identity providers
* REST APIs
* gRPC services

External integrations should be hidden behind abstractions.

---

# Messaging

Infrastructure implements messaging technologies.

Examples:

* RabbitMQ
* Kafka
* Azure Service Bus
* MassTransit

Business code should publish through abstractions.

Messaging frameworks remain isolated.

---

# Authentication

Authentication implementation belongs to Infrastructure.

Examples:

* JWT
* OAuth
* OpenID Connect
* Microsoft Identity
* Cookie Authentication

Business authorization remains inside the Application layer.

---

# Logging

Infrastructure configures logging.

Examples:

* Serilog
* OpenTelemetry
* Elastic
* Loki

Business objects should never perform logging directly.

---

# Caching

Caching implementation belongs to Infrastructure.

Examples:

* Redis
* Memory Cache
* FusionCache

Business behaviour must remain correct regardless of whether caching exists.

---

# Dependency Rule

Infrastructure may depend on:

* Application
* Domain

Neither the Domain nor the Application may depend on Infrastructure implementations.

---

# Configuration

Infrastructure owns:

* Connection strings.
* Provider configuration.
* Third-party configuration.
* Client configuration.

Configuration should never leak into the Domain.

---

# Error Translation

Infrastructure should translate technical failures into meaningful application-level failures where appropriate.

Avoid exposing database or framework exceptions directly.

---

# Testing

Infrastructure should primarily be tested using integration tests.

Examples:

* Repository tests.
* Database tests.
* Messaging tests.
* External API tests.
* Cache tests.

Mocking Infrastructure should not replace integration testing.

---

# Evolution

Infrastructure should evolve independently.

Replacing one implementation should require little or no change to the Domain or Application layers.

---

# Anti-Patterns

Avoid:

* Business rules inside repositories.
* Business rules inside DbContext.
* Business validation inside EF configurations.
* Domain references to EF Core.
* SQL inside the Application layer.
* Framework types inside the Domain.
* Direct access to Infrastructure implementations from API.

---

# Infrastructure Layer Checklist

Before completing an Infrastructure implementation, verify:

* Business logic is absent.
* Interfaces are implemented correctly.
* Technical concerns remain isolated.
* Dependency direction is correct.
* EF Core is isolated.
* External systems are hidden behind abstractions.
* Configuration remains outside the Domain.
* Integration tests exist.

---

# Guiding Principle

The Infrastructure Layer is replaceable.

If every technical implementation changed tomorrow, the Domain and Application layers should require little or no modification.
