# Architecture Decision Records (ADR)

Version: 1.0

---

# Purpose

This document defines how Architectural Decision Records (ADRs) are created, maintained, and evolved within the repository.

Architecture decisions are long-lived knowledge.

Every significant architectural decision should be documented so future engineers understand **why** the decision was made—not just **what** was implemented.

---

# Primary Principle

Document decisions, not implementations.

Code shows **what** was built.

An ADR explains **why** it was built that way.

---

# When to Create an ADR

Create an ADR whenever a decision affects the long-term architecture.

Examples include:

* Selecting an architectural style.
* Choosing a persistence technology.
* Introducing a messaging platform.
* Defining module boundaries.
* Adopting CQRS.
* Introducing Event Sourcing.
* Selecting authentication mechanisms.
* Choosing a caching strategy.
* Selecting testing architecture.
* Establishing deployment strategy.

Routine implementation decisions do not require ADRs.

---

# ADR Structure

Every ADR should contain:

* Title
* Status
* Date
* Context
* Decision
* Consequences
* Alternatives Considered
* References (optional)

---

# Status

Typical statuses include:

* Proposed
* Accepted
* Superseded
* Deprecated
* Rejected

Only one active ADR should define the current architectural decision.

---

# Context

The Context explains:

* The business problem.
* Existing constraints.
* Technical limitations.
* Requirements.
* Risks.

The Context explains **why the decision was necessary**.

---

# Decision

The Decision describes:

* What was chosen.
* Why it was chosen.
* Scope of the decision.
* Expected impact.

The Decision should be explicit.

---

# Consequences

Every decision has consequences.

Document both:

Positive:

* Simplicity
* Scalability
* Maintainability

Negative:

* Complexity
* Migration cost
* Performance trade-offs
* Operational impact

---

# Alternatives

Document realistic alternatives that were considered.

For each alternative, explain:

* Advantages
* Disadvantages
* Why it was rejected

This preserves architectural reasoning.

---

# Repository Location

ADRs should be stored in a dedicated folder.

Example:

```text id="j6h2rk"
docs/

    adr/

        ADR-0001-clean-architecture.md

        ADR-0002-cqrs.md

        ADR-0003-modular-monolith.md
```

Number ADRs sequentially.

---

# Naming

ADR titles should describe the decision.

Good:

```text id="g7m4yn"
Adopt CQRS

Use Modular Monolith

Use Strongly Typed IDs

Choose PostgreSQL
```

Avoid vague names such as:

```text id="m5r8pa"
Architecture

Decision

Update
```

---

# Immutability

Accepted ADRs should never be rewritten.

If a decision changes:

* Create a new ADR.
* Mark the previous ADR as Superseded.
* Reference the replacement.

Architectural history should remain preserved.

---

# Review

Architectural decisions should be reviewed before acceptance.

Major decisions affect the entire repository.

Consensus is preferred.

---

# Relationship to Code

The repository should reflect accepted ADRs.

If code contradicts an accepted ADR:

* Update the code, or
* Create a new ADR replacing the old decision.

Architecture documentation must remain truthful.

---

# AI Responsibilities

Before introducing a significant architectural change, the AI should determine:

* Does an ADR already exist?
* Does the implementation follow the ADR?
* Does the new proposal require a new ADR?

The AI should never silently contradict established architecture.

---

# Evolution

Architecture evolves through ADRs.

Each significant decision becomes part of the repository's engineering history.

Future engineers should be able to understand the evolution of the architecture by reading the ADRs.

---

# Anti-Patterns

Avoid:

* Undocumented architectural decisions.
* Rewriting accepted ADRs.
* Contradicting accepted ADRs.
* Documenting implementation details instead of decisions.
* Creating ADRs for minor coding choices.
* Removing historical architectural knowledge.

---

# ADR Checklist

Before accepting an ADR, verify:

* The problem is clearly described.
* The decision is explicit.
* Alternatives were considered.
* Consequences are documented.
* Status is correct.
* Repository structure reflects the decision.
* Future engineers can understand the reasoning.

---

# Guiding Principle

Architectural decisions outlive individual implementations.

A well-maintained ADR repository allows future engineers—and AI agents—to understand not only how the system is built, but why it was designed that way.
