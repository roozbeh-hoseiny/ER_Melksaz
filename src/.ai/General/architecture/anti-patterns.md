# Architectural Anti-Patterns

Version: 1.0

---

# Purpose

This document defines the architectural anti-patterns that are prohibited within the repository.

An anti-pattern is a recurring solution that appears useful but causes long-term architectural degradation.

Every AI agent and developer must recognise and avoid these patterns.

---

# Guiding Principle

A solution that works today but damages tomorrow's maintainability is not an acceptable solution.

Long-term architectural integrity always has higher priority than short-term implementation speed.

---

# God Class

A class must never own multiple unrelated responsibilities.

Typical symptoms include:

* Hundreds of lines of code.
* Numerous dependencies.
* Multiple business concepts.
* Large public APIs.
* Frequent modifications.

When detected:

* Split responsibilities.
* Extract cohesive behaviour.
* Preserve encapsulation.

---

# God Service

Application Services should coordinate a single use case.

Avoid services responsible for:

* Validation
* Business Rules
* Persistence
* Notifications
* External Integrations
* Mapping

within the same class.

---

# Anemic Domain Model

Domain objects must contain behaviour.

Avoid Domain models that only contain:

* Properties
* Getters
* Setters

while business rules exist elsewhere.

Business behaviour belongs inside the Domain.

---

# Business Logic in Infrastructure

Infrastructure exists only to implement technical concerns.

Never implement business rules inside:

* Repository implementations
* DbContext
* Messaging adapters
* Cache providers
* File storage
* External integrations

---

# Business Logic in API

Endpoints are transport adapters.

Never implement:

* Business decisions
* State transitions
* Validation rules
* Domain behaviour

inside API endpoints.

---

# Fat Handlers

Application handlers coordinate work.

Handlers should not contain:

* Complex business logic
* Long algorithms
* Multiple workflows
* Persistence logic
* Mapping logic

Business behaviour belongs in the Domain.

---

# Service Locator

Never resolve dependencies manually.

Avoid:

```csharp
var service = provider.GetRequiredService<T>();
```

outside the composition root.

Dependencies must remain explicit.

---

# Static State

Avoid mutable static members.

Static state creates:

* Hidden dependencies
* Shared mutable state
* Test interference
* Concurrency issues

---

# Generic Helper Classes

Avoid classes named:

* Helper
* Utils
* Common
* Manager
* Processor
* Toolkit

Names should describe business responsibility.

---

# Primitive Obsession

Do not model business concepts using primitive types alone.

Prefer dedicated domain concepts when they represent meaningful business values.

Examples include:

* Money
* Email
* CustomerId
* InvoiceNumber
* Quantity

---

# Boolean Flags

Avoid methods whose behaviour changes using boolean parameters.

Bad:

```csharp
Save(true);
```

Prefer explicit methods.

---

# Long Parameter Lists

Large parameter lists usually indicate poor design.

Group related data into dedicated types where appropriate.

---

# Circular Dependencies

Circular dependencies are prohibited.

They increase coupling and reduce maintainability.

Every dependency must have a clear direction.

---

# Deep Inheritance

Prefer composition over inheritance.

Inheritance should model true "is-a" relationships.

Never inherit solely for code reuse.

---

# Feature Envy

A class should primarily operate on its own data.

If a class manipulates another object's internal state extensively, the behaviour probably belongs elsewhere.

---

# Shotgun Surgery

Avoid designs where a single business change requires modifications across many unrelated files.

Related behaviour should remain cohesive.

---

# Duplicate Knowledge

Business rules should exist in one place.

Do not duplicate:

* Validation
* Calculations
* State transitions
* Business policies

Duplication creates inconsistent behaviour.

---

# Magic Values

Avoid unexplained constants.

Replace repeated literals with named concepts.

Business meaning should be explicit.

---

# Catch-All Exceptions

Avoid:

```csharp
catch (Exception)
{
}
```

unless the exception is immediately rethrown or transformed according to repository policy.

Never swallow exceptions.

---

# Returning Null

Avoid returning null to indicate business outcomes.

Prefer:

* Result Pattern
* Empty collections
* Optional abstractions

Null introduces ambiguity.

---

# Premature Abstraction

Do not introduce abstractions until they provide measurable value.

Avoid designing for hypothetical future requirements.

---

# Framework-Driven Design

Business design must not be dictated by:

* ORM limitations
* Framework conventions
* Transport protocols
* Database structure

Business concepts always come first.

---

# Excessive Configuration

Avoid making every behaviour configurable.

Configuration should solve real requirements—not hypothetical flexibility.

---

# Over-Engineering

Do not introduce:

* Extra layers
* Generic frameworks
* Complex patterns
* Unnecessary extension points

without a clear business need.

---

# Repository Review

Whenever one of these anti-patterns appears, the AI agent should:

1. Detect it.
2. Explain why it is harmful.
3. Refactor toward the repository architecture.
4. Preserve behaviour.

---

# Anti-Pattern Checklist

Before completing an implementation, verify that none of the following exist:

* God Classes
* God Services
* Anemic Domain Models
* Fat Handlers
* Business Logic in API
* Business Logic in Infrastructure
* Service Locator
* Static Mutable State
* Circular Dependencies
* Primitive Obsession
* Boolean Flags
* Duplicate Business Rules
* Magic Values
* Catch-All Exceptions
* Premature Abstractions
* Framework-Driven Design
* Over-Engineering

---

# Guiding Principle

Good architecture is not only defined by the patterns it uses.

It is equally defined by the anti-patterns it consistently avoids.
