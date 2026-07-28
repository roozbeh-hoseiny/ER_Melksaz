# Refactoring Rules

Version: 1.0

---

# Purpose

This document defines how an AI agent should perform refactoring within this repository.

Refactoring improves the internal structure of the code without changing its externally observable behaviour.

Every refactoring should increase maintainability while preserving correctness.

---

# Primary Principle

Refactor structure—not behaviour.

Unless explicitly requested, a refactoring must not change business functionality or public contracts.

---

# Preserve Behaviour

The AI must preserve:

* Business behaviour
* Public APIs
* Integration contracts
* Database semantics
* External interfaces

Any intentional behavioural change must be explicitly identified.

---

# Understand Before Refactoring

Before proposing changes, the AI must understand:

* The existing implementation
* The architectural intent
* Dependencies
* Business purpose
* Existing repository conventions

Never refactor code that has not been understood.

---

# Small Incremental Changes

Prefer multiple small refactorings over one large transformation.

Each refactoring should have a single objective.

Examples:

* Improve naming
* Extract a method
* Remove duplication
* Simplify logic
* Improve cohesion

Avoid combining unrelated refactorings.

---

# Preserve Architecture

Refactoring must not violate:

* Clean Architecture
* Dependency Rule
* DDD boundaries
* Module ownership
* Repository conventions

Architecture should improve—not degrade.

---

# Simplify

The preferred direction of every refactoring is toward simplicity.

Reduce:

* Complexity
* Nesting
* Coupling
* Duplication
* Cognitive load

Avoid replacing simple code with clever code.

---

# Improve Readability

Prefer:

* Clear names
* Small methods
* Explicit control flow
* Focused responsibilities

Future maintainers should understand the code quickly.

---

# Remove Duplication

Duplicate logic should be consolidated when it represents the same responsibility.

Avoid creating abstractions for coincidental similarity.

Only extract common behaviour when it has a shared meaning.

---

# Respect Existing Patterns

Follow repository conventions.

Reuse:

* Existing abstractions
* Existing extension points
* Existing utilities
* Existing naming conventions

Avoid introducing competing patterns.

---

# Avoid Premature Abstractions

Do not introduce:

* Generic base classes
* Generic repositories
* Framework-like utilities
* Additional interfaces

unless a demonstrated need exists.

---

# Preserve Public Contracts

When refactoring public APIs:

* Preserve signatures whenever practical.
* Preserve semantics.
* Maintain backward compatibility.

Breaking changes require explicit justification.

---

# Maintain Testability

Refactoring should improve or preserve testability.

Dependencies should remain explicit.

Hidden dependencies should be eliminated.

---

# Remove Dead Code

Dead code should be removed when it is clearly unused.

Examples:

* Unreachable branches
* Obsolete methods
* Unused private fields
* Redundant abstractions

Do not remove code when its usage is uncertain without confirmation.

---

# Naming Improvements

Improve names when they:

* Better reflect intent.
* Use the Ubiquitous Language.
* Reduce ambiguity.
* Increase clarity.

Avoid renaming solely for stylistic preference.

---

# Extract Methods

Extract methods when they:

* Improve readability.
* Reduce duplication.
* Represent a meaningful concept.

Avoid extracting trivial one-line methods with no semantic value.

---

# Class Decomposition

Large classes should be decomposed when they have multiple responsibilities.

New classes should have:

* Clear ownership
* Focused responsibilities
* Meaningful names

---

# Preserve Performance

Do not sacrifice performance significantly unless readability gains clearly justify the trade-off.

Likewise, do not sacrifice readability for negligible performance improvements.

---

# Update Supporting Artifacts

When refactoring affects public behaviour or architecture, update:

* Tests
* Documentation
* ADRs (when appropriate)
* Configuration
* Generated code (if applicable)

Keep all supporting artifacts consistent.

---

# AI Responsibilities

When refactoring, the AI must:

* Preserve behaviour.
* Reduce complexity.
* Improve readability.
* Remove duplication.
* Respect architecture.
* Follow repository conventions.
* Keep changes focused.

---

# Anti-Patterns

Avoid:

* Behavioural changes disguised as refactoring.
* Large unrelated refactorings.
* Introducing unnecessary abstractions.
* Breaking public contracts.
* Mixing formatting changes with structural changes.
* Refactoring code that is not understood.
* Architectural regressions.

---

# Refactoring Checklist

Before completing a refactoring, verify:

* Behaviour is preserved.
* Architecture is respected.
* Complexity has been reduced.
* Readability has improved.
* Duplication has been removed where appropriate.
* Tests remain valid.
* Supporting documentation is updated if necessary.
* Repository conventions are followed.

---

# Guiding Principle

A successful refactoring leaves the system easier to understand, easier to maintain, and easier to extend—while behaving exactly as it did before the refactoring.
