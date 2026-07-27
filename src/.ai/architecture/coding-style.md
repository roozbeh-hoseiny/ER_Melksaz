# Coding Style

Version: 1.0

---

# Purpose

This document defines the coding style used throughout the repository.

Coding style is intended to improve readability, consistency, maintainability, and long-term evolution of the codebase.

Style exists to reduce cognitive load, not personal expression.

---

# General Principles

Every piece of code should be:

* Readable
* Predictable
* Consistent
* Explicit
* Maintainable
* Self-documenting

Code is read far more often than it is written.

Always optimise for the reader.

---

# Consistency

Repository consistency is more important than personal preference.

When multiple valid styles exist, always choose the style already used within the repository.

---

# Readability

Code should communicate intent without requiring additional explanation.

Prefer descriptive code over comments.

Good code explains itself.

---

# Simplicity

Prefer the simplest implementation that correctly solves the problem.

Avoid unnecessary:

* Abstractions
* Design patterns
* Generic types
* Indirection
* Clever implementations

Simple code is easier to review, test, and maintain.

---

# Explicitness

Prefer explicit behaviour over implicit behaviour.

Examples include:

* Explicit constructors
* Explicit dependencies
* Explicit return values
* Explicit state transitions

Avoid hidden side effects.

---

# Single Responsibility

Every type should have one primary responsibility.

Every method should perform one logical operation.

When responsibilities become mixed, refactor the implementation.

---

# Class Size

Classes should remain focused.

Very large classes usually indicate multiple responsibilities.

Refactor before complexity becomes difficult to manage.

---

# Method Size

Methods should remain concise.

A method should express one complete idea.

Avoid deeply nested implementations.

Extract meaningful private methods where appropriate.

---

# Parameter Count

Methods should accept only the parameters they require.

Large parameter lists often indicate poor design.

Prefer encapsulating related data into dedicated types.

---

# Immutability

Prefer immutable objects whenever practical.

Especially:

* Value Objects
* Commands
* Queries
* DTOs
* Configuration objects

Mutable state should be controlled carefully.

---

# Null Handling

Avoid returning null.

Prefer:

* Empty collections
* Optional types
* Repository Result types
* Explicit failure objects

Null should not be used to communicate business outcomes.

---

# Magic Values

Avoid magic numbers and magic strings.

Replace repeated literals with appropriately named constants or domain concepts.

---

# Boolean Parameters

Avoid boolean parameters that change behaviour.

Bad:

```csharp
Save(true);
```

Prefer:

```csharp
SaveDraft();

Publish();
```

Methods should clearly communicate intent.

---

# Comments

Comments should explain **why**, not **what**.

Do not comment code that is already self-explanatory.

Remove obsolete comments immediately.

---

# Regions

Avoid `#region` directives.

Large regions usually indicate excessive class size or poor organisation.

Refactor instead of hiding complexity.

---

# Nesting

Keep nesting shallow.

Prefer:

* Guard clauses
* Early returns
* Small methods

Deep nesting reduces readability.

---

# Conditional Logic

Avoid complex conditional logic.

Prefer:

* Polymorphism
* Specifications
* Strategy pattern
* Dedicated business methods

when complexity grows.

---

# Exceptions

Throw exceptions only for exceptional situations.

Business validation failures should not rely on exceptions.

---

# Asynchronous Code

Use asynchronous programming consistently.

Avoid:

* Blocking asynchronous calls
* `.Result`
* `.Wait()`
* Thread blocking

Async code should remain asynchronous throughout the call chain.

---

# LINQ

Prefer readable LINQ expressions.

Avoid overly complex query chains.

When readability suffers, use explicit loops.

---

# Collections

Return interfaces rather than concrete implementations where appropriate.

Expose only the operations consumers require.

Avoid leaking mutable collections.

---

# Extension Methods

Extension methods should:

* Improve readability
* Represent natural behaviour
* Remain side-effect free

Do not use extension methods to hide complex business logic.

---

# Helper Classes

Avoid generic helper classes.

Examples of discouraged names:

* Helper
* Utils
* Common
* Extensions

Prefer modelling behaviour through proper domain concepts.

---

# Static Classes

Use static classes only for:

* Pure utility functions
* Constants
* Stateless operations

Business logic should rarely be static.

---

# Dependency Injection

Dependencies must be injected explicitly.

Do not resolve dependencies manually.

Avoid service locator patterns.

---

# Formatting

Formatting should remain consistent throughout the repository.

Prefer:

* One statement per line
* One responsibility per block
* Consistent indentation
* Consistent spacing

Formatting should improve readability, not aesthetics.

---

# Code Duplication

Avoid duplication.

Before introducing new code, determine whether equivalent behaviour already exists.

Extract reusable behaviour only when the abstraction is stable.

---

# Refactoring

Leave code better than you found it.

Small, continuous improvements are preferred over large disruptive rewrites.

---

# Review Checklist

Before considering code complete, verify:

* Is the code easy to read?
* Is the intent obvious?
* Is complexity justified?
* Are responsibilities well separated?
* Is duplication avoided?
* Is the implementation consistent with the repository?
* Would another developer immediately understand it?

---

# Guiding Principle

Write code that another experienced developer can understand, trust, and modify without requiring additional explanation.
