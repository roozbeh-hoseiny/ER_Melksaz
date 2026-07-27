# AI Engineering Guide

Version: 1.0

This document is the primary source of truth for every AI coding assistant working in this repository.

It defines architectural constraints, engineering standards, coding conventions, quality gates, and implementation rules.

If any instruction conflicts with generated code, this document always wins.

---

# Primary Objective

Generate production-ready software.

Never generate sample code.

Never generate tutorial code.

Never generate placeholder implementations.

Never violate architectural boundaries.

Always generate maintainable, testable and extensible code.

---

# Repository Philosophy

This repository follows:

- Domain Driven Design (DDD)
- Clean Architecture
- Modular Monolith
- Vertical Slice Architecture
- SOLID
- Explicit Dependencies
- Composition Root
- CQRS where appropriate

Every generated file must follow these principles.

---

# General Rules

## G-001

Always analyse existing code before generating new code.

Never invent a different convention if an existing convention already exists.

---

## G-002

Generated code must look like it was written by the repository owner.

Do not introduce personal preferences.

Follow repository conventions.

---

## G-003

Never create duplicate implementations.

Reuse existing abstractions.

---

## G-004

Prefer extending existing components over introducing new frameworks.

---

## G-005

Never introduce new NuGet packages unless explicitly requested.

---

## G-006

Never replace existing architectural patterns.

Extend them.

---

## G-007

Never change public APIs unless requested.

---

## G-008

Every generated file must compile.

No TODO.

No NotImplementedException.

No placeholders.

---

## G-009

Never generate dead code.

---

## G-010

Always generate production-quality code.

---

# Architecture Overview

The repository is divided into reusable platform components and business modules.

Platform projects provide reusable infrastructure.

Business modules contain business capabilities.

Business modules must remain independent.

---

# Architectural Layers

Every module contains four logical layers.

Domain

Application

Infrastructure

Api

Generated code must be placed into the correct layer.

Never mix responsibilities.

---

# Dependency Rules

Allowed dependencies

Api
↓

Application
↓

Domain

Infrastructure
↓

Application

Infrastructure
↓

Domain

Forbidden dependencies

Domain → Infrastructure

Domain → Api

Application → Api

Application → EF Core

Application → ASP.NET Core

Domain → Logging

Domain → HTTP

Domain → Serialization

---

# Layer Responsibilities

## Domain

Contains

- Aggregates
- Entities
- Value Objects
- Domain Events
- Specifications
- Business Rules

Must NOT contain

- EF Core
- Logging
- HttpContext
- IConfiguration
- DbContext
- JSON
- ASP.NET

---

## Application

Contains

- Commands
- Queries
- DTOs
- Validators
- Interfaces
- Use Cases

Must NOT contain

Business rules.

Business rules belong in Domain.

---

## Infrastructure

Contains

- EF Core
- Repository implementations
- External integrations
- Messaging
- Caching
- Persistence

Infrastructure implements interfaces defined by Application.

---

## Api

Contains

- Endpoints
- Authentication
- Authorization
- OpenAPI
- Middleware
- Request Mapping

Endpoints orchestrate.

They never implement business rules.

---

# AI Behaviour

When asked

"Create Invoice"

the AI must determine every required artifact.

The AI must never generate only one class.

Instead it must identify the complete feature.

Example

Invoice Aggregate

↓

Commands

↓

Queries

↓

Validators

↓

Endpoints

↓

Repository

↓

Persistence

↓

Tests

↓

Dependency Injection

↓

Documentation

---

# Quality Rules

Every generated feature must satisfy

✓ Builds

✓ Passes unit tests

✓ Passes integration tests

✓ Uses repository conventions

✓ Uses existing libraries

✓ Uses existing architecture

✓ Uses repository naming

✓ Uses repository patterns

---

# Code Generation Rules

Before generating code

1. Search for similar implementation.

2. Reuse existing patterns.

3. Follow naming conventions.

4. Reuse existing abstractions.

5. Reuse existing pipelines.

Never invent a different style.

---

# Tests

Every new feature requires

- Unit Tests

- Integration Tests

Do not omit tests.

---

# Logging

Use the repository logging abstraction.

Do not introduce Console.WriteLine.

---

# Dependency Injection

Register new services using the repository convention.

Never manually instantiate services.

---

# Exceptions

Prefer repository Result/Error abstractions over exceptions.

Throw exceptions only for exceptional situations.

---

# Performance

Avoid

- unnecessary allocations

- reflection

- boxing

- unnecessary LINQ

Prefer readability without sacrificing performance.

---

# Documentation

Public APIs require XML documentation only if the repository convention requires it.

Do not generate unnecessary comments.

Code should be self-explanatory.

---

# Forbidden

Never

- Generate demo code

- Generate placeholder methods

- Ignore repository conventions

- Ignore architecture

- Add random NuGet packages

- Use a different Result pattern

- Mix layers

- Put business logic into endpoints

- Put EF Core into Domain

- Put HTTP into Domain

- Put infrastructure inside Application

---

# Success Criteria

The generated code should be indistinguishable from code written by the repository maintainers.

If uncertain,

search the repository,

find the closest implementation,

and imitate it.