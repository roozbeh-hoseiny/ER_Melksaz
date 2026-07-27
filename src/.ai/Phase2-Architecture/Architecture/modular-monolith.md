# Modular Monolith

Version: 1.0

---

# Purpose

This document defines the Modular Monolith architecture used throughout the repository.

A Modular Monolith organises the application into independently owned business modules while deploying as a single application.

The objective is to achieve the maintainability of microservices without introducing unnecessary distributed system complexity.

---

# Primary Principle

The unit of architecture is the **business module**, not the application.

Modules are independent.

Deployment is shared.

---

# Definition

A Modular Monolith consists of:

* One deployment unit.
* Multiple independent business modules.
* Explicit module boundaries.
* Independent business models.
* Shared runtime.

Modules communicate through contracts rather than direct implementation dependencies.

---

# Module

A module represents one business capability.

Examples:

* Identity
* Billing
* Accounting
* Orders
* Inventory
* Notifications

Each module owns its business model.

---

# Module Ownership

Every module owns:

* Domain Model
* Application Layer
* Infrastructure
* API
* Persistence
* Tests

Ownership must always be explicit.

---

# Internal Structure

A typical module contains:

```text id="c9e3xk"
Billing/

    Domain/

    Application/

    Infrastructure/

    Api/

    Tests/
```

Each module is internally organised using Clean Architecture.

---

# Independence

Modules should be independently understandable.

Changes inside one module should have minimal impact on other modules.

Dependencies between modules should remain minimal.

---

# Communication

Modules communicate only through explicit contracts.

Examples:

* Public Application Interfaces
* Integration Events
* Internal Messaging
* Published Contracts

Avoid direct access to another module's internal implementation.

---

# Encapsulation

Everything inside a module is private unless explicitly exposed.

Implementation details should remain hidden.

Public contracts define the module boundary.

---

# Dependencies

A module may depend only on:

* Shared Kernel (if one exists)
* Published contracts of another module

A module must never depend on another module's internal classes.

---

# Database Ownership

Each module owns its own persistence model.

A module should not directly manipulate another module's tables.

Database ownership follows module ownership.

---

# Transactions

Business transactions should remain inside one module whenever possible.

Cross-module workflows should use:

* Domain Events
* Integration Events
* Process Managers
* Sagas (when required)

Avoid large distributed transactions.

---

# Shared Kernel

Only truly shared concepts belong in the Shared Kernel.

Examples may include:

* Base abstractions
* Common primitives
* Cross-cutting contracts

Business concepts should rarely belong here.

Keep the Shared Kernel small.

---

# Cross-Cutting Concerns

Cross-cutting concerns should be implemented consistently across all modules.

Examples include:

* Logging
* Metrics
* Authorization
* Validation
* Transactions
* Caching

Implementation should not weaken module boundaries.

---

# Scalability

A Modular Monolith should support future extraction into microservices if business needs require it.

However, modules should **not** be designed as premature microservices.

Design for modularity first.

---

# Testing

Modules should be testable independently.

Typical tests include:

* Domain Tests
* Application Tests
* Integration Tests
* API Tests

Module tests should not depend on unrelated modules.

---

# Evolution

Modules should evolve independently.

Adding a new business capability should normally involve extending one module or creating a new one—not modifying many unrelated modules.

---

# Anti-Patterns

Avoid:

* Shared business logic across modules.
* Shared database tables.
* Direct references to internal module classes.
* Circular module dependencies.
* Large "Common" projects containing business logic.
* Modules organised around technical concerns instead of business capabilities.

---

# Modular Monolith Checklist

Before completing a feature, verify:

* Business ownership is clear.
* Module boundaries are respected.
* Internal implementation remains encapsulated.
* Communication uses explicit contracts.
* Database ownership is preserved.
* Cross-module coupling is minimal.
* Tests remain isolated to the module.
* Shared Kernel has not grown unnecessarily.

---

# Guiding Principle

A Modular Monolith should feel like a collection of well-designed business systems living inside one deployment.

Modules should be able to evolve independently while remaining part of a cohesive application.
