# Properties

Version: 1.0

---

# Purpose

This document defines the mandatory rules for designing properties throughout the repository.

Properties represent object state.

They should expose information—not behaviour.

---

# Primary Principle

Properties expose state.

Methods perform behaviour.

If accessing a member performs meaningful work, it should probably be a method.

---

# Property Responsibility

A property should:

* Return state.
* Be inexpensive.
* Have no surprising side effects.

Avoid performing business logic inside property getters.

---

# Auto Properties

Use auto-properties whenever additional logic is unnecessary.

Example:

```csharp id="f4m9qa"
public string Name { get; }
```

Prefer simplicity.

---

# Read-Only by Default

Properties should be immutable whenever possible.

Prefer:

```csharp id="y2t7ph"
public CustomerId Id { get; }

public DateTime CreatedAt { get; }
```

Avoid public setters unless mutation is required.

---

# Private Setters

If mutation is necessary, prefer:

```csharp id="r7x3mk"
public string Name { get; private set; }
```

This preserves encapsulation.

---

# Init Properties

Use `init` properties for immutable objects that require object initialization.

Typical candidates:

* Records
* Configuration
* DTOs
* Value Objects (when appropriate)

---

# Public Setters

Avoid public setters on Domain objects.

Bad:

```csharp id="q9v4zr"
public decimal Balance { get; set; }
```

Prefer:

```text id="u5m2kc"
ReceivePayment()

Withdraw()

Deposit()
```

Business state changes through behaviour.

---

# Computed Properties

Computed properties should:

* Be inexpensive.
* Be deterministic.
* Avoid database access.
* Avoid network access.

Example:

```csharp id="x6k8jn"
public decimal OutstandingBalance =>
    Total - PaidAmount;
```

---

# Collections

Never expose mutable collections.

Prefer:

```csharp id="v3z1et"
public IReadOnlyCollection<OrderLine> OrderLines =>
    _orderLines;
```

Avoid:

```csharp id="g5a2rm"
public List<OrderLine> OrderLines { get; set; }
```

---

# Nullability

Use nullable reference types intentionally.

Avoid unnecessary nullable properties.

Required business state should not be nullable.

---

# Boolean Properties

Boolean properties should read naturally.

Good:

```text id="d8j6wp"
IsApproved

HasExpired

CanCancel

ShouldRetry
```

Avoid:

```text id="b2r9fm"
Approved

Expired

Retry
```

---

# Expensive Operations

Do not hide expensive work behind a property.

Avoid:

```text id="m7t5vk"
Customer.InvoiceHistory
```

if it performs:

* Database queries.
* API calls.
* File access.

Use a method instead.

---

# Lazy Loading

Business code should not rely on lazy-loading properties.

Object graphs should be explicit.

---

# Validation

Property setters should not contain complex business validation.

Business validation belongs inside behaviour.

Simple argument validation is acceptable when necessary.

---

# Property Order

Inside a class, use the following order:

1. Constants
2. Static fields
3. Private fields
4. Constructors
5. Public properties
6. Internal properties
7. Protected properties
8. Private properties
9. Methods

Maintain the same order consistently.

---

# Records

Immutable records may expose init-only properties.

Example:

```csharp id="w4h8lx"
public sealed record Address(
    string Street,
    string City,
    string Country);
```

---

# DTO Properties

DTOs may use mutable properties when required for:

* Serialization.
* Model binding.
* Framework compatibility.

Business rules must not exist inside DTOs.

---

# Domain Properties

Domain properties should represent business state.

They should never expose infrastructure details.

Avoid:

* EF Core types.
* HTTP types.
* Serialization attributes (unless repository conventions explicitly allow them).

---

# Comments

Properties should rarely require comments.

Meaningful naming should communicate intent.

---

# Anti-Patterns

Avoid:

* Public mutable state.
* Expensive getters.
* Hidden side effects.
* Lazy-loading business logic.
* Mutable collections.
* Business rules inside setters.
* Nullable properties without justification.

---

# Property Checklist

Before completing a property, verify:

* It exposes state rather than behaviour.
* It has no surprising side effects.
* It is immutable where possible.
* Mutable collections are hidden.
* Public setters are justified.
* Nullability is intentional.
* Naming follows repository conventions.

---

# Guiding Principle

A property should simply answer the question:

> "What is the current state?"

If obtaining the value performs meaningful work or changes behaviour, it should be implemented as a method instead.
