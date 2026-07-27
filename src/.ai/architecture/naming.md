# Naming Conventions

Version: 1.0

---

# Purpose

This document defines the naming conventions used throughout the repository.

Consistent naming improves readability, discoverability, maintainability, and communication.

Names should communicate business intent rather than implementation details.

---

# General Principles

Names should be:

* Meaningful
* Consistent
* Unambiguous
* Business-oriented
* Easy to pronounce
* Easy to search
* Stable over time

Avoid abbreviations unless they are universally understood.

---

# Business Language

Always use the ubiquitous language of the business domain.

Prefer:

* Invoice
* Customer
* Payment
* Product

Avoid technical terminology when modelling business concepts.

---

# Classes

Class names should:

* Be nouns
* Use PascalCase
* Clearly describe responsibility
* Avoid implementation details

Good:

```
Invoice
Customer
Payment
Order
```

Bad:

```
InvoiceHelper
InvoiceManager
InvoiceProcessor
DataClass
```

---

# Interfaces

Interfaces should:

* Represent capabilities
* Use the `I` prefix
* Be named after responsibilities

Examples:

```
IInvoiceRepository
ICurrentUser
IClock
IEmailSender
```

Avoid naming interfaces after implementations.

---

# Methods

Methods should:

* Be verbs
* Describe behaviour
* Express intent

Examples:

```
Create()
Cancel()
Approve()
CalculateTotal()
AddItem()
```

Avoid vague names such as:

```
Process()
Handle()
ExecuteLogic()
DoWork()
Run()
```

unless their responsibility is already implied by context.

---

# Properties

Properties should be nouns or adjectives.

Examples:

```
Id
Number
Status
Customer
Items
CreatedAt
TotalAmount
```

Avoid redundant prefixes.

Bad:

```
InvoiceNumberValue
CustomerObject
```

---

# Variables

Variable names should clearly communicate purpose.

Prefer:

```
invoice
customer
order
lineItem
payment
```

Avoid:

```
obj
data
value
item1
tmp
x
```

except for very small scopes where meaning is obvious.

---

# Boolean Variables

Boolean names should read naturally.

Examples:

```
isActive
isDeleted
hasPermission
canApprove
requiresValidation
```

Avoid:

```
flag
status
check
condition
```

---

# Collections

Collections should use plural names.

Examples:

```
customers
orders
products
lineItems
permissions
```

---

# Commands

Commands represent business actions.

Command names should begin with a verb.

Examples:

```
CreateInvoiceCommand
CancelInvoiceCommand
ApprovePaymentCommand
AssignRoleCommand
```

---

# Queries

Queries represent requests for information.

Examples:

```
GetInvoiceQuery
SearchInvoicesQuery
GetCustomerByIdQuery
ListProductsQuery
```

---

# Handlers

Handlers should follow a predictable naming convention.

Examples:

```
CreateInvoiceCommandHandler
SearchInvoicesQueryHandler
```

---

# Validators

Validators should match the object they validate.

Examples:

```
CreateInvoiceCommandValidator
UpdateCustomerCommandValidator
```

---

# Domain Events

Domain Events describe something that has already occurred.

Use past tense.

Examples:

```
InvoiceCreated
InvoiceCancelled
PaymentCompleted
CustomerRegistered
```

Avoid imperative names.

Bad:

```
CreateInvoice
CancelInvoice
```

---

# Integration Events

Integration Events should also use past tense.

Examples:

```
InvoiceCreatedIntegrationEvent
CustomerDeletedIntegrationEvent
```

---

# Enumerations

Enumeration names should be singular.

Examples:

```
InvoiceStatus
PaymentStatus
OrderType
```

Enumeration values should be concise.

Examples:

```
Pending
Approved
Cancelled
Completed
```

---

# Exceptions

Exception names should clearly describe failure conditions.

Examples:

```
InvoiceNotFoundException
DuplicateCustomerException
UnauthorizedOperationException
```

---

# Repository Interfaces

Repository names should represent aggregates.

Examples:

```
IInvoiceRepository
ICustomerRepository
```

Avoid generic repositories.

Bad:

```
IGenericRepository
IDataRepository
```

---

# DTOs

DTO names should communicate their purpose.

Examples:

```
InvoiceDto
CustomerSummaryDto
ProductResponse
CreateInvoiceRequest
```

---

# API Endpoints

Endpoint names should represent business capabilities.

Examples:

```
CreateInvoice
CancelInvoice
GetInvoice
SearchInvoices
```

Avoid implementation terminology.

---

# Files

File names should match the primary type they contain.

One primary type per file.

Examples:

```
Invoice.cs
InvoiceCreated.cs
CreateInvoiceCommand.cs
```

---

# Projects

Project names should communicate architectural responsibility.

Examples:

```
Company.Sales.Domain
Company.Sales.Application
Company.Sales.Infrastructure
Company.Sales.Api
```

---

# Namespaces

Namespaces should mirror the folder structure.

Avoid unrelated namespaces.

Namespace hierarchies should remain stable.

---

# Acronyms

Use standard .NET naming conventions.

Examples:

```
HttpClient
XmlDocument
JsonSerializer
Id
Api
Grpc
```

Do not capitalise every letter of an acronym inside identifiers.

Avoid:

```
HTTPClient
XMLDocument
CustomerID
```

---

# Generic Type Parameters

Use meaningful generic names.

Examples:

```
TEntity
TRequest
TResponse
TAggregate
TResult
```

Avoid single-letter names unless they are conventional.

---

# Consistency

When multiple valid names exist, prefer the name already used elsewhere in the repository.

Consistency is more important than personal preference.

---

# Naming Review Checklist

Before introducing a new name, verify:

* Does it use business terminology?
* Is it unambiguous?
* Does it describe responsibility?
* Is it consistent with existing names?
* Does it avoid unnecessary abbreviations?
* Will another developer immediately understand it?

---

# Guiding Principle

A well-chosen name should communicate intent without requiring additional explanation.
