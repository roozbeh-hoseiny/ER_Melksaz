# Definition of Done (DoD)

Version: 1.0

---

# Purpose

This document defines the mandatory Definition of Done (DoD) for every change made to the repository.

A task is not considered complete when the code compiles.

A task is complete only when every requirement in this document has been satisfied.

This definition applies equally to human developers and AI agents.

---

# Primary Principle

A completed feature must be:

* Correct
* Maintainable
* Tested
* Documented
* Consistent
* Production-ready

Every completed change should improve the repository.

---

# Functional Completion

Verify that:

* Every requested requirement has been implemented.
* Every acceptance criterion is satisfied.
* Edge cases have been considered.
* Existing behaviour has not regressed.
* No requested functionality has been omitted.

---

# Architectural Completion

Verify that:

* Clean Architecture is preserved.
* Dependency direction is correct.
* Module boundaries are respected.
* Responsibilities remain separated.
* No architectural violations exist.

---

# Domain Completion

Verify that:

* Business rules exist only inside the Domain.
* Domain invariants are protected.
* Business terminology is used consistently.
* No infrastructure concerns exist inside the Domain.

---

# Application Completion

Verify that:

* Use cases are fully implemented.
* Commands and Queries follow repository conventions.
* Validation is complete.
* Handlers remain orchestration only.
* Business logic has not leaked into the Application layer.

---

# Infrastructure Completion

Verify that:

* Infrastructure implements abstractions.
* Persistence is correctly configured.
* External services are properly integrated.
* Infrastructure details remain isolated.
* No business rules exist inside Infrastructure.

---

# API Completion

Verify that:

* Endpoints are implemented.
* Request validation is complete.
* Response mapping is correct.
* Transport concerns remain isolated.
* Endpoints contain no business logic.

---

# Dependency Completion

Verify that:

* Constructor Injection is used.
* Dependencies are explicit.
* No Service Locator exists.
* No circular dependencies exist.
* Layer boundaries are respected.

---

# Naming Completion

Verify that:

* Naming follows repository conventions.
* Business terminology is used.
* Files, folders, namespaces, and types are consistent.
* No unnecessary abbreviations exist.

---

# Code Quality

Verify that:

* Code is readable.
* Responsibilities are clear.
* Complexity is justified.
* Duplication is avoided.
* Formatting matches repository standards.

---

# Error Handling

Verify that:

* Business failures use the Result Pattern.
* Exceptions are used only for unexpected failures.
* Sensitive information is protected.
* Logging follows repository conventions.

---

# Performance

Verify that:

* No unnecessary allocations exist.
* Async code remains asynchronous.
* No blocking calls exist.
* No obvious performance regressions have been introduced.
* Expensive operations are justified.

---

# Security

Verify that:

* Validation is complete.
* Authentication is respected.
* Authorization is enforced.
* Sensitive information is protected.
* Security boundaries remain intact.

---

# Testing

Verify that:

* Unit Tests are implemented.
* Integration Tests are implemented where appropriate.
* Business rules are tested.
* Edge cases are tested.
* Regression tests are added when fixing defects.
* Existing tests continue to pass.

Testing is mandatory.

---

# Documentation

Verify that:

* Public APIs are documented where required.
* Architectural documentation is updated.
* Repository documentation reflects the implementation.
* Obsolete documentation has been removed or updated.

---

# Build Verification

Verify that:

* The solution builds successfully.
* No compiler warnings have been introduced unless explicitly accepted.
* Static analysis passes.
* Formatting checks pass.
* Code generation (if applicable) succeeds.

---

# Repository Consistency

Verify that:

* Existing conventions have been followed.
* Existing patterns have been reused.
* No unnecessary libraries have been introduced.
* Folder structure remains consistent.
* Namespace hierarchy remains consistent.

---

# AI Self Verification

Before returning the final implementation, the AI agent must verify:

* I understood the business requirement.
* I inspected the repository.
* I reused existing patterns.
* I preserved the architecture.
* I generated every required artefact.
* I added all required tests.
* I reviewed the implementation.
* I would approve this implementation as a senior reviewer.

If any answer is "No", the task is not complete.

---

# Completion Checklist

A task is considered complete only if all of the following are true:

* ✓ Requirements implemented
* ✓ Architecture preserved
* ✓ Business rules correctly located
* ✓ Dependencies correct
* ✓ Naming consistent
* ✓ Code reviewed
* ✓ Tests implemented
* ✓ Documentation updated
* ✓ Build succeeds
* ✓ Repository quality improved

---

# Definition of Done

A feature is **Done** only when another senior engineer can review, understand, test, deploy, and maintain it without requiring additional clarification or modifications.

---

# Guiding Principle

Completion is measured by repository quality—not by the amount of code written.
