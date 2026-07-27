# AI Engineering Handbook

Version: 1.0

---

# Purpose

The AI Engineering Handbook defines the engineering standards, architectural principles, coding conventions, generation rules, templates, and workflows used by AI agents when working with this repository.

The objective is to ensure that AI-generated code is indistinguishable from code written by experienced engineers following the repository standards.

This handbook is the authoritative source for all AI-assisted development.

---

# Goals

The handbook exists to achieve the following goals:

- Preserve architectural consistency.
- Reduce repetitive development work.
- Produce production-ready code.
- Minimize manual corrections after AI generation.
- Keep generated code maintainable.
- Prevent architectural drift.
- Standardize engineering practices across the repository.

---

# Scope

This handbook applies to every AI agent generating, reviewing, refactoring, or analysing code within this repository.

Examples include:

- OpenAI Codex
- Claude Code
- Cursor
- GitHub Copilot
- Gemini CLI
- Continue
- Any future AI development assistant

---

# Document Hierarchy

All documents have equal authority unless explicitly stated otherwise.

When multiple documents apply to the same topic, precedence is determined as follows:

1. Repository-specific rules
2. Company standards
3. Technology standards
4. Generic engineering rules

More specific rules always override more general rules.

---

# Handbook Structure

The handbook is organized into the following sections.

## Architecture

Defines architectural principles, dependency rules, coding standards, error handling, logging, configuration, security, and related concerns.

---

## Domain-Driven Design

Defines aggregates, entities, value objects, domain events, repositories, factories, specifications, and business modelling rules.

---

## Application

Defines commands, queries, handlers, validators, pipelines, authorization, caching, mapping, idempotency, and application behaviour.

---

## Infrastructure

Defines persistence, messaging, caching, databases, Docker, observability, gRPC, RabbitMQ, Redis, EF Core, and related implementation standards.

---

## API

Defines Minimal APIs, gRPC endpoints, routing, authentication, versioning, Problem Details, and OpenAPI conventions.

---

## Testing

Defines unit testing, integration testing, architecture testing, fixtures, builders, assertions, and testing infrastructure.

---

## Recipes

Defines repeatable generation workflows for complete features and architectural components.

---

## Templates

Defines canonical implementations that AI should follow when generating code.

---

## Prompts

Defines AI reasoning instructions for generation, review, refactoring, security analysis, architecture analysis, and performance analysis.

---

## Checklists

Defines completion criteria for features and architectural components.

---

## Anti-Patterns

Defines prohibited designs and common implementation mistakes.

---

## Examples

Provides repository-approved implementation examples.

---

# AI Responsibilities

An AI agent working with this repository shall:

- Understand the requested business capability.
- Follow the documented architecture.
- Reuse existing conventions.
- Prefer consistency over creativity.
- Generate complete implementations.
- Protect architectural integrity.
- Generate production-ready code.
- Generate corresponding tests.
- Verify generated code before presenting it.

---

# AI Limitations

AI agents shall never:

- Invent new architectural patterns.
- Introduce new frameworks without explicit instruction.
- Ignore existing repository conventions.
- Bypass architectural layers.
- Generate placeholder implementations.
- Produce incomplete features.
- Duplicate existing functionality.
- Leak infrastructure concerns into the Domain layer.

---

# Repository Philosophy

The repository values:

- Simplicity
- Consistency
- Maintainability
- Explicitness
- Testability
- Performance
- Separation of concerns
- Domain-driven design
- Clean architecture

Generated code shall reflect these values.

---

# Feature Completeness

A feature is considered complete only when all required architectural layers have been generated.

Depending on the feature, this may include:

- Domain Model
- Commands
- Queries
- Validators
- Handlers
- Repository Interfaces
- Repository Implementations
- Persistence Configuration
- API Endpoints
- Dependency Injection
- Unit Tests
- Integration Tests
- Documentation

Generating only a subset of these components is considered incomplete unless explicitly requested.

---

# Consistency

Whenever possible, AI agents shall imitate existing repository implementations instead of introducing new patterns.

Consistency is preferred over theoretical perfection.

---

# Quality Requirements

Every generated implementation should:

- Compile successfully.
- Follow repository naming conventions.
- Respect dependency direction.
- Avoid duplicated logic.
- Protect business invariants.
- Be testable.
- Be maintainable.
- Be production-ready.

---

# Maintenance

This handbook is maintained alongside the repository.

Changes to engineering standards should be reflected in the corresponding handbook documents before they are adopted within the codebase.

---

# Versioning

Every handbook document shall contain a version number.

Breaking architectural changes should increment the major version of the affected document.

Minor clarifications should increment the minor version.

---

# Phase Structure

The handbook is developed in four phases.

## Phase 1

Generic AI Engineering

Repository-independent engineering standards.

---

## Phase 2

Technology Stack

Technology-specific implementation standards.

---

## Phase 3

Company Standards

Organization-specific conventions, templates, prompts, and recipes.

---

## Phase 4

Repository Alignment

Repository-specific knowledge, examples, and AI guidance.

---

# Success Criteria

The handbook is considered complete when an AI agent can generate production-ready features that:

- Follow the documented architecture.
- Require minimal manual modification.
- Pass architectural review.
- Align with repository standards.
- Maintain long-term consistency.

---

# Guiding Principle

The primary objective of this handbook is not to generate code faster.

The primary objective is to generate code that consistently reflects the engineering standards of the repository.