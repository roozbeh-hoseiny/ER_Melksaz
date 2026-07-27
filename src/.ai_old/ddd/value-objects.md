# Value Object Design Rules

Version: 1.0

This document defines how Value Objects are designed, implemented and used throughout the repository.

Value Objects are one of the primary building blocks of the domain model.

AI must always prefer Value Objects over primitive types whenever the value has business meaning.

---

# Definition

A Value Object

- has no identity
- is immutable
- is compared by value
- encapsulates validation
- encapsulates business meaning
- cannot exist in an invalid state

---

# Philosophy

A Value Object represents a concept.

Not a storage type.

Examples

Good

Money

Email

PhoneNumber

CustomerId

InvoiceNumber

Address

Percentage

TaxRate

Quantity

NationalCode

DateRange

Currency

PostalCode

Coordinate

Weight

Volume

Length

Age

Username

PasswordHash

Bad

string

Guid

decimal

int

double

---

# General Rules

## VO-001

Every Value Object represents a meaningful business concept.

---

## VO-002

Never create a Value Object merely to wrap a primitive.

It must add meaning or behaviour.

---

## VO-003

A Value Object has no identity.

Two equal values are the same object conceptually.

---

## VO-004

Value Objects are immutable.

Never expose setters.

---

## VO-005

Once created, a Value Object never changes.

Operations return a new instance.

---

## VO-006

Invalid Value Objects must never exist.

---

## VO-007

Validation happens during creation.

Not afterwards.

---

## VO-008

A Value Object should always be valid.

There is no "Initialize()" step.

---

## VO-009

Prefer factory methods over public constructors.

Example

```csharp
Email.Create(value)

Money.Create(amount, currency)

CustomerId.Create(value)
```

---

## VO-010

If construction can fail, return the repository's Result type rather than throwing exceptions for expected validation failures.

---

# Equality

## VO-011

Equality is based entirely on contained values.

---

## VO-012

Reference equality is irrelevant.

---

## VO-013

Two Value Objects containing identical values are equal.

---

## VO-014

Hash codes must be consistent with equality.

---

## VO-015

Do not include transient values in equality.

---

# Immutability

## VO-016

Expose read-only properties only.

---

## VO-017

Collections inside a Value Object must also be immutable.

---

## VO-018

Never expose mutable arrays.

---

## VO-019

Never expose mutable lists.

---

## VO-020

Do not allow mutation through methods.

Bad

```csharp
money.ChangeAmount(...)
```

Good

```csharp
money.Add(...)
money.Subtract(...)
```

which return new instances.

---

# Strongly Typed IDs

## VO-021

Entity identifiers should be Value Objects.

Preferred

CustomerId

InvoiceId

ProductId

OrderId

UserId

---

## VO-022

Never expose Guid directly in business APIs.

---

## VO-023

Never compare raw Guid values throughout the codebase.

Compare Value Objects.

---

## VO-024

Identifiers should encapsulate parsing.

---

## VO-025

Identifiers should encapsulate formatting.

---

# Money

## MONEY-001

Money should be a Value Object.

---

## MONEY-002

Money contains

Amount

Currency

---

## MONEY-003

Money validates currency.

---

## MONEY-004

Money supports

Add

Subtract

Multiply

Divide

Compare

---

## MONEY-005

Money operations must reject incompatible currencies.

---

# Email

## EMAIL-001

Email is a Value Object.

---

## EMAIL-002

Email validates format during creation.

---

## EMAIL-003

Email stores normalized values.

---

## EMAIL-004

Email comparison rules must be explicitly defined by the business.

---

# Phone Number

## PHONE-001

PhoneNumber validates format.

---

## PHONE-002

Store normalized values.

---

## PHONE-003

Formatting is presentation logic.

The Value Object stores canonical form.

---

# Date Range

## DATE-001

DateRange is a Value Object.

---

## DATE-002

End must be greater than or equal to Start.

---

## DATE-003

DateRange exposes

Contains()

Overlaps()

Duration()

Intersect()

---

# Percentage

## PERCENT-001

Percentage validates range.

---

## PERCENT-002

Do not represent percentages using raw decimals.

---

# Address

## ADDRESS-001

Address is immutable.

---

## ADDRESS-002

Address contains only address data.

No geolocation services.

---

# Quantity

## QUANTITY-001

Quantity validates business limits.

---

## QUANTITY-002

Negative quantities require explicit business support.

---

# Behaviour

## VO-026

Value Objects contain behaviour.

Avoid passive data holders.

---

## VO-027

Methods should model business operations.

---

## VO-028

Keep Value Objects small.

---

## VO-029

A Value Object should have one responsibility.

---

# Serialization

## VO-030

Serialization concerns belong outside the Value Object whenever possible.

---

## VO-031

Do not add JSON attributes unless required by repository conventions.

---

# Persistence

## VO-032

Persistence ignorance is required.

---

## VO-033

Do not reference EF Core.

---

## VO-034

Do not reference database attributes.

---

## VO-035

Mapping belongs in Infrastructure.

---

# Performance

## VO-036

Keep Value Objects lightweight.

---

## VO-037

Avoid unnecessary allocations.

---

## VO-038

Avoid reflection.

---

## VO-039

Avoid mutable caches.

---

# Exceptions

## VO-040

Expected validation failures should use the repository Result/Error abstraction.

Exceptions are reserved for unexpected failures.

---

# Anti-Patterns

Never

- expose setters
- expose mutable collections
- expose Guid
- expose string identifiers
- bypass validation
- delay validation
- use primitive obsession
- depend on EF Core
- depend on ASP.NET
- depend on Infrastructure
- perform IO
- call repositories
- call services
- access configuration

---

# AI Generation Rules

Whenever the AI detects one of the following concepts, it should automatically generate a Value Object instead of using a primitive type.

Invoice Number

Customer Number

Email

Phone Number

Postal Code

Country Code

National Code

Money

Currency

Percentage

Tax Rate

Discount

Address

Coordinates

Weight

Height

Length

Volume

Age

Birth Date

User Name

Password Hash

Product Code

SKU

Barcode

Tracking Number

Order Number

Invoice Id

Customer Id

User Id

Product Id

Category Id

Tenant Id

Company Id

Correlation Id

Request Id

Session Id

Message Id

Aggregate Id

Entity Id

---

# AI Verification Checklist

Before generating a Value Object, verify:

✓ Immutable

✓ Self-validating

✓ Value-based equality

✓ No identity

✓ No public setters

✓ No infrastructure dependency

✓ No EF Core dependency

✓ No ASP.NET dependency

✓ No IO

✓ Behaviour included where appropriate

✓ Uses repository Result/Error abstraction for validation failures

✓ Persistence ignorant

✓ Represents a real business concept

A Value Object should make the domain model richer, safer, and more expressive while reducing primitive obsession.