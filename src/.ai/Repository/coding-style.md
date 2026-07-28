# Coding Style

Version: 1.0

Status: Repository Convention

---

# Purpose

This document defines the coding style used throughout the repository.

The objective is consistency.

Every new source file should look as though it was written by the same engineer.

Code style is part of the architecture.

---

# General Principles

Code should be:

- Explicit
- Readable
- Predictable
- Maintainable
- Consistent

Never optimise readability for cleverness.

---

# Language Version

Use the latest approved C# language features adopted by the repository.

Do not introduce language features that are not already used consistently.

Consistency is preferred over novelty.

---

# Readability First

Code is read far more often than it is written.

Always optimise for the next developer reading the code.

---

# Class Design

Classes should have a single responsibility.

Avoid large classes with multiple unrelated concerns.

Prefer composition over inheritance.

---

# Sealed Classes

## Observed Pattern

Repository analysis indicates a preference for concrete classes over inheritance.

When a class is not intended to be extended, prefer:

```csharp
public sealed class CustomerService
{
}
```

Do not leave classes inheritable without a reason.

---

# Small Methods

Methods should express one idea.

If a method requires extensive comments to explain its behaviour, consider extracting smaller methods.

---

# Method Ordering

Members should appear in a predictable order.

Recommended order:

- Constants
- Static fields
- Instance fields
- Public properties
- Constructors
- Public methods
- Protected methods
- Private methods

Neighbouring files should remain consistent.

---

# Constructors

Constructor dependencies should be explicit.

Avoid constructors performing significant work.

Constructors should establish valid object state.

---

# Fields

Private fields should follow the repository convention.

Observed style:

```csharp
private readonly ILogger<CustomerService> _logger;
```

Fields should remain immutable whenever possible.

---

# Properties

Properties should communicate state.

Avoid complex business logic inside property getters.

Computed behaviour belongs inside methods.

---

# Expression-bodied Members

Use expression-bodied members only when they improve readability.

Do not convert large methods into expression-bodied syntax.

---

# var Usage

Use `var` when the type is obvious.

Example:

```csharp
var customer = new Customer();
```

Prefer explicit types when they improve readability.

Avoid unnecessary verbosity.

---

# Nullability

Nullable Reference Types should remain enabled.

Never suppress nullable warnings without understanding the root cause.

Avoid:

```csharp
#pragma warning disable
```

or

```csharp
!
```

unless justified.

---

# Object Initializers

Use object initializers when they improve clarity.

Avoid large object initializers that hide important construction logic.

---

# Collection Initialisation

Use the repository's preferred collection syntax consistently.

Do not mix different collection styles within neighbouring code.

---

# Pattern Matching

Use pattern matching where it improves readability.

Avoid pattern matching solely because it is newer syntax.

---

# Magic Values

Avoid magic numbers and magic strings.

Extract meaningful constants when values have semantic meaning.

---

# Comments

Code should explain itself.

Comments should explain:

- Why
- Architectural decisions
- Business intent

Avoid comments that merely describe what the code already says.

---

# Regions

## Current Convention

The repository uses regions to provide a consistent navigation experience.

Regions are part of the repository coding style and should be used consistently across all non-trivial classes.

The preferred region order is:

```csharp
#region " Fields "

#endregion

#region " Properties "

#endregion

#region " Constructors "

#endregion

#region " Factory "

#endregion

#region " Methods "

#endregion

#region " Private Methods "

#endregion
```

### Fields

Contains:

- private fields
- readonly fields
- static readonly fields

---

### Properties

Contains:

- public properties
- internal properties
- protected properties

---

### Constructors

Contains:

- constructors
- dependency injection constructors

---

### Factory

Contains:

- static Create(...)
- static From(...)
- Parse(...)
- TryParse(...)
- Build(...)
- Clone(...)

Factory methods should be grouped together inside this region.

If a class has no factory methods, omit this region.

---

### Methods

Contains:

- public methods
- internal methods
- protected methods

Methods should be ordered by importance and usage.

---

### Private Methods

Contains:

- private helper methods

Private methods should appear after all public-facing members.

---

# Region Rules

- Preserve the defined region order.
- Omit empty regions.
- Do not create additional regions unless approved by the repository.
- Do not nest regions.
- Keep each region focused on a single responsibility.

The only approved regions are:

- Fields
- Properties
- Constructors
- Factory
- Methods
- Private Methods

---

# Partial Classes

Use partial classes only when required.

Examples:

- Generated code
- Source generators
- Large generated models

Do not split ordinary business classes unnecessarily.

---

# Static Classes

Static classes should contain:

- Extension methods
- Pure helper functions

Avoid using static classes to hide mutable state.

---

# Exceptions

Do not use exceptions for normal control flow.

Business failures should follow the repository's Result convention.

---

# LINQ

Use LINQ when it improves readability.

Avoid deeply nested LINQ expressions.

Prefer clarity over compactness.

---

# Async Code

Always propagate asynchronous execution.

Avoid blocking calls such as:

```csharp
.Result

.Wait()
```

Public asynchronous methods should end with:

```text
Async
```

---

# CancellationToken

Accept CancellationToken where operations may block or perform I/O.

Propagate tokens to downstream dependencies.

Do not ignore supplied tokens.

---

# Dependency Access

Dependencies should only be accessed through constructor injection.

Avoid:

- Service Locator
- Static service access
- Global state

---

# Immutability

Prefer immutable state whenever practical.

Mutable state should have a clearly defined owner.

---

# Defensive Programming

Validate assumptions at system boundaries.

Do not duplicate validation throughout the application.

---

# Formatting

Formatting should be enforced automatically.

Do not rely on manual formatting.

Use the repository formatter and editor configuration.

---

# AI Instructions

Before generating code, verify:

1. Does the code resemble neighbouring files?
2. Is the implementation unnecessarily clever?
3. Can complexity be reduced?
4. Are responsibilities clearly separated?
5. Is the style consistent with the repository?

If the answer to any question is "No", revise before producing code.

---

# Repository Convention

The repository values consistency over personal preference.

When uncertain, prefer matching the style of surrounding code rather than introducing a new style.