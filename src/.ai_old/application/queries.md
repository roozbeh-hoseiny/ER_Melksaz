# Query Design Rules

Version: 1.0

This document defines how Queries are designed and implemented throughout the repository.

Queries retrieve information.

Queries never change business state.

Queries are optimized for reading.

Queries should be independent from write models whenever practical.

---

# Definition

A Query

- requests information
- does not modify state
- has one handler
- returns data
- has no side effects

Examples

Good

GetInvoice

GetCustomer

SearchInvoices

GetInvoiceDetails

ListProducts

FindOrders

Bad

ProcessInvoices

Execute

LoadData

Run

InvoiceQuery

---

# Philosophy

Commands express

"What should happen?"

Queries answer

"What is true?"

These responsibilities must never be mixed.

---

# Query Rules

## QUERY-001

Queries never modify state.

---

## QUERY-002

Queries should be side-effect free.

---

## QUERY-003

Queries should be deterministic.

The same input should produce the same output unless underlying data changes.

---

## QUERY-004

Queries must never publish Domain Events.

---

## QUERY-005

Queries must never execute business workflows.

---

## QUERY-006

Queries never call Aggregate methods that change state.

---

## QUERY-007

Queries may use specialized read models.

They are not required to use Aggregate Roots.

---

## QUERY-008

Queries may bypass Aggregates when reading.

Business invariants are enforced during writes.

---

## QUERY-009

Queries should return DTOs.

Never return Entities.

Never return EF entities.

---

## QUERY-010

Return only the data required.

Avoid returning large object graphs.

---

# Query Handler

Each Query has exactly one Handler.

---

## HANDLER-001

One Query.

One Handler.

---

## HANDLER-002

Handlers coordinate data retrieval.

---

## HANDLER-003

Handlers contain no business rules.

---

## HANDLER-004

Handlers should remain small.

Target

20–100 lines.

---

## HANDLER-005

Handlers should express application flow.

Retrieve

↓

Map

↓

Return

---

## HANDLER-006

Avoid unnecessary intermediate objects.

---

## HANDLER-007

Prefer projection over materialization.

Good

Select()

Bad

Load Entity

↓

Map

↓

Return

when direct projection is possible.

---

# CQRS

## CQRS-001

Read models may differ from write models.

---

## CQRS-002

Queries should optimize for read performance.

---

## CQRS-003

Never force Query models to match Aggregate models.

---

## CQRS-004

Denormalized projections are acceptable.

---

# DTO Rules

## DTO-001

DTOs are immutable whenever practical.

---

## DTO-002

DTOs contain no behaviour.

---

## DTO-003

DTOs never contain business logic.

---

## DTO-004

DTOs may flatten object graphs.

---

## DTO-005

DTOs belong to the Application layer.

---

# Mapping

## MAP-001

Project directly into DTOs whenever possible.

---

## MAP-002

Avoid loading unnecessary navigation properties.

---

## MAP-003

Avoid mapping entire Aggregates for read-only scenarios.

---

# Repositories

## REPO-001

Query handlers may use dedicated read repositories.

---

## REPO-002

Read repositories may differ from write repositories.

---

## REPO-003

Query handlers should avoid unnecessary transactions.

---

# Database

## DB-001

Use AsNoTracking() unless tracking is required.

---

## DB-002

Select only required columns.

---

## DB-003

Avoid SELECT * behaviour.

---

## DB-004

Avoid N+1 queries.

---

## DB-005

Prefer pagination.

Never return unlimited collections.

---

# Pagination

## PAGE-001

Collections should support paging.

---

## PAGE-002

Sorting should be explicit.

---

## PAGE-003

Filtering belongs in Queries.

---

# Performance

## PERF-001

Optimize for latency.

---

## PERF-002

Avoid unnecessary allocations.

---

## PERF-003

Prefer streaming for very large datasets when supported by repository conventions.

---

## PERF-004

Never materialize data that will immediately be discarded.

---

# Caching

## CACHE-001

Queries may be cached.

---

## CACHE-002

Caching is transparent to Query logic.

---

## CACHE-003

Handlers should not implement caching directly.

Cross-cutting infrastructure should.

---

# Security

## AUTH-001

Authorization occurs before Query execution.

---

## AUTH-002

Queries should return only data the caller is allowed to see.

---

# Logging

## LOG-001

Avoid excessive logging.

Cross-cutting logging belongs in pipelines.

---

# Anti-Patterns

Never

- modify Entities
- call SaveChanges()
- publish Domain Events
- return EF entities
- return DbContext objects
- expose Infrastructure models
- return entire Aggregates unnecessarily
- execute business workflows
- mix reads and writes
- load more data than required

---

# AI Generation Rules

Whenever generating a Query automatically generate

✓ Query

✓ Handler

✓ DTO

✓ Validator (when input validation is required)

✓ Endpoint

✓ Unit Tests

✓ Integration Tests

✓ Mapping

✓ Documentation (if repository convention requires it)

---

# AI Verification Checklist

Before presenting generated code verify

✓ Query has one responsibility

✓ One handler exists

✓ No state modification

✓ No Domain Events

✓ No SaveChanges()

✓ Returns DTOs

✓ Uses projection

✓ Supports paging where appropriate

✓ Uses AsNoTracking() when applicable

✓ No unnecessary allocations

✓ Supports CancellationToken

✓ Follows repository naming conventions

The generated Query should retrieve information efficiently while remaining completely independent from business state changes.