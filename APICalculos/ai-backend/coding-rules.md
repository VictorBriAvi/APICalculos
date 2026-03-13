# Coding Rules

Language: C#

All code must follow SOLID principles.

---

# Controller Rules

Controllers must:

- Be thin
- Call only Application services
- Return DTO responses

Bad example:

Controller → DbContext

Good example:

Controller → Service → Repository

---

# Service Rules

Services must:

- Contain business logic
- Use repositories
- Validate domain rules

Services must not access HTTP context.

---

# Repository Rules

Repositories must:

- Use Entity Framework Core
- Access DbContext
- Contain database logic only

Repositories must not contain business logic.

---

# DTO Rules

DTOs must be used:

- for requests
- for responses

Entities must never be exposed to the API directly.

---

# Mapping

Use AutoMapper.

Mapping location:

Application/Mapping