# Bounded Context

Version: 1.0

---

# Purpose

This document defines how Bounded Contexts are identified, designed, and maintained within the repository.

A Bounded Context is the primary architectural boundary of the business model.

Every business capability belongs to exactly one Bounded Context.

---

# Primary Principle

A business concept has one meaning inside one Bounded Context.

Different contexts may use the same word with different meanings.

This is expected.

---

# Definition

A Bounded Context defines:

* A business boundary.
* A language boundary.
* An ownership boundary.
* A deployment boundary (where applicable).
* A consistency boundary.

Everything inside a context should speak the same business language.

---

# Business Ownership

Each Bounded Context owns:

* Its business rules.
* Its domain model.
* Its aggregates.
* Its database schema.
* Its APIs.
* Its integration events.

Ownership must never be ambiguous.

---

# Ubiquitous Language

Every Bounded Context has its own Ubiquitous Language.

The same business term may legitimately have different meanings in different contexts.

Example:

```text id="qg7e2p"
Accounting → Invoice

Sales → Invoice

Tax → Invoice
```

They are not necessarily the same concept.

---

# Independence

A Bounded Context should be independently understandable.

Developers should be able to understand the business model of one context without understanding every other context.

---

# Internal Consistency

Inside a context:

* Naming must be consistent.
* Business rules must be consistent.
* Aggregates must be consistent.
* Events must be consistent.

Avoid multiple competing models.

---

# Communication Between Contexts

Bounded Contexts communicate through explicit contracts.

Examples include:

* APIs
* gRPC
* Integration Events
* Messaging

Never expose internal domain models directly.

---

# Shared Database

Bounded Contexts must never share database tables.

Each context owns its persistence.

Shared databases increase coupling and reduce autonomy.

---

# Shared Domain Models

Do not share:

* Aggregates
* Entities
* Value Objects

between contexts.

Instead, communicate using contracts specifically designed for integration.

---

# Anti-Corruption Layer

When integrating with another context, use an Anti-Corruption Layer (ACL).

Responsibilities include:

* Translation
* Mapping
* Model isolation
* Terminology isolation

The internal domain model must remain protected.

---

# Context Boundaries

A Bounded Context should contain:

* Domain
* Application
* Infrastructure
* API

for that business capability.

The context owns its complete implementation.

---

# Repository Structure

Typical structure:

```text id="3m5k7a"
Billing/

    Domain

    Application

    Infrastructure

    Api
```

Each context evolves independently.

---

# Dependencies

Contexts should communicate through contracts.

Avoid direct project references between Domains.

A Domain must never depend on another Domain.

---

# Events

Integration Events communicate between contexts.

Domain Events remain internal to their owning context.

Never publish internal Domain Events directly to other contexts.

---

# Database Ownership

Each context owns:

* Tables
* Indexes
* Migrations
* Persistence configuration

No other context should modify another context's persistence.

---

# Team Ownership

Where possible, one team should own one Bounded Context.

Ownership reduces ambiguity and simplifies evolution.

---

# Evolution

Contexts evolve independently.

Changes inside one context should have minimal impact on others.

Stable contracts enable independent deployment.

---

# Anti-Patterns

Avoid:

* Shared Domain Models.
* Shared Entities.
* Shared Value Objects.
* Shared Database Tables.
* Cross-context Aggregate references.
* Direct Domain dependencies.
* Leaking internal terminology.

---

# Review Checklist

Before introducing or modifying a Bounded Context, verify:

* Does it represent one business capability?
* Is ownership clear?
* Is the language consistent?
* Are boundaries explicit?
* Are integrations contract-based?
* Is persistence independently owned?
* Are internal models protected?

---

# Guiding Principle

A Bounded Context is not a technical boundary.

It is a business boundary that protects the integrity of the domain model while enabling independent evolution.
