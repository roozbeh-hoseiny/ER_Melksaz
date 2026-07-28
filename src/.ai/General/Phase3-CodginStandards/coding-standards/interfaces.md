# Interfaces

Version: 1.0

---

# Purpose

This document defines the mandatory rules for designing interfaces throughout the repository.

Interfaces define contracts—not implementations.

A well-designed interface communicates capabilities while hiding implementation details.

---

# Primary Principle

Create an interface only when multiple implementations or architectural boundaries justify it.

Do not create interfaces automatically.

---

# When to Create an Interface

Interfaces are appropriate when they represent:

* Architectural boundaries.
* Module boundaries.
* Infrastructure abstractions.
* External dependencies.
* Stable contracts.

Examples include:

* Repositories
* Application Services
* Domain Services (when multiple implementations exist)
* External API clients
* Message publishers
* Time providers

---

# When Not to Create an Interface

Avoid interfaces for:

* Simple entities.
* Value Objects.
* Aggregates.
* Validators.
* Commands.
* Queries.
* DTOs.
* Static utility classes.

Do not introduce interfaces purely for testing.

---

# Naming

Interface names begin with `I`.

Examples:

```text id="j7v4xm"
IInvoiceRepository

IClock

IEmailSender

IEventPublisher
```

Use meaningful business names.

---

# Single Responsibility

An interface should represent one capability.

Avoid interfaces with unrelated responsibilities.

Good:

```text id="g5m2fr"
IClock

INotificationSender

IPaymentGateway
```

Bad:

```text id="s9q4ka"
ISystemManager

ICommonService

IUtility
```

---

# Small Contracts

Interfaces should remain small.

Large interfaces usually indicate multiple responsibilities.

Prefer several focused interfaces over one large interface.

---

# Behaviour-Oriented

Interfaces should describe behaviour rather than implementation.

Good:

```text id="x2b6pk"
PublishAsync()

ExistsAsync()

Generate()

SendAsync()
```

Avoid implementation-specific methods.

---

# Repository Interfaces

Repositories belong to the Domain.

They expose Aggregate operations.

Example:

```text id="a1z8cf"
IInvoiceRepository
```

Repositories should not expose:

* IQueryable
* EF Core types
* SQL concepts

---

# Application Interfaces

Application interfaces define use-case level capabilities.

They coordinate work but do not expose infrastructure details.

---

# Infrastructure Interfaces

Infrastructure implements interfaces defined by inner layers.

Infrastructure should rarely define interfaces consumed by the Domain.

---

# Dependency Injection

Interfaces should exist only when they participate in dependency inversion.

Do not create interfaces solely because dependency injection is used.

---

# Generic Interfaces

Use generics only when they communicate meaningful abstractions.

Good:

```text id="c8h4ly"
IRepository<TEntity>

IValidator<T>

IRequestHandler<TRequest, TResponse>
```

Avoid generic interfaces that hide business meaning.

---

# Interface Size

Prefer interfaces with a limited number of members.

If an interface becomes large:

* Split responsibilities.
* Apply interface segregation.

---

# Default Implementations

Avoid default interface implementations for business logic.

Business behaviour belongs in concrete classes.

---

# Asynchronous Methods

Async members must end with `Async`.

Examples:

```text id="v6r9td"
LoadAsync()

SaveAsync()

PublishAsync()
```

---

# Exceptions

Interfaces define expected behaviour.

They should not document implementation-specific exceptions.

---

# Documentation

Public interfaces should clearly communicate their purpose through:

* Meaningful names.
* Small contracts.
* Consistent method naming.

XML documentation may be added for public APIs when repository conventions require it.

---

# Testing

Mock interfaces only when they represent external collaborators.

Avoid mocking Domain objects.

Prefer testing real business behaviour whenever practical.

---

# Anti-Patterns

Avoid:

* One interface per class.
* Marker interfaces without clear architectural purpose.
* Large "god" interfaces.
* Infrastructure-specific contracts in the Domain.
* Generic helper interfaces.
* Creating interfaces solely for future possibilities.

---

# Interface Checklist

Before creating an interface, verify:

* It represents a stable abstraction.
* It has one responsibility.
* It supports dependency inversion.
* Its methods describe behaviour.
* Its name reflects business intent.
* It is not created solely for testing.
* Existing interfaces cannot satisfy the requirement.

---

# Guiding Principle

Interfaces should express architectural boundaries and stable capabilities—not implementation details.

Every interface should justify its existence by improving flexibility, decoupling, or architectural clarity.
