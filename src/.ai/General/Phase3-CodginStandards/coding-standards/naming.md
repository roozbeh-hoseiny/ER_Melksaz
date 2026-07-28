# Naming Conventions

Version: 1.0

---

# Purpose

This document defines the mandatory naming conventions for the repository.

Naming is one of the most important aspects of software quality.

Well-named code requires less documentation, fewer comments, and is easier for both engineers and AI agents to understand.

---

# Primary Principle

Names should describe business intent.

A reader should understand the purpose of a type or member without reading its implementation.

---

# General Rules

Names must be:

* Clear
* Consistent
* Descriptive
* Unambiguous
* Business-oriented

Avoid abbreviations unless they are universally recognised.

---

# Use Business Language

Always use the Ubiquitous Language.

Good:

```text
Invoice
InvoiceNumber
ApproveInvoice
ReceivePayment
CreditLimit
```

Bad:

```text
Inv
Obj
Processor
Manager
Execute
HandleData
```

---

# English Only

All source code must use English.

Never use another language for:

* Class names
* Method names
* Variable names
* Property names
* Namespace names
* File names

---

# PascalCase

Use PascalCase for:

* Classes
* Interfaces
* Records
* Structs
* Enums
* Properties
* Methods
* Events
* Constants
* Public fields (avoid public fields entirely)

Examples:

```text
Invoice

InvoiceNumber

ReceivePayment()

CustomerId
```

---

# camelCase

Use camelCase for:

* Local variables
* Parameters

Examples:

```text
invoice

customerId

paymentAmount
```

---

# Private Fields

Private fields use a leading underscore.

Examples:

```text
_repository

_clock

_logger

_invoiceRepository
```

---

# Interfaces

Interfaces begin with `I`.

Examples:

```text
IInvoiceRepository

IClock

IEmailSender

ICustomerNumberGenerator
```

---

# Abstract Classes

Abstract classes do **not** use prefixes.

Good:

```text
AggregateRoot

DomainEvent

Entity
```

Avoid:

```text
BaseAggregate

AbstractEntity
```

unless repository conventions already require them.

---

# Generic Type Parameters

Use meaningful generic names.

Good:

```text
TEntity

TAggregate

TRequest

TResponse

TCommand

TQuery
```

Avoid:

```text
T

T1

T2

X
```

---

# Method Names

Methods should be verbs.

Examples:

```text
Approve()

Reject()

ReceivePayment()

CalculateTotal()

CreateInvoice()
```

Avoid:

```text
Data()

Manager()

Process()

Run()
```

---

# Boolean Members

Boolean members should read naturally.

Good:

```text
IsApproved

HasExpired

CanCancel

ShouldRetry
```

Avoid:

```text
Approved

Expired

Retry
```

---

# Collections

Plural names represent collections.

Examples:

```text
Invoices

Customers

OrderLines

Payments
```

Singular names represent single objects.

---

# Async Methods

Asynchronous methods must end with `Async`.

Examples:

```text
SaveAsync()

PublishAsync()

ExistsAsync()

LoadAsync()
```

---

# Events

Events are named in the past tense.

Examples:

```text
InvoiceApproved

CustomerRegistered

PaymentReceived
```

---

# Commands

Commands use imperative verbs.

Examples:

```text
CreateInvoice

ApproveInvoice

RegisterCustomer

CancelOrder
```

---

# Queries

Queries describe the requested information.

Examples:

```text
GetInvoice

GetCustomer

SearchInvoices

FindOpenOrders
```

---

# Handlers

Handlers use the pattern:

```text
<CreateInvoice>Handler

<GetInvoice>Handler
```

---

# Validators

Validators use:

```text
<CreateInvoice>Validator

<RegisterCustomer>Validator
```

---

# Endpoints

Endpoints use:

```text
CreateInvoiceEndpoint

GetInvoiceEndpoint
```

---

# DTOs

Suffix DTOs consistently.

Examples:

```text
InvoiceDto

CustomerDto

PaymentDto
```

If the repository prefers Request/Response models instead, follow that convention consistently.

---

# Exceptions

Exception classes end with:

```text
Exception
```

Examples:

```text
BusinessRuleViolationException

ConcurrencyException

CustomerNotFoundException
```

---

# Repository Names

Repositories use:

```text
IInvoiceRepository

InvoiceRepository
```

Never use:

```text
InvoiceDao

InvoiceStorage

InvoiceAccessor
```

---

# File Names

Each public type should normally have its own file.

The file name must exactly match the primary type.

Example:

```text
Invoice.cs
```

---

# Namespace Naming

Namespaces follow the project structure.

Avoid unnecessary nesting.

Namespaces should describe business ownership.

---

# Avoid Generic Suffixes

Avoid names such as:

* Helper
* Utility
* Common
* Misc
* Data
* Processor
* Manager
* Service (unless it is a Domain/Application/Infrastructure Service with a clear architectural role)

Names should communicate purpose.

---

# Acronyms

Use standard .NET casing.

Examples:

```text
HttpClient

XmlReader

JsonSerializer

GrpcChannel
```

Avoid inconsistent casing.

---

# Naming Checklist

Before completing an implementation, verify:

* Business language is used.
* Names describe intent.
* No unnecessary abbreviations exist.
* Methods are verbs.
* Booleans read naturally.
* Collections are plural.
* Async methods end with `Async`.
* Existing repository naming conventions are preserved.

---

# Guiding Principle

If a developer cannot understand what a type or method does by reading its name alone, the name should be improved before the implementation is considered complete.
