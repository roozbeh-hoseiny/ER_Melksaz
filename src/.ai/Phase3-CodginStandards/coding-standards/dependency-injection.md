# Dependency Injection

Version: 1.0

---

# Purpose

This document defines the mandatory Dependency Injection (DI) rules for the repository.

Dependency Injection is used to make dependencies explicit, preserve architectural boundaries, and improve testability.

It is **not** used as a replacement for good design.

---

# Primary Principle

Objects should receive their dependencies.

They should never locate their own dependencies.

---

# Constructor Injection

Constructor injection is the default and preferred approach.

Example:

```csharp
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

---

# No Service Locator

Never inject:

```csharp
IServiceProvider
```

or

```csharp
IServiceScopeFactory
```

into business classes.

Avoid:

* GetRequiredService()
* GetService()
* Resolve()
* Service Locator patterns

Dependencies must remain explicit.

---

# Explicit Dependencies

Every constructor parameter should represent a genuine dependency.

Do not inject services "just in case."

Unused dependencies must be removed.

---

# One Responsibility

A constructor with many dependencies usually indicates multiple responsibilities.

Recommended:

* 3–5 dependencies.

If significantly more are required, reconsider the class design.

---

# Domain Layer

The Domain must not depend on the DI container.

The Domain should never reference:

* IServiceProvider
* IServiceCollection
* Microsoft.Extensions.DependencyInjection

The Domain is framework-independent.

---

# Application Layer

Application services receive:

* Repositories
* Domain services
* Infrastructure abstractions
* Cross-cutting services

They do not resolve services dynamically.

---

# Infrastructure Layer

Infrastructure registers implementations.

Infrastructure may depend on the DI framework.

Registration belongs here—not in the Domain.

---

# API Layer

The API is responsible for configuring the application's dependency graph.

Registration should remain centralized.

Avoid scattered registration logic.

---

# Service Registration

Prefer extension methods for registration.

Example:

```text
AddApplication()

AddInfrastructure()

AddPersistence()

AddMessaging()
```

Each project should register its own services.

---

# Lifetime Selection

Choose the smallest correct lifetime.

Typical guidance:

* Singleton → Stateless, thread-safe services.
* Scoped → Request or unit-of-work services.
* Transient → Lightweight, short-lived services.

Avoid using Singleton for mutable state.

---

# Repository Lifetime

Repositories should normally be Scoped.

They participate in a single unit of work.

---

# DbContext Lifetime

DbContext should normally be Scoped.

Never register DbContext as Singleton.

---

# Stateless Services

Prefer stateless services whenever possible.

Stateless services are easier to:

* Test.
* Reuse.
* Scale.
* Reason about.

---

# Optional Dependencies

Avoid optional dependencies.

If a dependency is optional, reconsider whether it belongs in the class.

---

# Circular Dependencies

Circular dependencies are forbidden.

If one appears:

* Reconsider responsibilities.
* Extract behaviour.
* Introduce an appropriate abstraction.

Do not work around circular dependencies with IServiceProvider.

---

# Factories

Use factories only when:

* Runtime construction is required.
* Object creation depends on runtime data.
* Construction is genuinely complex.

Factories should not replace normal DI.

---

# Open Generics

Open generic registrations are acceptable when they express a repository-wide pattern.

Example:

```text
IRepository<T>

IValidator<T>

IPipelineBehavior<TRequest, TResponse>
```

Avoid unnecessary generic abstractions.

---

# Static Dependencies

Avoid static service access.

Business code should never rely on globally accessible services.

---

# Configuration

Inject strongly typed configuration objects.

Avoid injecting:

```text
IConfiguration
```

throughout the application.

Bind configuration once.

---

# Testing

Dependency Injection should simplify testing.

Production code should not exist solely to support mocking.

Design for good architecture—not for test frameworks.

---

# AI Responsibilities

When generating code, the AI must:

* Use constructor injection.
* Keep dependencies explicit.
* Choose the correct lifetime.
* Avoid Service Locator.
* Preserve Dependency Inversion.
* Reuse existing registration patterns.

---

# Anti-Patterns

Avoid:

* Service Locator.
* Hidden dependencies.
* Static service access.
* Constructor injection with excessive dependencies.
* Singleton mutable services.
* Manual dependency resolution.
* Circular dependencies.
* Injecting everything by default.

---

# Dependency Injection Checklist

Before completing an implementation, verify:

* Constructor injection is used.
* Dependencies are explicit.
* No IServiceProvider exists in business code.
* Correct service lifetime is chosen.
* Registration follows repository conventions.
* No circular dependencies exist.
* The Dependency Rule is preserved.

---

# Guiding Principle

Dependency Injection exists to make dependencies visible—not invisible.

A class should clearly communicate everything it needs simply by reading its constructor.
