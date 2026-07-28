# Code Review

Version: 1.0

---

# Purpose

This document defines the mandatory code review standards for the repository.

Code reviews exist to improve software quality, share knowledge, enforce architectural consistency, and reduce defects before code reaches production.

A code review is a collaborative engineering activity—not an approval ceremony.

---

# Primary Principle

Review the code, not the developer.

Feedback should always be objective, constructive, and based on repository standards.

---

# Review Goals

Every review should verify:

* Correctness
* Readability
* Maintainability
* Architecture
* Security
* Performance
* Testability
* Consistency

A review is not limited to finding bugs.

---

# Reviewer Responsibilities

A reviewer should:

* Verify the implementation satisfies the requirements.
* Ensure repository standards are followed.
* Protect architectural boundaries.
* Identify unnecessary complexity.
* Suggest simpler solutions where appropriate.
* Ask questions when intent is unclear.

Reviewers should not rewrite code simply to match personal preferences.

---

# Author Responsibilities

The author should:

* Submit focused pull requests.
* Explain non-obvious decisions.
* Include appropriate tests.
* Update documentation when required.
* Respond constructively to feedback.
* Resolve review comments before merging.

Large, unrelated changes should be split into separate pull requests.

---

# Review Scope

Every review should consider:

* Correctness
* Business behaviour
* Edge cases
* Failure scenarios
* Error handling
* Security
* Performance
* Resource management
* Maintainability
* Testing
* Documentation

---

# Architectural Compliance

Verify that the implementation respects:

* Clean Architecture
* Dependency Rule
* DDD boundaries
* CQRS (where applicable)
* Repository conventions

Architecture violations should be addressed before merging.

---

# Naming

Ensure names are:

* Clear
* Consistent
* Domain-oriented
* Intention-revealing

Poor naming should be corrected before approval.

---

# Simplicity

Prefer the simplest implementation that satisfies the requirements.

Look for:

* Unnecessary abstractions
* Premature optimisation
* Duplicate logic
* Excessive indirection

Complexity should always require justification.

---

# Correctness

Verify:

* Boundary conditions
* Null handling
* Validation
* Concurrency considerations
* Exception handling
* Resource disposal

Code should behave correctly under expected and unexpected conditions.

---

# Security

Review for:

* Input validation
* Authorization
* Authentication boundaries
* Secret handling
* Injection vulnerabilities
* Sensitive logging
* Secure defaults

Security concerns should block approval.

---

# Performance

Consider:

* N+1 queries
* Unnecessary allocations
* Excessive database calls
* Reflection in hot paths
* Large object graphs
* Blocking asynchronous code

Only recommend optimisation when there is clear value.

---

# Testing

Verify that:

* Appropriate tests exist.
* Behaviour is tested.
* New functionality is covered.
* Existing tests remain meaningful.
* Tests are deterministic.

Missing critical tests should block approval.

---

# Documentation

Ensure updates include documentation when necessary:

* README
* ADRs
* XML documentation
* Module documentation
* Configuration guidance

Documentation should evolve with the code.

---

# Pull Request Size

Prefer small pull requests.

Recommended guideline:

* A single logical change.

Very large pull requests are harder to review effectively.

---

# Review Comments

Comments should:

* Explain the concern.
* Reference repository standards when applicable.
* Suggest improvements where practical.
* Remain respectful and professional.

Prefer asking clarifying questions over making assumptions.

---

# Blocking Issues

Examples of issues that should block approval:

* Architectural violations
* Security vulnerabilities
* Missing validation
* Missing tests
* Incorrect business behaviour
* Resource leaks
* Data corruption risks
* Breaking public contracts

Minor style issues should rarely block a merge.

---

# Non-Blocking Suggestions

Examples include:

* Naming improvements
* Readability enhancements
* Minor refactoring
* Documentation improvements
* Small performance optimisations

These should be clearly identified as suggestions.

---

# Automated Checks

Before review approval, verify that:

* Build succeeds.
* Static analysis passes.
* Formatting is correct.
* Tests pass.
* CI completes successfully.

Automation complements human review but does not replace it.

---

# AI Responsibilities

When reviewing code, the AI must:

* Prioritise correctness over style.
* Protect architectural boundaries.
* Explain the reasoning behind suggestions.
* Distinguish blocking issues from recommendations.
* Follow repository conventions consistently.

---

# Anti-Patterns

Avoid:

* Reviewing personal coding style instead of repository standards.
* Approving code without understanding it.
* Ignoring architectural violations.
* Requesting unnecessary abstractions.
* Large unrelated pull requests.
* Unconstructive or subjective feedback.

---

# Code Review Checklist

Before approving a change, verify:

* Requirements are satisfied.
* Architecture is respected.
* Code is readable.
* Naming is consistent.
* Security concerns are addressed.
* Performance is acceptable.
* Tests are sufficient.
* Documentation is updated where necessary.
* CI passes successfully.

---

# Guiding Principle

A successful code review improves both the software and the engineering team.

Every review should leave the codebase clearer, safer, and easier to maintain than before.
