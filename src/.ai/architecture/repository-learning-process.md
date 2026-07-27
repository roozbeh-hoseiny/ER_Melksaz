# Repository Learning Process

Version: 1.0

---

# Purpose

This document defines how an AI agent should learn an existing repository before implementing any feature.

Unlike a human developer, an AI agent has no long-term memory of the repository. Therefore, it must rebuild its understanding of the codebase every time it starts working.

This process ensures that generated code is consistent with the repository rather than with generic programming knowledge.

---

# Primary Principle

The repository is the AI's source of truth.

The AI must learn the repository before attempting to modify it.

---

# Learning Order

The repository should always be learned in the following order.

1. AI Handbook
2. Solution Structure
3. Project Structure
4. Architecture
5. Modules
6. Existing Features
7. Coding Conventions
8. Existing Tests
9. Existing Documentation

Never skip steps.

---

# Step 1 — Read the AI Handbook

Before analysing source code, the AI must read every handbook document located under:

```text
.ai/
```

Repository-specific rules always override general software engineering knowledge.

---

# Step 2 — Learn the Solution

Identify:

* Solution name
* Projects
* Project responsibilities
* Build configuration
* Test projects
* Shared projects

The AI should understand how the solution is organised before inspecting implementation details.

---

# Step 3 — Learn the Architecture

Identify:

* Architecture style
* Layer responsibilities
* Dependency direction
* Composition root
* Shared abstractions
* Cross-cutting concerns

The AI must never infer architecture from isolated files.

---

# Step 4 — Learn the Business Modules

Identify:

* Business modules
* Bounded contexts
* Feature organisation
* Shared capabilities

Business ownership should be understood before implementation begins.

---

# Step 5 — Learn Existing Features

For the requested feature, search for similar implementations.

Inspect:

* Domain Models
* Commands
* Queries
* Validators
* Repositories
* Endpoints
* Tests

Always reuse repository patterns.

---

# Step 6 — Learn Naming

Learn existing naming conventions for:

* Projects
* Folders
* Files
* Classes
* Interfaces
* Methods
* Variables

Generated code should blend naturally into the repository.

---

# Step 7 — Learn Dependencies

Identify:

* Existing libraries
* Existing frameworks
* Existing abstractions
* Existing infrastructure

Do not introduce duplicate capabilities.

---

# Step 8 — Learn Testing

Understand:

* Testing framework
* Test naming
* Test builders
* Fixtures
* Integration test strategy
* Assertion style

Generated tests should match the existing repository.

---

# Step 9 — Learn Documentation

Read:

* README
* Architecture documentation
* ADRs
* Design documents
* AI handbook

Documentation often contains decisions that are not visible in the source code.

---

# Pattern Recognition

The AI should identify repository patterns before creating new implementations.

Examples include:

* Aggregate structure
* Command organisation
* Repository implementation
* Endpoint style
* Validation style
* Mapping conventions

Consistency has higher priority than novelty.

---

# Behaviour Recognition

The AI should understand how business behaviour is modelled.

Look for:

* Aggregate methods
* State transitions
* Business rules
* Validation
* Result Pattern usage

Business behaviour should be extended—not reinvented.

---

# Reuse Strategy

Before creating:

* Interface
* Base Class
* Utility
* Extension Method
* Generic Type
* Helper

verify whether an equivalent implementation already exists.

Reuse whenever practical.

---

# Knowledge Validation

Before implementation, the AI should be able to answer:

* Which module owns this feature?
* Which layer owns this responsibility?
* Which existing implementation is most similar?
* Which conventions apply?
* Which libraries are already used?
* Which tests should be created?
* Which documentation should be updated?

If any answer is unknown, continue learning.

---

# Continuous Learning

Repository learning does not stop after implementation begins.

Whenever unfamiliar code is encountered:

* Pause implementation.
* Learn the surrounding code.
* Continue only after understanding it.

---

# AI Restrictions

The AI must never:

* Guess repository conventions.
* Assume architecture.
* Ignore existing implementations.
* Replace repository patterns with generic best practices.
* Optimise for speed instead of consistency.

---

# Learning Checklist

Before generating code, verify:

* AI handbook read.
* Solution understood.
* Architecture understood.
* Module identified.
* Similar implementation found.
* Naming conventions learned.
* Dependencies understood.
* Testing conventions understood.
* Documentation reviewed.

---

# Guiding Principle

A senior engineer studies a repository before changing it.

An AI agent should do exactly the same.
