# Project Structure

Version: 1.0

---

# Purpose

This document defines how projects are organised within the solution.

A consistent project structure improves maintainability, scalability, discoverability, and AI-assisted development.

Projects represent architectural boundaries, not implementation details.

---

# Objectives

The project structure should:

* Reflect the architecture.
* Reflect business capabilities.
* Minimise coupling.
* Maximise cohesion.
* Support independent testing.
* Support long-term evolution.

---

# Solution Structure

A solution should be organised around architectural layers and business modules.

Example:

```text
src/

    Company.Product.Domain

    Company.Product.Application

    Company.Product.Infrastructure

    Company.Product.Api

tests/

    Company.Product.Domain.Tests

    Company.Product.Application.Tests

    Company.Product.Infrastructure.Tests

    Company.Product.Api.Tests
```

---

# Project Responsibilities

Each project should have exactly one architectural responsibility.

Avoid projects that mix multiple concerns.

---

# Domain Project

The Domain project contains:

* Aggregates
* Entities
* Value Objects
* Domain Events
* Specifications
* Domain Services
* Repository Contracts
* Business Rules

The Domain project must not reference any external frameworks.

---

# Application Project

The Application project contains:

* Commands
* Queries
* Handlers
* Validators
* Application Services
* Behaviours
* Mapping
* Contracts
* Authorization
* Caching Abstractions

Business workflows are coordinated here.

---

# Infrastructure Project

The Infrastructure project contains implementation details.

Examples include:

* EF Core
* Repository Implementations
* Database Configurations
* Message Brokers
* Redis
* Logging
* File Storage
* Identity Providers
* Email Providers

Infrastructure should implement abstractions defined by inner layers.

---

# API Project

The API project exposes application functionality.

Typical contents include:

* Endpoints
* Requests
* Responses
* Middleware
* Authentication
* Authorization
* OpenAPI
* Dependency Registration

The API project should remain thin.

---

# Test Projects

Each production project should have a corresponding test project.

Example:

```text
Company.Product.Domain

↓

Company.Product.Domain.Tests
```

Tests should mirror the production architecture.

---

# One Responsibility Per Project

Projects should never mix unrelated responsibilities.

Avoid projects such as:

```text
Company.Common

Company.Utilities

Company.Helpers

Company.SharedLogic
```

unless their responsibility is clearly defined and stable.

---

# Naming

Project names should follow a predictable format.

Example:

```text
Company.Module.Layer
```

Examples:

```text
Company.Identity.Domain

Company.Identity.Application

Company.Identity.Infrastructure

Company.Identity.Api
```

---

# Dependency Direction

Projects must respect architectural dependency rules.

Allowed dependencies:

```text
Api

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

The Domain project must have no project dependencies.

---

# Shared Projects

Create shared projects only when the abstraction is genuinely shared across multiple modules.

Avoid extracting code solely to reduce duplication.

Shared projects increase coupling.

---

# External Libraries

External packages should be referenced only by the projects that require them.

Avoid unnecessary package references.

The Domain project should have the fewest dependencies.

---

# Build Independence

Projects should compile independently whenever possible.

A project should expose only its intended public surface.

---

# Internal Visibility

Implementation details should remain internal whenever practical.

Expose only contracts intended for consumption by other projects.

---

# Configuration

Configuration belongs to outer layers.

The Domain project must never depend on configuration files or configuration providers.

---

# Resources

Static resources should remain within the project that owns them.

Avoid cross-project resource dependencies.

---

# Documentation

Every project should have a clearly defined architectural purpose.

Its responsibility should be obvious from its name.

---

# Growth

New projects should only be created when they introduce a new architectural boundary.

Do not create projects merely to reduce file counts.

---

# Review Checklist

Before creating a new project, verify:

* Does it represent a single architectural responsibility?
* Does it respect dependency direction?
* Does it minimise coupling?
* Is there an existing project that already fulfils this purpose?
* Is its name consistent with the repository?
* Will another developer immediately understand its role?

Only introduce a new project when it provides clear architectural value.

---

# Guiding Principle

Projects define architectural boundaries.

Every project should exist for one reason, have one responsibility, and expose one clear purpose.
