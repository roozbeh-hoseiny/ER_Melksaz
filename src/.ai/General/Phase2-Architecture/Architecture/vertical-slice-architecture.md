# Vertical Slice Architecture

Version: 1.0

---

# Purpose

This document defines the Vertical Slice Architecture principles used throughout the repository.

Vertical Slice Architecture organises software around business capabilities rather than technical layers.

Each feature is implemented as an independent slice that contains everything required for that business capability.

---

# Primary Principle

Organise code by business feature—not by technical responsibility.

Business capabilities should be easy to locate, understand, and evolve independently.

---

# Definition

A Vertical Slice represents one business use case.

A slice contains everything required to implement that use case.

Typical artefacts include:

* Command
* Query
* Handler
* Validator
* Request
* Response
* Endpoint
* Mapping
* Tests

---

# Slice Ownership

Each slice owns:

* Business workflow.
* Validation.
* Transport models.
* Mapping.
* Tests.

Business rules remain inside the Domain.

---

# Feature Organisation

Features should be organised around business capabilities.

Example:

```text id="v1kq5s"
Billing/

    CreateInvoice/

        CreateInvoiceCommand

        CreateInvoiceValidator

        CreateInvoiceHandler

        CreateInvoiceEndpoint

        CreateInvoiceRequest

        CreateInvoiceResponse

        Tests
```

Avoid organising by technical type.

---

# One Use Case Per Slice

Each slice should implement exactly one business use case.

Examples:

* Create Invoice
* Cancel Invoice
* Approve Payment
* Register Customer

Avoid combining unrelated workflows.

---

# Handler Responsibility

Each Handler should:

* Coordinate one business operation.
* Load Aggregates.
* Invoke Domain behaviour.
* Persist changes.
* Return a Result.

Handlers should not contain business rules.

---

# Validation

Validation belongs to the slice.

Typical validation includes:

* Required fields.
* Input format.
* Simple consistency checks.

Business validation remains inside the Domain.

---

# Request Models

Request models belong only to the transport layer.

They must never become Domain Models.

Requests should represent client input only.

---

# Response Models

Responses belong to the application boundary.

They should expose only the information required by the client.

Avoid exposing Domain objects directly.

---

# Mapping

Mapping belongs inside the slice.

Mapping should remain explicit.

Avoid global mapping mechanisms unless repository standards require them.

---

# Dependencies

A slice may depend on:

* Domain
* Application abstractions
* Infrastructure abstractions

A slice must never bypass architectural boundaries.

---

# Independence

Each slice should be understandable in isolation.

A developer should be able to open one slice and understand the complete workflow.

---

# Reuse

Reuse business behaviour.

Do not reuse application workflows.

If two slices share business rules, move those rules into the Domain.

Avoid shared Handler logic.

---

# Cross-Cutting Concerns

Cross-cutting concerns such as:

* Logging
* Transactions
* Authorization
* Validation
* Metrics

should be applied consistently without polluting individual slices.

---

# Testing

Each slice should have dedicated tests.

Typical tests include:

* Unit Tests
* Integration Tests
* Endpoint Tests

Tests should verify the complete behaviour of the slice.

---

# Evolution

New business capabilities should normally create new slices.

Existing slices should not continuously grow to handle unrelated scenarios.

Keep slices focused.

---

# Anti-Patterns

Avoid:

* Large Handlers.
* Shared business logic between slices.
* Generic application services replacing slices.
* CRUD-oriented feature organisation.
* Technical folders replacing business folders.
* Exposing Domain models directly to clients.

---

# Vertical Slice Checklist

Before completing a slice, verify:

* One business use case is implemented.
* Handler has one responsibility.
* Business rules remain in the Domain.
* Validation is complete.
* Mapping is explicit.
* Requests and Responses are transport models.
* Tests cover the slice.
* The slice is independently understandable.

---

# Guiding Principle

A Vertical Slice should tell a complete business story.

A developer should be able to understand the entire use case by reading a single feature folder without navigating the rest of the solution.
