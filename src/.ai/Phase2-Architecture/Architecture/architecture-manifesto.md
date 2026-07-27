# Architecture Manifesto

Version: 1.0

---

# Purpose

This document defines the architectural philosophy of this repository.

Unlike the other documents, this is **not** a list of rules.

It explains the beliefs that drive every architectural decision.

Every engineer and every AI agent working on this repository should understand and internalise these principles before writing code.

---

# We Build Software Around the Business

Software exists to solve business problems.

The Domain is the product.

Everything else exists to support it.

When a technical decision conflicts with the business model, the business model always wins.

---

# Business Rules Must Survive Technology

Databases change.

Frameworks change.

Message brokers change.

Cloud providers change.

Programming languages evolve.

Business rules live for decades.

Therefore, business rules must never depend on technology.

Technology is replaceable.

Business knowledge is not.

---

# Architecture Exists to Reduce Change Cost

The purpose of architecture is not to make software "beautiful."

Its purpose is to make change inexpensive.

Every architectural decision should answer one question:

> Will this make the next change easier or harder?

If the answer is harder, reconsider the design.

---

# Simplicity Beats Cleverness

Readable software outlives clever software.

The best solution is usually the one that:

* is easiest to understand,
* has the fewest moving parts,
* introduces the least unnecessary abstraction,
* communicates its intent clearly.

Complexity must always justify itself.

---

# Behaviour Defines Objects

Objects are not containers for data.

Objects exist to perform behaviour.

Whenever behaviour becomes detached from the data it governs, the design begins to deteriorate.

Business behaviour belongs where the business state lives.

---

# Every Module Owns Its Business

Business ownership is explicit.

Every concept belongs to exactly one module.

No module owns another module's business.

Communication happens through contracts—not implementation details.

Ownership prevents chaos.

---

# Boundaries Matter

Every architectural boundary exists for a reason.

Examples include:

* Layer boundaries
* Module boundaries
* Aggregate boundaries
* Transaction boundaries
* Context boundaries

Ignoring boundaries creates coupling.

Respecting boundaries creates maintainability.

---

# Consistency Is a Feature

A consistent repository is easier to:

* understand,
* navigate,
* review,
* extend,
* automate.

Consistency is more valuable than individual preference.

Engineers and AI agents should extend existing patterns before introducing new ones.

---

# Explicit Is Better Than Implicit

Hidden behaviour creates confusion.

Important architectural decisions should always be visible.

Examples include:

* explicit dependencies,
* explicit transactions,
* explicit mappings,
* explicit contracts,
* explicit boundaries.

The architecture should explain itself.

---

# Testing Is Part of the Design

Untestable software is poorly designed software.

Every architectural decision should improve:

* isolation,
* determinism,
* repeatability,
* confidence.

Testing is not a separate activity.

It is a design constraint.

---

# AI Is an Engineering Multiplier

AI is not a replacement for architecture.

AI accelerates implementation.

Architecture guides implementation.

An AI agent should generate code that looks as though it was written by the engineering team—not by a different author.

Consistency is the primary objective.

---

# We Optimise for the Next Ten Years

Short-term convenience should never damage long-term maintainability.

When choosing between:

* a shortcut,
* or a sustainable design,

prefer the sustainable design.

The repository is expected to evolve for many years.

---

# Software Should Explain Itself

A new engineer should understand the repository by reading:

* project structure,
* naming,
* architecture,
* module organisation,
* business language.

Documentation supports understanding.

Architecture should make documentation almost unnecessary.

---

# We Prefer Evolution Over Revolution

Architecture should evolve continuously.

Large rewrites are signs that previous evolution failed.

Small, safe, incremental improvements are preferred over dramatic redesigns.

---

# Engineering Decisions Are Intentional

Nothing enters the repository accidentally.

Every:

* dependency,
* abstraction,
* package,
* interface,
* framework,
* pattern,

must have a clear purpose.

If a decision cannot be justified, it should not exist.

---

# AI Responsibilities

Every AI agent working in this repository must:

* Preserve the architecture.
* Respect the Dependency Rule.
* Follow the Ubiquitous Language.
* Reuse established patterns.
* Protect business invariants.
* Generate production-quality code.
* Avoid speculative abstractions.
* Prefer clarity over cleverness.
* Leave the repository more consistent than it was before.

---

# Manifesto Summary

We believe:

* Business is the centre.
* Architecture protects the business.
* Simplicity scales.
* Consistency compounds.
* Boundaries matter.
* Behaviour belongs in the Domain.
* Technology is replaceable.
* Good software evolves.
* AI should amplify engineering discipline—not replace it.

---

# Final Principle

Every line of code should make the repository easier to understand, easier to change, and easier to trust.

If an implementation does not achieve all three, it is not finished.
