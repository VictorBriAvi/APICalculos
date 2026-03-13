# Backend Architecture

This API follows **Clean Architecture**.

Layers:

API
Application
Domain
Infrastructure

Dependency rule:

API → Application → Domain
Infrastructure → Domain

Domain must never depend on Application or Infrastructure.

---

# Layer Responsibilities

## API

Location:

API/Controllers

Responsibilities:

- Receive HTTP requests
- Validate input
- Call Application services
- Return responses

Controllers must contain **no business logic**.

---

## Application

Location:

Application/

Responsibilities:

- Business use cases
- DTO definitions
- Service interfaces
- Service implementations

Application coordinates business operations.

---

## Domain

Location:

Domain/

Responsibilities:

- Entities
- Value Objects
- Domain rules
- Domain exceptions

Domain must be **framework independent**.

---

## Infrastructure

Location:

Infrastructure/

Responsibilities:

- EF Core DbContext
- Repository implementations
- Database configurations
- External integrations