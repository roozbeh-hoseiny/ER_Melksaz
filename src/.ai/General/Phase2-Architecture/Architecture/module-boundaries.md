# Module Boundaries

Version: 1.0

---

# Purpose

This document defines how business modules are separated, how they communicate, and how their boundaries are protected.

Module boundaries are one of the most important architectural decisions in the repository.

Poor boundaries create coupling.

Strong boundaries create maintainable software.

---

# Primary Principle

Every business capability belongs to exactly one module.

A module owns its business.

Other modules may use its public contracts, but never its implementation.

---

# Definition

A module is an autonomous business unit.

It owns:

* Domain Model
* Application Layer
* Infrastructure
* API
* Database
* Business Rules
* Business Language

A module should be independently understandable.

---

# Ownership

Each module owns:

* Business concepts
* Aggregates
* Entities
* Value Objects
* Repositories
* Domain Events
* Integration Events
* Persistence
* APIs

Ownership must always be explicit.

---

# Public Surface

A module exposes only its public contracts.

Examples:

* Application Interfaces
* Public Commands
* Public Queries
* Integration Events
* Published DTOs

Everything else remains internal.

---

# Internal Implementation

The following should remain private:

* Aggregates
* EF Core configuration
* Repository implementations
* Internal services
* Internal events
* Validation rules
* Infrastructure implementation

No other module should access them directly.

---

# Communication

Modules communicate through explicit contracts only.

Allowed communication mechanisms include:

* Application Interfaces
* Integration Events
* Internal Messaging
* gRPC
* HTTP APIs

Avoid direct implementation dependencies.

---

# Dependency Direction

Allowed:

```text id="g9h2pk"
Module A

↓

Public Contract

↓

Module B
```

Forbidden:

```text id="n4d7rx"
Module A

↓

Module B Internal Class
```

---

# Domain Isolation

One module must never directly manipulate another module's Domain Model.

Avoid:

* Loading another module's Aggregate.
* Modifying another module's Entity.
* Referencing another module's Value Object.

Communicate through contracts instead.

---

# Persistence Isolation

Each module owns its own persistence.

A module must never:

* Update another module's tables.
* Execute SQL against another module's schema.
* Share migrations.

Database ownership follows business ownership.

---

# Shared Code

Only infrastructure-independent abstractions belong inside the Shared Kernel.

Never move business concepts into Shared merely to avoid duplication.

Duplication is often preferable to coupling.

---

# Cross-Module Transactions

Prefer one module per transaction.

When multiple modules participate:

* Publish Integration Events.
* Use asynchronous coordination.
* Consider Process Managers or Sagas when appropriate.

Avoid distributed transactions whenever possible.

---

# Versioning

Public contracts are versioned.

Internal implementation may evolve freely.

Changes to public contracts require careful compatibility analysis.

---

# Testing

Modules should be testable independently.

Tests for one module should not require:

* Another module's internal classes.
* Another module's database.
* Another module's test data.

Only public contracts may be referenced.

---

# Evolution

Modules should evolve independently.

Adding new features should normally affect one module.

If a feature consistently requires modifying many modules, reconsider the module boundaries.

---

# Anti-Patterns

Avoid:

* Shared Domain Models.
* Shared database tables.
* Cross-module repository access.
* Internal class references.
* Circular module dependencies.
* "Common" projects containing business logic.
* Friend assemblies exposing implementation details.

---

# Module Boundary Checklist

Before completing an implementation, verify:

* Business ownership is clear.
* Only public contracts are exposed.
* Internal implementation remains hidden.
* Persistence ownership is respected.
* Cross-module communication is explicit.
* Dependencies remain one-way.
* Modules can evolve independently.
* No unnecessary coupling has been introduced.

---

# Guiding Principle

A module should behave like an independent product inside the solution.

Other modules should know **what** it offers, but never **how** it is implemented.
