# Dependency Injection

Version: 1.0

---

# Purpose

This document defines the dependency injection principles used throughout the repository.

Dependency Injection (DI) enables loose coupling, testability, maintainability, and clear dependency management.

Dependencies should be explicit, predictable, and resolved by the application's composition root.

---

# Objectives

Dependency Injection exists to:

* Reduce coupling.
* Improve testability.
* Separate contracts from implementations.
* Make dependencies explicit.
* Centralise object composition.
* Preserve architectural boundaries.

---

# Core Principle

Classes should declare what they need.

They should never decide how those dependencies are created.

Object creation belongs to the composition root.

---

# Constructor Injection

Constructor injection is the default and preferred injection mechanism.

Every required dependency should be provided through the constructor.

Example:

```csharp
public sealed class CreateInvoiceCommandHandler
{
    public CreateInvoiceCommandHandler(
        IInvoiceRepository repository,
        IUnitOfWork unitOfWork)
    {
    }
}
```

---

# Explicit Dependencies

Every dependency should appear in the constructor.

Hidden dependencies are prohibited.

Consumers should immediately understand what a class requires.

---

# Required Dependencies

Constructor parameters represent required dependencies.

Avoid optional dependencies.

If a dependency is optional, reconsider the design.

---

# Composition Root

Object composition should occur only at the application's composition root.

Examples include:

* Application startup
* Host configuration
* Dependency registration

Business code must never construct its own dependencies.

---

# Service Locator

The Service Locator pattern is prohibited.

Avoid:

* IServiceProvider
* GetService()
* GetRequiredService()
* Manual service resolution

outside the composition root.

Dependencies should always be explicit.

---

# Property Injection

Property injection is prohibited.

Required dependencies must never be assigned after object construction.

---

# Method Injection

Method injection should only be used when a dependency is required exclusively for a single operation.

It must not replace constructor injection for normal dependencies.

---

# Static Dependencies

Avoid static service access.

Examples:

* Static repositories
* Static loggers
* Static current user providers
* Static configuration providers

Static dependencies reduce testability.

---

# Lifetime Selection

Choose the narrowest lifetime that satisfies the component's responsibility.

Every lifetime decision should be intentional.

Avoid unnecessarily long-lived objects.

---

# Dependency Count

Constructors should remain reasonably small.

An excessive number of dependencies usually indicates that a class has multiple responsibilities.

Prefer refactoring over accepting large constructors.

---

# Interface Ownership

Interfaces belong to the layer that consumes them.

Implementations belong to outer layers.

Example:

Application defines:

```text
IInvoiceRepository
```

Infrastructure implements:

```text
InvoiceRepository
```

---

# Registration

Dependency registration should remain:

* Centralised
* Predictable
* Consistent

Avoid scattered registration logic throughout the solution.

---

# Conditional Registration

Conditional registrations should be rare.

When required, document the reason clearly.

Avoid runtime complexity where simpler designs exist.

---

# Open Generics

Open generic registrations should only be used when they provide clear architectural value.

Avoid introducing generic abstractions solely to reduce code volume.

---

# Business Logic

Business objects should never resolve services dynamically.

The Domain should remain completely independent of the DI container.

---

# Infrastructure

Infrastructure implementations should be registered behind abstractions.

Consumers should never depend directly on infrastructure implementations.

---

# Testing

Dependency Injection should make testing straightforward.

Tests should easily replace infrastructure implementations with test doubles where appropriate.

No production code should require modification to support testing.

---

# Circular Dependencies

Circular dependencies are prohibited.

If two services depend on each other, redesign the architecture.

Circular dependencies indicate incorrect responsibility allocation.

---

# Disposable Services

Object lifetime management belongs to the DI container.

Consumers should not manually dispose injected dependencies unless ownership has been explicitly transferred.

---

# Configuration

Configuration should be injected through dedicated configuration abstractions.

Business components should not read configuration directly.

---

# Anti-Patterns

Avoid:

* Service Locator.
* Property Injection.
* Static service access.
* Manual dependency construction.
* Hidden dependencies.
* Excessive constructor parameters.
* Circular dependencies.
* Infrastructure dependencies inside the Domain.

---

# Dependency Injection Review Checklist

Before completing an implementation, verify:

* Are all dependencies explicit?
* Is constructor injection used?
* Is the composition root the only place where objects are composed?
* Are abstractions preferred over implementations?
* Does the Domain remain DI-container independent?
* Are lifetimes appropriate?
* Are circular dependencies avoided?

---

# Guiding Principle

Classes should describe the services they require—not how those services are created.

Object composition belongs to the composition root, while business behaviour belongs to the application itself.
