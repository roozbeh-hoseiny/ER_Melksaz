# Entity

Version: 1.0

---

# Purpose

This document defines the design rules for Entities within the Domain Model.

An Entity represents a business concept that has a unique identity and a lifecycle.

Entities are behaviour-rich business objects—not data containers.

---

# Primary Principle

Identity defines an Entity.

State may change.

Identity never changes.

---

# Definition

An Entity is characterised by:

* Identity
* Lifecycle
* Mutable state
* Business behaviour

Two Entities with the same identity represent the same business object regardless of their current state.

---

# Identity

Every Entity must have a unique identity.

Identity should represent a business concept whenever possible.

Examples:

```text id="a3k8pm"
CustomerId

InvoiceId

OrderId

PaymentId
```

Avoid exposing database-generated identifiers as business concepts.

---

# Equality

Entity equality is determined solely by identity.

Two Entities with the same identity are equal, even if some properties differ.

Business behaviour should never depend on reference equality.

---

# Lifecycle

Entities evolve over time.

Typical lifecycle operations include:

* Creation
* Modification
* Activation
* Suspension
* Completion
* Cancellation
* Archival

Lifecycle transitions must follow business rules.

---

# Behaviour

Entities encapsulate business behaviour.

Examples:

```text id="i2j7lr"
Approve()

Cancel()

Rename()

Activate()

Deactivate()

AssignTo()

MarkAsPaid()
```

Avoid CRUD-style methods.

Business methods should communicate intent.

---

# State Protection

Entity state must be modified only through business operations.

Avoid:

* Public setters.
* Mutable public fields.
* Direct state manipulation.

Entities control their own consistency.

---

# Business Rules

Entities are responsible for enforcing business rules that belong to them.

Business validation belongs inside the Entity.

External code must not bypass these rules.

---

# Encapsulation

Hide implementation details.

Expose behaviour rather than data.

Collections should be read-only from outside the Entity.

---

# Constructors

Every Entity must be created in a valid state.

Required business data should be provided during construction.

Avoid partially initialised Entities.

---

# Mutability

Entities are mutable because their business state changes over time.

However, mutations must always occur through controlled business behaviour.

---

# Relationships

Entities may reference:

* Value Objects
* Child Entities
* Aggregate Root
* Other Aggregates by identity

Avoid direct object references between Aggregates.

Prefer identifiers.

---

# Persistence

Persistence is an implementation detail.

Entities must not contain:

* EF Core attributes.
* ORM configuration.
* SQL.
* Database-specific logic.

Entities remain persistence ignorant.

---

# Domain Events

Entities may trigger Domain Events through their Aggregate Root when important business events occur.

Domain Events describe completed business facts.

---

# Validation

Separate two kinds of validation:

Input validation:

* Format
* Length
* Required fields

belongs outside the Entity.

Business validation:

* State transitions
* Business policies
* Invariants

belongs inside the Entity.

---

# Immutability

Identity should never change.

Business state changes through explicit behaviour.

Value Objects inside an Entity should remain immutable.

---

# Testing

Entity tests should verify:

* Business behaviour.
* Valid state transitions.
* Invalid operations.
* Identity equality.
* Business rule enforcement.

Tests should avoid persistence concerns.

---

# Anti-Patterns

Avoid:

* Public setters.
* Getter/Setter-only models.
* Anemic Entities.
* Business logic in services instead of Entities.
* Infrastructure dependencies.
* ORM attributes.
* Static mutable state.
* Primitive Obsession.

---

# Entity Checklist

Before completing an Entity, verify:

* Identity is clearly defined.
* Equality is based on identity.
* Business behaviour exists.
* State is protected.
* Business rules are enforced.
* Persistence concerns are absent.
* Public setters are avoided.
* Constructors create valid instances.

---

# Guiding Principle

An Entity is not a database record.

It is a living business object that owns its identity, protects its state, and encapsulates the business behaviour associated with its lifecycle.
