# Folder Structure

Version: 1.0

---

# Purpose

This document defines the mandatory folder organization inside projects.

A consistent folder structure improves discoverability, reduces cognitive load, and allows both engineers and AI agents to locate code quickly.

---

# Primary Principle

Folders represent architectural or business responsibilities.

They must never be created merely for convenience.

---

# General Rules

Folders should:

* Have one clear responsibility.
* Use consistent naming.
* Follow the architectural structure.
* Reflect business concepts where appropriate.

Avoid arbitrary nesting.

---

# Domain Project

Typical structure:

```text id="v3k8pw"
Domain/

    Aggregates/

    Entities/

    ValueObjects/

    Events/

    Services/

    Specifications/

    Repositories/

    Exceptions/

    Enums/
```

Organize by business concept if the module grows large.

---

# Application Project

Typical structure:

```text id="t6a9rd"
Application/

    Commands/

    Queries/

    Behaviors/

    Validators/

    Interfaces/

    Services/

    Transactions/
```

If using Vertical Slice Architecture, organize by feature instead of technical folders.

---

# Vertical Slice Structure

Preferred for medium and large modules:

```text id="g8y4mk"
Features/

    CreateInvoice/

        Command.cs

        Handler.cs

        Validator.cs

        Endpoint.cs

    ApproveInvoice/

        Command.cs

        Handler.cs

        Endpoint.cs
```

Everything for one use case lives together.

---

# Infrastructure Project

Typical structure:

```text id="e5n1zc"
Infrastructure/

    Persistence/

    Repositories/

    Configurations/

    Migrations/

    Messaging/

    Authentication/

    Authorization/

    Caching/

    Logging/

    Services/

    External/
```

Infrastructure folders should reflect technical concerns.

---

# API Project

Typical structure:

```text id="x7u3af"
Api/

    Endpoints/

    Middleware/

    Filters/

    Authentication/

    Authorization/

    Requests/

    Responses/

    OpenApi/
```

Avoid placing business logic here.

---

# Contracts Project

Typical structure:

```text id="m9k6wb"
Contracts/

    Events/

    Requests/

    Responses/

    Messages/

    Grpc/
```

Contracts should remain stable.

---

# Tests

Mirror the production structure whenever practical.

Example:

```text id="d2q8sy"
Features/

    CreateInvoice/

        CreateInvoiceTests.cs

    ApproveInvoice/

        ApproveInvoiceTests.cs
```

Related tests stay together.

---

# Documentation

Documentation should have dedicated folders.

Example:

```text id="h4z7vo"
docs/

    architecture/

    adr/

    api/

    deployment/

    operations/
```

---

# AI Documentation

The AI handbook should remain structured.

Example:

```text id="r8m1lx"
.ai/

    core/

    architecture/

    coding-standards/

    testing/

    patterns/

    templates/
```

---

# Naming

Folder names should:

* Use PascalCase where project conventions require it.
* Otherwise follow existing repository conventions.
* Avoid abbreviations.
* Clearly describe their responsibility.

---

# Folder Depth

Avoid excessive nesting.

Recommended maximum:

```text id="k3n5qw"
Module/

    Feature/

        Handler.cs
```

Deep folder hierarchies reduce discoverability.

---

# Feature Ownership

Each feature folder owns all files required for that feature.

Avoid scattering feature files across unrelated folders.

---

# Shared Folders

Avoid generic folders such as:

* Common
* Utils
* Helpers
* Misc
* Temp

Instead, create folders that communicate responsibility.

---

# When to Create a Folder

Create a new folder only when:

* It groups related concepts.
* It improves discoverability.
* It reduces complexity.
* It has a clear long-term purpose.

Do not create folders for one file unless a clear growth path exists.

---

# Anti-Patterns

Avoid:

* Deep nesting.
* Empty folders.
* Temporary folders.
* Generic names.
* Mixing technical and business concerns.
* Organizing by file type when organizing by feature is more appropriate.

---

# Folder Structure Checklist

Before adding a new folder, verify:

* It has one responsibility.
* Its name is descriptive.
* It follows repository conventions.
* It improves navigation.
* It does not introduce unnecessary hierarchy.
* Existing folders cannot reasonably contain the new files.

---

# Guiding Principle

A developer should be able to locate any file in the repository by understanding the architecture—not by memorizing folder paths.
