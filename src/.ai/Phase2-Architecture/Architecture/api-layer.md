# API Layer

Version: 1.0

---

# Purpose

This document defines the responsibilities, boundaries, and design principles of the API Layer.

The API Layer exposes the application's capabilities to external clients while remaining completely independent of business logic.

It acts as the application's transport boundary.

---

# Primary Principle

The API Layer answers one question:

> **How does the outside world communicate with the application?**

It must never answer:

> **How does the business work?**

---

# Responsibilities

The API Layer is responsible for:

* Receiving requests.
* Validating transport models.
* Authenticating users.
* Authorizing access.
* Invoking Application use cases.
* Returning responses.
* Defining API contracts.
* Configuring middleware.
* Configuring OpenAPI documentation.

The API should remain thin.

---

# Contains

Typical API components include:

* Minimal APIs
* Controllers
* Endpoints
* Request Models
* Response Models
* Middleware
* Filters
* Authentication
* Authorization
* Versioning
* OpenAPI Configuration
* Exception Mapping

---

# Does Not Contain

The API Layer must never contain:

* Business rules.
* Aggregate logic.
* Repository usage.
* EF Core.
* SQL.
* Domain calculations.
* Business decisions.

Business behaviour belongs to the Domain.

---

# Endpoint Responsibility

An endpoint should:

1. Receive the request.
2. Validate transport input.
3. Create a Command or Query.
4. Invoke the Application.
5. Return an appropriate response.

Nothing more.

---

# Request Models

Request models represent client input.

They:

* Are transport-specific.
* Are mutable when appropriate.
* Contain no business logic.
* Should not be reused as Domain models.

---

# Response Models

Response models represent API output.

They:

* Expose only required information.
* Hide internal implementation details.
* Should remain independent of Domain objects.

Never return Aggregates directly.

---

# Validation

The API performs transport validation only.

Examples:

* Required fields.
* Invalid JSON.
* Route parameters.
* Query string validation.
* Content type validation.

Business validation belongs to the Domain.

---

# Authentication

Authentication belongs to the API and Infrastructure.

The API determines:

* Who is calling.

It does not determine:

* What business operations are allowed.

---

# Authorization

Authorization policies are enforced before executing Application use cases.

Authorization should remain independent from business behaviour whenever possible.

---

# Mapping

The API maps:

```text id="d8k1qm"
HTTP Request

↓

Command / Query

↓

Application

↓

Response

↓

HTTP Response
```

Mapping should remain explicit.

---

# Exception Handling

Exceptions should be translated into appropriate HTTP responses.

Examples:

* 400 Bad Request
* 401 Unauthorized
* 403 Forbidden
* 404 Not Found
* 409 Conflict
* 500 Internal Server Error

Avoid exposing internal exception details.

---

# HTTP Independence

The Application and Domain layers should remain unaware of HTTP.

HTTP concepts must not appear outside the API.

Examples:

* HttpContext
* HttpRequest
* IActionResult
* IResult

remain inside the API.

---

# Versioning

API versioning should protect clients from breaking changes.

When introducing breaking changes:

* Create a new version.
* Preserve existing contracts where practical.
* Document migration paths.

---

# OpenAPI

Every public endpoint should include:

* Description.
* Parameters.
* Request schema.
* Response schema.
* Status codes.
* Authentication requirements.

API documentation should remain accurate.

---

# Middleware

Middleware belongs exclusively to the API Layer.

Typical middleware includes:

* Exception handling.
* Authentication.
* Authorization.
* Correlation IDs.
* Logging.
* Metrics.
* Rate limiting.
* Compression.

Middleware must not contain business rules.

---

# Dependency Rule

The API may depend on:

* Application
* Domain (only when repository conventions explicitly allow)

The Domain must never depend on the API.

---

# Testing

API tests should verify:

* Routing.
* Authentication.
* Authorization.
* Request validation.
* Response mapping.
* Status codes.
* Serialization.

Business rules should be tested separately.

---

# Anti-Patterns

Avoid:

* Business logic inside Endpoints.
* Repository access inside Controllers.
* Direct DbContext usage.
* Returning Domain objects.
* HTTP types inside Application.
* SQL inside API.
* Fat Controllers.
* Large Endpoint methods.

---

# API Layer Checklist

Before completing an endpoint, verify:

* The endpoint remains thin.
* Business logic exists only in the Domain.
* Requests are mapped explicitly.
* Responses hide internal models.
* HTTP concerns remain inside the API.
* Authentication is configured.
* Authorization is enforced.
* Appropriate status codes are returned.

---

# Guiding Principle

The API Layer is the application's front door.

Its responsibility is to translate communication between clients and the Application—not to implement business behaviour.
