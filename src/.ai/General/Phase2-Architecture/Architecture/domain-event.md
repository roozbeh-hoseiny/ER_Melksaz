# Domain Event

Version: 1.0

---

# Purpose

This document defines the design rules for Domain Events within the Domain Model.

A Domain Event represents a significant business fact that has already occurred.

Domain Events capture important changes in the business and allow other parts of the Domain or Application to react without increasing coupling.

---

# Primary Principle

A Domain Event describes something that **has happened**.

It never describes something that **should happen**.

---

# Definition

A Domain Event is:

* Immutable.
* Expressed in business language.
* Raised by the Domain.
* Raised after successful business behaviour.
* A representation of a completed business fact.

---

# Naming

Domain Events must always be named in the past tense.

Examples:

```text id="j6g5xr"
InvoiceCreated

InvoicePaid

CustomerRegistered

OrderCancelled

PaymentReceived
```

Avoid names such as:

```text id="m9a4tp"
CreateInvoice

PayInvoice

RegisterCustomer

CancelOrder
```

Commands describe intentions.

Domain Events describe facts.

---

# Ownership

A Domain Event belongs to exactly one Aggregate.

The Aggregate that raises the event owns its meaning.

---

# When to Raise Events

Raise a Domain Event when:

* Business state changes.
* An important business milestone is reached.
* Other business processes may need to react.
* The event has business significance.

Do not raise events for every property change.

---

# Business Meaning

A Domain Event should communicate business meaning rather than implementation details.

Good:

```text id="p8t2ka"
InvoicePaid
```

Avoid:

```text id="h1z9cv"
InvoiceStatusChanged
```

Business language should always be preferred.

---

# Immutability

Domain Events must be immutable.

After creation:

* No properties may change.
* No state may change.

A Domain Event represents historical truth.

---

# Event Data

Include only information necessary to describe the business fact.

Typical information includes:

* Aggregate Identifier
* Business values
* Timestamp (when appropriate)
* Business metadata

Avoid including technical information.

---

# Domain Independence

Domain Events must not depend on:

* EF Core
* ASP.NET
* Messaging libraries
* JSON
* HTTP
* Dependency Injection

They remain pure Domain objects.

---

# Raising Events

Only the Aggregate Root should raise Domain Events.

Child Entities should communicate through the Aggregate Root.

This preserves Aggregate consistency.

---

# Publishing Events

The Domain raises events.

The Application or Infrastructure publishes them.

The Domain should never know:

* Event Bus
* RabbitMQ
* Kafka
* Azure Service Bus
* MassTransit

Publishing is an implementation detail.

---

# Domain vs Integration Events

Domain Events are internal.

Integration Events are external.

Example:

```text id="v4k8dq"
InvoicePaid

↓

Application

↓

InvoicePaidIntegrationEvent

↓

Message Broker
```

Do not expose Domain Events directly to external systems.

---

# Event Handlers

Domain Event Handlers should:

* React to business facts.
* Coordinate additional business behaviour.
* Remain independent.

Avoid placing unrelated workflows inside Aggregates.

---

# Event Ordering

Business correctness must never depend on the order in which event handlers execute.

Handlers should be independent whenever possible.

---

# Transactions

Domain Events are raised within the business transaction.

Publishing to external systems should occur only after the transaction succeeds.

---

# Event Granularity

Events should represent meaningful business milestones.

Avoid extremely fine-grained events.

Good:

* PaymentReceived
* ShipmentDispatched

Avoid:

* InvoiceNameChanged
* QuantityUpdated

unless these changes have business significance.

---

# Versioning

When business meaning changes significantly:

* Create a new event.
* Preserve compatibility where required.
* Avoid silently changing event semantics.

---

# Testing

Tests should verify:

* Correct events are raised.
* Incorrect events are not raised.
* Event data is correct.
* Business rules remain intact.

Tests should verify behaviour rather than implementation details.

---

# Anti-Patterns

Avoid:

* Mutable events.
* Technical event names.
* Infrastructure dependencies.
* Publishing directly from the Domain.
* Using events for simple method calls.
* Raising events for insignificant changes.

---

# Domain Event Checklist

Before completing a Domain Event, verify:

* Name is in the past tense.
* Represents a completed business fact.
* Is immutable.
* Contains only business information.
* Is raised by an Aggregate Root.
* Has no infrastructure dependencies.
* Is not exposed directly outside the Domain.

---

# Guiding Principle

A Domain Event records an important business fact.

It allows the business model to communicate significant changes while remaining independent of technical implementation details.
