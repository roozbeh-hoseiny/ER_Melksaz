# Asynchronous Programming

Version: 1.0

---

# Purpose

This document defines the mandatory asynchronous programming rules for the repository.

Correct asynchronous programming improves scalability, responsiveness, resource utilization, and reliability.

Every engineer and every AI agent must follow these conventions.

---

# Primary Principle

Use asynchronous programming for I/O-bound work.

Do not use async where synchronous execution is more appropriate.

---

# Async by Default

Operations involving:

* Database access
* HTTP communication
* File I/O
* Message brokers
* Distributed caches
* External services

must use asynchronous APIs.

---

# CPU-Bound Work

Do not make CPU-bound methods asynchronous unless they truly execute asynchronously.

Avoid:

```csharp
public async Task<int> CalculateAsync()
{
    return value1 + value2;
}
```

Use:

```csharp
public int Calculate()
{
    return value1 + value2;
}
```

---

# Naming

Every asynchronous method must end with:

```text
Async
```

Examples:

```text
SaveAsync()

PublishAsync()

LoadAsync()

ExistsAsync()
```

---

# Return Types

Prefer:

```text
Task

Task<T>

ValueTask

ValueTask<T>
```

Never use:

```text
async void
```

except for UI event handlers.

---

# CancellationToken

Every asynchronous operation that may be cancelled should accept:

```csharp
CancellationToken cancellationToken
```

The CancellationToken must always be the final parameter.

Example:

```csharp
Task SaveAsync(
    Invoice invoice,
    CancellationToken cancellationToken);
```

---

# Propagate Cancellation

Pass the CancellationToken to every downstream async call whenever possible.

Do not ignore cancellation.

---

# ConfigureAwait

ASP.NET Core applications should not use:

```text
ConfigureAwait(false)
```

unless repository conventions explicitly require it for shared libraries.

---

# Avoid Blocking

Never block asynchronous code.

Avoid:

```csharp
.Result

.Wait()

GetAwaiter().GetResult()
```

Blocking can cause:

* Deadlocks
* Thread starvation
* Reduced scalability

---

# Await Immediately

Await asynchronous operations as soon as practical.

Avoid storing incomplete tasks unless concurrent execution is intentional.

---

# Parallelism

Use parallel execution only when:

* Operations are independent.
* Ordering does not matter.
* Resource consumption is acceptable.

Example:

```csharp
await Task.WhenAll(task1, task2);
```

Avoid unnecessary parallelism.

---

# Fire-and-Forget

Avoid fire-and-forget tasks.

Every asynchronous operation should be:

* awaited, or
* intentionally scheduled through an approved background processing mechanism.

---

# Background Work

Long-running background work belongs in:

* Hosted Services
* Background workers
* Message consumers
* Job schedulers

Never execute background work from request handlers without proper infrastructure.

---

# ValueTask

Use `ValueTask` only when:

* Performance measurements justify it.
* The operation frequently completes synchronously.

Otherwise prefer `Task`.

---

# Exception Handling

Exceptions should naturally flow through asynchronous calls.

Do not suppress exceptions inside async methods.

---

# Async Streams

Use:

```text
IAsyncEnumerable<T>
```

only for genuine streaming scenarios.

Avoid using async streams for small collections.

---

# Resource Disposal

Use asynchronous disposal when available.

Example:

```csharp
await using var stream = ...;
```

---

# Timeouts

Prefer explicit cancellation and timeout policies over manually cancelling tasks.

Timeout behaviour should be consistent across the repository.

---

# Repository Layer

Repository methods performing I/O should always be asynchronous.

Avoid synchronous database APIs.

---

# Application Layer

Handlers should remain asynchronous from entry to exit.

Do not introduce synchronous bottlenecks.

---

# Domain Layer

Domain logic should remain synchronous.

Business rules rarely require asynchronous execution.

The Domain should not depend on asynchronous infrastructure.

---

# Logging

Log asynchronous failures normally.

Do not swallow exceptions simply because execution is asynchronous.

---

# AI Responsibilities

When generating asynchronous code, the AI must:

* Use async only where appropriate.
* Append the `Async` suffix.
* Accept CancellationToken where applicable.
* Propagate cancellation.
* Avoid blocking.
* Avoid fire-and-forget.
* Follow repository conventions.

---

# Anti-Patterns

Avoid:

* async void
* Task.Run() inside ASP.NET request handling
* .Result
* .Wait()
* Ignored CancellationTokens
* Fire-and-forget tasks
* Unnecessary ValueTask usage
* Fake asynchronous methods

---

# Async Checklist

Before completing an implementation, verify:

* Async methods end with `Async`.
* CancellationToken is accepted where appropriate.
* Cancellation is propagated.
* No blocking calls exist.
* Fire-and-forget has been avoided.
* Exceptions are preserved.
* Repository conventions have been followed.

---

# Guiding Principle

Asynchronous programming exists to improve scalability—not to make every method asynchronous.

Use async intentionally, propagate cancellation correctly, and never block asynchronous execution.
