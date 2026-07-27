# Architectural Principles

Version: 1.0

---

# Purpose

This document defines the fundamental architectural principles that govern every solution built in this repository.

These principles are mandatory.

Whenever there is uncertainty, these principles take precedence over implementation preferences.

---

# Principle 1 — Business First

The business is the centre of the architecture.

Every technical decision must support the business.

Technology must never dictate the business model.

---

# Principle 2 — Domain is King

The Domain Layer owns:

* Business behaviour
* Business rules
* Business terminology
* Business invariants

No other layer may implement business logic.

---

# Principle 3 — Technology is Replaceable

The following are implementation details:

* Database
* ORM
* Message Broker
* Cache
* Web Framework
* Logging
* Authentication Provider

Replacing any of these should require minimal changes outside Infrastructure.

---

# Principle 4 — Dependencies Always Point Inward

Every dependency must move toward the Domain.

Never allow:

* Domain → Infrastructure
* Domain → API
* Application → Infrastructure Implementation

The Dependency Rule is never violated.

---

# Principle 5 — Explicit Architecture

Architecture must be visible.

Project structure should clearly communicate:

* Business modules
* Layer boundaries
* Dependencies
* Ownership

Avoid hidden architectural decisions.

---

# Principle 6 — One Responsibility

Every component should have one clear responsibility.

Examples:

* Aggregate protects invariants.
* Repository persists Aggregates.
* Handler coordinates a use case.
* Endpoint exposes a transport contract.

Responsibilities must not overlap.

---

# Principle 7 — Encapsulation

Every business concept owns its own behaviour.

Avoid exposing internal state.

Objects should be modified through business operations—not by changing properties directly.

---

# Principle 8 — Ubiquitous Language

The repository uses one consistent business language.

Class names, methods, events, and documentation should match the language used by domain experts.

Technical naming should not replace business terminology.

---

# Principle 9 — Behaviour Over Data

Objects exist to perform behaviour.

Avoid models that contain only data.

Rich Domain Models are preferred over Anemic Domain Models.

---

# Principle 10 — Explicit Boundaries

Every architectural boundary must be obvious.

Examples:

* Module boundaries
* Layer boundaries
* Aggregate boundaries
* Transaction boundaries

Boundaries reduce coupling.

---

# Principle 11 — Composition Over Inheritance

Prefer composition whenever possible.

Inheritance should only represent true "is-a" relationships.

Deep inheritance hierarchies should be avoided.

---

# Principle 12 — Immutability by Default

Objects should be immutable unless mutation is required by the business.

Especially:

* Value Objects
* Events
* DTOs (where practical)
* Configuration

Immutability improves correctness.

---

# Principle 13 — Explicit Dependencies

Dependencies should be injected explicitly.

Avoid:

* Service Locators
* Global State
* Static Dependencies

Required collaborators should be visible in constructors.

---

# Principle 14 — Fail Fast

Invalid state should be rejected immediately.

Do not allow business objects to enter an invalid state.

Protect invariants as early as possible.

---

# Principle 15 — Small Public Surface

Expose only what must be public.

Everything else should remain internal.

Reducing the public surface reduces coupling.

---

# Principle 16 — Simplicity First

Prefer the simplest design that satisfies the business requirements.

Do not introduce abstraction until it provides clear value.

Avoid speculative architecture.

---

# Principle 17 — Evolutionary Design

The architecture should evolve incrementally.

Do not optimise for hypothetical future requirements.

Optimise for maintainability and adaptability.

---

# Principle 18 — Testability

Every architectural decision should improve testability.

Business behaviour should be testable without:

* Databases
* HTTP
* External services
* Message brokers

Fast tests are preferred.

---

# Principle 19 — Consistency

Consistency is more valuable than personal preference.

Follow existing patterns unless there is a compelling architectural reason to introduce a new one.

---

# Principle 20 — AI Consistency

Every AI-generated implementation must:

* Follow repository conventions.
* Respect architectural boundaries.
* Reuse existing patterns.
* Preserve naming consistency.
* Produce production-ready code.
* Avoid introducing unnecessary abstractions.

The AI should optimise for consistency with the repository—not creativity.

---

# Architecture Decision Priority

When multiple rules appear to conflict, follow this priority:

1. Business correctness.
2. Architectural principles.
3. Module boundaries.
4. Layer responsibilities.
5. Repository conventions.
6. Coding style.
7. Personal preference.

---

# Architectural Principles Checklist

Before completing any implementation, verify:

* Business is protected.
* Domain owns behaviour.
* Dependencies point inward.
* Responsibilities are clear.
* Boundaries are respected.
* Technology remains replaceable.
* Design is simple.
* Architecture remains consistent.

---

# Guiding Principle

Architecture is the continuous practice of protecting the business from unnecessary complexity.

Every implementation should make the system easier to understand, easier to change, and easier to extend without compromising its long-term integrity.
