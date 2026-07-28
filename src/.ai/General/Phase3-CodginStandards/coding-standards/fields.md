# Fields

Version: 1.0

---

# Purpose

This document defines the mandatory rules for declaring and using fields throughout the repository.

Fields represent the internal state of a class.

They should remain hidden and protected from external access.

---

# Primary Principle

Fields are implementation details.

Objects expose behaviour and properties—not fields.

---

# Accessibility

Fields should almost always be:

* private

Use:

* protected

only when a valid inheritance hierarchy requires it.

Avoid:

* public fields
* internal mutable fields
* protected mutable fields

---

# Naming

Private fields use camelCase with a leading underscore.

Examples:

```text id="g8k2nv"
_repository

_logger

_clock

_orderLines

_currentBalance
```

Never use:

```text id="r4j8xa"
repository

Repository

m_repository

repositoryField
```

---

# Readonly by Default

Fields should be `readonly` whenever possible.

Good:

```csharp id="d9m6zt"
private readonly IClock _clock;
private readonly IInvoiceRepository _repository;
```

Mutable fields should exist only when object state genuinely changes.

---

# Dependency Fields

Injected dependencies should always be stored in readonly fields.

Example:

```csharp id="w3b9rp"
private readonly ILogger<CreateInvoiceHandler> _logger;
private readonly IClock _clock;
```

Dependencies should never change after construction.

---

# Business State

Business state should remain private.

Example:

```csharp id="n2q5cf"
private decimal _balance;
private InvoiceStatus _status;
private readonly List<OrderLine> _orderLines = [];
```

External code interacts through methods and properties.

---

# Collections

Mutable collections should always remain private.

Expose read-only views.

Good:

```csharp id="f6y1lh"
private readonly List<OrderLine> _orderLines = [];

public IReadOnlyCollection<OrderLine> OrderLines =>
    _orderLines;
```

Avoid exposing the mutable collection itself.

---

# Static Fields

Use static fields only when:

* The value is shared.
* The value is immutable.
* The value is thread-safe.

Avoid mutable static state.

---

# Constants

Prefer `const` for compile-time constants.

Example:

```csharp id="p8m4sk"
private const int MaxRetryCount = 3;
```

Use `static readonly` for runtime constants.

---

# Initialization

Fields should be initialized:

* At declaration, or
* In the constructor.

Objects should never exist in a partially initialized state.

---

# Ordering

Within a class, fields should appear in this order:

1. Constants
2. Static readonly fields
3. Static fields
4. Readonly instance fields
5. Mutable instance fields

Then:

* Constructors
* Properties
* Methods

Maintain this ordering consistently.

---

# Nullable Fields

Avoid nullable fields unless the lifecycle requires them.

If a field is nullable:

* Explain the lifecycle through code.
* Avoid unnecessary null checks.
* Initialize as early as possible.

---

# Thread Safety

Shared mutable fields must be protected appropriately.

Avoid exposing mutable shared state.

Thread safety should be intentional.

---

# Lazy Initialization

Use lazy initialization only when:

* Construction is expensive.
* Delayed creation provides measurable value.

Do not use laziness as premature optimisation.

---

# Backing Fields

Use backing fields only when property behaviour requires them.

Avoid unnecessary backing fields for simple auto-properties.

---

# Dependency Injection

Every injected dependency should correspond to exactly one readonly field.

Avoid resolving dependencies dynamically after construction.

---

# Reflection

Business logic must never depend on modifying private fields through reflection.

Reflection should remain an infrastructure concern.

---

# Comments

Fields rarely require comments.

Meaningful naming should communicate intent.

---

# Anti-Patterns

Avoid:

* Public fields.
* Mutable static fields.
* Non-readonly dependency fields.
* Exposed collections.
* Generic field names.
* Hidden shared state.
* Partially initialized objects.

---

# Field Checklist

Before completing a class, verify:

* Fields are private.
* Dependencies are readonly.
* Mutable collections remain hidden.
* Ordering follows repository conventions.
* Naming uses `_camelCase`.
* Initialization is complete.
* Static state is justified.

---

# Guiding Principle

Fields are private implementation details.

They should protect an object's internal state while allowing the object's public behaviour to define how that state changes.
