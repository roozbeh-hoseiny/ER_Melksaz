# Architecture Glossary

Version: 1.0

---

# Purpose

This document defines the official architectural vocabulary used throughout the repository.

Every engineer and every AI agent must use these terms consistently.

If a term exists in this glossary, it should be preferred over alternative wording.

---

# Aggregate

A cluster of Domain objects that is treated as a single unit for consistency.

An Aggregate enforces business invariants.

An Aggregate has exactly one Aggregate Root.

---

# Aggregate Root

The single public entry point into an Aggregate.

External code may reference only the Aggregate Root.

Child Entities are never accessed directly.

---

# Entity

A Domain object with identity.

Its identity remains constant even if its state changes.

Entities contain business behaviour.

---

# Value Object

A Domain object defined entirely by its values.

Characteristics:

* Immutable
* No identity
* Self-validating
* Behaviour-rich

---

# Domain

The business itself.

Contains:

* Business language
* Business behaviour
* Business rules
* Business policies

The Domain is independent of technology.

---

# Domain Event

A record of something important that has already happened in the business.

Characteristics:

* Immutable
* Named in past tense
* Represents a completed business fact

Example:

```text id="5j7qkm"
InvoiceApproved

PaymentReceived

CustomerRegistered
```

---

# Domain Service

A Domain object that represents business behaviour that does not naturally belong to one Entity or Aggregate.

A Domain Service contains business logic.

It is **not** a technical service.

---

# Specification

A reusable business rule.

A Specification answers a business question.

Example:

```text id="8t3zfa"
EligibleForDiscountSpecification
```

---

# Repository

A business-oriented abstraction that provides access to Aggregate Roots.

Repositories hide persistence.

Repository implementations belong to Infrastructure.

---

# Unit of Work

Coordinates persistence of multiple Repository operations within one transaction.

The Unit of Work belongs to the Application/Infrastructure boundary.

---

# Command

A request to change business state.

Examples:

* CreateInvoice
* CancelInvoice
* ApproveInvoice

Commands have side effects.

---

# Query

A request to retrieve information.

Queries never modify business state.

---

# Command Handler

Coordinates execution of one business use case.

Responsibilities include:

* Loading Aggregates
* Invoking Domain behaviour
* Persisting changes
* Returning Results

---

# Query Handler

Retrieves information for clients.

Should not contain business rules.

---

# DTO (Data Transfer Object)

A simple object used for communication between layers.

DTOs:

* Contain data only
* Contain no business behaviour
* Are not Domain Models

---

# Module

An independently owned business capability.

A Module contains:

* Domain
* Application
* Infrastructure
* API
* Tests

Modules communicate through contracts.

---

# Bounded Context

A boundary within which a particular business model is valid.

Business terminology may differ between Bounded Contexts.

Each Bounded Context owns its own language.

---

# Shared Kernel

The minimal set of concepts intentionally shared across multiple modules.

It must remain:

* Small
* Stable
* Framework independent

---

# Clean Architecture

An architectural style that protects business logic from technology.

Dependencies always point inward.

Business rules remain independent.

---

# CQRS

Command Query Responsibility Segregation.

Commands modify state.

Queries retrieve information.

They are implemented independently.

---

# Vertical Slice

An implementation of one complete business use case.

A slice contains everything required for that feature.

---

# Infrastructure

The technical implementation layer.

Examples include:

* Database
* Messaging
* Email
* Cache
* External APIs

Infrastructure contains no business rules.

---

# API

The transport boundary.

Responsible for:

* Receiving requests
* Invoking Application use cases
* Returning responses

The API contains no business logic.

---

# Application Layer

Coordinates business use cases.

It orchestrates the Domain.

It does not implement business rules.

---

# Domain Layer

The heart of the application.

Contains business behaviour.

Owns all business decisions.

---

# Infrastructure Layer

Provides implementations of abstractions defined by inner layers.

All technical details belong here.

---

# Dependency Rule

Source code dependencies always point toward the Domain.

Inner layers never depend on outer layers.

---

# Ubiquitous Language

The shared business vocabulary used consistently by developers, architects, AI agents, and domain experts.

Every important business concept should have exactly one official name.

---

# Invariant

A business rule that must always remain true.

Aggregates are responsible for protecting invariants.

---

# Business Rule

A rule that defines how the business operates.

Business rules belong only inside the Domain.

---

# Integration Event

An event published to notify other modules or external systems that something has occurred.

Unlike Domain Events, Integration Events cross module or system boundaries.

---

# Result Pattern

An explicit representation of operation outcomes.

Typical outcomes include:

* Success
* Failure
* Validation Error
* Not Found
* Conflict

Expected business failures should be represented by Results rather than exceptions.

---

# Architecture Vocabulary Rules

Always use:

* Aggregate Root
* Value Object
* Domain Event
* Command
* Query
* Handler
* Repository
* Module
* Bounded Context
* Shared Kernel

Avoid replacing these with inconsistent synonyms.

Consistency improves communication.

---

# Guiding Principle

Architecture begins with language.

When every engineer and every AI agent uses the same vocabulary, the codebase becomes easier to understand, maintain, and evolve.
