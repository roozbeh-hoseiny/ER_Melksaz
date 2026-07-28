# Testing

Version: 1.0

---

# Purpose

This document defines the mandatory testing standards for the repository.

Testing provides confidence that the software behaves correctly, remains maintainable, and can evolve safely.

Tests are executable specifications.

---

# Primary Principle

Test behaviour—not implementation.

A test should describe what the system does, not how it does it.

---

# Testing Pyramid

Prefer the following distribution:

* Unit Tests
* Integration Tests
* End-to-End Tests

The majority of tests should be unit tests.

---

# Unit Tests

Unit tests verify:

* Domain logic
* Business rules
* Value Objects
* Entities
* Aggregates
* Domain Services
* Pure functions

Unit tests should execute quickly and deterministically.

---

# Integration Tests

Integration tests verify collaboration between components.

Examples:

* Database access
* Messaging
* External services
* HTTP APIs
* gRPC services
* Repository implementations

Integration tests should use real infrastructure whenever practical.

---

# End-to-End Tests

End-to-End tests verify complete user scenarios.

They should be:

* Few
* Stable
* Business-focused

Avoid excessive E2E testing.

---

# Behaviour-Oriented Tests

Tests should describe business behaviour.

Good:

```text id="x7r4pa"
Should_Approve_Invoice_When_Payment_Is_Received
```

Avoid implementation-focused names.

---

# Test Structure

Use the Arrange–Act–Assert pattern.

Example:

```text id="t8n2cf"
Arrange

Act

Assert
```

Keep sections clearly separated.

---

# One Assertion Concept

Each test should verify one behaviour.

Multiple assertions are acceptable when they verify the same outcome.

Avoid testing unrelated behaviour in a single test.

---

# Deterministic Tests

Tests must produce the same result every time.

Avoid dependencies on:

* Current time
* Random values
* External state
* Test execution order

Inject abstractions where necessary.

---

# Isolation

Tests must not depend on each other.

Each test should prepare and clean up its own state.

Tests should be executable independently.

---

# Real Infrastructure

Prefer real implementations over mocks when testing infrastructure behaviour.

Examples:

* SQL Server
* Redis
* RabbitMQ
* gRPC
* File systems

Use containerized infrastructure where appropriate.

---

# Mocking

Mock only true external collaborators.

Avoid mocking:

* Domain objects
* Value Objects
* Aggregates
* Business rules

Mocks should support isolation—not replace behaviour.

---

# Test Data

Use clear and meaningful test data.

Prefer:

```text id="r3m8vk"
ApprovedInvoice

ExistingCustomer

ExpiredToken
```

Avoid meaningless values such as:

```text id="v5k2ln"
Test1

Value123

ObjectA
```

---

# Builders

Use Test Builders or Object Mothers when object construction becomes repetitive.

Builders should improve readability.

---

# Assertions

Assertions should clearly express intent.

Avoid indirect or overly complex assertions.

Tests should fail with meaningful messages.

---

# Database Tests

Database integration tests should:

* Use isolated databases.
* Clean up after execution.
* Avoid shared mutable state.

Each test should be independent.

---

# Time

Never rely on:

```text id="g8d4wh"
DateTime.UtcNow
```

directly in business tests.

Use an injected time abstraction.

---

# Randomness

Avoid uncontrolled randomness.

Random values should be deterministic or explicitly seeded.

---

# Performance

Tests should remain fast.

Slow tests reduce developer productivity and CI efficiency.

---

# Test Naming

Preferred naming style:

```text id="y2p7ra"
Should_[ExpectedBehaviour]_When_[Condition]
```

Example:

```text id="h4v9mc"
Should_Return_NotFound_When_Customer_Does_Not_Exist
```

---

# Integration Test Environment

Integration tests should resemble production behaviour as closely as practical.

Avoid replacing core infrastructure with mocks unless absolutely necessary.

---

# Continuous Integration

All tests must execute successfully in CI.

Tests must not require:

* Manual intervention
* Local machine configuration
* Developer-specific settings

---

# Flaky Tests

Flaky tests are unacceptable.

Any nondeterministic test should be corrected or removed immediately.

---

# Coverage

Code coverage is a diagnostic metric—not a quality metric.

Prioritize meaningful behavioural coverage over high percentages.

---

# AI Responsibilities

When generating tests, the AI must:

* Test observable behaviour.
* Keep tests deterministic.
* Use Arrange–Act–Assert.
* Avoid unnecessary mocking.
* Prefer meaningful names.
* Follow repository testing conventions.

---

# Anti-Patterns

Avoid:

* Testing private methods.
* Mocking the Domain.
* Shared mutable test state.
* Hidden test dependencies.
* Flaky tests.
* Testing implementation details.
* Large monolithic test methods.

---

# Testing Checklist

Before completing a test, verify:

* Behaviour is tested.
* The test is deterministic.
* Arrange–Act–Assert is followed.
* Naming is meaningful.
* Dependencies are isolated.
* Assertions are clear.
* Repository testing conventions are followed.

---

# Guiding Principle

A good test documents expected behaviour, executes reliably, and gives engineers the confidence to change the implementation without changing the behaviour.
