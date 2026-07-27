# Repository

Version: 1.0

---

# Purpose

This document defines the responsibilities and design rules for Repositories within the Domain-Driven Design architecture.

A Repository provides the illusion of an in-memory collection of Aggregate Roots while hiding persistence details from the Domain and Application layers.

Repositories exist to support the Domain—not the database.

---

# Primary Principle

A Repository manages Aggregate Roots.

It is **not** a generic data access layer.

---

# Definition

A Repository:

* Loads Aggregate Roots.
* Persists Aggregate Roots.
* Hides persistence implementation.
* Exposes business-oriented operations.
* Belongs to the Domain abstraction.

Repository implementations belong to Infrastructure.

Repository contracts belong to the Domain.

---

# Responsibility

A Repository is responsible for:

* Retrieving Aggregates.
* Persisting Aggregates.
* Removing Aggregates.
* Performing Aggregate-specific queries required by the Domain.

It is **not** responsible for business logic.

---

# Aggregate Root Only

Repositories should expose only Aggregate Roots.

Good:

```text id="gq7r4m"
IInvoiceRepository
```

Bad:

```text id="7xk2pw"
IInvoiceItemRepository
```

Child Entities are managed by their Aggregate Root.

---

# Repository Interface

Repository interfaces belong to the Domain.

Example responsibilities:

* Get by identifier
* Add
* Update
* Delete
* Exists
* Business-specific lookup

Avoid exposing persistence concepts.

---

# Repository Implementation

Implementations belong to Infrastructure.

Examples:

* EF Core
* Dapper
* MongoDB
* Cosmos DB

The Application and Domain layers should not know which implementation is used.

---

# Business-Oriented API

Repository methods should reflect business intent.

Good:

```text id="4p8rwm"
GetByInvoiceNumber()

Exists(CustomerId)

FindOpenInvoices()
```

Avoid technical names such as:

```text id="o6f3jc"
FindByColumn()

ExecuteSql()

RunQuery()
```

---

# Persistence Ignorance

Repositories isolate persistence details.

Business code must never know:

* SQL
* LINQ provider behaviour
* Database schema
* ORM configuration

Persistence remains replaceable.

---

# Query Responsibility

Repositories should retrieve Aggregates.

Complex reporting and read models belong elsewhere.

Repositories are not reporting engines.

---

# Commands vs Queries

Repositories support transactional business operations.

Read models intended for reporting or dashboards should use dedicated query mechanisms.

Avoid loading Aggregates solely for reporting.

---

# Transactions

Repositories do not manage transactions.

Transaction management belongs to the Application layer or Unit of Work implementation.

---

# Unit of Work

Where a Unit of Work exists:

* Repositories participate in it.
* Repositories do not create or commit transactions.

Persistence coordination belongs outside the Repository.

---

# Collection Semantics

Repositories should behave like collections.

Typical operations include:

* Add
* Remove
* Get
* Find
* Exists

Avoid exposing persistence-specific operations.

---

# Return Types

Repositories should return:

* Aggregate Roots
* Collections of Aggregate Roots
* Business identifiers
* Optional results where appropriate

Avoid returning persistence-specific objects.

---

# Async

Repository operations involving I/O should be asynchronous.

Avoid synchronous database access.

---

# Specifications

When the repository supports Specifications:

* Specifications express business intent.
* Repository implementations translate them into persistence queries.

Specifications should not expose persistence details.

---

# Error Handling

Repositories should not contain business validation.

Business failures belong to the Domain.

Infrastructure failures should be translated appropriately.

---

# Testing

Repository interfaces should be testable through Application and Domain tests.

Repository implementations should be verified using integration tests.

Avoid mocking persistence behaviour unnecessarily when integration tests provide better confidence.

---

# Anti-Patterns

Avoid:

* Generic Repository for every entity.
* CRUD repositories for child entities.
* Business logic inside repositories.
* Returning IQueryable.
* Exposing DbContext.
* SQL in the Domain.
* Persistence-specific naming.
* Repository methods that expose implementation details.

---

# Repository Checklist

Before completing a Repository, verify:

* Repository manages an Aggregate Root.
* Interface belongs to the Domain.
* Implementation belongs to Infrastructure.
* Business terminology is used.
* No business logic exists inside the Repository.
* Persistence details remain hidden.
* Async operations are used where appropriate.
* Repository does not expose ORM-specific types.

---

# Guiding Principle

A Repository exists to make persistence invisible to the business.

It should feel like accessing an in-memory collection of Aggregate Roots, regardless of how or where the data is actually stored.
