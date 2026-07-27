# Architecture Review Checklist

Version: 1.0

---

# Purpose

This document defines the mandatory architectural review checklist that every implementation must satisfy before it is considered complete.

The objective is to ensure architectural consistency across the entire repository.

Every Pull Request, generated feature, and AI-generated implementation should pass this checklist.

---

# Primary Principle

Architecture is enforced continuously—not reviewed after the fact.

Every implementation must preserve the architectural integrity of the repository.

---

# Domain Review

Verify:

* Business rules exist only in the Domain.
* Aggregates protect invariants.
* Entities contain behaviour.
* Value Objects are immutable.
* Domain Events represent completed business facts.
* Repository interfaces belong to the Domain.
* Domain has no infrastructure dependencies.
* Domain language is consistent.

---

# Application Review

Verify:

* Each Handler implements one use case.
* Handlers coordinate rather than decide.
* Business logic is not implemented in Handlers.
* Transactions are coordinated correctly.
* Validation is limited to application concerns.
* Repository abstractions are used.
* Infrastructure implementations are not referenced.

---

# Infrastructure Review

Verify:

* Infrastructure only implements abstractions.
* Repository implementations contain no business rules.
* EF Core remains isolated.
* External services are hidden behind interfaces.
* Configuration remains outside the Domain.
* Technical failures are translated appropriately.

---

# API Review

Verify:

* Endpoints remain thin.
* Business rules are absent.
* Requests are transport models.
* Responses are transport models.
* Appropriate HTTP status codes are returned.
* Authentication is configured.
* Authorization is enforced.
* Mapping remains explicit.

---

# Dependency Review

Verify:

* Dependencies point inward.
* No circular project references exist.
* Infrastructure depends on Application or Domain only.
* Domain depends on nothing.
* Framework types do not leak into inner layers.
* Public contracts are respected.

---

# DDD Review

Verify:

* Aggregate boundaries are correct.
* Aggregate Roots are the only repository entry points.
* Business terminology is used consistently.
* Value Objects replace important primitive types.
* Domain Services are justified.
* Specifications represent business policies.
* Domain Events describe business facts.

---

# Module Review

Verify:

* Module ownership is clear.
* Cross-module communication uses contracts.
* Internal implementation remains private.
* Shared Kernel remains small.
* No shared database ownership exists.
* Module boundaries remain intact.

---

# CQRS Review

Verify:

* Commands modify state only.
* Queries read state only.
* Read Models are separate from Domain Models.
* Aggregates are not loaded for reporting.
* Handlers have a single responsibility.
* Business behaviour remains in the Domain.

---

# Clean Architecture Review

Verify:

* Layer responsibilities are respected.
* Business is independent of technology.
* Infrastructure remains replaceable.
* Transport concerns remain isolated.
* Business rules remain framework independent.

---

# Code Quality Review

Verify:

* Naming follows repository conventions.
* Code duplication is avoided.
* Complexity is justified.
* Methods remain focused.
* Classes have a single responsibility.
* Dead code has not been introduced.

---

# Testing Review

Verify:

* Domain behaviour is unit tested.
* Application workflows are tested.
* Infrastructure is integration tested.
* API endpoints are tested.
* New business rules have corresponding tests.
* Regression tests accompany bug fixes.

---

# Documentation Review

Verify:

* Architecture documentation remains accurate.
* ADRs are updated when required.
* Public APIs are documented.
* Breaking changes are documented.
* AI handbook remains consistent.

---

# Performance Review

Verify:

* No unnecessary allocations.
* No unnecessary database queries.
* N+1 queries are avoided.
* Expensive operations are justified.
* Async I/O is used appropriately.
* Scalability has been considered.

---

# Security Review

Verify:

* Authorization is enforced.
* Sensitive data is protected.
* Validation exists.
* Injection vulnerabilities are avoided.
* Secrets are not hardcoded.
* Public APIs expose only necessary data.

---

# AI Self-Review

Before completing any implementation, the AI must internally verify:

* I followed the repository handbook.
* I preserved architectural boundaries.
* I reused existing patterns.
* I avoided unnecessary abstractions.
* I generated production-ready code.
* I would approve this implementation during a senior architecture review.

If any answer is negative, continue improving the implementation.

---

# Final Checklist

Every completed implementation should satisfy:

* ✓ Architecture preserved.
* ✓ Domain protected.
* ✓ Dependencies correct.
* ✓ Modules isolated.
* ✓ Business rules correctly placed.
* ✓ Tests completed.
* ✓ Documentation updated.
* ✓ No architectural violations introduced.

---

# Guiding Principle

Architecture review is not a final step.

It is a continuous engineering discipline that ensures every change leaves the repository more consistent, more maintainable, and more aligned with its long-term design.
