# Naming Conventions

Version: 1.0

Status: Repository Convention

---

# Purpose

This document defines the naming conventions used throughout the repository.

Consistent naming improves readability, discoverability, and architectural consistency.

Names should communicate responsibility rather than implementation details.

---

# General Principles

Every name should answer one question:

> What is this responsible for?

Avoid names that describe how something works.

Prefer names that describe why it exists.

---

# Ubiquitous Language

Always use the project's domain language.

Do not invent new terminology when an equivalent concept already exists.

For example, if the repository consistently uses:

- Endpoint

do not introduce:

- Route
- Action
- HttpHandler

The repository should speak one language.

---

# Classes

Class names should be nouns.

Examples:

Good:

- Customer
- Invoice
- ApiEndpointBase
- GlobalExceptionHandler
- ResultHandlerDefault

Avoid:

- DoCustomer
- ExecuteInvoice
- ProcessData

---

# Interfaces

Interfaces describe capabilities.

Use the `I` prefix.

Examples:

- ICommand
- IValidator
- IRepository
- IService

Do not prefix concrete classes with "Default" unless multiple implementations exist.

---

# Abstract Classes

Abstract classes should describe a reusable concept.

Examples:

- Entity
- AggregateRoot
- ApiEndpointBase

Avoid abstract classes that merely share implementation.

---

# Endpoints

## Observed Pattern

The repository uses dedicated endpoint infrastructure.

Endpoint classes should clearly communicate the operation they expose.

Examples:

- CreateCustomerEndpoint
- UpdateInvoiceEndpoint
- DeleteProductEndpoint

Avoid generic names:

- CustomerController2
- CustomerHandler
- ApiAction

---

# Commands

Commands represent an intention.

Use imperative names.

Examples:

- CreateOrderCommand
- UpdateUserCommand
- DeleteInvoiceCommand

Commands should describe the requested action.

---

# Queries

Queries describe requested information.

Examples:

- GetCustomerQuery
- SearchOrdersQuery
- FindInvoiceQuery

Avoid vague names such as:

- CustomerData
- QueryHandler1

---

# Events

Events describe something that has already happened.

Use the past tense.

Examples:

- CustomerCreated
- InvoiceApproved
- UserDeleted

Events should never describe future intentions.

---

# Exceptions

Exception classes should end with:

Exception

Examples:

- ValidationException
- BusinessRuleViolationException
- EntityNotFoundException

Do not abbreviate exception names.

---

# Validators

Validators should end with:

Validator

Examples:

- CreateCustomerValidator
- UpdateInvoiceValidator

Each validator should validate one request type.

---

# Handlers

Handlers should describe what they handle.

Examples:

- CreateOrderHandler
- DeleteInvoiceHandler

Avoid generic names:

- Handler
- RequestProcessor

---

# Builders

Builders should end with:

Builder

Examples:

- CustomerBuilder
- EndpointBuilder

Builders construct objects.

They should not execute business logic.

---

# Factories

Factories should end with:

Factory

Examples:

- AggregateFactory
- EndpointFactory

Factories create objects.

They should not coordinate workflows.

---

# Strategies

Strategies should end with:

Strategy

Examples:

- RetryStrategy
- SerializationStrategy

Strategies encapsulate interchangeable behaviour.

---

# Extensions

Extension classes should end with:

Extensions

Examples:

- ServiceCollectionExtensions
- EndpointExtensions

Extension classes should contain only extension methods.

---

# Converters

Converters should end with:

Converter

Examples:

- ValueObjectJsonConverter

Converters transform one representation into another.

---

# Options

Configuration objects should end with:

Options

Examples:

- JwtOptions
- CacheOptions

Options should contain configuration only.

---

# DTOs

DTO names should clearly communicate purpose.

Examples:

- CreateCustomerRequest
- CustomerResponse

Avoid generic suffixes such as:

Dto

unless the repository consistently uses that terminology.

---

# Result Types

Result-related classes should use consistent terminology.

Observed example:

- ResultHandlerDefault

Future Result classes should preserve the existing vocabulary.

---

# Base Classes

Only introduce "Base" classes when they represent reusable behaviour.

Examples:

- ApiEndpointBase

Avoid unnecessary inheritance hierarchies.

---

# Generic Type Parameters

Use meaningful generic parameter names.

Examples:

- TEntity
- TResult
- TRequest
- TResponse

Avoid:

- T1
- TData
- TItem

unless they genuinely represent arbitrary items.

---

# Acronyms

Treat acronyms as words.

Examples:

- HttpClient
- JsonConverter
- ApiEndpoint

Avoid inconsistent capitalisation.

---

# Boolean Properties

Boolean names should read naturally.

Examples:

- IsEnabled
- HasChildren
- CanExecute

Avoid:

- EnabledFlag
- ExecutePossible

---

# Collections

Plural names indicate collections.

Examples:

- Customers
- Orders
- Events

Singular names indicate individual objects.

---

# Async Methods

Asynchronous methods should end with:

Async

Examples:

- SaveAsync
- ValidateAsync
- PublishAsync

Avoid omitting the suffix for asynchronous APIs.

---

# Private Fields

Use the repository's existing private field convention consistently.

Do not introduce alternative styles within the same project.

Examples may include:

- _logger
- _repository
- _validator

This convention should be inferred from neighbouring classes.

---

# File Names

The file name should match the primary public type.

One primary type per file.

Avoid multiple unrelated public types within the same file.

---

# AI Instructions

Before introducing a new name, verify:

1. Does the repository already use this terminology?
2. Does the name describe responsibility?
3. Is the name consistent with neighbouring classes?
4. Does the suffix communicate architectural intent?
5. Would another developer immediately understand its purpose?

If not, rename before generating code.

---

# Repository Convention

Names are part of the architecture.

A consistent vocabulary reduces cognitive load and makes the repository easier to navigate.

When in doubt, prefer consistency with existing repository terminology over personal preference.