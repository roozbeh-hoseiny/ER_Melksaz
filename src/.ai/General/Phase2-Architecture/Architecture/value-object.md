# Value Object

Version: 1.0

---

# Purpose

This document defines the design rules for Value Objects within the Domain Model.

A Value Object represents a meaningful business concept that has no identity.

It exists solely because of the value it represents.

---

# Primary Principle

A Value Object is defined by **what it is**, not **who it is**.

Identity is irrelevant.

Only the value matters.

---

# Definition

A Value Object has the following characteristics:

* No identity.
* Immutable.
* Equality by value.
* Self-validating.
* Behaviour-rich.
* Side-effect free.

---

# Examples

Typical Value Objects include:

* Money
* Email
* Address
* FullName
* PhoneNumber
* Currency
* Quantity
* Percentage
* TaxRate
* InvoiceNumber
* CustomerNumber

These concepts represent business meaning rather than identity.

---

# Immutability

A Value Object must be immutable.

After creation:

* No property may change.
* No internal state may change.
* No mutable collections may exist.

A new Value Object should be created whenever a value changes.

---

# Equality

Two Value Objects are equal when all of their values are equal.

Example:

```text id="9t3jlwm"
Money(100, USD)

==

Money(100, USD)
```

Identity is never considered.

---

# Self Validation

A Value Object validates itself during construction.

Invalid Value Objects must never exist.

Example business rules:

* Email format
* Currency code
* Percentage range
* Positive quantity
* Valid tax rate

Construction should fail if business rules are violated.

---

# Behaviour

Value Objects contain business behaviour related to their values.

Examples:

```text id="4bhx1kr"
Money.Add()

Money.Subtract()

Percentage.Apply()

Quantity.Increase()

Quantity.Decrease()

Address.Format()
```

Avoid exposing only data.

---

# Primitive Obsession

Avoid using primitive types when a business concept exists.

Prefer:

```text id="wz8u9dn"
Email
```

instead of

```text id="w90kbz3"
string
```

Prefer:

```text id="bnkz0ch"
Money
```

instead of

```text id="o0x7lpk"
decimal
```

Business concepts should be explicit.

---

# Construction

Construction should guarantee validity.

Avoid partially valid instances.

Provide factory methods only when construction requires additional business logic.

---

# Side Effects

Value Objects must never:

* Access databases.
* Call APIs.
* Publish events.
* Modify external state.

Behaviour should always be deterministic.

---

# Composition

Value Objects may contain other Value Objects.

Example:

```text id="ml9n1rx"
Address

 ├── Street

 ├── City

 ├── PostalCode

 └── Country
```

Composition is encouraged when it improves the business model.

---

# Persistence

Persistence is an implementation detail.

Value Objects must not contain:

* EF Core attributes.
* ORM configuration.
* Database logic.
* Serialization concerns.

---

# Serialization

Serialization requirements belong outside the Domain whenever practical.

Value Objects should remain independent of transport technologies.

---

# Operators

Where meaningful, Value Objects may support:

* Equality operators
* Comparison operators
* Arithmetic operators

Operators should express business meaning.

---

# Collections

Collections of Value Objects should remain immutable whenever practical.

Avoid exposing mutable collections publicly.

---

# Domain Rules

Business rules that concern only the value belong inside the Value Object.

Examples:

* Currency compatibility.
* Percentage limits.
* Email validation.
* Quantity calculations.

---

# Testing

Tests should verify:

* Equality.
* Immutability.
* Validation.
* Business behaviour.
* Invalid construction.
* Operator behaviour (if applicable).

Persistence should never be required.

---

# Anti-Patterns

Avoid:

* Public setters.
* Mutable state.
* Identity.
* Database dependencies.
* Infrastructure dependencies.
* Getter-only wrappers around primitive types.
* Primitive Obsession.
* Validation outside the Value Object.

---

# Value Object Checklist

Before completing a Value Object, verify:

* No identity exists.
* Immutable after construction.
* Equality is value-based.
* Self-validation is complete.
* Business behaviour exists.
* No infrastructure dependencies exist.
* Primitive types are replaced where appropriate.
* Invalid instances cannot be created.

---

# Guiding Principle

A Value Object should make the business language richer, safer, and more expressive.

Whenever a primitive value represents an important business concept, consider modelling it as a Value Object.
