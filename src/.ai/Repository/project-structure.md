# Project Structure

Version: 1.0

Status: Repository Convention

---

# Purpose

This document describes how the repository is organised.

It defines the responsibilities of each project, how projects interact, and where new functionality should be implemented.

Every contributor and AI agent must understand the repository structure before introducing new code.

---

# Design Philosophy

## Observed Pattern

The solution is organised around reusable projects rather than a single monolithic application.

Common infrastructure is extracted into reusable BuildingBlocks.

Business functionality should build upon these shared components instead of duplicating them.

---

# High-Level Structure

The repository is organised into several categories of projects.

Typical categories include:

- BuildingBlocks
- Business Modules
- Infrastructure
- Applications
- Tests

Each category has a clearly defined responsibility.

---

# BuildingBlocks

## Responsibility

BuildingBlocks contain reusable technical components that may be shared across multiple modules.

Examples observed include:

- BuildingBlocks.Api
- BuildingBlocks.Application

BuildingBlocks should contain reusable infrastructure—not business logic.

---

# Business Modules

## Responsibility

Business modules implement domain-specific behaviour.

A business module owns:

- Domain Model
- Application Logic
- Infrastructure Integration
- Public Contracts

Business modules should remain independent from one another.

---

# API Projects

## Responsibility

API projects expose functionality to external consumers.

Responsibilities include:

- Endpoint definitions
- Request binding
- Authorization
- Response generation

API projects should not contain business rules.

---

# Application Projects

## Responsibility

Application projects coordinate business operations.

Typical responsibilities include:

- Commands
- Queries
- Validation
- Transactions
- Use Case orchestration

Application projects should orchestrate work rather than implement business rules.

---

# Domain Projects

## Responsibility

Domain projects contain the business model.

Typical contents include:

- Entities
- Value Objects
- Aggregates
- Domain Services
- Domain Events

The Domain should remain independent of infrastructure frameworks.

---

# Infrastructure Projects

## Responsibility

Infrastructure projects integrate external technologies.

Examples include:

- Database
- Messaging
- Caching
- File Storage
- HTTP Clients

Infrastructure implements abstractions owned by higher layers.

---

# Shared Components

## Observed Pattern

Reusable infrastructure is centralised.

Examples observed include:

- Endpoint infrastructure
- Exception handling
- Result handling
- JSON converters
- Dependency registration

Avoid creating duplicate implementations inside business modules.

---

# Dependency Rules

Projects may depend only on lower-level abstractions.

Preferred dependency flow:

Application
↓

Domain

Infrastructure
↓

Application

API
↓

Application

Infrastructure should never become a dependency of the Domain.

---

# Module Ownership

Each business capability should belong to exactly one module.

Avoid splitting ownership across multiple projects.

If functionality belongs to a specific module, implement it there rather than inside BuildingBlocks.

---

# Feature Placement

Before creating a new class, determine:

- Which module owns the feature?
- Which project owns the responsibility?
- Does a similar implementation already exist?

Never create a new project simply to organise a small amount of code.

---

# Cross-Cutting Functionality

Cross-cutting concerns belong inside reusable infrastructure.

Examples include:

- Logging
- Validation
- Exception handling
- Serialization
- Endpoint utilities

Business modules should consume these services rather than reimplement them.

---

# Naming

Project names should communicate responsibility.

Examples:

Good:

- BuildingBlocks.Api
- BuildingBlocks.Application

Avoid project names that describe implementation details instead of responsibilities.

---

# Folder Organisation

Folders should reflect architectural responsibilities.

Avoid organising code by technical type when feature ownership is more important.

Within a project:

- Keep related classes together.
- Minimise unnecessary folder depth.
- Avoid "Misc", "Common", or "Helpers" folders.

Folders should describe business or architectural responsibility.

---

# Project References

Before adding a new project reference, verify:

- Is the dependency necessary?
- Does it violate architectural boundaries?
- Can an abstraction be introduced instead?

Project references are architectural decisions.

---

# AI Instructions

Before creating a new file, determine:

1. Which project owns this responsibility?
2. Does the project already contain similar functionality?
3. Can an existing BuildingBlock be reused?
4. Does adding this file preserve the dependency direction?
5. Does this change increase cohesion?

If the correct location is unclear, inspect neighbouring classes before creating new ones.

---

# Repository Convention

This document defines the current organisational structure observed in the repository.

More specific conventions are documented in:

- architecture.md
- naming-conventions.md
- dependency-injection.md
- endpoint-conventions.md
- packages.md

These documents provide detailed rules for their respective areas.