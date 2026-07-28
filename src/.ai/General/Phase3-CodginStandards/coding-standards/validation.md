# Validation

Version: 1.0

---

# Purpose

This document defines the mandatory validation rules for the repository.

Validation ensures that data entering the system satisfies the required constraints before business behaviour is executed.

Validation protects the system.

Business rules protect the business.

These are different responsibilities.

---

# Primary Principle

Validate input as early as possible.

Business invariants are enforced by the Domain.

Input validation is enforced by the Application boundary.

---

# Validation Layers

Validation exists at multiple layers.

### API

Validates:

* Required fields
* Data format
* Request structure
* Serialization

---

### Application

Validates:

* Commands
* Queries
* User input
* Cross-field rules

---

### Domain

Validates:

* Business invariants
* Aggregate consistency
* Entity consistency
* Value Object correctness

---

# Separation of Responsibilities

Input validation answers:

> "Is this request structurally valid?"

Business validation answers:

> "Is this operation allowed?"

These responsibilities must remain separate.

---

# FluentValidation

The repository standard for application validation is:

```text id="g6v2ke"
FluentValidation
```

Validators belong in the Application layer.

Example:

```text id="u5n8dx"
CreateInvoiceValidator

ApproveInvoiceValidator
```

---

# Validation Location

Validate:

* Commands
* Queries
* API Requests

before executing business behaviour.

Avoid validation inside handlers when a validator already exists.

---

# Domain Validation

The Domain must always protect its invariants.

Never assume upstream validation is sufficient.

Example:

```text id="z8t1fm"
Invoice total cannot be negative.

Customer credit limit cannot be exceeded.

Order cannot be shipped twice.
```

The Domain is the final authority.

---

# Required Fields

Required business data must be validated explicitly.

Avoid relying solely on nullable reference types.

Validation should communicate intent.

---

# Null Validation

Validate required reference types immediately.

Example:

```csharp id="k9m2bt"
ArgumentNullException.ThrowIfNull(customer);
```

Fail fast.

---

# String Validation

Validate:

* Empty
* Whitespace
* Length
* Format

Do not accept invalid textual input.

---

# Collection Validation

Validate:

* Empty collections
* Duplicate values
* Maximum size
* Required elements

Collections represent business intent.

---

# Cross-Property Validation

Application validators may validate relationships between properties.

Example:

```text id="j3w7la"
StartDate < EndDate
```

Business rules involving existing state belong in the Domain.

---

# Database Validation

Avoid database access inside validators unless repository conventions explicitly permit it.

Business decisions involving persistence belong in the Application or Domain.

---

# Validation Messages

Messages should:

* Be clear.
* Be concise.
* Describe the problem.
* Avoid implementation details.

Good:

```text id="m2y8fd"
Invoice number is required.
```

Avoid:

```text id="h4q9vn"
Invalid parameter.
```

---

# Exception Usage

Validation failures should not normally throw exceptions.

Return validation results through the repository's approved mechanism.

Exceptions remain reserved for unexpected failures.

---

# API Responses

Validation failures should return appropriate client responses.

Typical example:

```text id="t8n5cr"
400 Bad Request
```

Responses should include meaningful validation details.

---

# Domain Constructors

Constructors should reject invalid state immediately.

An invalid Domain object must never exist.

---

# Value Objects

Value Objects validate themselves during construction.

Invalid Value Objects must not be created.

---

# Aggregate Validation

Aggregate Roots are responsible for protecting all Aggregate invariants.

No external code should bypass Aggregate validation.

---

# Duplicate Validation

Avoid duplicate validation across layers.

Each layer validates what it owns.

Example:

* API → Request shape.
* Application → Input rules.
* Domain → Business rules.

---

# AI Responsibilities

When generating code, the AI must:

* Place validation in the correct layer.
* Use FluentValidation for application validation.
* Protect Domain invariants.
* Avoid duplicate validation.
* Keep validation messages clear.
* Follow repository conventions.

---

# Anti-Patterns

Avoid:

* Business validation inside controllers.
* Validation inside repositories.
* Validation inside DbContext.
* Duplicate validation.
* Generic validation messages.
* Exceptions for ordinary validation failures.
* Invalid Domain objects.

---

# Validation Checklist

Before completing an implementation, verify:

* Validation occurs in the correct layer.
* Domain invariants are protected.
* FluentValidation is used where appropriate.
* Validation messages are meaningful.
* Duplicate validation has been avoided.
* Invalid objects cannot be created.
* Repository conventions are followed.

---

# Guiding Principle

Validation ensures requests are valid.

The Domain ensures business decisions are valid.

Never confuse input validation with business behaviour.
