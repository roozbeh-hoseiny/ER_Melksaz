# Architecture Index

Version: 1.0

---

# Purpose

This document is the entry point for all architecture documentation in this repository.

Every engineer and every AI agent should begin here before reading individual architecture documents.

This file defines the recommended reading order and explains the purpose of each document.

---

# Reading Order

The documents should be read in the following order.

## 1. Architecture Manifesto

**File**

```text
architecture-manifesto.md
```

Defines the philosophy of the repository.

Read this first.

---

## 2. Architectural Principles

**File**

```text
architectural-principles.md
```

Defines the core engineering principles that govern all architectural decisions.

---

## 3. Dependency Rule

**File**

```text
dependency-rule.md
```

Defines dependency direction and architectural boundaries.

---

## 4. Layer Responsibilities

**File**

```text
layer-responsibilities.md
```

Explains the responsibilities of each architectural layer.

---

## 5. Domain Layer

**File**

```text
domain-layer.md
```

Defines all Domain design rules.

---

## 6. Application Layer

**File**

```text
application-layer.md
```

Defines orchestration rules and application use cases.

---

## 7. Infrastructure Layer

**File**

```text
infrastructure-layer.md
```

Defines technical implementation responsibilities.

---

## 8. API Layer

**File**

```text
api-layer.md
```

Defines transport responsibilities and API boundaries.

---

## 9. Clean Architecture

**File**

```text
clean-architecture.md
```

Defines the overall architectural style.

---

## 10. Domain-Driven Design

**Files**

```text
aggregate.md
entity.md
value-object.md
domain-event.md
domain-service.md
repository.md
specification.md
```

Defines the tactical DDD building blocks.

---

## 11. CQRS

**File**

```text
cqrs.md
```

Defines command/query separation.

---

## 12. Vertical Slice Architecture

**File**

```text
vertical-slice-architecture.md
```

Defines feature organisation.

---

## 13. Modular Monolith

**File**

```text
modular-monolith.md
```

Defines module organisation and ownership.

---

## 14. Shared Kernel

**File**

```text
shared-kernel.md
```

Defines shared abstractions and cross-module primitives.

---

## 15. Module Boundaries

**File**

```text
module-boundaries.md
```

Defines module isolation and communication.

---

## 16. Architecture Decision Records

**File**

```text
architecture-decision-records.md
```

Explains how architectural decisions are documented.

---

## 17. Architecture Review Checklist

**File**

```text
architecture-review-checklist.md
```

Defines the mandatory review checklist.

---

## 18. Architecture Glossary

**File**

```text
architecture-glossary.md
```

Defines the official architectural vocabulary.

---

## 19. AI Architecture Enforcement

**File**

```text
ai-architecture-enforcement.md
```

Defines mandatory AI behaviour while generating code.

---

# Architecture Hierarchy

The documents have the following priority:

```text
Architecture Manifesto
        ↓
Architectural Principles
        ↓
Dependency Rule
        ↓
Layer Responsibilities
        ↓
Architecture Style
        ↓
DDD Rules
        ↓
Module Rules
        ↓
Repository Conventions
        ↓
Implementation
```

Higher-level documents always override lower-level documents.

---

# AI Reading Sequence

Before generating code, an AI agent should internally process the documents in this order:

1. Architecture Manifesto
2. Architectural Principles
3. Dependency Rule
4. Layer Responsibilities
5. Module Boundaries
6. Shared Kernel
7. DDD Documents
8. CQRS
9. Vertical Slice Architecture
10. Repository Coding Standards
11. Testing Rules
12. Feature Request

This ensures generated code aligns with the repository before implementation begins.

---

# Maintenance

Whenever a new architecture document is added:

1. Add it to this index.
2. Place it in the correct reading order.
3. Update the hierarchy if necessary.
4. Verify there is no overlap with existing documents.

The index must always reflect the current architecture handbook.

---

# Guiding Principle

This document is the map of the architecture.

Every architecture document has a specific purpose, and together they form a complete handbook for engineers and AI agents to consistently build and evolve the repository.
