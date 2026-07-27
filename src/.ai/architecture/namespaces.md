# Namespace Conventions

Version: 1.0

---

# Purpose

This document defines the namespace conventions used throughout the repository.

A consistent namespace structure improves readability, discoverability, maintainability, and architectural clarity.

Namespaces represent architectural and business boundaries.

They must never be arbitrary.

---

# General Principles

Namespaces should:

* Reflect the architecture.
* Reflect the folder structure.
* Reflect business capabilities.
* Be predictable.
* Be stable over time.
* Avoid unnecessary depth.
* Avoid ambiguity.

---

# Namespace Hierarchy

Namespaces should follow this general hierarchy.

```
Company.Product.Layer.Feature.SubFeature
```

Example:

```
Company.Sales.Application.Commands.CreateInvoice
```

---

# Root Namespace

Every project should have a single root namespace.

Example:

```
Company.Product
```

Do not create multiple unrelated root namespaces within the same solution.

---

# Architecture Before Features

The architectural layer should appear before the feature.

Good:

```
Company.Sales.Application.Commands
```

Bad:

```
Company.Sales.Invoice.Application
```

The architectural responsibility should always be immediately visible.

---

# Folder Alignment

Namespaces must mirror the folder hierarchy.

Example:

Folder:

```
Application/

    Commands/

        CreateInvoice/
```

Namespace:

```
Company.Sales.Application.Commands.CreateInvoice
```

A developer should be able to predict the namespace from the folder structure and vice versa.

---

# Domain Layer

Typical Domain namespaces include:

```
Company.Sales.Domain.Aggregates
Company.Sales.Domain.Entities
Company.Sales.Domain.ValueObjects
Company.Sales.Domain.Events
Company.Sales.Domain.Specifications
Company.Sales.Domain.Repositories
```

The Domain namespace must never contain technology-specific terminology.

---

# Application Layer

Typical Application namespaces include:

```
Company.Sales.Application.Commands
Company.Sales.Application.Queries
Company.Sales.Application.Handlers
Company.Sales.Application.Validators
Company.Sales.Application.Authorization
Company.Sales.Application.Caching
```

---

# Infrastructure Layer

Infrastructure namespaces describe implementation details.

Examples:

```
Company.Sales.Infrastructure.Persistence
Company.Sales.Infrastructure.Repositories
Company.Sales.Infrastructure.Messaging
Company.Sales.Infrastructure.Identity
Company.Sales.Infrastructure.Logging
```

Technology-specific namespaces belong only here.

---

# API Layer

Typical API namespaces include:

```
Company.Sales.Api.Endpoints
Company.Sales.Api.Requests
Company.Sales.Api.Responses
Company.Sales.Api.Authentication
Company.Sales.Api.Middleware
```

Transport concerns should remain isolated within the API layer.

---

# Testing

Testing namespaces should mirror production namespaces.

Example:

Production:

```
Company.Sales.Application.Commands
```

Tests:

```
Company.Sales.Application.Tests.Commands
```

Developers should easily locate corresponding tests.

---

# Feature Namespaces

Feature-specific namespaces should remain concise.

Good:

```
Company.Sales.Application.Commands.CreateInvoice
```

Avoid:

```
Company.Sales.Application.Commands.Invoice.CreateInvoice.CommandImplementation
```

---

# Avoid Generic Namespaces

Do not create namespaces such as:

```
Common
General
Helpers
Utilities
Misc
Extensions
Manager
Services
```

unless they represent a well-defined architectural responsibility.

---

# Shared Components

Shared namespaces should exist only when the abstraction is genuinely shared across multiple business capabilities.

Avoid creating shared namespaces solely to reduce duplication.

---

# Namespace Depth

Keep namespace depth reasonable.

Prefer:

```
Company.Product.Application.Commands
```

Avoid:

```
Company.Product.Application.Business.Features.Commands.Internal.Handlers.Shared
```

Excessively deep namespaces reduce readability.

---

# One Responsibility

Each namespace should represent a single responsibility.

Avoid mixing unrelated concepts within the same namespace.

---

# Naming

Namespace segments should:

* Use PascalCase.
* Use singular nouns where appropriate.
* Avoid abbreviations.
* Avoid implementation-specific terminology.

---

# Global Using Directives

Use global using directives only for widely used framework namespaces.

Avoid using them for business namespaces.

Business dependencies should remain explicit.

---

# Aliases

Avoid namespace aliases unless resolving unavoidable naming conflicts.

Aliases should not hide architectural intent.

---

# Refactoring

When moving files, update namespaces to match the new folder structure.

Folder hierarchy and namespaces must always remain synchronized.

---

# Namespace Review Checklist

Before introducing a namespace, verify:

* Does it reflect the architecture?
* Does it match the folder structure?
* Does it communicate responsibility?
* Is it concise?
* Is it consistent with existing namespaces?
* Does it avoid unnecessary depth?
* Will another developer immediately understand its purpose?

---

# Guiding Principle

A namespace should communicate where a component belongs in the architecture before a developer opens the file.
