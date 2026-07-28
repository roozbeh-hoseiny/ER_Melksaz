# Performance

Version: 1.0

---

# Purpose

This document defines the mandatory performance engineering standards for the repository.

Performance is a quality attribute.

It must be considered during design and implementation without sacrificing correctness, readability, or maintainability.

---

# Primary Principle

Optimize only when there is measurable value.

Correctness comes first.

Readability comes second.

Performance comes third.

Premature optimization is prohibited.

---

# Measure Before Optimizing

Performance improvements must be based on:

* Profiling
* Benchmarks
* Production telemetry
* Metrics
* Traces

Never optimize based on assumptions.

---

# Algorithmic Complexity

Choose efficient algorithms before optimizing implementation details.

Prefer:

* O(1)
* O(log n)
* O(n)

Avoid unnecessary:

* O(n²)
* O(n³)

for large collections.

---

# Memory Allocations

Reduce unnecessary allocations.

Prefer:

* Reusing existing objects where appropriate.
* Avoiding temporary collections.
* Streaming large datasets.

Do not sacrifice readability for trivial allocation savings.

---

# Collections

Choose the correct collection type.

Examples:

* List<T>
* Dictionary<TKey, TValue>
* HashSet<T>
* Queue<T>
* Stack<T>

Avoid using List<T> when lookup performance is the primary requirement.

---

# LINQ

LINQ is encouraged when it improves readability.

Avoid:

* Multiple enumeration.
* Deeply nested LINQ.
* Unnecessary ToList().
* Unnecessary ToArray().

Prefer clear and efficient queries.

---

# Database Performance

Avoid:

* N+1 queries.
* Loading unnecessary columns.
* Loading unnecessary relationships.
* Unbounded queries.

Always request only the required data.

---

# Pagination

Large result sets should use pagination.

Avoid returning entire tables.

Use repository-approved pagination patterns.

---

# Asynchronous I/O

Use asynchronous APIs for:

* Database access.
* File access.
* HTTP communication.
* Messaging.
* Distributed cache.

Do not block asynchronous operations.

---

# Caching

Cache only when:

* Data is expensive to compute.
* Data is frequently read.
* Staleness is acceptable.

Caching must not change business correctness.

---

# Object Lifetime

Prefer short-lived objects.

Avoid retaining unnecessary references.

Release resources promptly.

---

# String Operations

Avoid repeated string concatenation inside loops.

Prefer:

```csharp id="v4q7pk"
StringBuilder
```

for large string construction.

Small concatenations are acceptable.

---

# Exceptions

Exceptions are expensive.

Do not use exceptions for normal control flow.

Use explicit business results for expected outcomes.

---

# Reflection

Avoid reflection in performance-critical paths.

Prefer:

* Source generators.
* Compiled expressions.
* Cached metadata.

Reflection should remain an infrastructure concern.

---

# Serialization

Serialize only required data.

Avoid:

* Large object graphs.
* Circular references.
* Unnecessary properties.

Transport payloads should remain compact.

---

# Network Calls

Minimize network round trips.

Batch operations where appropriate.

Avoid chatty communication between services.

---

# Database Transactions

Keep transactions as short as possible.

Do not perform:

* HTTP calls.
* External messaging.
* Long-running operations.

inside database transactions.

---

# Logging

Avoid excessive logging in hot paths.

Use structured logging.

Do not serialize large objects merely for logging.

---

# Thread Safety

Avoid unnecessary locking.

Shared mutable state should be minimized.

Thread safety should be intentional.

---

# Parallelism

Parallel execution should only be introduced when:

* Work is independent.
* Throughput improves.
* Resource usage remains acceptable.

Parallelism is not automatically faster.

---

# Span and Memory

Use:

* Span<T>
* ReadOnlySpan<T>
* Memory<T>

only when profiling demonstrates measurable benefit.

Do not introduce low-level optimizations prematurely.

---

# ValueTask

Use `ValueTask` only for measured performance improvements.

Default to `Task`.

---

# Dependency Injection

Avoid resolving services repeatedly.

Dependencies should be injected once.

---

# Resource Disposal

Dispose resources promptly.

Prefer:

```csharp id="r2n9wa"
using

await using
```

when appropriate.

---

# Benchmarks

Use benchmarking for performance-critical code.

Do not rely on intuition.

---

# AI Responsibilities

When generating code, the AI must:

* Prefer simple implementations first.
* Avoid premature optimization.
* Use asynchronous I/O correctly.
* Prevent unnecessary allocations.
* Avoid N+1 queries.
* Preserve readability.
* Follow repository performance conventions.

---

# Anti-Patterns

Avoid:

* Premature optimization.
* Blocking async code.
* Reflection in hot paths.
* N+1 queries.
* Excessive allocations.
* Unbounded queries.
* Large object graphs.
* Logging inside tight loops.
* Long-running database transactions.

---

# Performance Checklist

Before completing an implementation, verify:

* Performance assumptions are measurable.
* No N+1 queries exist.
* Async I/O is used correctly.
* Unnecessary allocations are avoided.
* Transactions remain short.
* Logging is appropriate.
* Readability has not been sacrificed unnecessarily.
* Repository conventions are followed.

---

# Guiding Principle

Performance should be engineered through good design, measurement, and efficient algorithms—not through premature micro-optimizations.
