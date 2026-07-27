# Aggregate Design Rules

Version: 1.0

This document defines how Aggregates, Entities, Value Objects and Domain Events are implemented.

These rules are mandatory.

The Aggregate is the most important concept in Domain Driven Design.

The AI must never generate an Aggregate that violates these rules.

---

# Aggregate Definition

An Aggregate is

- a consistency boundary
- a transactional boundary
- a business boundary

Everything inside an Aggregate is modified together.

Nothing outside an Aggregate may directly modify its internal state.

---

# Aggregate Root

The Aggregate Root is the only externally visible Entity.

Everything else is internal.

Example

```
Invoice
    InvoiceLine
    Payment
    Attachment
```

External code may only reference Invoice.

---

# Aggregate Responsibilities

The Aggregate Root is responsible for

- protecting invariants
- validating business rules
- managing child entities
- raising domain events
- coordinating state transitions

The Aggregate Root is NOT responsible for

- persistence
- logging
- HTTP
- serialization
- caching
- dependency injection

---

# Aggregate Rules

## AGG-001

Every Aggregate Root represents a business concept.

Never create aggregates from database tables.

---

## AGG-002

Aggregate Roots should have a dedicated identifier.

Good

InvoiceId

CustomerId

OrderId

Bad

Guid

string

int

---

## AGG-003

Aggregate identifiers should be immutable.

---

## AGG-004

Aggregate constructors should not be public.

Preferred

private

protected

internal (only when necessary)

---

## AGG-005

Creation should happen through

Create()

New()

Open()

Register()

Issue()

or another business-oriented factory.

Avoid exposing constructors.

---

## AGG-006

Factory methods must guarantee a valid Aggregate.

---

## AGG-007

Invalid Aggregates must never exist.

---

## AGG-008

Business validation belongs inside the Aggregate.

---

## AGG-009

Application validation belongs outside the Aggregate.

---

## AGG-010

Aggregate state must remain consistent after every public method.

---

## AGG-011

Every public method represents a business action.

Good

Approve()

Reject()

Cancel()

Pay()

Ship()

Activate()

Suspend()

Bad

SetStatus()

UpdateFlag()

SetValue()

---

## AGG-012

Never expose mutable collections.

Good

IReadOnlyCollection<T>

Bad

List<T>

HashSet<T>

---

## AGG-013

Collections should be backed by private fields.

Example

```
private readonly List<OrderItem> _items = [];

public IReadOnlyCollection<OrderItem> Items => _items;
```

---

## AGG-014

Collection modifications occur through business methods.

Examples

AddItem()

RemoveItem()

ChangeQuantity()

ReplaceAddress()

Never expose Add() or Remove() to external code.

---

## AGG-015

Never expose collection setters.

Forbidden

```
public List<Item> Items { get; set; }
```

---

## AGG-016

Aggregate methods should express business language.

The Ubiquitous Language is more important than technical terminology.

---

## AGG-017

Aggregates should be persistence ignorant.

They should not know

- EF Core
- SQL
- MongoDB
- Redis

---

## AGG-018

Aggregates should not know infrastructure services.

Forbidden

ILogger

DbContext

HttpClient

IConfiguration

---

## AGG-019

Aggregates should not perform IO.

---

## AGG-020

Aggregates should not publish integration events.

Only Domain Events.

---

# Entity Rules

## ENTITY-001

Entities have identity.

---

## ENTITY-002

Identity never changes.

---

## ENTITY-003

Entities belong to one Aggregate.

---

## ENTITY-004

Entities should not exist without an Aggregate Root unless explicitly modelled as Aggregate Roots themselves.

---

## ENTITY-005

Entities protect their own invariants.

---

## ENTITY-006

Entities should expose behaviour.

Avoid anemic models.

---

## ENTITY-007

Entities should avoid public setters.

---

## ENTITY-008

Entities should not contain infrastructure logic.

---

## ENTITY-009

Entities should not call repositories.

---

## ENTITY-010

Entities should not know persistence.

---

# Value Objects

## VO-001

Value Objects are immutable.

---

## VO-002

Value Objects have no identity.

---

## VO-003

Equality is based on values.

---

## VO-004

Value Objects should validate themselves during creation.

---

## VO-005

Invalid Value Objects must never exist.

---

## VO-006

Prefer Value Objects over primitive types.

Good

Money

Email

PhoneNumber

InvoiceNumber

NationalCode

CustomerId

Address

PostalCode

Percentage

Quantity

TaxRate

Bad

string

Guid

decimal

---

## VO-007

Value Objects should be small.

---

## VO-008

Value Objects should be side-effect free.

---

## VO-009

Methods should return new instances.

Never mutate existing instances.

---

## VO-010

Value Objects should encapsulate validation logic.

---

# Domain Events

## EVENT-001

Aggregates raise Domain Events.

---

## EVENT-002

Only business facts become Domain Events.

---

## EVENT-003

Events describe something that already happened.

Good

InvoiceCreated

PaymentReceived

OrderCancelled

Bad

CreateInvoice

CancelOrder

---

## EVENT-004

Domain Events are immutable.

---

## EVENT-005

Events contain business information only.

---

## EVENT-006

Events never reference infrastructure objects.

---

## EVENT-007

Events never contain DbContext.

---

## EVENT-008

Events never contain HttpContext.

---

## EVENT-009

Events should be raised inside Aggregate methods.

---

## EVENT-010

Application is responsible for dispatching Domain Events.

---

# Aggregate Size

## SIZE-001

Aggregates should be as small as possible.

---

## SIZE-002

Aggregates should be as large as necessary.

---

## SIZE-003

Do not split an Aggregate if doing so breaks consistency.

---

## SIZE-004

Do not merge Aggregates because of database relationships.

---

# Invariants

Every Aggregate must protect

- consistency
- identity
- business rules
- valid transitions

The Aggregate should never enter an invalid state.

---

# Concurrency

Aggregates are optimistic.

Concurrency conflicts should be detected outside the Domain unless the business requires explicit conflict handling.

---

# AI Generation Checklist

When generating an Aggregate, always generate:

✓ Aggregate Root

✓ Identifier Value Object

✓ Required Value Objects

✓ Child Entities

✓ Factory Method

✓ Behavioural Methods

✓ Domain Events

✓ Invariant Protection

✓ Read-only Collections

✓ Equality

✓ Validation

✓ XML documentation only if repository convention requires it

✓ Unit Tests

Never generate an anemic Aggregate.

Never expose mutable state.

Never put business rules in Application.

The Aggregate must always read like the language of the business rather than the language of the database.