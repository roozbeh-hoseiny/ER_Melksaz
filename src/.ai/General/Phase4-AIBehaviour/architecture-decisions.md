# Architecture Decisions

Version: 1.0

---

# Purpose

This document defines how an AI agent should reason about architectural decisions within this repository.

Architecture determines the long-term structure of the software.

Architectural decisions have a much longer lifespan than implementation details and therefore require significantly more care.

---

# Primary Principle

Prefer preserving a good architecture over implementing a quick solution.

Temporary convenience must never become permanent architecture.

---

# Preserve Existing Architecture

The AI must first understand the existing architecture before proposing changes.

Always preserve:

* Clean Architecture
* Domain-Driven Design boundaries
* Dependency Rule
* Module ownership
* Layer responsibilities
* Existing architectural conventions

Architecture is more valuable than local implementation improvements.

---

# Prefer Evolution Over Replacement

Improve the architecture incrementally.

Avoid rewriting major components unless explicitly requested.

Small evolutionary improvements are preferred over large disruptive redesigns.

---

# Respect the Dependency Rule

Dependencies must always point inward.

The AI must never introduce dependencies that violate architectural boundaries.

Examples of prohibited dependencies include:

* Domain → Infrastructure
* Domain → API
* Application → API
* Domain → Persistence framework

---

# Preserve Ubiquitous Language

Architectural decisions must reinforce the Domain's language.

Avoid introducing terminology that conflicts with established business concepts.

Consistent language improves communication and maintainability.

---

# Module Ownership

Every feature should belong to exactly one module.

Avoid:

* Shared ownership
* Cross-module business logic
* Duplicate implementations

Module boundaries should remain explicit.

---

# Bounded Contexts

Respect existing Bounded Contexts.

Avoid sharing:

* Domain models
* Aggregates
* Business rules

between contexts unless explicitly designed for reuse.

Integration should occur through well-defined contracts.

---

# Separation of Concerns

Ensure that responsibilities remain separated.

Typical concerns include:

* Presentation
* Application orchestration
* Domain behaviour
* Infrastructure
* Integration

Do not collapse multiple concerns into a single layer.

---

# Architectural Consistency

When multiple valid solutions exist, prefer the one that is most consistent with the existing repository.

Consistency is often more valuable than theoretical perfection.

---

# New Abstractions

Create new abstractions only when they solve a demonstrated problem.

Do not introduce interfaces, base classes, or generic frameworks "just in case."

Abstractions should emerge from repeated patterns.

---

# Technology Independence

The Domain should remain independent of technical frameworks.

Business logic must not depend on:

* ASP.NET Core
* Entity Framework Core
* gRPC
* RabbitMQ
* Serialization libraries
* Logging frameworks

Technology changes should not require Domain changes.

---

# Coupling

Minimise coupling between components.

Prefer:

* Explicit contracts
* Dependency inversion
* Clear ownership

Avoid hidden runtime dependencies.

---

# Cohesion

Keep related behaviour together.

A class, module, or component should have one clear responsibility.

High cohesion improves maintainability.

---

# Extensibility

Design for realistic extension points.

Avoid speculative extensibility.

Only introduce extension mechanisms when there is evidence they are needed.

---

# Backward Compatibility

When modifying public contracts:

* Preserve compatibility where practical.
* Introduce versioning when required.
* Avoid unnecessary breaking changes.

Public APIs are architectural commitments.

---

# Cross-Cutting Concerns

Cross-cutting concerns should remain separate from business logic.

Examples include:

* Logging
* Validation
* Caching
* Authorization
* Metrics
* Tracing

Business code should not become polluted with infrastructure concerns.

---

# Architectural Trade-offs

When trade-offs exist, the AI should consider:

1. Correctness
2. Maintainability
3. Simplicity
4. Testability
5. Performance
6. Extensibility

Performance optimisations should not compromise architecture without evidence.

---

# Refactoring

Architectural refactoring should:

* Preserve behaviour.
* Reduce coupling.
* Increase cohesion.
* Simplify dependencies.
* Improve clarity.

Avoid refactoring that produces architectural churn without measurable benefit.

---

# Documentation

Significant architectural changes should prompt updates to:

* Architecture documentation
* ADRs
* Module documentation
* Repository README (when applicable)

Architecture and documentation should remain aligned.

---

# AI Responsibilities

When making architectural decisions, the AI must:

* Preserve architectural boundaries.
* Reuse existing patterns.
* Avoid unnecessary abstractions.
* Protect the Dependency Rule.
* Keep modules cohesive.
* Prefer incremental improvement.
* Explain important trade-offs when relevant.

---

# Anti-Patterns

Avoid:

* Layer violations.
* Circular dependencies.
* Shared mutable state across modules.
* Framework leakage into the Domain.
* Generic "base" architectures without justification.
* God modules.
* Overengineering.
* Architecture driven solely by tooling limitations.

---

# Architecture Decision Checklist

Before proposing an architectural change, verify:

* Existing architecture has been understood.
* Architectural boundaries remain intact.
* The Dependency Rule is preserved.
* The change improves maintainability.
* New abstractions are justified.
* Module ownership is clear.
* Documentation will remain accurate.

---

# Guiding Principle

Architecture is a long-term investment.

Every architectural decision should make the repository easier to understand, easier to evolve, and more resilient to future change—not merely easier to implement today.
