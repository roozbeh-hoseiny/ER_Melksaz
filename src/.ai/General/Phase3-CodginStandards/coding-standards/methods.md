# Methods

Version: 1.0

---

# Purpose

This document defines the mandatory rules for designing methods throughout the repository.

Methods are the primary unit of behaviour.

Well-designed methods improve readability, maintainability, testability, and correctness.

---

# Primary Principle

A method should perform one well-defined operation.

A reader should understand its purpose from its name and signature.

---

# Single Responsibility

Each method should have one responsibility.

If a method performs multiple logical operations, split it into smaller methods.

---

# Method Size

Prefer small methods.

Recommended guidelines:

* Under 30 lines.
* Under 10 statements.
* One level of abstraction.

Large methods usually indicate multiple responsibilities.

---

# Method Naming

Method names should be verbs.

Examples:

```text id="v6k2qx"
Approve()

Reject()

CalculateTotal()

ReceivePayment()

PublishAsync()
```

Avoid generic names:

```text id="m3r9jw"
Process()

Run()

Execute()

HandleData()
```

---

# One Level of Abstraction

A method should operate at a single level of abstraction.

Avoid mixing:

* Business decisions
* Infrastructure operations
* Low-level implementation details

inside the same method.

---

# Parameters

Prefer fewer parameters.

Recommended:

* 0–3 parameters.

If more are required:

* Introduce a Parameter Object.
* Reconsider the method responsibility.

---

# Parameter Order

Use the following order when applicable:

1. Required business parameters.
2. Optional parameters.
3. CancellationToken (last).

Example:

```csharp id="k2r7wm"
Task SaveAsync(
    Invoice invoice,
    CancellationToken cancellationToken)
```

---

# Return Values

Methods should return meaningful results.

Prefer:

* Domain objects
* Value Objects
* Result
* DTOs

Avoid returning primitive values when richer types improve clarity.

---

# Void Methods

Avoid `void` unless:

* Raising events.
* Property setters.
* UI callbacks.

Business methods should normally return a meaningful result.

---

# Async Methods

Async methods:

* End with `Async`.
* Return `Task` or `Task<T>`.
* Accept `CancellationToken` where appropriate.

Avoid `async void`.

---

# Exceptions

Methods should:

* Throw only exceptional failures.
* Return `Result` for expected business failures when repository conventions require it.

Do not use exceptions for normal control flow.

---

# Guard Clauses

Validate method preconditions immediately.

Prefer:

```csharp id="n5x3lh"
ArgumentNullException.ThrowIfNull(invoice);
```

over deeply nested conditions.

---

# Early Return

Prefer early returns over deeply nested `if` statements.

Good:

```csharp id="g9z4vc"
if (!invoice.CanApprove)
{
    return;
}

invoice.Approve();
```

---

# Side Effects

A method should make its side effects obvious.

Avoid methods that both:

* Query data.
* Modify state.

unless the behaviour naturally requires both.

---

# Boolean Parameters

Avoid boolean flags.

Instead of:

```csharp id="x1m9jr"
Save(true)
```

Prefer:

```text id="f2d8wp"
SaveDraft()

Publish()
```

---

# Private Methods

Extract private methods to improve readability.

Do not extract methods that hide rather than simplify the logic.

---

# Overloads

Keep overloads simple.

Avoid large overload hierarchies.

Provide sensible defaults instead.

---

# Recursion

Use recursion only when it clearly expresses the problem.

Prefer iterative solutions for ordinary business logic.

---

# Comments

Method names should explain what the method does.

Comments should explain why an unusual implementation exists.

Avoid comments that merely describe the code.

---

# Purity

Prefer pure methods whenever practical.

Pure methods:

* Have no side effects.
* Depend only on inputs.
* Always return the same output for the same input.

Pure methods are easier to test.

---

# Visibility

Use the smallest visibility possible.

Prefer:

* private
* protected
* internal

Expose methods publicly only when required.

---

# Method Ordering

Inside a class, order methods as follows:

1. Public methods.
2. Internal methods.
3. Protected methods.
4. Private methods.

Within each group, place higher-level behaviour before implementation details.

---

# Anti-Patterns

Avoid:

* Long methods.
* Deep nesting.
* Boolean parameters.
* Multiple responsibilities.
* Hidden side effects.
* Generic method names.
* Copy-paste implementations.
* Excessive overloads.

---

# Method Checklist

Before completing a method, verify:

* One responsibility.
* Clear verb-based name.
* Small parameter list.
* Appropriate return type.
* Async conventions followed.
* Side effects are explicit.
* Guard clauses used.
* Readability preserved.
* Repository conventions followed.

---

# Guiding Principle

A well-designed method should read like a sentence in the business language and perform one coherent piece of work with minimal surprise.
