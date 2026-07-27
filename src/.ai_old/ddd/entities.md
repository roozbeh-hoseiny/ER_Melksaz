# Entity Design Rules

Version: 1.0

This document defines how Entities are designed and implemented throughout the repository.

Entities are one of the core building blocks of Domain Driven Design.

Unlike Value Objects, Entities have identity and lifecycle.

AI must never confuse an Entity with a Value Object.

---

# Definition

An Entity

- has identity
- has lifecycle
- changes over time
- owns behaviour
- protects invariants
- belongs to exactly one Aggregate

Identity is more important than values.

---

# Philosophy

An Entity models a business object whose identity matters.

Examples

Good

Invoice

Customer

Order

Shipment

Payment

OrderLine

InvoiceLine

Warehouse

Product

Contract

Policy

Subscription

Bad

DTO

EF Model

Database Row

Request

Response

ViewModel

---

# General Rules

## ENTITY-001

Every Entity has a unique identity.

---

## ENTITY-002

Identity never changes.

---

## ENTITY-003

Changing all properties does not create a new Entity.

The identity remains the same.

---

## ENTITY-004

Entity equality is identity equality.

Never compare Entities using every property.

---

## ENTITY-005

Entity identity should be a Strongly Typed ID.

Good

InvoiceId

OrderId

CustomerId

Bad

Guid

string

int

---

## ENTITY-006

Entity identifiers are immutable.

---

## ENTITY-007

Entities should expose behaviour.

Avoid anemic entities.

Bad

```
entity.Name = ...

entity.Status = ...
```

Good

```
Approve()

Reject()

Activate()

Suspend()

Cancel()

AssignRole()
```

---

# Aggregate Membership

## ENTITY-008

Every Entity belongs to exactly one Aggregate.

---

## ENTITY-009

Only Aggregate Roots are externally accessible.

---

## ENTITY-010

Child Entities are managed only through their Aggregate Root.

Never modify child Entities directly from Application.

---

## ENTITY-011

Repositories never load child Entities independently.

---

## ENTITY-012

Entity lifetime is controlled by the Aggregate Root.

---

# Constructors

## ENTITY-013

Constructors should not be public.

Prefer

private

internal

protected

depending on repository convention.

---

## ENTITY-014

Creation happens through business methods.

---

## ENTITY-015

Entities should never exist in an invalid state.

---

# Encapsulation

## ENTITY-016

Fields are private.

---

## ENTITY-017

Collections are private.

---

## ENTITY-018

Public setters are forbidden unless explicitly justified.

---

## ENTITY-019

State transitions occur through methods.

---

## ENTITY-020

Never expose mutable collections.

---

## ENTITY-021

Expose IReadOnlyCollection<T>.

---

# Behaviour

## ENTITY-022

Entities own business behaviour.

---

## ENTITY-023

Avoid data bags.

---

## ENTITY-024

Methods should model business language.

---

## ENTITY-025

Never expose technical methods.

Bad

```
SetStatus()

SetValue()

Update()

```

Prefer

```
Approve()

Cancel()

Receive()

Issue()

Archive()

```

---

# Validation

## ENTITY-026

Business validation belongs inside Entities.

---

## ENTITY-027

Infrastructure validation belongs outside Entities.

---

## ENTITY-028

An Entity method must leave the Entity valid.

---

# Identity

## ENTITY-029

Identity is immutable.

---

## ENTITY-030

Never expose identity setters.

---

## ENTITY-031

Identity should be assigned once.

---

## ENTITY-032

Identity should never be regenerated.

---

# Relationships

## ENTITY-033

Reference other Aggregates by ID.

Never by navigation property.

Good

```
CustomerId
```

Bad

```
Customer Customer
```

unless the referenced Entity belongs to the same Aggregate.

---

## ENTITY-034

Navigation across Aggregate boundaries is forbidden.

---

## ENTITY-035

Use repositories to load other Aggregates.

---

# Persistence Ignorance

## ENTITY-036

Entities know nothing about EF Core.

---

## ENTITY-037

Entities know nothing about SQL.

---

## ENTITY-038

Entities know nothing about MongoDB.

---

## ENTITY-039

Entities know nothing about Redis.

---

## ENTITY-040

No persistence attributes.

---

## ENTITY-041

No DbContext references.

---

# Infrastructure

Entities must never reference

- ILogger
- IConfiguration
- IServiceProvider
- HttpClient
- DbContext
- IMapper
- JsonSerializer

---

# Domain Services

## ENTITY-042

Entities may collaborate with Domain Services through the Aggregate when necessary.

---

## ENTITY-043

Entities never resolve dependencies.

---

# State Transitions

## ENTITY-044

State transitions should be explicit.

Bad

```
entity.Status = Approved;
```

Good

```
entity.Approve();
```

---

## ENTITY-045

Illegal state transitions must fail.

---

## ENTITY-046

Business invariants must be revalidated after every transition.

---

# Collections

## ENTITY-047

Only the Aggregate Root modifies collections.

---

## ENTITY-048

Child Entities should not insert siblings.

---

## ENTITY-049

Collection modifications should be atomic.

---

# Lifecycle

Typical lifecycle

Created

↓

Validated

↓

Active

↓

Modified

↓

Archived

↓

Deleted (if allowed)

Business methods should express these transitions.

---

# Equality

## ENTITY-050

Entities compare by identity.

---

## ENTITY-051

HashCode is based on identity.

---

## ENTITY-052

Reference equality alone is insufficient.

---

# Events

## ENTITY-053

Entities may participate in Domain Events through the Aggregate Root.

---

## ENTITY-054

The Aggregate Root decides when business events are raised.

---

# Performance

## ENTITY-055

Avoid unnecessary allocations.

---

## ENTITY-056

Avoid reflection.

---

## ENTITY-057

Avoid mutable static state.

---

# Anti-Patterns

Never

- expose public setters
- expose mutable collections
- use primitive IDs
- bypass Aggregate Root
- access repositories
- access DbContext
- perform IO
- access HTTP
- access configuration
- log directly
- send emails
- publish integration events
- depend on Infrastructure

---

# AI Generation Rules

Whenever an Entity is generated

always generate

✓ Strongly Typed ID

✓ Private constructor

✓ Business methods

✓ Read-only properties

✓ Read-only collections

✓ Encapsulation

✓ Identity equality

✓ Invariant protection

✓ Persistence ignorance

✓ Aggregate ownership

Never generate an anemic Entity.

---

# AI Verification Checklist

Before generating an Entity verify

✓ Has identity

✓ Identity is immutable

✓ Equality based on identity

✓ Behaviour included

✓ Business rules encapsulated

✓ No public setters

✓ No mutable collections

✓ No infrastructure dependency

✓ No EF Core dependency

✓ No HTTP dependency

✓ Aggregate boundaries respected

✓ References other Aggregates by ID only

✓ Cannot enter an invalid state

The resulting Entity should represent a rich business object rather than a database record.