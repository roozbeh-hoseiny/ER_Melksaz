# Layer Rules

Version: 1.0

This document defines the responsibilities, dependencies, and constraints of every architectural layer in the repository.

These rules are mandatory.

---

# Architecture Principles

The repository follows a strict dependency model.

```
Presentation (Api)
        │
        ▼
Application
        │
        ▼
Domain

Infrastructure
    │
    ├────────────► Application
    │
    └────────────► Domain
```

Dependencies always point toward business logic.

Business logic never depends on implementation details.

---

# Rule Format

Every rule contains

- ID
- Description
- Allowed
- Forbidden

---

# Domain Layer

The Domain layer represents business knowledge.

It contains no implementation details.

---

## DOMAIN-001

The Domain layer is the center of the architecture.

Everything depends on Domain.

Domain depends on nothing.

---

## DOMAIN-002

Domain must never reference

- Infrastructure
- Api
- Presentation
- Persistence
- EF Core
- ASP.NET Core
- Logging Frameworks
- Serialization Libraries

---

## DOMAIN-003

Domain contains only

- Aggregates
- Entities
- Value Objects
- Domain Services
- Specifications
- Business Rules
- Domain Events
- Enumerations

---

## DOMAIN-004

Domain objects must model business concepts.

Never create an entity simply because a database table exists.

---

## DOMAIN-005

Every Aggregate represents a consistency boundary.

---

## DOMAIN-006

Every Aggregate protects its own invariants.

---

## DOMAIN-007

Only Aggregate Roots may be loaded directly from repositories.

---

## DOMAIN-008

Entities inside an Aggregate are never loaded independently.

---

## DOMAIN-009

Value Objects must be immutable.

---

## DOMAIN-010

Business rules belong inside Domain.

Never inside Application.

---

## DOMAIN-011

Business validation belongs inside Domain.

---

## DOMAIN-012

Infrastructure validation belongs outside Domain.

---

## DOMAIN-013

Domain must not know SQL.

---

## DOMAIN-014

Domain must not know HTTP.

---

## DOMAIN-015

Domain must not know JSON.

---

## DOMAIN-016

Domain must not know caching.

---

## DOMAIN-017

Domain must not know authentication.

---

## DOMAIN-018

Domain must not know dependency injection.

---

## DOMAIN-019

Domain constructors should preserve invariants.

---

## DOMAIN-020

Invalid domain objects must never exist.

---

## DOMAIN-021

Never expose mutable collections.

Return read-only collections.

---

## DOMAIN-022

Collection modifications happen through methods.

Never expose setters.

---

## DOMAIN-023

Use methods that express business intent.

Prefer

Approve()

Reject()

Pay()

Cancel()

Instead of

SetStatus()

---

## DOMAIN-024

Business methods should read like business language.

---

## DOMAIN-025

Primitive Obsession is forbidden.

Replace primitive identifiers with Value Objects whenever practical.

---

# Application Layer

The Application layer coordinates use cases.

It contains workflows.

It does not contain business rules.

---

## APP-001

Application depends only on Domain.

---

## APP-002

Application never depends on Api.

---

## APP-003

Application never depends on EF Core.

---

## APP-004

Application communicates with Infrastructure through interfaces.

---

## APP-005

Application coordinates business objects.

It does not replace them.

---

## APP-006

Commands change state.

---

## APP-007

Queries never change state.

---

## APP-008

Handlers should remain thin.

---

## APP-009

Business rules belong in Domain.

---

## APP-010

Application validates requests before executing business logic.

---

## APP-011

Application maps external DTOs into Domain objects.

---

## APP-012

Repositories are abstractions.

Never implementations.

---

## APP-013

Application orchestrates transactions.

---

## APP-014

Application should be deterministic.

---

## APP-015

Application should not perform infrastructure work.

---

# Infrastructure Layer

Infrastructure provides implementations.

---

## INFRA-001

Infrastructure depends on Application.

---

## INFRA-002

Infrastructure may depend on Domain.

---

## INFRA-003

Infrastructure implements interfaces.

---

## INFRA-004

Infrastructure contains

- EF Core
- Messaging
- File Storage
- Email
- External APIs
- Authentication Providers
- Cache
- Logging Providers

---

## INFRA-005

Infrastructure should never contain business rules.

---

## INFRA-006

Repository implementations belong here.

---

## INFRA-007

DbContext belongs here.

---

## INFRA-008

External SDKs belong here.

---

## INFRA-009

Configuration belongs here.

---

## INFRA-010

Infrastructure hides implementation details.

---

# Api Layer

The API exposes use cases.

Nothing more.

---

## API-001

API depends on Application.

---

## API-002

API never accesses repositories directly.

---

## API-003

API never accesses DbContext directly.

---

## API-004

API contains no business rules.

---

## API-005

Endpoints are orchestration only.

---

## API-006

Authentication belongs here.

---

## API-007

Authorization belongs here.

---

## API-008

Model binding belongs here.

---

## API-009

HTTP mapping belongs here.

---

## API-010

Swagger configuration belongs here.

---

# Cross-Layer Rules

## ARCH-001

Every dependency points toward Domain.

---

## ARCH-002

Business rules never leak outside Domain.

---

## ARCH-003

Infrastructure implements abstractions.

---

## ARCH-004

API orchestrates use cases.

---

## ARCH-005

Application coordinates.

---

## ARCH-006

Domain decides.

---

## ARCH-007

Infrastructure executes.

---

## ARCH-008

No layer may bypass another layer.

---

## ARCH-009

No circular references.

---

## ARCH-010

A feature should span all layers while preserving dependency direction.

---

# Decision Matrix

| Concern | Domain | Application | Infrastructure | API |
|---------|--------|-------------|---------------|-----|
| Business Rules | ✅ | ❌ | ❌ | ❌ |
| Validation (Business) | ✅ | ⚠️ Request only | ❌ | ❌ |
| Validation (Request) | ❌ | ✅ | ❌ | ✅ |
| EF Core | ❌ | ❌ | ✅ | ❌ |
| HTTP | ❌ | ❌ | ❌ | ✅ |
| Logging | ❌ | ⚠️ Through abstraction | ✅ | ✅ |
| Caching | ❌ | ❌ | ✅ | ❌ |
| Serialization | ❌ | ❌ | ✅ | ✅ |
| Authentication | ❌ | ❌ | ✅ | ✅ |
| Authorization | ❌ | ⚠️ Policy abstraction | ⚠️ | ✅ |
| Transactions | ❌ | ✅ | ✅ | ❌ |

---

# AI Verification Checklist

Before generating code, verify:

✓ Correct project

✓ Correct layer

✓ Correct dependency direction

✓ No forbidden references

✓ Business rules remain in Domain

✓ Infrastructure only contains implementations

✓ API contains only HTTP concerns

✓ Application coordinates use cases

Failure to satisfy any rule requires revising the generated code before presenting it.