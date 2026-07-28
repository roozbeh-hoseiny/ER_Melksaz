# Domain Service

Version: 1.0

---

# Purpose

This document defines the design rules for Domain Services used within the Domain layer.

A Domain Service represents business behaviour that does not naturally belong to a single Aggregate, Entity, or Value Object.

A Domain Service exists to model business concepts—not technical operations.

---

# Primary Principle

Use a Domain Service only when business behaviour cannot naturally belong to an existing Domain object.

If behaviour belongs to an Aggregate or Value Object, keep it there.

---

# Definition

A Domain Service:

* Represents a business concept.
* Contains business logic.
* Has no identity.
* Is usually stateless.
* Operates on Domain objects.
* Belongs to the Domain layer.

It models business behaviour that spans multiple Domain objects.

---

# When to Use

Use a Domain Service when:

* Business logic involves multiple Aggregates.
* Business logic does not clearly belong to one Aggregate.
* Business rules require domain-wide knowledge.
* Behaviour represents a business capability.

---

# When Not to Use

Do not create a Domain Service when the behaviour belongs to:

* Aggregate
* Entity
* Value Object

Always prefer moving behaviour into existing Domain objects before introducing a new Domain Service.

---

# Examples

Examples of Domain Services include:

* ExchangeRateCalculator
* TaxCalculator
* PricingPolicy
* InvoiceNumberGenerator
* CreditLimitEvaluator
* ShipmentPlanner

Each represents a business capability rather than a technical concern.

---

# Business Language

The name of a Domain Service must use business terminology.

Good:

```text id="n2d7kp"
TaxCalculator

PricingPolicy

CreditEvaluator
```

Avoid technical names such as:

```text id="r7f1mv"
BusinessManager

CalculationHelper

DomainProcessor
```

---

# Statelessness

Domain Services should normally be stateless.

Business state belongs inside Aggregates.

The service should perform calculations or coordinate business rules without storing mutable state.

---

# Dependencies

A Domain Service may depend on:

* Repository Contracts
* Domain Policies
* Domain Interfaces

It must never depend on:

* EF Core
* ASP.NET
* Logging
* Messaging
* Configuration
* Dependency Injection
* HTTP

The Domain remains framework independent.

---

# Repository Usage

When necessary, a Domain Service may depend on Repository interfaces defined within the Domain.

It must never depend on repository implementations.

---

# Business Behaviour

Methods should represent business actions.

Examples:

```text id="j8w4ta"
CalculateTax()

EvaluateCredit()

DeterminePrice()

GenerateInvoiceNumber()
```

Avoid generic method names such as:

```text id="c4m8yn"
Execute()

Run()

Handle()

Process()
```

---

# Side Effects

Prefer pure business behaviour.

When practical, Domain Services should avoid side effects.

External communication belongs outside the Domain.

---

# Domain Policies

If a business rule changes frequently because of business policy rather than implementation, encapsulate that rule inside a Domain Service or Policy.

Business policies should remain explicit.

---

# Collaboration

Domain Services coordinate Domain objects.

They should not manipulate internal Aggregate state directly.

Aggregates remain responsible for protecting their own invariants.

---

# Validation

Business validation performed by a Domain Service should represent business rules.

Input validation belongs outside the Domain.

---

# Persistence

Persistence remains an implementation detail.

A Domain Service must never contain:

* SQL
* EF Core logic
* Database transactions
* ORM-specific behaviour

---

# Testing

Domain Services should be tested as pure business components.

Tests should verify:

* Business calculations.
* Business decisions.
* Business policies.
* Edge cases.
* Failure scenarios.

Tests should not require infrastructure.

---

# Anti-Patterns

Avoid:

* Technical services inside the Domain.
* Stateful Domain Services.
* Generic "Manager" classes.
* Infrastructure dependencies.
* Business logic duplicated from Aggregates.
* Domain Services that simply forward method calls.

---

# Domain Service Checklist

Before completing a Domain Service, verify:

* Behaviour does not belong to an Aggregate.
* Behaviour does not belong to a Value Object.
* Business terminology is used.
* Service is stateless.
* No infrastructure dependencies exist.
* Business rules remain explicit.
* Repository implementations are not referenced.
* Behaviour is independently testable.

---

# Guiding Principle

A Domain Service should represent an important business capability that cannot naturally belong to any single Domain object.

It exists to strengthen the Domain Model—not to compensate for poor Aggregate design.
