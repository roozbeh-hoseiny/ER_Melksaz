# Module Rules

Version: 1.0

---

# Purpose

This document defines how software modules are designed, implemented, and evolved within the repository.

A module is a cohesive unit that encapsulates a single business capability.

Modules are the primary building blocks of the system.

---

# Definition

A module represents a business capability.

Examples include:

- Identity
- Customers
- Products
- Orders
- Invoicing
- Payments
- Inventory

A module is not a technical grouping.

Never create modules based on technologies.

Bad examples:

- Helpers
- Utilities
- CommonLogic
- Services
- Managers

---

# Objectives

Every module should:

- Encapsulate a business capability.
- Minimise dependencies.
- Maximise cohesion.
- Be independently maintainable.
- Be independently testable.
- Expose a well-defined public interface.

---

# Module Ownership

Every business concept belongs to exactly one module.

Ownership must be explicit.

Business logic must never be duplicated across modules.

---

# Single Responsibility

A module must have one primary responsibility.

If a module has multiple unrelated responsibilities, it should be divided into separate modules.

---

# Internal Structure

A module should contain only the components required to implement its business capability.

Typical contents include:

- Domain
- Application
- Infrastructure
- API
- Tests

The internal structure should remain consistent across modules.

---

# Encapsulation

Modules should hide implementation details.

Only expose what other modules legitimately require.

Internal implementation should remain private.

---

# Public Contracts

A module communicates through explicit contracts.

Examples include:

- Commands
- Queries
- DTOs
- Events
- Interfaces

Avoid exposing internal entities.

---

# Dependencies

Modules should have as few dependencies as possible.

Every dependency should be intentional.

Unnecessary dependencies increase coupling and reduce maintainability.

---

# Dependency Direction

Modules must not create circular dependencies.

Example:

```
Sales

↓

Payments

↓

Sales
```

This architecture is prohibited.

---

# Communication

Modules should communicate using explicit contracts.

Preferred communication mechanisms include:

- Application interfaces
- Domain Events
- Integration Events
- Commands
- Queries

Avoid direct knowledge of another module's internal implementation.

---

# Shared Code

Do not create shared libraries merely to reduce duplication.

Only extract shared functionality when:

- The behaviour is genuinely shared.
- The abstraction is stable.
- Multiple modules require identical behaviour.

Premature sharing creates unnecessary coupling.

---

# Cross-Module Business Rules

A business rule belongs to the module that owns the corresponding business capability.

Do not duplicate business rules in consuming modules.

---

# Data Ownership

Each module owns its own data.

Another module must never modify that data directly.

Access should occur through published contracts.

---

# Database Access

Modules must not directly access another module's persistence layer.

Never bypass module boundaries through database queries.

---

# Internal Classes

Internal implementation classes should remain inaccessible outside the module whenever possible.

Only expose types intended for public consumption.

---

# Naming

Module names should:

- Represent business terminology.
- Be singular where appropriate.
- Be stable.
- Be understandable by domain experts.

Avoid technical terminology.

---

# Versioning

Public contracts should evolve carefully.

Breaking changes should be minimised.

Where breaking changes are unavoidable, they must be documented.

---

# Testing

Each module should be testable in isolation.

Module tests should verify:

- Public behaviour
- Business rules
- Public contracts
- Integration points

Avoid tests that rely unnecessarily on unrelated modules.

---

# Extensibility

Modules should be open for extension without requiring modifications to unrelated modules.

Prefer adding behaviour over modifying existing stable functionality.

---

# Refactoring

When refactoring modules:

- Preserve public contracts whenever possible.
- Improve cohesion.
- Reduce coupling.
- Remove duplication.
- Maintain behaviour.

---

# Anti-Patterns

Avoid:

- God Modules
- Circular dependencies
- Shared mutable state
- Leaking implementation details
- Cross-module database access
- Business rule duplication
- Technology-driven module design

---

# Module Review Checklist

Before introducing or modifying a module, verify:

- Does it represent a business capability?
- Does it have a single responsibility?
- Does it own its business rules?
- Does it minimise dependencies?
- Does it expose only necessary contracts?
- Is it independently testable?
- Does it avoid circular dependencies?
- Does it preserve architectural boundaries?

---

# Guiding Principle

A module should represent a cohesive business capability with clear ownership, well-defined boundaries, minimal dependencies, and explicit contracts.