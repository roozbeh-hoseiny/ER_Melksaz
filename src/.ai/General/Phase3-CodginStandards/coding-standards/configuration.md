# Configuration

Version: 1.0

---

# Purpose

This document defines the mandatory configuration standards for the repository.

Configuration provides application behaviour without modifying source code.

Configuration is infrastructure—not business logic.

---

# Primary Principle

Business logic must never depend directly on configuration providers.

Configuration should be read once, validated, and injected as strongly typed objects.

---

# Strongly Typed Configuration

Always bind configuration into strongly typed options.

Prefer:

```csharp
public sealed class RedisOptions
{
    public required string ConnectionString { get; init; }

    public required string InstanceName { get; init; }
}
```

Avoid reading configuration values throughout the codebase.

---

# Options Pattern

Use the .NET Options Pattern.

Preferred registrations:

```csharp
builder.Services
    .AddOptions<RedisOptions>()
    .BindConfiguration("Redis")
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

Configuration should be validated during application startup.

---

# IConfiguration Usage

`IConfiguration` should only be used during composition.

Allowed locations:

* Program.cs
* Startup configuration
* Infrastructure registration

Avoid injecting:

```text
IConfiguration
```

into business services.

---

# Configuration Ownership

Each module owns its own configuration.

Example:

```text
IdentityOptions

RabbitMqOptions

JwtOptions

GrpcOptions

SqlServerOptions
```

Avoid large global configuration classes.

---

# Validation

Configuration must be validated.

Startup should fail fast when required configuration is missing or invalid.

Never allow an application to start with invalid configuration.

---

# Secrets

Secrets must never be stored in source control.

Examples:

* Passwords
* API Keys
* Client Secrets
* Certificates
* Connection Strings (production)

Use the approved secret management solution for each environment.

---

# Environment-Specific Configuration

Configuration may vary by environment.

Typical environments:

* Development
* Test
* Staging
* Production

Business behaviour should remain consistent across environments.

---

# Default Values

Avoid hidden default values.

If a value is required:

* Make it required.
* Validate it.

Use defaults only when they represent safe and intentional behaviour.

---

# Feature Flags

Feature flags belong in configuration.

Business code should depend on abstractions rather than directly reading feature flag providers.

---

# Domain Layer

The Domain must never depend on configuration.

Business rules should receive required values through constructors or method parameters.

---

# Application Layer

The Application Layer may receive strongly typed options through abstractions when configuration influences orchestration.

It should never parse configuration.

---

# Infrastructure Layer

Infrastructure owns:

* Configuration binding
* Configuration validation
* Provider integration
* Secret retrieval

---

# API Layer

The API configures options during startup.

Endpoints should not read configuration directly.

---

# Reloadable Configuration

Use reloadable configuration only when the application genuinely benefits from runtime updates.

Most business configuration should remain stable during execution.

---

# Connection Strings

Connection strings belong in Infrastructure.

Repositories should receive already-configured dependencies.

They should never build connection strings.

---

# Logging Configuration

Logging providers should be configured centrally.

Application code should never configure logging behaviour.

---

# Serialization Settings

Serialization configuration belongs in the API or Infrastructure layer.

Business objects should not contain serializer-specific behaviour unless explicitly required.

---

# AI Responsibilities

When generating code, the AI must:

* Use strongly typed options.
* Validate configuration at startup.
* Avoid injecting IConfiguration into business code.
* Keep secrets out of source code.
* Follow repository configuration conventions.

---

# Anti-Patterns

Avoid:

* Magic configuration keys.
* Injecting IConfiguration everywhere.
* Reading configuration inside Domain objects.
* Hard-coded secrets.
* Hidden default values.
* Building connection strings inside repositories.
* Scattered configuration binding.

---

# Configuration Checklist

Before completing an implementation, verify:

* Strongly typed options are used.
* Configuration is validated.
* Secrets are not hard-coded.
* IConfiguration is used only during composition.
* Business code remains configuration-independent.
* Repository conventions are followed.

---

# Guiding Principle

Configuration defines how the application is deployed—not how the business behaves.

Business code should receive the values it needs, never the mechanism used to retrieve them.
