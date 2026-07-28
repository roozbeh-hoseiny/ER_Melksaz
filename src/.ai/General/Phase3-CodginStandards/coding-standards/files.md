# File Organization

Version: 1.0

---

# Purpose

This document defines how source files are organized throughout the repository.

A consistent file organization improves readability, navigation, code reviews, and AI-generated implementations.

---

# Primary Principle

One file should have one clear responsibility.

Each file should communicate its purpose immediately.

---

# One Public Type Per File

Every public type should normally have its own file.

Examples:

```text id="a9m2xh"
Invoice.cs

Customer.cs

CreateInvoiceHandler.cs

InvoiceRepository.cs
```

Avoid placing multiple unrelated public types in the same file.

---

# File Name

The file name must exactly match the primary type.

Good:

```text id="f6j8wd"
Invoice.cs

CustomerId.cs

ApproveInvoiceHandler.cs
```

Bad:

```text id="r4q3yn"
Models.cs

Helpers.cs

Classes.cs
```

---

# File Size

Files should remain focused.

Recommended target:

* Under 300 lines.
* Under 500 lines unless justified.

Large files usually indicate multiple responsibilities.

---

# File Responsibility

Each file should represent exactly one concept.

Examples:

* One Aggregate
* One Entity
* One Value Object
* One Handler
* One Endpoint
* One Repository

Avoid combining unrelated concepts.

---

# Namespace

The namespace should match the folder structure.

Example:

```text id="u7c1ke"
Modules.Billing.Application.Features.CreateInvoice
```

Namespaces should reflect architectural ownership.

---

# Using Directives

Place using directives at the top of the file.

Order:

1. System namespaces
2. Microsoft namespaces
3. Third-party namespaces
4. Solution namespaces

Separate groups with a blank line.

Example:

```text id="n8v4ta"
using System;
using System.Threading;

using Microsoft.Extensions.Logging;

using FluentValidation;

using Billing.Domain;
```

---

# File Layout

Recommended order:

```text id="j3w7ps"
using statements

namespace

type declaration

fields

constructors

properties

methods

private methods
```

Use the same order consistently.

---

# Regions

Do not use `#region` to hide poor organization.

If a file requires regions to be understandable, split the file into smaller files.

Regions should be rare.

---

# Partial Classes

Avoid partial classes unless required by:

* Source generators
* Designer-generated code
* Framework-generated code

Business code should not rely on partial classes.

---

# Nested Types

Avoid nested classes unless they are tightly coupled and private.

Public nested types reduce readability.

---

# Comments

Code should be self-explanatory.

Prefer good naming over comments.

Comments should explain:

* Why something exists.
* Why an unusual decision was made.

Avoid comments that simply restate the code.

---

# File Ordering

Within a folder, files should be ordered naturally by feature or business concept.

Avoid prefixes such as:

```text id="q6r9mv"
01_

02_

03_
```

The file system should not encode execution order.

---

# Generated Files

Generated code should be placed in dedicated locations when possible.

Generated files should:

* Be clearly identifiable.
* Not be manually edited.
* Be regenerated rather than modified.

---

# Test Files

Test file names should match the production type.

Examples:

```text id="p5y2zd"
InvoiceTests.cs

ApproveInvoiceHandlerTests.cs

CustomerRepositoryTests.cs
```

The relationship should be obvious.

---

# Extension Methods

Each extension class should have its own file.

Example:

```text id="x2h8lg"
ServiceCollectionExtensions.cs

EndpointRouteBuilderExtensions.cs
```

Avoid grouping unrelated extension methods.

---

# Constants

Avoid large constants files.

Constants should remain close to the business concepts that use them.

---

# Helpers

Avoid generic helper files.

Instead of:

```text id="m1a6vr"
Helpers.cs

Utilities.cs

Common.cs
```

Create purpose-specific types with meaningful names.

---

# File Organization Checklist

Before completing a file, verify:

* One primary responsibility.
* File name matches the primary type.
* Namespace matches the folder.
* File remains reasonably small.
* No unnecessary regions.
* No unrelated types.
* Existing repository layout has been followed.

---

# Guiding Principle

A developer should understand a file's purpose from its name, location, and primary type before reading its implementation.
