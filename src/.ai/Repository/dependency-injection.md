# Dependency Injection

Version: 1.0

Status: Repository Convention

---

# Purpose

This document defines how dependency injection is implemented throughout the repository.

Dependency Injection is considered part of the architecture.

Every module must follow the same registration strategy.

---

# Design Principles

The repository prefers:

- Explicit registrations
- Centralized registration
- Constructor Injection
- Composition Root
- Stateless services

Avoid service registration scattered throughout the solution.

---

# Composition Root

## Observed Pattern

The repository centralizes service registration.

Observed example:

- ServiceCollectionExtensions

This indicates that modules expose registration through extension methods instead of registering services directly inside application startup code.

---

# Registration Strategy

Every module should expose a single registration entry point.

Example:

```csharp
services.AddApplication();
services.AddInfrastructure();
services.AddApi();
```

Avoid registering dozens of services directly inside Program.cs.

Program.cs should compose modules—not describe implementations.

---

# Constructor Injection

Constructor Injection is the preferred injection mechanism.

Example:

```csharp
public sealed class CustomerService
{
    private readonly IRepository _repository;

    public CustomerService(IRepository repository)
    {
        _repository = repository;
    }
}
```

Avoid:

- Property Injection
- Method Injection
- Service Locator

---

# Service Locator

Never resolve services manually using:

```csharp
IServiceProvider.GetService(...)
```

or

```csharp
IServiceProvider.GetRequiredService(...)
```

inside business code.

Dependencies should be explicit.

---

# Service Lifetimes

The repository should use the smallest lifetime necessary.

General guidance:

Singleton

Only for:

- Stateless services
- Configuration
- Caches
- Factories (when thread-safe)

Scoped

Preferred for:

- Request-based services
- Unit of Work
- DbContext
- Business orchestration

Transient

Use only when a new instance is genuinely required.

Avoid choosing a lifetime without understanding the ownership model.

---

# Stateless Services

Prefer stateless services.

Services should avoid mutable internal state.

This improves:

- Thread safety
- Predictability
- Testability

---

# Registration Location

Each project should register its own services.

Avoid:

Application registering Infrastructure.

Infrastructure registering API.

Each layer owns its registrations.

---

# Extension Methods

## Observed Pattern

The repository uses extension methods for registration.

Examples observed:

- ServiceCollectionExtensions

All registrations for a project should remain together.

---

# Interface Ownership

Interfaces belong to higher layers.

Implementations belong to lower layers.

Example:

Application

↓

IRepository

Infrastructure

↓

SqlRepository

Infrastructure depends on the abstraction—not the reverse.

---

# Open Generic Registration

When multiple implementations follow the same generic pattern, prefer open generic registrations.

Avoid repetitive registrations when a generic registration expresses the same intent.

---

# Assembly Scanning

Recommendation:

If assembly scanning is used, it should follow explicit conventions.

Avoid registering every public class automatically.

Registrations should remain deterministic.

---

# Conditional Registration

Conditional registrations should remain centralized.

Avoid scattered environment checks.

Good:

Development registration

↓

AddDevelopmentInfrastructure()

Avoid:

if (...) inside multiple registration methods.

---

# Configuration Objects

Configuration should use strongly typed options.

Example:

JwtOptions

CacheOptions

MessagingOptions

Do not inject IConfiguration into business services.

Bind configuration once.

Inject typed options.

---

# Logging

Inject:

ILogger<T>

Do not create loggers manually.

Logging is infrastructure.

---

# HTTP Clients

Use:

IHttpClientFactory

Avoid:

new HttpClient()

inside services.

---

# DbContext

DbContext should be injected.

Do not create DbContext manually.

DbContext lifetime should align with the request scope unless explicitly documented otherwise.

---

# Circular Dependencies

Circular dependencies indicate an architectural problem.

Refactor responsibilities rather than introducing lazy resolution.

Avoid:

Lazy<T>

IServiceProvider

Factory delegates

as workarounds for poor architecture.

---

# Optional Dependencies

Avoid optional constructor dependencies.

If a dependency is optional, reconsider the responsibility of the class.

---

# Testing

Constructor Injection improves testing.

Services should be testable by replacing dependencies with fakes or mocks.

No special testing hooks should be required.

---

# AI Instructions

Before registering a new service, verify:

1. Does an existing registration already exist?
2. Which layer owns the service?
3. What lifetime is appropriate?
4. Can an existing abstraction be reused?
5. Does the registration belong inside the project's registration extension?

Never introduce a second registration strategy.

---

# Repository Convention

Observed repository conventions include:

- Centralized registration
- Registration through extension methods
- Constructor Injection
- Explicit dependencies
- Composition Root

Future changes should reinforce these conventions rather than introducing alternative approaches.