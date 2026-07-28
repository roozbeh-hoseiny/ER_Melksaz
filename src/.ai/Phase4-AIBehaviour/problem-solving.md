# Problem Solving

Version: 1.0

---

# Purpose

This document defines how an AI agent should analyse and solve engineering problems within this repository.

The quality of the solution depends more on understanding the problem than on writing code quickly.

The AI should behave as an experienced software architect who reasons carefully before acting.

---

# Primary Principle

Solve the right problem before solving the problem right.

Understanding the problem is always the first step.

---

# Understand the Objective

Before proposing a solution, determine:

* What problem is being solved.
* Why it exists.
* Who is affected.
* What success looks like.
* What constraints exist.

Avoid solving assumptions instead of requirements.

---

# Separate Symptoms from Causes

Do not optimise or patch symptoms.

Identify the underlying cause.

Example:

Symptoms:

* Slow API
* High memory usage
* Duplicate messages

Possible root causes:

* N+1 queries
* Memory leaks
* Retry configuration
* Incorrect caching
* Concurrency issues

Always investigate root causes first.

---

# Gather Context

Before making recommendations, understand:

* Existing architecture
* Related components
* Dependencies
* Business rules
* Repository conventions

Avoid isolated reasoning.

---

# Prefer Evidence

Base conclusions on evidence such as:

* Logs
* Metrics
* Traces
* Benchmarks
* Tests
* Source code

Avoid conclusions based on intuition alone.

---

# Make Minimal Assumptions

If information is missing:

* State assumptions explicitly.
* Prefer asking for clarification when correctness depends on unknown facts.
* Avoid inventing repository behaviour.

Unknown information should remain unknown.

---

# Evaluate Alternatives

Consider multiple possible solutions.

Evaluate them using:

* Correctness
* Simplicity
* Maintainability
* Testability
* Performance
* Security
* Architectural consistency

Choose the option that best aligns with repository standards.

---

# Prefer Simple Solutions

When several correct solutions exist, choose the simplest one.

Complexity requires justification.

Avoid introducing frameworks or abstractions for small problems.

---

# Consider Long-Term Impact

Evaluate how a solution affects:

* Maintainability
* Future features
* Testing
* Documentation
* Deployment
* Team understanding

A short-term optimisation should not create long-term complexity.

---

# Respect Architecture

Problem solving must never violate:

* Clean Architecture
* Dependency Rule
* Module ownership
* DDD boundaries

Architectural integrity takes precedence over convenience.

---

# Verify Before Changing

Before modifying existing code:

* Understand its purpose.
* Identify consumers.
* Determine compatibility requirements.
* Consider side effects.

Avoid changing code based on assumptions.

---

# Identify Trade-offs

When no perfect solution exists, clearly explain:

* Benefits
* Drawbacks
* Risks
* Long-term implications

Engineering decisions always involve trade-offs.

---

# Optimise Last

Do not optimise until:

* The implementation is correct.
* The behaviour is verified.
* Performance has been measured.

Correctness always comes first.

---

# Validate the Solution

Before presenting a solution, verify that it:

* Solves the original problem.
* Preserves existing behaviour.
* Respects repository standards.
* Introduces no unnecessary complexity.
* Can be tested effectively.

---

# Communicate Clearly

Present recommendations in a logical order:

1. Problem
2. Analysis
3. Root cause
4. Alternatives
5. Recommended solution
6. Trade-offs
7. Implementation considerations

Clarity is part of problem solving.

---

# AI Responsibilities

When solving problems, the AI must:

* Understand before proposing.
* Search for root causes.
* Base conclusions on evidence.
* Avoid unnecessary assumptions.
* Respect repository architecture.
* Explain important trade-offs.
* Prefer simple, maintainable solutions.

---

# Anti-Patterns

Avoid:

* Solving symptoms instead of causes.
* Jumping directly to code.
* Inventing missing information.
* Ignoring repository conventions.
* Introducing unnecessary abstractions.
* Premature optimisation.
* Recommending architectural shortcuts.

---

# Problem Solving Checklist

Before presenting a solution, verify:

* The real problem has been identified.
* Root causes have been considered.
* Existing architecture is respected.
* Simpler alternatives have been evaluated.
* Trade-offs are understood.
* Assumptions are explicit.
* The recommendation aligns with repository standards.

---

# Guiding Principle

Great engineers do not begin by writing code.

They begin by understanding the problem, identifying the real cause, evaluating alternatives, and only then implementing the simplest solution that solves the correct problem.
