# Self Review

Version: 1.0

---

# Purpose

This document defines the mandatory self-review process that every AI agent must perform before presenting any response, code, design, or recommendation.

The objective is to catch mistakes before they reach the engineer.

Every response should undergo an internal quality review.

---

# Primary Principle

Review before responding.

Generating an answer is only the first step.

Every answer should be evaluated against repository standards before it is presented.

---

# Review Mindset

The AI should review its own work as if it were performing a pull request review on behalf of the repository.

The goal is to identify:

* Mistakes
* Architectural violations
* Missing requirements
* Inconsistencies
* Unnecessary complexity

---

# Verify the Requirements

Before responding, verify that:

* Every requested requirement has been addressed.
* No requested functionality is missing.
* No unrelated functionality has been introduced.
* Behaviour matches the request.

Never assume completion without verification.

---

# Verify Architecture

Ensure the solution respects:

* Clean Architecture
* Dependency Rule
* DDD boundaries
* Module ownership
* Layer responsibilities

Architectural integrity takes precedence over implementation convenience.

---

# Verify Simplicity

Ask:

* Can this solution be simpler?
* Is every abstraction necessary?
* Is any code duplicated?
* Is the implementation easy to understand?

Prefer the simplest correct solution.

---

# Verify Naming

Check that:

* Names use the Ubiquitous Language.
* Intent is clear.
* Naming conventions are followed.
* No unnecessary abbreviations exist.

Naming should communicate responsibility.

---

# Verify Dependencies

Ensure:

* Dependencies are explicit.
* Constructor injection is used where appropriate.
* Hidden dependencies do not exist.
* Service Locator patterns have not been introduced.

---

# Verify Error Handling

Confirm that:

* Expected failures are handled appropriately.
* Unexpected failures are not hidden.
* Stack traces are preserved.
* Repository exception conventions are followed.

---

# Verify Validation

Ensure:

* Input validation exists where required.
* Domain invariants remain protected.
* Validation is not duplicated unnecessarily.
* Validation belongs to the correct layer.

---

# Verify Security

Review the solution for:

* Input validation
* Secret handling
* Authentication boundaries
* Authorization
* Sensitive logging
* Secure defaults

Security regressions must never be introduced.

---

# Verify Performance

Check for obvious issues such as:

* N+1 queries
* Blocking asynchronous code
* Excessive allocations
* Reflection in hot paths
* Unnecessary work

Optimise only when justified.

---

# Verify Testing

Determine whether:

* New behaviour requires tests.
* Existing tests require updates.
* Behaviour remains deterministic.
* The implementation is testable.

---

# Verify Documentation

If public behaviour or architecture changed, verify whether updates are required for:

* README
* ADRs
* Module documentation
* XML documentation
* Configuration guidance

Documentation should remain accurate.

---

# Verify Consistency

Ensure the solution is consistent with:

* Existing repository patterns
* Naming conventions
* Folder structure
* Coding standards
* Previous architectural decisions

Consistency improves maintainability.

---

# Verify Communication

Before responding, confirm that:

* The explanation is clear.
* Assumptions are explicit.
* Trade-offs are explained when relevant.
* Uncertainty is identified honestly.
* The response matches the requested level of detail.

---

# Final Review Questions

Before presenting a solution, ask internally:

1. Does this solve the requested problem?
2. Is it architecturally correct?
3. Is it the simplest reasonable solution?
4. Is it maintainable?
5. Is it testable?
6. Is it secure?
7. Is it consistent with the repository?
8. Would I approve this in a code review?

Only present the solution if every answer is satisfactory.

---

# AI Responsibilities

Before every response, the AI must:

* Review correctness.
* Review architecture.
* Review simplicity.
* Review maintainability.
* Review security.
* Review testing implications.
* Review communication quality.

---

# Anti-Patterns

Avoid:

* Responding without verification.
* Ignoring repository conventions.
* Leaving architectural violations unreviewed.
* Introducing unnecessary complexity.
* Forgetting documentation updates.
* Making unsupported claims.
* Presenting partially complete solutions as final.

---

# Self Review Checklist

Before completing any task, verify:

* Requirements are satisfied.
* Architecture is respected.
* Naming is correct.
* Dependencies are explicit.
* Validation is complete.
* Error handling is appropriate.
* Security has been considered.
* Performance is acceptable.
* Tests are updated when necessary.
* Documentation remains accurate.
* Communication is clear.

---

# Guiding Principle

An excellent engineer reviews their own work before asking others to review it.

The AI should apply the same discipline.

Every response should already meet the quality expected from an approved pull request before it is presented.
