# Repository Architecture

Version: 1.0

Status: Repository Convention

---

# Purpose

This document describes the actual architectural style used by this repository.

It defines how the solution is organised, where responsibilities belong, and how new features must integrate with the existing architecture.

All contributors and AI agents must follow this document before introducing new functionality.

---

# Architectural Style

## Observed Pattern

The repository follows a layered architecture centred around reusable Building Blocks.

The solution is not a traditional N-Tier architecture.

It also does not implement a strict Vertical Slice Architecture.

Instead, it combines:

- Clean Architecture principles
- Domain-Driven Design
- Modular components
- Shared Building Blocks
- Feature-oriented APIs

This document refers to this architecture as:

> Modular Clean Architecture

---

# Architectural Principles

The architecture is based on the following principles.

1. Business logic is independent from infrastructure.

2. Cross-cutting concerns are implemented once and reused.

3. Common functionality belongs inside BuildingBlocks.

4. Infrastructure must never leak into Domain logic.

5. Features should compose existing Building Blocks instead of reimplementing them.

---

# Building Blocks

## Observed Pattern

The repository contains a dedicated BuildingBlocks layer.

Examples observed include:

- BuildingBlocks.Api
- BuildingBlocks.Application

These projects provide reusable infrastructure that is shared across business modules.

Business modules must consume BuildingBlocks rather than duplicate functionality.

---

# Layer Responsibilities

## API

Responsible for:

- HTTP endpoints
- Request binding
- Authentication
- Authorization
- Response generation

The API layer should remain thin.

Business rules must not live here.

---

## Application

Responsible for:

- Use cases
- Command orchestration
- Query orchestration
- Validation orchestration
- Transactions
- Coordination between Domain and Infrastructure

The Application layer coordinates work.

It should not contain infrastructure concerns.

---

## Domain

Responsible for:

- Business rules
- Domain behaviour
- Entities
- Value Objects
- Domain Events
- Aggregates

The Domain must not depend on infrastructure technologies.

---

## Infrastructure

Responsible for:

- Database access
- Messaging
- External services
- File systems
- Caching
- Logging implementation

Infrastructure implements contracts defined by higher layers.

---

# Dependency Direction

Dependencies must always point inward.

The preferred dependency graph is:

API
↓
Application
↓
Domain

Infrastructure implements contracts owned by Application or Domain.

Domain must never reference:

- ASP.NET Core
- Entity Framework
- Logging frameworks
- Serialization libraries
- Messaging frameworks

---

# Cross-Cutting Concerns

## Observed Pattern

Cross-cutting concerns are centralised inside reusable BuildingBlocks.

Examples observed include:

- Global exception handling
- Endpoint extensions
- Result handling
- JSON converters
- Service registration

These concerns should not be reimplemented inside individual modules.

---

# Endpoint Architecture

## Observed Pattern

The repository defines reusable endpoint infrastructure.

Observed examples include:

- ApiEndpointBase
- EndpointExtensions

Endpoints should build upon these abstractions rather than introducing alternative endpoint patterns.

---

# Result Pattern

## Observed Pattern

The repository contains a dedicated Result handling mechanism.

Observed component:

- ResultHandlerDefault

New APIs should integrate with the existing Result pipeline instead of returning inconsistent response formats.

A dedicated Result Pattern document defines the complete convention.

---

# Exception Handling

## Observed Pattern

Global exception handling is centralised.

Observed component:

- GlobalExceptionHandler

Controllers and endpoints should not perform repetitive exception translation.

Unexpected exceptions should flow into the global handler.

---

# Serialization

## Observed Pattern

The repository provides custom JSON converters.

Observed component:

- ValueObjectJsonConverter

Serialization behaviour should remain consistent across the solution.

Modules should reuse existing converters instead of creating new ones.

---

# Dependency Injection

## Observed Pattern

Service registration is centralised.

Observed component:

- ServiceCollectionExtensions

Modules should expose registration through the established registration mechanism.

Avoid ad-hoc registration scattered throughout the solution.

---

# Module Independence

Business modules should remain independent.

A module may depend on:

- BuildingBlocks
- Shared abstractions

A module should not depend directly on another module's implementation.

Communication should occur through contracts.

---

# Extensibility

The preferred extension strategy is composition.

Prefer extending existing pipelines and abstractions over replacing them.

Avoid introducing parallel frameworks that duplicate existing infrastructure.

---

# Architectural Rules

All new functionality must:

- Respect existing BuildingBlocks.
- Preserve dependency direction.
- Reuse cross-cutting infrastructure.
- Keep API layers thin.
- Keep business logic inside Domain.
- Avoid infrastructure leakage.
- Follow existing endpoint conventions.

---

# Prohibited Patterns

Do not introduce:

- Business logic inside endpoints.
- Infrastructure references inside Domain.
- Duplicate cross-cutting components.
- Multiple Result implementations.
- Multiple exception handling strategies.
- Alternative endpoint frameworks.
- Module-to-module implementation dependencies.

---

# AI Instructions

Before generating code, the AI must determine:

1. Which architectural layer owns the behaviour?
2. Does an existing BuildingBlock already solve this problem?
3. Can an existing abstraction be reused?
4. Will the new implementation preserve dependency direction?
5. Does the implementation follow the existing endpoint and result conventions?

If any answer is uncertain, inspect the repository before generating code.

---

# Repository Convention

This document defines the architectural conventions currently observed in the repository.

More specialised conventions are documented in:

- naming-conventions.md
- dependency-injection.md
- endpoint-conventions.md
- result-pattern.md
- exception-handling.md
- validation.md

These documents extend the rules defined here.