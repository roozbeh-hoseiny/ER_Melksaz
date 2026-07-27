# Shared Kernel

Version: 1.0

---

# Purpose

This document defines the design rules for the Shared Kernel.

The Shared Kernel contains the minimal set of concepts that are intentionally shared across multiple Bounded Contexts or Modules.

Its primary goal is to reduce duplication **without introducing coupling**.

The Shared Kernel must remain small, stable, and highly disciplined.

---

# Primary Principle

Share only what is truly universal.

Business concepts belong inside their owning Bounded Context unless there is a compelling reason to share them.

---

# Definition

The Shared Kernel contains abstractions and primitives that are stable across the entire solution.

It is **not** a place for miscellaneous reusable code.

---

# Ownership

The Shared Kernel is jointly owned by all modules that depend on it.

Any change must be treated as a breaking architectural decision.

Changes require careful review.

---

# May Contain

Typical contents include:

* Strongly Typed IDs
* Result Pattern
* Error abstractions
* Guard utilities
* Clock abstractions
* Common interfaces
* Base Domain abstractions
* Base Entity classes
* Base Aggregate classes
* Domain Event abstractions
* Value Object base classes
* Enumeration base classes
* Cross-cutting contracts

Everything should be framework independent.

---

# Must Not Contain

The Shared Kernel must never contain:

* Business rules
* Aggregates
* Business services
* Module-specific entities
* Module-specific Value Objects
* Repositories
* Application services
* Infrastructure implementations
* DTOs
* API models

Business belongs to business modules.

---

# Business Independence

A Shared Kernel must never create hidden business coupling.

If two modules evolve independently, they should not share business models.

Duplicate business concepts are preferable to incorrect sharing.

---

# Stability

Objects placed inside the Shared Kernel should change rarely.

If a type changes frequently, it probably belongs inside a module instead.

---

# Framework Independence

The Shared Kernel must remain independent of:

* ASP.NET Core
* EF Core
* SQL
* Redis
* RabbitMQ
* MassTransit
* Logging
* Configuration

The Shared Kernel should compile without infrastructure.

---

# Base Classes

Base classes should remain minimal.

Examples:

* Entity
* AggregateRoot
* ValueObject
* DomainEvent

Avoid large inheritance hierarchies.

---

# Interfaces

Shared interfaces should represent stable abstractions.

Examples:

```text id="q8w2pk"
IClock

IDomainEvent

IEntity

IAggregateRoot
```

Interfaces should not expose implementation details.

---

# Result Pattern

A shared Result type may exist when used consistently throughout the solution.

The Result abstraction should remain lightweight and independent of transport technologies.

---

# Strongly Typed IDs

Strongly Typed IDs belong naturally inside the Shared Kernel when used consistently across modules.

Examples:

```text id="x6m9ra"
CustomerId

InvoiceId

OrderId
```

Generation mechanisms should remain infrastructure independent.

---

# Cross-Cutting Contracts

Contracts shared across modules may include:

* Time providers
* Identifier generators
* Domain Event abstractions
* Error abstractions

Implementations belong elsewhere.

---

# Versioning

Changes to the Shared Kernel should be rare.

Whenever possible:

* Extend instead of modifying.
* Preserve backward compatibility.
* Avoid unnecessary breaking changes.

---

# Dependency Rule

Business modules may depend on the Shared Kernel.

The Shared Kernel must never depend on business modules.

Dependency direction must always point inward.

---

# Testing

The Shared Kernel should have comprehensive unit tests.

Shared abstractions become foundational building blocks for the entire repository.

---

# Anti-Patterns

Avoid:

* Utility dumping grounds.
* Business entities.
* Module-specific abstractions.
* Infrastructure implementations.
* Generic helper classes with unrelated functionality.
* Frequently changing types.
* Hidden dependencies between modules.

---

# Shared Kernel Checklist

Before adding anything to the Shared Kernel, verify:

* Is it truly shared?
* Is it framework independent?
* Is it stable?
* Does it avoid business ownership conflicts?
* Can every module safely depend on it?
* Does it avoid unnecessary coupling?
* Is duplication a better alternative?

If any answer is uncertain, keep the type inside its owning module.

---

# Guiding Principle

The Shared Kernel is the foundation of the architecture.

Every type placed inside it becomes a dependency for the entire solution.

Keep it intentionally small, stable, and independent.
