# Specification

Version: 1.0

---

# Purpose

This document defines the design rules for Specifications within the Domain Model.

A Specification encapsulates reusable business rules that can be evaluated consistently across the Domain.

Specifications improve readability, reduce duplication, and express business intent explicitly.

---

# Primary Principle

A Specification represents a business rule.

It should answer a business question.

---

# Definition

A Specification encapsulates:

* Business criteria.
* Business policy.
* Business eligibility.
* Business constraints.

A Specification should describe **what** is true rather than **how** it is evaluated.

---

# When to Use

Use a Specification when:

* A business rule is reused.
* A business decision is complex.
* Multiple Aggregates use the same business policy.
* Business language becomes clearer.

---

# When Not to Use

Do not create Specifications for:

* Simple property checks.
* Technical filtering.
* Persistence concerns.
* Framework limitations.

Avoid unnecessary abstraction.

---

# Naming

Specifications should be named using business language.

Good:

```text id="0e2zsu"
EligibleForDiscountSpecification

CanApproveInvoiceSpecification

CustomerHasOutstandingDebtSpecification
```

Avoid:

```text id="k5s8ad"
InvoiceFilter

CustomerChecker

BusinessValidator
```

---

# Business Language

A Specification should express a business statement.

Examples:

* Customer is eligible for credit.
* Invoice can be cancelled.
* Payment exceeds credit limit.
* Order qualifies for free shipping.

The name should be understandable by domain experts.

---

# Composition

Specifications should be composable.

Common operations include:

* AND
* OR
* NOT

Composition should remain readable and reflect business intent.

---

# Pure Business Logic

Specifications contain only business rules.

They must not contain:

* Database queries.
* HTTP logic.
* Messaging.
* Logging.
* Infrastructure concerns.

---

# Statelessness

Specifications should be stateless.

Evaluation depends only on:

* Input.
* Business rules.

State should never be stored inside a Specification.

---

# Repository Usage

Specifications should not execute queries directly.

Repository implementations may translate Specifications into persistence queries where appropriate.

The Specification remains persistence independent.

---

# Side Effects

Evaluating a Specification must not:

* Modify state.
* Publish events.
* Persist data.
* Call external systems.

Specifications are pure business predicates.

---

# Return Value

A Specification should answer a clear business question.

Typical result:

```text id="n3w6jq"
true

false
```

Avoid returning technical information.

---

# Validation

Specifications are not input validators.

Input validation belongs outside the Domain.

Specifications determine business eligibility.

---

# Reuse

Whenever a business rule appears in multiple places, consider extracting it into a Specification.

Avoid duplicating business policies.

---

# Testing

Every Specification should have dedicated unit tests.

Tests should verify:

* Matching cases.
* Non-matching cases.
* Edge cases.
* Composition behaviour.
* Business intent.

Testing should remain independent of infrastructure.

---

# Performance

Specifications should remain lightweight.

Avoid expensive computations unless they represent unavoidable business behaviour.

Performance optimisation must never obscure business intent.

---

# Anti-Patterns

Avoid:

* Technical Specifications.
* Persistence-specific Specifications.
* SQL inside Specifications.
* Infrastructure dependencies.
* Stateful Specifications.
* Generic "Filter" classes.
* Specifications that modify business state.

---

# Specification Checklist

Before completing a Specification, verify:

* Represents a business rule.
* Uses business terminology.
* Is reusable.
* Is stateless.
* Has no infrastructure dependencies.
* Can be composed when appropriate.
* Has dedicated unit tests.
* Improves Domain readability.

---

# Guiding Principle

A Specification makes business policies explicit.

If a business expert can read its name and understand the rule it represents, the Specification has achieved its purpose.
