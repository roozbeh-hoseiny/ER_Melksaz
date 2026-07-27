# AI Prompting Guide

Version: 1.0

---

# Purpose

This document defines how developers should communicate with AI agents when working with this repository.

A well-written prompt produces better software.

The AI should receive enough information to understand:

* Business objective
* Architectural constraints
* Repository conventions
* Expected deliverables
* Success criteria

---

# Primary Principle

Never ask the AI to "write some code."

Instead, describe the business capability that should be implemented.

The AI should determine the implementation details by following the repository handbook.

---

# Preferred Prompt Structure

Every implementation request should include, when applicable:

* Business objective
* Existing feature or new feature
* Module
* Expected behaviour
* Constraints
* Deliverables

---

# Example

Good

```text
Implement the Invoice aggregate.

The aggregate belongs to the Billing module.

It must follow all repository conventions.

Generate every required artefact including:

- Aggregate
- Commands
- Queries
- Validators
- Repository
- EF Configuration
- API Endpoints
- Unit Tests
- Integration Tests
```

Bad

```text
Create Invoice.cs
```

---

# Describe Business Behaviour

Prefer describing business behaviour.

Good

```text
An invoice can only be cancelled if it has not been paid.
```

Avoid describing implementation.

Bad

```text
Add an IsCancelled property.
```

Business requirements should drive implementation.

---

# Reference Existing Features

Whenever possible, reference existing repository features.

Example

```text
Implement Payments using the same architectural style as Orders.
```

The AI should reuse repository conventions instead of inventing new ones.

---

# Ask for Complete Features

Prefer requesting complete features rather than individual files.

Good

```text
Implement Customer Management.
```

Instead of

```text
Create Customer.cs
```

The AI should determine every required artefact.

---

# Request Architectural Compliance

Explicitly instruct the AI to follow the repository handbook.

Example

```text
Follow every handbook document under .ai/.
```

This reminds the AI to prioritise repository conventions over generic best practices.

---

# Specify Constraints

Include important constraints.

Examples include:

* No reflection.
* No MediatR.
* Use Minimal API.
* Use Result Pattern.
* Use Clean Architecture.
* No external libraries.
* Support AOT.
* Must be thread-safe.

Constraints prevent incorrect assumptions.

---

# Request Production-Ready Code

Prefer prompts such as:

```text
Generate production-ready code.
```

instead of

```text
Generate sample code.
```

The repository should never contain demonstration implementations.

---

# Ask for Tests

Always request tests.

Example

```text
Include:

- Unit Tests
- Integration Tests
```

Testing should be considered part of implementation.

---

# Ask for Documentation

For significant features, request documentation.

Examples include:

* Architecture updates
* ADRs
* README changes
* API documentation

---

# Encourage Repository Reuse

The AI should first search for:

* Existing abstractions
* Existing services
* Existing utilities
* Existing repositories
* Existing tests

before creating new implementations.

---

# Request Self-Review

A good prompt includes:

```text
Review the generated implementation against every handbook document before finishing.
```

This reduces inconsistencies.

---

# Avoid Implementation Bias

Avoid instructing the AI to use specific patterns unless required.

Instead of:

```text
Use Repository Pattern.
```

Prefer:

```text
Follow repository conventions.
```

The repository should decide the implementation.

---

# Feature Prompt Template

A recommended feature request:

```text
Implement <Business Capability>.

Requirements:

- Follow every handbook document.
- Preserve the architecture.
- Reuse existing patterns.
- Generate every required artefact.
- Generate unit tests.
- Generate integration tests.
- Update documentation where required.
- Produce production-ready code.
```

---

# Bug Fix Prompt Template

```text
Fix the reported defect.

Requirements:

- Identify the root cause.
- Preserve existing behaviour.
- Avoid unrelated refactoring.
- Add regression tests.
- Review the implementation before completion.
```

---

# Refactoring Prompt Template

```text
Refactor this feature.

Requirements:

- Preserve behaviour.
- Improve readability.
- Reduce complexity.
- Reduce duplication.
- Preserve public contracts.
- Follow repository conventions.
```

---

# AI Expectations

The AI is expected to:

* Analyse before coding.
* Search before creating.
* Reuse before inventing.
* Review before finishing.

Generating code is only one part of the workflow.

---

# Review Checklist

Before sending a prompt to the AI, verify:

* Does it describe the business objective?
* Does it identify the module?
* Does it define constraints?
* Does it request complete implementation?
* Does it request tests?
* Does it request architectural compliance?

Better prompts produce better software.

---

# Guiding Principle

Treat the AI as a senior software engineer, not a code generator.

Describe **what** the business needs.

Allow the repository handbook to determine **how** it should be implemented.
