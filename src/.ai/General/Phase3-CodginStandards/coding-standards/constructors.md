# Constructors

Version: 1.0

---

# Purpose

This document defines the mandatory rules for designing constructors throughout the repository.

Constructors establish valid object state and explicitly declare an object's dependencies.

A well-designed constructor makes object creation predictable and safe.

---

# Primary Principle

A constructor must leave an object in a valid state.

After construction, the object should be immediately usable.

---

# Constructor Responsibility

Constructors should only:

* Initialize fields.
* Validate required arguments.
* Establish valid state.

Constructors should not execute business workflows.

---

# Dependency Injection

All required dependencies must be injected through the constructor.

Example:

```csharp id="p8x3zn"
public sealed class CreateInvoiceHandler
{
    private readonly IInvoiceRepository _repository;
    private readonly IClock _clock;

    public CreateInvoiceHandler(
        IInvoiceRepository repository,
        IClock clock)
    {
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(clock);

        _repository = repository;
        _clock = clock;
    }
}
```

Dependencies must remain explicit.

---

# Required Arguments

Every required argument must be validated immediately.

Prefer:

```csharp id="r2k7lh"
ArgumentNullException.ThrowIfNull(customer);
```

Fail fast.

---

# Optional Arguments

Avoid optional constructor parameters unless they represent genuine optional configuration.

If many optional values exist, consider:

* Options pattern.
* Builder pattern.
* Factory pattern.

---

# Constructor Size

Constructors should remain small.

Recommended guidelines:

* Fewer than 20 lines.
* Simple initialization only.

Large constructors often indicate too many dependencies.

---

# Number of Dependencies

A constructor with many dependencies usually indicates excessive responsibility.

Recommended:

* 3–5 dependencies.

If significantly more are required, reconsider the class design.

---

# Business Logic

Do not place business logic inside constructors.

Avoid:

* Domain decisions.
* Database access.
* Network calls.
* Event publishing.

Constructors initialize objects—they do not execute workflows.

---

# Side Effects

Constructors must not produce observable side effects.

Avoid:

* Logging.
* Database writes.
* HTTP requests.
* Message publishing.
* File access.

Object construction should be deterministic.

---

# Async Work

Constructors cannot be asynchronous.

If asynchronous initialization is required, use:

* Factory methods.
* Factory services.
* Explicit InitializeAsync methods only when repository conventions require them.

Never block asynchronous work inside constructors.

---

# Exceptions

Constructors should throw only when:

* Required arguments are invalid.
* Object invariants cannot be established.

Do not swallow exceptions.

---

# Overloads

Avoid excessive constructor overloads.

Prefer one primary constructor whenever practical.

Multiple overloads should represent meaningful creation scenarios.

---

# Static Factory Methods

When construction becomes complex, prefer named factory methods.

Example:

```text id="z5n4qa"
Create()

Restore()

FromSnapshot()

FromPersistence()
```

Factory methods communicate intent more clearly than overloaded constructors.

---

# Domain Objects

Domain constructors should establish valid business state.

An invalid Aggregate or Entity must never be created.

Business invariants should hold immediately after construction.

---

# Value Objects

Value Objects should be fully initialized during construction.

After construction, they should be immutable.

---

# Dependency Lifetime

Constructors should not resolve services dynamically.

Dependencies are provided by the caller.

Avoid:

* Service Locator.
* IServiceProvider.
* Static service access.

---

# Ordering

Constructor parameters should follow a consistent order:

1. Primary business dependencies.
2. Supporting services.
3. Technical services.
4. CancellationToken (only when applicable to factory methods).

Consistency improves readability.

---

# Comments

Constructors should rarely require comments.

If explanation is necessary, reconsider the design.

---

# Anti-Patterns

Avoid:

* Large constructors.
* Hidden dependency resolution.
* Business logic.
* Asynchronous work.
* Side effects.
* Excessive overloads.
* Optional dependency injection.
* Partial initialization.

---

# Constructor Checklist

Before completing a constructor, verify:

* Required dependencies are injected.
* Arguments are validated.
* The object is fully initialized.
* Business invariants hold.
* No side effects exist.
* Constructor remains small.
* Dependency count is reasonable.
* Hidden dependencies do not exist.

---

# Guiding Principle

A constructor has one purpose:

> Create a fully initialized, valid object with explicit dependencies and no observable side effects.
