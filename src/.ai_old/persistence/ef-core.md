# EF Core Design Rules

Version: 1.0

This document defines the EF Core conventions used throughout the repository.

EF Core is an infrastructure concern.

It exists only to persist and retrieve Domain objects.

The Domain Model must remain persistence ignorant.

---

# Architecture

Dependency direction

```
Domain
    ▲
Application
    ▲
Infrastructure (EF Core)
```

EF Core belongs exclusively to Infrastructure.

---

# General Rules

## EF-001

Never reference EF Core from Domain.

---

## EF-002

Never reference EF Core from Value Objects.

---

## EF-003

Never reference EF Core from Entities.

---

## EF-004

Never reference EF Core from Aggregates.

---

## EF-005

Application depends on repository abstractions.

Never DbContext.

---

# DbContext

## DB-001

One DbContext per bounded context.

---

## DB-002

DbContext represents persistence.

Not business logic.

---

## DB-003

Never expose DbContext outside Infrastructure.

---

## DB-004

DbContext contains

- DbSets
- Configurations
- Interceptors
- Transaction configuration

Nothing else.

---

## DB-005

Never place business logic inside DbContext.

---

# Configurations

## CFG-001

Every Entity has exactly one configuration class.

---

## CFG-002

Configurations implement

```
IEntityTypeConfiguration<TEntity>
```

---

## CFG-003

Never configure Entities inside OnModelCreating directly.

Use ApplyConfigurationsFromAssembly().

---

## CFG-004

Each configuration lives beside Infrastructure persistence code.

---

# Aggregate Mapping

## MAP-001

Persist Aggregate Roots.

---

## MAP-002

Child Entities are configured through Aggregate relationships.

---

## MAP-003

Aggregate boundaries determine persistence boundaries.

---

## MAP-004

Never map an Aggregate according to database convenience.

Map according to the Domain.

---

# Value Objects

## VO-001

Map Value Objects using

- Owned Types
- ValueConverters

depending on the scenario.

---

## VO-002

Value Objects remain immutable.

---

## VO-003

Value Objects never expose EF attributes.

---

## VO-004

Persistence concerns stay inside Infrastructure.

---

# IDs

## ID-001

Use Strongly Typed IDs throughout the model.

---

## ID-002

Configure ValueConverters for Strongly Typed IDs.

---

## ID-003

Never expose Guid throughout the Domain.

---

# Repositories

## REP-001

Repositories return Aggregates.

---

## REP-002

Repositories never expose IQueryable.

---

## REP-003

Repositories express business intent.

Bad

```
Get()

Find()
```

Good

```
GetActiveInvoice()

GetPendingPayment()

LoadCustomerForOrdering()
```

---

## REP-004

Repositories never return EF entities.

---

# Transactions

## TX-001

Transactions belong to the Application pipeline.

---

## TX-002

Repositories do not manage transactions.

---

## TX-003

One Command.

One Commit.

---

# SaveChanges

## SAVE-001

Application should execute one SaveChanges per successful command.

---

## SAVE-002

Avoid intermediate SaveChanges.

---

## SAVE-003

Never call SaveChanges inside repositories unless this is an explicit repository convention.

---

# Queries

## QUERY-001

Read operations should use

```
AsNoTracking()
```

unless tracking is required.

---

## QUERY-002

Project directly into DTOs whenever possible.

---

## QUERY-003

Avoid loading Aggregates for read-only scenarios.

---

## QUERY-004

Load only required columns.

---

## QUERY-005

Avoid Include() chains unless absolutely necessary.

---

# Loading

## LOAD-001

Prefer explicit loading.

---

## LOAD-002

Avoid Lazy Loading.

---

## LOAD-003

Prefer projection.

---

## LOAD-004

Load only required relationships.

---

# Migrations

## MIG-001

Migrations belong only to Infrastructure.

---

## MIG-002

Never modify generated migration files manually unless absolutely necessary.

---

## MIG-003

Migration names must express intent.

Examples

```
AddInvoiceTable

CreateCustomerIndexes

IntroducePaymentHistory
```

---

# Concurrency

## CON-001

Use optimistic concurrency.

---

## CON-002

Concurrency handling belongs to Application or Infrastructure.

---

## CON-003

Domain should not know concurrency tokens.

---

# Performance

## PERF-001

Disable tracking for queries.

---

## PERF-002

Avoid N+1 queries.

---

## PERF-003

Avoid unnecessary Includes.

---

## PERF-004

Use compiled queries only after measurement.

---

## PERF-005

Never optimise prematurely.

---

# Interceptors

## INT-001

Cross-cutting persistence behaviour belongs in interceptors.

Examples

- Auditing
- Soft Delete
- Domain Event collection
- Multi-tenancy
- Outbox support

---

# Soft Delete

## DEL-001

Soft Delete is implemented consistently.

Never duplicate filtering logic.

---

# Auditing

## AUD-001

Audit fields are infrastructure concerns.

---

## AUD-002

Do not pollute Domain Entities with persistence behaviour.

---

# Naming

Tables

Singular or plural according to repository convention.

The AI must inspect existing mappings before generating new ones.

Never invent a different naming strategy.

---

# Forbidden

Never

- inject DbContext into Application
- expose IQueryable outside Infrastructure
- use EF attributes in Domain
- use Lazy Loading
- put business rules into repositories
- call SaveChanges repeatedly
- expose EF models
- duplicate mapping logic
- return tracked entities for read-only operations

---

# AI Generation Rules

Whenever generating persistence automatically generate

✓ Entity Configuration

✓ Strongly Typed ID converters

✓ Value Object mapping

✓ Repository implementation

✓ Repository interface implementation

✓ Migration (when requested)

✓ Index configuration (when required)

✓ Foreign key configuration

✓ Concurrency configuration (if applicable)

✓ Unit Tests where repository logic exists

✓ Integration Tests for persistence

---

# AI Verification Checklist

Before presenting EF Core code verify

✓ Domain is persistence ignorant

✓ One configuration per Entity

✓ Uses ApplyConfigurationsFromAssembly()

✓ Strongly Typed IDs mapped

✓ Value Objects mapped correctly

✓ No IQueryable leakage

✓ Uses AsNoTracking() for reads

✓ No Lazy Loading

✓ Single SaveChanges

✓ Repository abstractions respected

✓ Aggregate boundaries preserved

EF Core should faithfully persist the Domain Model without influencing its design.