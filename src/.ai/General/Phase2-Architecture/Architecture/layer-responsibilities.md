# Layer Responsibilities

Version: 1.0

---

# Purpose

This document defines the responsibilities of every architectural layer in the repository.

Every piece of code must belong to exactly one layer.

Responsibilities must never overlap.

When in doubt, move behaviour toward the Domain.

---

# Primary Principle

Each layer has a single responsibility.

A layer should neither know nor care about responsibilities that belong to another layer.

---

# Layer Overview

The repository consists of four architectural layers:

```text
API

↓

Application

↓

Domain

Infrastructure
```

Each layer exists for a specific purpose.

---

# Domain Layer

## Responsibility

The Domain contains the business.

It defines:

* Business concepts
* Business rules
* Business behaviour
* Business invariants

The Domain answers:

> "How does the business work?"

---

## Contains

The Domain may contain:

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

---

## Must Never Contain

The Domain must never contain:

* Controllers
* Endpoints
* HTTP
* gRPC
* EF Core
* SQL
* Logging
* Configuration
* Dependency Injection
* Message Brokers
* Serialization
* DTOs

---

# Application Layer

## Responsibility

The Application coordinates business use cases.

It answers:

> "What business operation should be executed?"

---

## Contains

Typical Application components include:

* Commands
* Queries
* Handlers
* Validators
* Interfaces
* DTOs
* Result Models
* Authorization Policies
* Mapping
* Transaction Coordination

---

## Responsibilities

The Application:

* Loads Aggregates.
* Invokes Domain behaviour.
* Coordinates repositories.
* Coordinates transactions.
* Publishes events.
* Returns results.

---

## Must Never Contain

The Application must never contain:

* Business rules
* SQL
* HTTP implementation
* Infrastructure implementation
* EF Core configuration

---

# Infrastructure Layer

## Responsibility

Infrastructure provides technical implementations.

It answers:

> "How is this technically implemented?"

---

## Contains

Infrastructure may contain:

* EF Core
* DbContext
* Repository Implementations
* Redis
* RabbitMQ
* Email
* Blob Storage
* External APIs
* Authentication Providers
* Logging
* Background Jobs

---

## Responsibilities

Infrastructure:

* Persists data.
* Calls external systems.
* Implements interfaces.
* Provides technical services.

---

## Must Never Contain

Infrastructure must never contain:

* Business decisions
* Domain policies
* Aggregate logic
* Business validation

---

# API Layer

## Responsibility

The API exposes business capabilities.

It answers:

> "How does the outside world communicate with the application?"

---

## Contains

The API may contain:

* Minimal APIs
* Controllers
* Endpoints
* Filters
* Middleware
* Authentication
* Authorization
* OpenAPI
* Request Models
* Response Models

---

## Responsibilities

The API:

* Receives requests.
* Validates transport models.
* Invokes Application use cases.
* Returns responses.

---

## Must Never Contain

The API must never contain:

* Business rules
* Persistence logic
* Repository usage
* Complex orchestration

---

# Cross-Cutting Concerns

Cross-cutting concerns span multiple layers.

Examples include:

* Logging
* Validation
* Transactions
* Authorization
* Metrics
* Tracing
* Caching

They should be implemented without violating layer responsibilities.

---

# Responsibility Matrix

| Responsibility        |    Domain    | Application |   Infrastructure   |      API      |
| --------------------- | :----------: | :---------: | :----------------: | :-----------: |
| Business Rules        |       ✓      |      ✗      |          ✗         |       ✗       |
| Use Case Coordination |       ✗      |      ✓      |          ✗         |       ✗       |
| Persistence           |       ✗      |      ✗      |          ✓         |       ✗       |
| External Services     |       ✗      |      ✗      |          ✓         |       ✗       |
| Transport             |       ✗      |      ✗      |          ✗         |       ✓       |
| Authentication        |       ✗      |  ✓ (Policy) | ✓ (Implementation) |       ✓       |
| Authorization         |       ✗      |      ✓      |          ✓         |       ✓       |
| Validation            | ✓ (Business) |  ✓ (Input)  |          ✗         | ✓ (Transport) |
| Mapping               |       ✗      |      ✓      |   ✓ (Persistence)  | ✓ (Transport) |

---

# Decision Guide

When writing new code, ask:

1. Is this a business rule?
   → Domain

2. Is this coordinating a use case?
   → Application

3. Is this talking to a database or external service?
   → Infrastructure

4. Is this handling HTTP, gRPC, or transport?
   → API

---

# Common Mistakes

Avoid:

* Business rules inside Handlers.
* Business rules inside Controllers.
* SQL inside Application.
* EF Core inside Domain.
* HTTP models inside Domain.
* Repository implementations inside Application.
* Logging inside business entities.
* Validation logic duplicated across layers.

---

# Layer Checklist

Before completing an implementation, verify:

* The code belongs to the correct layer.
* Responsibilities are not duplicated.
* Dependencies follow the Dependency Rule.
* Business rules exist only in the Domain.
* Application coordinates but does not decide.
* Infrastructure implements but does not orchestrate.
* API exposes but does not contain business logic.

---

# Guiding Principle

A well-designed architecture is one where every piece of code has an obvious home.

If you cannot clearly identify the correct layer for a piece of code, the design likely needs to be reconsidered.
