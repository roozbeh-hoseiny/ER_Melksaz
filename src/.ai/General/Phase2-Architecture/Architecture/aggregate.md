# Aggregate

Version: 1.0

---

# Purpose

This document defines the design rules for Aggregates used throughout the repository.

An Aggregate is the primary consistency boundary of the Domain Model.

It protects business invariants and ensures that business rules are enforced regardless of how the system evolves.

---

# Primary Principle

An Aggregate exists to protect business consistency.

Everything inside an Aggregate changes together.

Everything outside an Aggregate communicates through the Aggregate Root.

---

# Definition

An Aggregate is a cluster of:

* Aggregate Root
* Entities
* Value Objects

that together represent a single business consistency boundary.

Only the Aggregate Root is visible outside the Aggregate.

---

# Aggregate Root

Every Aggregate must have exactly one Aggregate Root.

The Aggregate Root:

* Owns the Aggregate.
* Controls all modifications.
* Protects invariants.
* Coordinates child entities.
* Raises Domain Events when appropriate.

Repositories work only with Aggregate Roots.

---

# Consistency Boundary

Business rules that must always remain true belong inside the same Aggregate.

If two business rules require immediate consistency, they belong in the same Aggregate.

---

# Encapsulation

All state changes must pass through the Aggregate Root.

External code must never:

* Modify child entities directly.
* Modify collections directly.
* Change internal state through public setters.

The Aggregate controls its own consistency.

---

# Business Behaviour

Aggregates expose business operations.

Examples:

```text id="7bm2pr"
Approve()

Reject()

Cancel()

AddItem()

RemoveItem()

ReceivePayment()
```

Avoid exposing data manipulation methods.

Business behaviour is preferred over CRUD operations.

---

# State Changes

Every state change should:

* Validate business rules.
* Protect invariants.
* Produce a valid Aggregate state.

Invalid state should never exist inside an Aggregate.

---

# Invariants

The Aggregate is responsible for protecting all business invariants.

Examples:

* Invoice total cannot become negative.
* Order must contain at least one item.
* Payment cannot exceed remaining balance.
* Shipment cannot be dispatched twice.

No external component may bypass these rules.

---

# Child Entities

Child Entities exist only within the Aggregate.

They:

* Have identity.
* Are owned by the Aggregate.
* Cannot exist independently.

External code should never reference child entities directly.

---

# Value Objects

Value Objects may be freely used inside Aggregates.

Value Objects should:

* Be immutable.
* Self-validate.
* Represent meaningful business concepts.

---

# Aggregate Size

Aggregates should remain small.

Large Aggregates:

* Reduce scalability.
* Increase contention.
* Increase transaction duration.

Include only data required to enforce business consistency.

---

# Transactions

One Aggregate equals one transactional consistency boundary.

A transaction should normally modify one Aggregate.

Multiple Aggregates should only participate in the same business process when explicitly required.

---

# References

Aggregates should reference other Aggregates by identity rather than object references.

Good:

```text id="hrhcgq"
CustomerId
```

Avoid:

```text id="jlwm8v"
Customer
```

This reduces coupling.

---

# Persistence

Persistence is an implementation detail.

Aggregates must not contain:

* EF Core attributes.
* ORM configuration.
* Database logic.
* SQL knowledge.

Aggregates remain persistence ignorant.

---

# Constructors

Aggregates should always start in a valid state.

Construction should enforce mandatory business rules.

Avoid creating partially initialised Aggregates.

---

# Mutability

Internal state may change only through business methods.

Avoid exposing mutable collections.

Prefer read-only views.

---

# Domain Events

When important business events occur, the Aggregate may raise Domain Events.

Examples:

* InvoiceCreated
* InvoicePaid
* OrderCancelled

Domain Events describe completed business facts.

---

# Repository Access

Repositories:

* Load Aggregate Roots.
* Persist Aggregate Roots.

Repositories never expose internal child entities.

---

# Validation

Input validation belongs outside the Aggregate.

Business validation belongs inside the Aggregate.

The Aggregate protects business correctness.

---

# Lifecycle

The Aggregate Root controls:

* Creation
* Modification
* State transitions
* Completion
* Archival (where applicable)

The Aggregate lifecycle should always remain explicit.

---

# Testing

Aggregates should be tested through business behaviour.

Tests should verify:

* State transitions.
* Business rules.
* Domain Events.
* Invalid operations.
* Invariant protection.

Avoid testing implementation details.

---

# Anti-Patterns

Avoid:

* Public setters.
* Anemic Aggregates.
* CRUD-style methods.
* Exposing child collections.
* Cross-Aggregate object references.
* Persistence logic.
* Infrastructure dependencies.
* Business logic outside the Aggregate.

---

# Review Checklist

Before completing an Aggregate, verify:

* Exactly one Aggregate Root exists.
* Invariants are protected.
* Behaviour is business-oriented.
* Internal state is encapsulated.
* Child entities are inaccessible externally.
* Value Objects are immutable.
* Domain remains persistence ignorant.
* Aggregate size is justified.

---

# Guiding Principle

An Aggregate is not a collection of objects.

It is the guardian of business consistency and the only authority permitted to change its own business state.
