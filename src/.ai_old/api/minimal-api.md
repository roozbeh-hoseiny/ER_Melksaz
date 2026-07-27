# Minimal API Design Rules

Version: 1.0

This document defines how HTTP endpoints are implemented throughout the repository.

The repository uses ASP.NET Core Minimal APIs.

Endpoints expose Application use cases.

Endpoints are adapters.

They never contain business logic.

---

# Architecture

```
HTTP

↓

Minimal API Endpoint

↓

Application Command / Query

↓

Handler

↓

Domain

↓

Infrastructure
```

The endpoint adapts HTTP into an Application request.

Nothing more.

---

# Endpoint Responsibilities

Endpoints are responsible for

✓ Route definition

✓ Request binding

✓ Authorization

✓ Calling the Application layer

✓ Returning HTTP responses

Endpoints are NOT responsible for

✗ Business Rules

✗ Transactions

✗ Validation

✗ Logging

✗ Mapping Domain state

✗ Persistence

---

# General Rules

## API-001

One endpoint represents one use case.

---

## API-002

Endpoints are thin.

Target

5–20 lines.

---

## API-003

Business logic never belongs inside an endpoint.

---

## API-004

Endpoints call one Application request.

---

## API-005

Endpoints should contain no loops implementing business workflows.

---

# Routing

## ROUTE-001

Routes represent business capabilities.

Bad

```
/api/process

/api/execute

/api/data
```

Good

```
/api/invoices

/api/invoices/{id}

/api/customers

/api/orders
```

---

## ROUTE-002

Use plural resource names.

---

## ROUTE-003

Avoid verbs unless representing actions.

Examples

```
POST /invoices

POST /invoices/{id}/cancel

POST /orders/{id}/approve
```

---

## ROUTE-004

Route names should remain stable.

---

# Request Models

## REQ-001

Endpoints receive transport models only.

---

## REQ-002

Transport models map into Commands or Queries.

---

## REQ-003

Never expose Domain Entities as request models.

---

## REQ-004

Never expose EF entities.

---

# Responses

## RES-001

Endpoints return transport models.

Never return Domain Entities.

---

## RES-002

Success responses use the repository Result abstraction.

---

## RES-003

Errors are mapped consistently.

---

## RES-004

HTTP status codes are determined by the Result mapping policy.

---

# Validation

## VALID-001

Validation occurs before Handler execution.

---

## VALID-002

Endpoints should not manually validate business rules.

---

## VALID-003

Use the repository validation pipeline.

---

# Authorization

## AUTH-001

Authorization belongs to the endpoint or pipeline.

---

## AUTH-002

Business authorization belongs in the Domain when required.

---

# Dependency Injection

## DI-001

Inject only required services.

---

## DI-002

Prefer mediator/dispatcher abstractions over injecting many services.

---

## DI-003

Never resolve services manually.

Forbidden

```csharp
app.Services.GetRequiredService(...)
```

inside endpoint handlers.

---

# Cancellation

## CANCEL-001

Every asynchronous endpoint accepts CancellationToken.

---

## CANCEL-002

Pass CancellationToken through every layer.

---

# OpenAPI

## DOC-001

Every endpoint has

- Summary

- Description

- Tags

- Response types

where repository conventions support them.

---

## DOC-002

Operation names are stable.

---

# Versioning

## VER-001

Follow the repository versioning strategy.

Never invent a different approach.

---

# Idempotency

## IDEMP-001

PUT should be idempotent.

---

## IDEMP-002

DELETE should be idempotent where business rules allow.

---

## IDEMP-003

POST commands that may be retried should support idempotency when required by the business.

---

# Logging

## LOG-001

Endpoints do not log business events.

---

## LOG-002

Request logging belongs to middleware.

---

# Error Handling

## ERR-001

Endpoints never catch exceptions merely to return HTTP responses.

---

## ERR-002

Global exception handling maps unexpected failures.

---

## Performance

## PERF-001

Endpoints should allocate as little as practical.

---

## PERF-002

Never perform multiple Application requests inside one endpoint unless the use case explicitly requires orchestration.

---

# Folder Structure

Recommended

```
Api

└── Invoices

    CreateInvoiceEndpoint.cs

    CancelInvoiceEndpoint.cs

    GetInvoiceEndpoint.cs

    SearchInvoicesEndpoint.cs
```

Group endpoints by feature.

Never by HTTP verb.

---

# Anti-Patterns

Never

- inject DbContext
- inject repositories directly
- implement business rules
- call SaveChanges()
- return Domain Entities
- expose EF models
- manually create scopes
- use Service Locator
- duplicate validation logic
- bypass Application

---

# AI Generation Rules

Whenever generating an endpoint automatically generate

✓ Route

✓ Request model

✓ Command or Query invocation

✓ OpenAPI metadata

✓ Authorization

✓ Result mapping

✓ CancellationToken support

✓ Endpoint tests (if repository convention includes them)

---

# AI Verification Checklist

Before presenting endpoint code verify

✓ Thin endpoint

✓ One use case

✓ No business logic

✓ Uses Application layer

✓ Uses transport models

✓ Returns Result mapping

✓ Supports CancellationToken

✓ Uses repository routing conventions

✓ No infrastructure leakage

✓ No DbContext dependency

Endpoints should be boring.

If an endpoint becomes interesting, business logic has leaked into the wrong layer.