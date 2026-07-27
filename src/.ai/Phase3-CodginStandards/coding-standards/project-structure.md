# Project Structure

Version: 1.0

---

# Purpose

This document defines the mandatory project structure for the repository.

A consistent project structure makes the solution easier to navigate, understand, maintain, and extend.

Every engineer and every AI agent must preserve this structure.

---

# Primary Principle

Projects should represent architectural responsibilities—not technical convenience.

A project exists because it has a clear architectural purpose.

---

# Solution Structure

A typical solution should follow this structure:

```text
src/

    SharedKernel/

    Modules/

        Billing/

        Identity/

        Orders/

        Customers/

    Host/

tests/

docs/

.ai/

build/
```

The exact module names depend on the business domain.

---

# Layer Structure Inside a Module

Each module should contain separate projects for each architectural layer.

Example:

```text
Billing/

    Billing.Domain/

    Billing.Application/

    Billing.Infrastructure/

    Billing.Api/

    Billing.Contracts/
```

Each project has one clear responsibility.

---

# Domain Project

Contains:

* Aggregates
* Entities
* Value Objects
* Domain Events
* Domain Services
* Specifications
* Repository Interfaces
* Business Exceptions

The Domain depends on nothing.

---

# Application Project

Contains:

* Commands
* Queries
* Handlers
* Validators
* Application Services
* Interfaces
* Transaction coordination

The Application depends only on the Domain.

---

# Infrastructure Project

Contains:

* EF Core
* DbContext
* Repository implementations
* Messaging
* Authentication implementations
* External integrations
* Caching
* Logging
* Configuration

Infrastructure implements abstractions defined by inner layers.

---

# API Project

Contains:

* Endpoints
* Controllers
* Middleware
* Request models
* Response models
* OpenAPI configuration
* Authentication configuration

The API exposes the Application.

---

# Contracts Project

Contains public contracts shared outside the module.

Examples:

* gRPC contracts
* Integration Events
* Public DTOs
* API Contracts

Contracts should remain technology-neutral whenever possible.

---

# Shared Kernel

The Shared Kernel is a separate project.

It contains only:

* Stable abstractions
* Base Domain types
* Shared primitives
* Cross-cutting contracts

It never contains business logic.

---

# Tests

Tests should mirror the production structure.

Example:

```text
tests/

    Billing.Domain.Tests/

    Billing.Application.Tests/

    Billing.Infrastructure.Tests/

    Billing.Api.Tests/
```

Test projects should be easy to locate.

---

# Documentation

Repository documentation should be organized separately.

Example:

```text
docs/

    adr/

    architecture/

    api/

    operations/
```

Documentation is part of the repository.

---

# Build Scripts

Build automation belongs in a dedicated folder.

Example:

```text
build/

    build.ps1

    build.sh

    test.ps1

    publish.ps1
```

Build scripts should not be scattered throughout the repository.

---

# AI Handbook

AI documentation belongs in:

```text
.ai/
```

Example:

```text
.ai/

    core/

    architecture/

    coding-standards/

    testing/

    patterns/

    templates/
```

---

# Dependencies

Project references must follow the Dependency Rule.

Allowed:

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

Forbidden:

* Domain → Infrastructure
* Domain → API
* Application → Infrastructure Implementation

---

# Project Naming

Projects should use consistent naming.

Examples:

```text
Billing.Domain

Billing.Application

Billing.Infrastructure

Billing.Api

Billing.Contracts
```

Avoid inconsistent suffixes.

---

# Repository Growth

When adding new functionality:

* Reuse existing projects.
* Avoid creating unnecessary projects.
* Keep module ownership explicit.
* Preserve architectural boundaries.

Do not create projects for temporary convenience.

---

# Anti-Patterns

Avoid:

* Shared "Common" projects containing business logic.
* Utility projects with unrelated responsibilities.
* Mixed architectural layers in one project.
* Circular project references.
* Feature-specific infrastructure projects.
* Random folder structures.

---

# Project Structure Checklist

Before creating a new project, verify:

* It has a single architectural responsibility.
* It follows the Dependency Rule.
* It belongs to one module.
* Existing projects cannot reasonably contain the new functionality.
* Naming follows repository conventions.
* The project improves—not complicates—the architecture.

---

# Guiding Principle

The project structure should communicate the architecture at a glance.

A new engineer should understand the system's organisation simply by exploring the solution tree.
