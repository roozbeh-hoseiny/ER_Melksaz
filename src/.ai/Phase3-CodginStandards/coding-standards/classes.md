# Classes

Version: 1.0

---

# Purpose

This document defines the mandatory rules for designing classes throughout the repository.

Classes are the primary building blocks of the solution.

Every class should have one clear responsibility and fit naturally within the architecture.

---

# Primary Principle

A class should represent exactly one concept.

If a class has multiple reasons to change, it should be split.

---

# Single Responsibility

Each class should have one responsibility.

Examples:

* Aggregate
* Entity
* Value Object
* Repository
* Handler
* Validator
* Endpoint

Avoid combining unrelated behaviour.

---

# Class Size

Classes should remain focused.

Recommended guidelines:

* Prefer fewer than 15 methods.
* Prefer fewer than 300 lines.
* Avoid large God Objects.

Large classes usually indicate multiple responsibilities.

---

# Accessibility

Use the smallest visibility possible.

Prefer:

* private
* protected
* internal

Only make a class `public` when it forms part of the module's public API.

---

# Sealed by Default

Concrete classes should be `sealed` unless inheritance is explicitly required.

Good:

```csharp
public sealed class InvoiceRepository
{
}
```

Avoid unnecessary inheritance.

---

# Abstract Classes

Create an abstract class only when:

* There is a true "is-a" relationship.
* Shared behaviour cannot reasonably be achieved through composition.

Avoid inheritance for simple code reuse.

---

# Static Classes

Static classes are acceptable only for:

* Extension methods.
* Pure utility functions with no state.
* Constants that cannot belong elsewhere.

Avoid static classes that hide dependencies.

---

# State

Classes should own and protect their state.

Avoid exposing mutable internal state.

State changes should occur through behaviour.

---

# Constructors

A constructor should:

* Fully initialize the object.
* Leave the object in a valid state.
* Fail immediately if required dependencies are missing.

Constructors should not perform expensive work.

---

# Dependencies

Dependencies must be explicit.

Inject collaborators through the constructor.

Avoid:

* Service Locator.
* Static service access.
* Hidden dependencies.

---

# Behaviour

Methods should represent meaningful behaviour.

Good:

```text id="m4z8jr"
Approve()

Reject()

ReceivePayment()

CalculateOutstandingBalance()
```

Avoid generic methods such as:

```text id="r9n2xb"
Execute()

Process()

Run()

Handle()
```

unless repository conventions require them.

---

# Encapsulation

Keep implementation details private.

Public members should expose behaviour—not implementation.

Prefer private helper methods over public utility methods.

---

# Inheritance

Prefer composition over inheritance.

Inheritance should model true specialization.

Avoid deep inheritance hierarchies.

---

# Mutable Collections

Never expose mutable collections directly.

Prefer:

```csharp
public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;
```

instead of:

```csharp
public List<OrderLine> OrderLines { get; set; }
```

---

# Class Relationships

A class should collaborate through abstractions whenever possible.

Avoid unnecessary coupling to concrete implementations.

---

# Partial Classes

Business classes should not be partial.

Partial classes are reserved for:

* Source generators.
* Framework-generated code.
* Designer-generated code.

---

# Base Classes

Keep base classes minimal.

Base classes should provide only universally applicable behaviour.

Do not create inheritance hierarchies for convenience.

---

# Comments

Well-designed classes rarely require comments.

If a class needs extensive comments to explain its purpose, reconsider its design.

---

# Class Naming

Class names should:

* Be nouns.
* Reflect business terminology.
* Clearly communicate responsibility.

Examples:

```text id="z6q5lv"
Invoice

Customer

InvoiceRepository

ApproveInvoiceHandler
```

---

# Anti-Patterns

Avoid:

* God Objects.
* Utility classes.
* Manager classes.
* Processor classes.
* Classes with multiple responsibilities.
* Hidden dependencies.
* Large inheritance hierarchies.
* Mutable public state.

---

# Class Checklist

Before completing a class, verify:

* It has one responsibility.
* It has one reason to change.
* Dependencies are explicit.
* State is encapsulated.
* Behaviour is meaningful.
* Visibility is minimal.
* It is sealed unless inheritance is required.
* It follows repository naming conventions.

---

# Guiding Principle

A well-designed class should communicate its purpose through its name, protect its own state, and encapsulate one coherent piece of behaviour.
