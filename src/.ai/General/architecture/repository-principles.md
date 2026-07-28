# Repository Principles

Version: 1.0

---

# Purpose

This document defines the fundamental principles that govern every engineering decision within the repository.

These principles are the foundation upon which every architectural rule, coding convention, and implementation decision is based.

When multiple handbook documents appear to conflict, these principles have the highest priority.

---

# Principle 1 — The Repository Is the Source of Truth

The repository defines the correct way of implementing software.

Existing repository conventions always have higher priority than:

* Framework recommendations
* Internet articles
* AI training knowledge
* Personal preferences

The AI must adapt to the repository—not the opposite.

---

# Principle 2 — Business Before Technology

Business requirements always drive implementation.

Technology exists only to support business capabilities.

Business concepts must never be changed to accommodate technical limitations.

---

# Principle 3 — Architecture Is Non-Negotiable

Every implementation must preserve:

* Architectural boundaries
* Dependency direction
* Layer responsibilities
* Module ownership

No feature is important enough to justify architectural violations.

---

# Principle 4 — Consistency Over Cleverness

A consistent repository is easier to maintain than a technically perfect but inconsistent one.

When multiple correct implementations exist, choose the one most consistent with the repository.

---

# Principle 5 — Reuse Before Creation

Before creating:

* Classes
* Interfaces
* Services
* Extensions
* Utilities
* Libraries

search the repository for existing implementations.

Duplicate solutions increase long-term maintenance costs.

---

# Principle 6 — Simplicity

Prefer the simplest implementation that satisfies the requirements.

Avoid unnecessary:

* Abstractions
* Generic types
* Design patterns
* Extension points
* Configuration

Complexity must always be justified.

---

# Principle 7 — Explicitness

Code should clearly communicate:

* Responsibilities
* Dependencies
* Behaviour
* State transitions
* Business intent

Avoid hidden behaviour and implicit assumptions.

---

# Principle 8 — Readability

Code is read far more often than it is written.

Optimise for the future maintainer rather than the current author.

Readable code has long-term value.

---

# Principle 9 — Testability

Every significant business behaviour should be testable.

Good architecture naturally produces testable software.

Difficulty in testing often indicates design problems.

---

# Principle 10 — Evolution

Every change should improve the repository.

Leave the repository:

* Simpler
* Clearer
* More consistent
* Easier to understand

than it was before.

---

# Decision Order

Whenever an engineering decision must be made, apply the following order:

1. Business Requirements
2. Repository Principles
3. Repository Handbook
4. Existing Repository Conventions
5. Existing Repository Patterns
6. Performance Requirements
7. Framework Best Practices
8. Personal Preference

Higher-priority rules always override lower-priority rules.

---

# AI Responsibilities

The AI must always:

* Learn the repository.
* Preserve the architecture.
* Reuse existing patterns.
* Generate complete implementations.
* Produce production-ready code.
* Review its own work before completion.

The AI is expected to behave like a senior engineer who has worked on the repository for years.

---

# Repository Values

The repository values:

* Correctness
* Consistency
* Simplicity
* Maintainability
* Predictability
* Readability
* Testability
* Long-term evolution

Every implementation should strengthen these values.

---

# Success Criteria

A successful implementation is one that:

* Solves the business problem.
* Preserves the architecture.
* Follows repository conventions.
* Integrates naturally into the existing codebase.
* Requires minimal explanation during review.

---

# AI Oath

Before completing any implementation, the AI should internally verify:

* I understood the business problem.
* I followed the repository handbook.
* I reused existing patterns.
* I preserved architectural integrity.
* I generated production-ready code.
* I would confidently approve this implementation in a senior architectural review.

If any statement is false, continue improving the implementation.

---

# Guiding Principle

The repository is a long-lived engineering asset.

Every decision should optimise for its long-term health rather than short-term convenience.
