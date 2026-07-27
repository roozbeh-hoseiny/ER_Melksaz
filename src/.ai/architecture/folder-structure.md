# Folder Structure

Version: 1.0

---

# Purpose

This document defines the folder organization rules for the repository.

A predictable folder structure improves discoverability, maintainability, onboarding, and AI-assisted development.

Every project within the repository should follow these conventions unless explicitly documented otherwise.

---

# Guiding Principles

The folder structure should:

* Reflect the architecture.
* Reflect the business domain.
* Be predictable.
* Minimise navigation time.
* Group related components together.
* Avoid unnecessary nesting.
* Remain stable over time.

---

# Architecture First

Folders represent architectural responsibilities before implementation details.

Prefer:

```
Domain/
Application/
Infrastructure/
Api/
```

Avoid:

```
Classes/
Helpers/
Managers/
Services/
Misc/
Common/
```

---

# Business Before Technology

Business capabilities should define folder organization.

Prefer:

```
Invoices/
Customers/
Products/
Payments/
```

Avoid:

```
Sql/
Redis/
Grpc/
Utilities/
```

at the business level.

Technology belongs inside Infrastructure.

---

# Consistency

The same concept should always appear in the same location.

Developers should never have to guess where a file belongs.

Consistency is more important than personal preference.

---

# One Responsibility Per Folder

Each folder should represent one responsibility.

Avoid folders containing unrelated concepts.

Bad example:

```
Common/
```

Good example:

```
Validators/
Commands/
Queries/
Repositories/
```

---

# Shallow Hierarchies

Prefer shallow folder structures.

Avoid excessive nesting.

Good:

```
Application/
    Commands/
    Queries/
    Validators/
```

Avoid:

```
Application/

    Business/

        Features/

            Invoice/

                Commands/

                    Create/

                        Internal/

                            Handlers/
```

---

# Domain Layer

The Domain layer should contain only business concepts.

Typical folders include:

```
Aggregates/
Entities/
ValueObjects/
Events/
Specifications/
Factories/
Services/
Exceptions/
```

No infrastructure concerns belong here.

---

# Application Layer

The Application layer coordinates business use cases.

Typical folders include:

```
Commands/
Queries/
Handlers/
Validators/
Behaviors/
Mappings/
Authorization/
Caching/
```

Organize by responsibility rather than technology.

---

# Infrastructure Layer

Infrastructure contains implementation details.

Typical folders include:

```
Persistence/
Repositories/
Configurations/
Messaging/
Caching/
Identity/
Logging/
Telemetry/
Storage/
Services/
```

Technology-specific code belongs here.

---

# API Layer

The API layer exposes application functionality.

Typical folders include:

```
Endpoints/
Contracts/
Requests/
Responses/
Filters/
Middleware/
OpenApi/
Authentication/
```

Keep transport concerns isolated.

---

# Tests

Tests should mirror the production structure.

Example:

```
Domain/

    Invoice.cs

↓

Tests/

    Domain/

        InvoiceTests.cs
```

Developers should easily locate corresponding tests.

---

# Feature Organization

Related files should remain close together.

When using feature-based organization, group:

* Commands
* Queries
* Validators
* Handlers
* Tests

around the same business capability.

---

# Avoid Generic Folders

Do not create folders named:

```
Helpers
Utils
Misc
General
Stuff
Manager
Temp
Old
New
```

Folder names should communicate responsibility.

---

# File Placement

Every file should have exactly one logical location.

Avoid duplicate implementations.

If multiple folders appear appropriate, reconsider the design.

---

# Namespace Alignment

Folder hierarchy should match namespace hierarchy.

Example:

```
Application/

    Commands/

        CreateInvoice/

            CreateInvoiceCommand.cs
```

Namespace:

```
Company.Project.Application.Commands.CreateInvoice
```

Folder structure and namespaces should evolve together.

---

# Growth

New folders should only be introduced when they represent a new architectural responsibility.

Do not create folders for a single file unless future growth is expected.

---

# Module Independence

Business modules should avoid sharing implementation folders.

Each module should own its own structure.

Avoid central "shared" folders for business logic.

---

# Documentation

Every top-level architectural folder should have a clear and well-defined purpose.

Developers should understand where new code belongs without consulting existing implementations.

---

# Folder Review Checklist

Before creating a new folder, verify:

* Does it represent a single responsibility?
* Does it align with the architecture?
* Does it use business terminology where appropriate?
* Is there an existing folder that already serves this purpose?
* Will another developer immediately understand its purpose?
* Does it minimise future maintenance?

Only create the folder if every answer supports the architectural goals.

---

# Guiding Principle

A developer—or an AI agent—should be able to predict the location of any file before searching for it.
