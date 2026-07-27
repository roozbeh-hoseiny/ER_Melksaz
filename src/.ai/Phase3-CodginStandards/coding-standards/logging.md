# Logging

Version: 1.0

---

# Purpose

This document defines the mandatory logging standards for the repository.

Logging exists to support:

* Diagnostics
* Monitoring
* Operations
* Incident investigation
* Auditing (where applicable)

Logs are operational data—not business logic.

---

# Primary Principle

Log meaningful events.

Do not log everything.

Every log entry should help someone understand, diagnose, or operate the system.

---

# Structured Logging

Always use structured logging.

Prefer:

```csharp
_logger.LogInformation(
    "Invoice {InvoiceId} approved by {UserId}.",
    invoice.Id,
    user.Id);
```

Avoid:

```csharp
_logger.LogInformation(
    $"Invoice {invoice.Id} approved by {user.Id}");
```

Structured logs enable searching, filtering, and aggregation.

---

# Log Levels

Use log levels consistently.

### Trace

Very detailed diagnostic information.

Only for temporary troubleshooting.

---

### Debug

Developer-focused diagnostic information.

Normally disabled in production.

---

### Information

Important business or application events.

Examples:

* User authenticated.
* Invoice approved.
* Background job started.
* Import completed.

---

### Warning

Unexpected but recoverable situations.

Examples:

* Retry performed.
* Missing optional configuration.
* External service temporarily unavailable.

---

### Error

Operation failed.

Examples:

* Database unavailable.
* External API failure.
* Unexpected exception.

---

### Critical

Application cannot continue safely.

Examples:

* Startup failure.
* Configuration corruption.
* Fatal infrastructure failure.

---

# What to Log

Useful information includes:

* Correlation ID
* Request ID
* Aggregate ID
* Entity ID
* User ID (where appropriate)
* Tenant ID (if applicable)
* Operation name
* Duration
* Retry attempts

---

# What Not to Log

Never log:

* Passwords
* Secrets
* Access tokens
* Refresh tokens
* API keys
* Connection strings
* Encryption keys
* Personally sensitive information unless explicitly required and protected

Sensitive information must never appear in logs.

---

# Exception Logging

Log exceptions once.

Example:

```csharp
_logger.LogError(
    exception,
    "Failed to publish invoice {InvoiceId}.",
    invoice.Id);
```

Avoid logging the same exception at multiple layers.

---

# Domain Layer

The Domain must not perform logging.

Business behaviour should remain independent of operational concerns.

---

# Application Layer

Application services may log:

* Use case execution.
* Significant business operations.
* Long-running workflows.

Avoid excessive logging.

---

# Infrastructure Layer

Infrastructure logs:

* Database failures.
* HTTP requests (where configured).
* Message broker activity.
* External integrations.
* Retry operations.

Infrastructure is responsible for technical diagnostics.

---

# API Layer

API logging should include:

* Request start.
* Request completion.
* Response status.
* Duration.
* Correlation identifiers.

Avoid logging request bodies unless explicitly required.

---

# Correlation IDs

Every request should include a correlation identifier.

The correlation ID should flow through:

* HTTP requests
* gRPC
* Messaging
* Background jobs

This enables end-to-end tracing.

---

# Performance Logging

Long-running operations should include execution duration.

Example:

```csharp
_logger.LogInformation(
    "Import completed in {ElapsedMilliseconds} ms.",
    elapsedMilliseconds);
```

---

# Business Events

Important business events may be logged.

Examples:

* Customer registered.
* Payment received.
* Invoice approved.

Avoid logging every internal method call.

---

# Retry Logging

When retries occur, include:

* Retry attempt
* Delay
* Failure reason
* Target operation

Retry behaviour should be observable.

---

# Startup Logging

Application startup should log:

* Application version
* Environment
* Loaded modules
* Configuration summary (excluding secrets)

---

# Shutdown Logging

Application shutdown should log:

* Shutdown reason (when known)
* Duration
* Outstanding operations (if applicable)

---

# Log Message Style

Messages should:

* Be concise.
* Use present or past tense consistently.
* Include structured properties.
* Avoid unnecessary punctuation.

Good:

```text
Invoice {InvoiceId} approved.
```

Bad:

```text
The invoice approval operation has been successfully completed!!!
```

---

# Logging Libraries

The repository should use the approved logging abstraction.

Application code should depend only on:

```text
ILogger<T>
```

Concrete logging providers belong in Infrastructure.

---

# AI Responsibilities

When generating code, the AI must:

* Use structured logging.
* Choose appropriate log levels.
* Avoid duplicate logging.
* Protect sensitive information.
* Preserve correlation identifiers.
* Follow repository logging conventions.

---

# Anti-Patterns

Avoid:

* String interpolation in log messages.
* Logging sensitive information.
* Logging every method entry.
* Duplicate exception logging.
* Logging business logic from the Domain.
* Excessive Debug logging in production.
* Unstructured log messages.

---

# Logging Checklist

Before completing an implementation, verify:

* Structured logging is used.
* Appropriate log levels are selected.
* Sensitive information is protected.
* Exceptions are logged once.
* Correlation identifiers are preserved.
* The Domain contains no logging.
* Repository conventions are followed.

---

# Guiding Principle

Logs should help operators understand what happened without exposing sensitive information or obscuring important events with unnecessary noise.
