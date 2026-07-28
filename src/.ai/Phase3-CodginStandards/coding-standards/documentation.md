# Documentation

Version: 1.0

---

# Purpose

This document defines the mandatory documentation standards for the repository.

Documentation explains **why** the system exists, **how** it is intended to be used, and **where** important decisions were made.

Good documentation reduces onboarding time, improves maintainability, and preserves architectural knowledge.

---

# Primary Principle

Code explains **how**.

Documentation explains **why**.

Do not duplicate implementation details already obvious from the code.

---

# Documentation Hierarchy

Documentation should exist at the appropriate level.

Typical hierarchy:

* Repository documentation
* Architecture documentation
* Module documentation
* Public API documentation
* Code comments

Lower-level documentation should never replace higher-level documentation.

---

# Repository Documentation

Every repository should contain a `README.md`.

It should include:

* Purpose
* Project structure
* Prerequisites
* Build instructions
* Running the application
* Running tests
* Configuration overview
* Useful links

The README is the primary entry point for new contributors.

---

# Architecture Documentation

Architecture decisions should be documented separately from implementation.

Examples include:

* Clean Architecture
* DDD boundaries
* CQRS
* Event flows
* Deployment topology
* Integration patterns

Architecture documentation should evolve with the system.

---

# ADRs

Significant architectural decisions should be recorded as Architecture Decision Records (ADRs).

Each ADR should include:

* Context
* Decision
* Consequences
* Alternatives considered

Do not bury important architectural decisions in pull requests.

---

# Module Documentation

Large modules should include documentation describing:

* Responsibilities
* Public interfaces
* Dependencies
* Extension points
* Integration boundaries

---

# Public APIs

Public APIs should provide XML documentation where repository conventions require it.

Documentation should explain:

* Purpose
* Parameters
* Return values
* Exceptions (when relevant)

Avoid repeating method names.

---

# Code Comments

Comments should explain:

* Why something exists.
* Why an unusual approach was chosen.
* Business rationale.
* Performance trade-offs.
* Security considerations.

Avoid comments that simply restate the code.

Bad:

```csharp id="q6m1zr"
// Increment i
i++;
```

Good:

```csharp id="x4v8ta"
// Retry is limited to avoid duplicate payment processing.
```

---

# TODO Comments

Avoid permanent TODO comments.

If work must be deferred:

* Create a work item.
* Reference the issue identifier.

Example:

```text id="v8q2py"
TODO: Remove temporary compatibility layer. (#421)
```

---

# XML Documentation

Public libraries should document public types when appropriate.

XML documentation should remain concise and accurate.

Do not document obvious implementation details.

---

# Generated Code

Generated code should clearly indicate:

* It is generated.
* The generator responsible.
* Whether manual modifications are allowed.

Avoid editing generated files manually.

---

# Examples

Documentation should include examples when they significantly improve understanding.

Examples should remain minimal and executable where practical.

---

# Diagrams

Architecture diagrams should be maintained for complex systems.

Examples:

* C4 diagrams
* Sequence diagrams
* Deployment diagrams
* Event flow diagrams

Diagrams should reflect the current implementation.

---

# Change Documentation

Major architectural changes should update:

* README
* ADRs
* Architecture documents
* Module documentation

Documentation should evolve alongside code.

---

# Naming

Documentation should use repository terminology consistently.

Avoid introducing alternative names for the same concept.

Use the Ubiquitous Language defined by the Domain.

---

# External References

When referencing external standards, include:

* Official specifications
* RFCs
* Vendor documentation

Avoid relying on personal blog posts as authoritative references.

---

# AI Responsibilities

When generating documentation, the AI must:

* Explain intent rather than implementation.
* Preserve repository terminology.
* Keep documentation concise.
* Update related documents when architecture changes.
* Avoid duplicating code.

---

# Anti-Patterns

Avoid:

* Outdated documentation.
* Comments explaining obvious code.
* Large blocks of commented-out code.
* TODOs without tracking.
* Documentation copied from implementation.
* Missing architectural rationale.

---

# Documentation Checklist

Before completing documentation, verify:

* Purpose is clearly explained.
* Repository terminology is consistent.
* Architectural decisions are documented.
* Examples are accurate.
* Comments explain *why*, not *what*.
* Documentation matches the current implementation.

---

# Guiding Principle

Documentation should preserve knowledge that cannot be reliably inferred from the source code.

If the code changes, the documentation should change with it.
