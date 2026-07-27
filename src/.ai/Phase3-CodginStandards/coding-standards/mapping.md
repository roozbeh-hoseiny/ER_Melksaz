# Object Mapping

Version: 1.0

---

# Purpose

This document defines the mandatory object mapping rules for the repository.

Mapping converts one representation of data into another.

Mapping is a technical concern.

Business logic must never be implemented inside mappers.

---

# Primary Principle

Mappings transform data.

They do not make business decisions.

---

# Mapping Locations

Mapping is allowed in:

* API
* Application
* Infrastructure

Mapping is **not** allowed in the Domain.

---

# Domain Layer

The Domain must never know:

* DTOs
* HTTP models
* gRPC messages
* Database entities
* Serialization models

The Domain only works with Domain objects.

---

# API Layer

The API maps:

* HTTP Request → Command
* Query Parameters → Query
* Application Result → HTTP Response

The API must not expose Domain objects directly.

---

# Application Layer

The Application may map:

* Domain → DTO
* DTO → Domain (only when appropriate)
* Integration Event → Domain Command
* Domain Event → Integration Event

Application mappings should remain simple.

---

# Infrastructure Layer

Infrastructure maps:

* Database Entity ↔ Domain
* External API Models ↔ Domain
* gRPC Models ↔ Domain
* Queue Messages ↔ Domain

Infrastructure owns technology-specific models.

---

# Explicit Mapping

Mappings should be explicit.

Prefer:

```csharp id="t5j8mn"
new CustomerDto(
    customer.Id,
    customer.Name,
    customer.Email);
```

Avoid hidden mapping behaviour.

---

# AutoMapper

Avoid AutoMapper unless repository conventions explicitly require it.

Explicit mapping is preferred because it:

* Improves readability.
* Improves debuggability.
* Makes breaking changes obvious.
* Produces predictable code.

---

# Mapping Responsibility

A mapper should:

* Copy values.
* Transform representations.
* Convert types.

It must not:

* Validate business rules.
* Access repositories.
* Execute business logic.
* Publish events.

---

# Mapping Classes

Dedicated mapper classes are acceptable.

Example:

```text id="d9v2xa"
CustomerMapper

InvoiceMapper

OrderMessageMapper
```

Keep mapper responsibilities focused.

---

# Extension Methods

Extension methods are acceptable for small mappings.

Example:

```text id="k7r5bz"
ToDto()

ToDomain()

ToGrpc()
```

Avoid very large extension classes.

---

# Bidirectional Mapping

Do not automatically implement both directions.

Only create mappings that are actually required.

---

# Null Handling

Mappings should explicitly handle null values where appropriate.

Avoid hidden null conversions.

---

# Collections

Map collections explicitly.

Example:

```csharp id="m4p7te"
customers
    .Select(CustomerMapper.ToDto)
    .ToList();
```

Avoid deeply nested mapping logic.

---

# Value Objects

Map Value Objects explicitly.

Avoid flattening Value Objects unnecessarily.

Preserve business meaning.

---

# Enums

Convert enums intentionally.

Never rely on matching integer values across system boundaries.

---

# Identifiers

Preserve identifier semantics.

Do not convert identifiers into primitive strings unless required by the transport protocol.

---

# Date and Time

Map temporal values consistently.

Prefer:

* UTC
* Instant
* DateOnly
* TimeOnly

according to repository conventions.

Avoid implicit timezone conversions.

---

# Performance

Mappings should be lightweight.

Avoid:

* Reflection
* Dynamic runtime mapping
* Hidden allocations

Performance-critical mappings should remain explicit.

---

# Testing

Complex mappings should have dedicated tests.

Tests should verify:

* Every required property.
* Null handling.
* Enum conversion.
* Collection mapping.
* Value Object mapping.

---

# AI Responsibilities

When generating code, the AI must:

* Keep mappings explicit.
* Avoid business logic.
* Respect architectural boundaries.
* Preserve Domain independence.
* Reuse existing mapping conventions.

---

# Anti-Patterns

Avoid:

* Business logic inside mappers.
* Repository access inside mappers.
* Reflection-based mapping.
* Mapping directly into Aggregate internals.
* Leaking infrastructure models into the Domain.
* Overusing AutoMapper.

---

# Mapping Checklist

Before completing an implementation, verify:

* Mapping is explicit.
* Business logic is absent.
* Domain independence is preserved.
* Collections are handled correctly.
* Value Objects remain meaningful.
* Mapping direction is intentional.
* Repository conventions are followed.

---

# Guiding Principle

Mapping translates data between layers.

It should be transparent, predictable, and free of business behaviour.
