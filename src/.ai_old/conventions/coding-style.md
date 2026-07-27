# C# Coding Standards

Version: 1.0

This document defines the coding conventions used throughout the repository.

These rules exist to ensure that generated code appears to have been written by a single engineering team.

If any rule conflicts with default AI behaviour, these rules take precedence.

---

# General Principles

Code is read more often than it is written.

Prioritize

- readability
- consistency
- maintainability
- correctness

Do not sacrifice readability for cleverness.

---

# Modern C#

The repository uses the latest stable C# language features supported by the target framework.

Prefer modern syntax when it improves readability.

Never use modern syntax merely because it exists.

---

# Namespaces

## CS-001

Use file-scoped namespaces.

Good

```csharp
namespace Company.Project.Domain;
```

Bad

```csharp
namespace Company.Project.Domain
{
}
```

---

# One Type Per File

## CS-002

One public type per file.

File name equals type name.

---

## CS-003

Nested types are allowed only when they are implementation details.

---

# Visibility

## CS-004

Prefer the most restrictive visibility.

Priority

private

private protected

protected

internal

public

Never make something public without a reason.

---

## CS-005

Types are internal by default.

Public APIs require explicit justification.

---

## CS-006

Fields are private.

Never expose mutable fields.

---

# Fields

## CS-007

Readonly whenever possible.

Good

```csharp
private readonly IRepository _repository;
```

---

## CS-008

Prefer constructor injection.

Never resolve dependencies through IServiceProvider.

---

# Constructors

## CS-009

Keep constructors small.

---

## CS-010

Validate constructor arguments immediately.

Prefer

```csharp
ArgumentNullException.ThrowIfNull(service);
```

---

## CS-011

Avoid constructor logic.

---

# Primary Constructors

## CS-012

Do not use primary constructors unless the repository already uses them consistently.

Repository consistency is more important than language features.

---

# Properties

## CS-013

Use auto-properties whenever behaviour is unnecessary.

---

## CS-014

Avoid public setters.

Prefer

```csharp
public string Name { get; private set; }
```

or

```csharp
public string Name { get; }
```

---

## CS-015

Use required when appropriate.

---

# Collections

## CS-016

Prefer collection expressions.

Good

```csharp
List<int> numbers = [];
```

---

## CS-017

Never expose mutable collections.

---

## CS-018

Return IReadOnlyCollection<T> whenever mutation is not intended.

---

# Nullability

## CS-019

Nullable reference types must remain enabled.

Never disable nullable annotations.

---

## CS-020

Do not suppress warnings using !

Fix the cause.

---

# Pattern Matching

## CS-021

Prefer pattern matching over complex if statements.

---

## CS-022

Prefer switch expressions when readability improves.

---

# var

## CS-023

Use var only when the type is obvious.

Good

```csharp
var customer = new Customer();
```

Bad

```csharp
var value = GetSomethingComplicated();
```

---

# Records

## CS-024

Use records only for immutable data carriers.

Never use records for Entities.

---

# Classes

## CS-025

Entities are classes.

Aggregates are classes.

Infrastructure services are classes.

---

# Interfaces

## CS-026

Create interfaces only when abstraction exists.

Do not create interfaces for every class.

---

## CS-027

Application depends on interfaces.

Infrastructure implements interfaces.

---

# Methods

## CS-028

Methods should perform one responsibility.

---

## CS-029

Method names should express intent.

Avoid

```
Process()

Execute()

HandleStuff()

DoWork()
```

Prefer

```
ApproveInvoice()

CalculateTax()

RegisterUser()

AssignRole()
```

---

## CS-030

Avoid boolean parameters.

Bad

```csharp
Save(true);
```

---

# Async

## CS-031

Asynchronous methods end with Async.

---

## CS-032

Avoid async void.

Only event handlers may use async void.

---

## CS-033

Always pass CancellationToken when available.

---

## CS-034

Do not ignore returned Tasks.

---

# Exceptions

## CS-035

Exceptions represent exceptional situations.

Do not use exceptions for business validation.

---

## CS-036

Throw the most specific exception possible.

---

## CS-037

Never swallow exceptions.

---

# LINQ

## CS-038

Use LINQ for readability.

Do not chain excessive LINQ operations.

---

## CS-039

Avoid multiple enumeration.

---

## CS-040

Prefer Any() over Count() > 0.

---

## CS-041

Prefer FirstOrDefault() only when absence is acceptable.

Otherwise use Single() or SingleOrDefault() when uniqueness is required.

---

# Performance

## CS-042

Avoid unnecessary allocations.

---

## CS-043

Avoid reflection unless explicitly required.

---

## CS-044

Avoid boxing.

---

## CS-045

Avoid ToList() unless materialization is required.

---

## CS-046

Prefer foreach over List<T>.ForEach().

---

# Strings

## CS-047

Prefer string interpolation.

Good

```csharp
$"{firstName} {lastName}"
```

Bad

```csharp
string.Format(...)
```

---

# Magic Values

## CS-048

Never embed business constants.

Use named constants or Value Objects.

---

# Comments

## CS-049

Code should explain itself.

Avoid comments that describe obvious code.

---

## CS-050

Explain

WHY

not

WHAT.

---

# Regions

## CS-051

Never use #region.

---

# Partial Classes

## CS-052

Avoid partial classes except when generated code requires them.

---

# XML Documentation

## CS-053

Document public APIs only if repository convention requires XML documentation.

Do not generate redundant XML comments.

---

# Usings

## CS-054

Remove unused using directives.

---

## CS-055

Order using directives consistently.

System namespaces first.

---

# Formatting

## CS-056

One blank line between logical sections.

Avoid excessive whitespace.

---

## CS-057

Avoid deeply nested code.

Return early whenever possible.

---

# Dependency Injection

## CS-058

Never instantiate dependencies manually.

Always rely on dependency injection.

---

# Date & Time

## CS-059

Never use DateTime.Now directly.

Use the repository's time abstraction if one exists.

Otherwise prefer DateTime.UtcNow.

---

# GUIDs

## CS-060

Never call Guid.NewGuid() inside business logic if an ID generation abstraction exists.

---

# Logging

## CS-061

Never log from Domain.

---

## CS-062

Structured logging only.

Never concatenate log messages.

---

# Testing

## CS-063

Code should be testable without modification.

---

## CS-064

Avoid static state.

---

## CS-065

Design for deterministic tests.

---

# AI Generation Checklist

Before presenting generated code, verify:

✓ File-scoped namespace

✓ Nullable enabled

✓ Correct visibility

✓ No mutable public state

✓ No unnecessary allocations

✓ No duplicated logic

✓ No TODOs

✓ No NotImplementedException

✓ No dead code

✓ Modern C# syntax where appropriate

✓ Consistent formatting

✓ Repository naming conventions

Generated code should appear indistinguishable from manually written production code.