# Repository Analysis Rules

Version: 1.0

---

# Purpose

This document defines the mandatory analysis process that every AI agent must perform before generating, modifying, or reviewing code.

The AI must understand the repository before attempting to change it.

No implementation should begin without completing this analysis.

---

# Primary Principle

Understand first.

Generate second.

The repository is always the source of truth.

---

# Repository Discovery

Before writing code, inspect the repository to understand:

* Solution structure
* Projects
* Modules
* Architectural layers
* Existing conventions
* Existing patterns
* Existing libraries

Never assume the architecture.

---

# Solution Analysis

Identify:

* Solution name
* Projects
* Project responsibilities
* Dependency graph
* Build configuration
* Test projects

Understand how the solution is organised before making changes.

---

# Architectural Analysis

Determine:

* Architecture style
* Layer boundaries
* Dependency direction
* Composition root
* Shared projects
* Cross-cutting concerns

Never violate the existing architecture.

---

# Module Analysis

Identify:

* Business modules
* Bounded contexts
* Feature organisation
* Shared functionality

Determine where the requested change belongs.

---

# Existing Feature Discovery

Search for similar features.

Inspect:

* Existing aggregates
* Commands
* Queries
* Validators
* Endpoints
* Repository implementations
* Tests

Reuse existing patterns whenever possible.

---

# Naming Analysis

Identify existing conventions for:

* Projects
* Folders
* Files
* Namespaces
* Classes
* Interfaces
* Methods

Generated code must follow the same conventions.

---

# Dependency Analysis

Identify:

* Existing abstractions
* Existing interfaces
* Existing services
* Existing packages
* Existing framework usage

Do not introduce duplicate abstractions.

---

# Library Analysis

Before introducing any dependency, verify whether an equivalent capability already exists.

Reuse existing libraries before introducing new ones.

---

# Domain Analysis

Identify:

* Aggregates
* Entities
* Value Objects
* Domain Events
* Specifications
* Domain Services

Understand the business model before extending it.

---

# Application Analysis

Identify:

* Commands
* Queries
* Handlers
* Validators
* Behaviours
* Mapping

Follow existing application workflows.

---

# Infrastructure Analysis

Identify:

* Persistence
* Repositories
* Messaging
* Logging
* Caching
* Authentication
* External integrations

Generated infrastructure should match existing implementations.

---

# API Analysis

Inspect:

* Endpoint style
* Routing conventions
* Request models
* Response models
* Validation
* Error handling

New endpoints should appear identical to existing endpoints.

---

# Testing Analysis

Determine:

* Testing framework
* Test naming
* Test folder structure
* Fixtures
* Builders
* Test utilities

Generated tests should follow existing patterns.

---

# Configuration Analysis

Inspect:

* Dependency registration
* Options pattern
* Configuration classes
* Environment configuration

Do not introduce inconsistent configuration mechanisms.

---

# Pattern Discovery

Search for existing implementations before creating:

* Base classes
* Interfaces
* Behaviours
* Specifications
* Extensions
* Utilities

Repository reuse has higher priority than creating new abstractions.

---

# Duplicate Detection

Before generating code, verify whether equivalent functionality already exists.

Avoid duplicate:

* Validation
* Mapping
* Business rules
* Infrastructure
* Utilities

---

# Documentation Analysis

Inspect:

* README
* Architecture documentation
* ADRs
* AI handbook
* Coding standards

Repository documentation overrides generic best practices.

---

# Analysis Output

Before implementation, the AI should understand:

* Where the change belongs.
* Which files are affected.
* Which dependencies already exist.
* Which conventions must be followed.
* Which tests must be added.
* Which documentation must be updated.

---

# AI Behaviour

If repository analysis is incomplete:

* Stop implementation.
* Continue analysing.
* Ask questions if necessary.

Never guess repository conventions.

---

# Analysis Checklist

Before generating code, verify:

* Architecture understood.
* Module identified.
* Existing feature located.
* Naming conventions identified.
* Dependencies understood.
* Existing abstractions discovered.
* Testing conventions identified.
* Documentation reviewed.

---

# Guiding Principle

Every implementation begins with understanding the repository.

The AI should behave like a senior engineer joining an existing codebase—not like a code generator starting from an empty project.
