# Security

Version: 1.0

---

# Purpose

This document defines the mandatory security standards for the repository.

Security is a fundamental quality attribute.

Every implementation must protect confidentiality, integrity, and availability by default.

---

# Primary Principle

Secure by default.

Every component should assume that external input is untrusted until validated.

---

# Defense in Depth

Security must exist at multiple layers:

* Network
* API
* Application
* Domain
* Infrastructure
* Database

No single control should be relied upon exclusively.

---

# Authentication

Every protected resource must require authentication.

Authentication mechanisms should be implemented using the repository-approved identity solution.

Business logic should never perform authentication directly.

---

# Authorization

Authentication answers:

> "Who is the caller?"

Authorization answers:

> "What is the caller allowed to do?"

Authorization decisions belong at application boundaries.

Never trust client-side authorization.

---

# Least Privilege

Every component should operate with the minimum permissions required.

Avoid granting unnecessary:

* Database permissions
* API permissions
* File system permissions
* Administrative privileges

---

# Input Validation

All external input must be validated before processing.

Examples include:

* HTTP requests
* gRPC requests
* Queue messages
* File uploads
* Configuration
* User input

Never trust external data.

---

# Output Encoding

Encode output appropriate to the destination.

Examples:

* HTML
* JSON
* XML
* SQL parameters

Avoid output injection vulnerabilities.

---

# SQL Injection

Always use parameterized queries.

Never concatenate user input into SQL statements.

Example:

```text id="s8x2nv"
WHERE CustomerId = @CustomerId
```

Never build SQL through string concatenation.

---

# Command Injection

Never execute operating system commands using untrusted input.

Validate and restrict all command parameters.

---

# Secrets

Never store secrets in:

* Source code
* Configuration files committed to source control
* Logs
* Exception messages

Use the approved secret management solution.

---

# Passwords

Passwords must never be:

* Logged
* Returned to clients
* Stored in plain text

Use approved password hashing algorithms.

---

# Tokens

Access tokens and refresh tokens must never be logged.

Treat all authentication tokens as secrets.

---

# Cryptography

Use only approved cryptographic libraries.

Do not implement custom cryptographic algorithms.

Use modern, industry-standard algorithms.

---

# HTTPS

All external communication must use HTTPS or another approved encrypted transport.

Never transmit sensitive information over unencrypted channels.

---

# Sensitive Data

Protect sensitive data throughout its lifecycle.

Examples:

* Personal information
* Financial data
* Authentication credentials
* Internal identifiers (where applicable)

Collect only the data that is required.

---

# Logging

Logs must never contain:

* Passwords
* Tokens
* Secrets
* Encryption keys
* Sensitive personal information unless explicitly required and protected

Review log messages carefully.

---

# Error Messages

Error responses should not reveal:

* Internal implementation details
* Stack traces
* SQL statements
* Connection strings
* Server configuration

Clients should receive safe, meaningful messages.

---

# File Uploads

Validate uploaded files:

* File type
* File size
* File name
* Content (when appropriate)

Never trust client-provided metadata.

---

# Serialization

Deserialize only trusted formats.

Avoid deserializing untrusted object graphs.

Use safe serializer configurations.

---

# Dependency Management

Use supported package versions.

Regularly update dependencies to address known security vulnerabilities.

Remove unused packages.

---

# Third-Party Libraries

Only approved libraries may be introduced.

Every new dependency should be evaluated for:

* Security
* Maintenance
* Community support
* Licensing

---

# API Security

Protected APIs should implement:

* Authentication
* Authorization
* Rate limiting (where appropriate)
* Request validation
* Secure transport

---

# Database Security

Use least-privilege database accounts.

Never connect using administrative credentials unless operationally required.

---

# Principle of Fail Secure

When security decisions cannot be completed safely, deny access by default.

Avoid insecure fallback behaviour.

---

# Auditing

Security-sensitive operations should be auditable.

Examples:

* Authentication
* Authorization failures
* Administrative actions
* Permission changes

Audit logs should be protected from tampering.

---

# AI Responsibilities

When generating code, the AI must:

* Validate all external input.
* Protect secrets.
* Avoid insecure defaults.
* Use parameterized database access.
* Preserve authentication and authorization boundaries.
* Follow repository security conventions.

---

# Anti-Patterns

Avoid:

* Hard-coded secrets.
* SQL string concatenation.
* Logging tokens or passwords.
* Returning internal exception details.
* Custom cryptography.
* Excessive privileges.
* Trusting client input.
* Disabling security checks for convenience.

---

# Security Checklist

Before completing an implementation, verify:

* External input is validated.
* Secrets are protected.
* Sensitive information is not logged.
* Authentication and authorization are enforced.
* Parameterized database access is used.
* Secure transport is required.
* Least privilege has been applied.
* Repository security conventions are followed.

---

# Guiding Principle

Security is not a feature added later.

Every component should be designed to fail safely, protect sensitive information, and treat all external input as untrusted until proven otherwise.
