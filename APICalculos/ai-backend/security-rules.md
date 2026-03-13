# Security Rules

This API must follow secure development practices.

Security is mandatory in all implementations.

---

# Authentication

Authentication must use:

- JWT tokens
- Secure password hashing
- Role-based authorization

Never store plain text passwords.

Passwords must be hashed using:

BCrypt or ASP.NET Identity password hasher.

---

# Authorization

Controllers must protect sensitive endpoints using:

[Authorize]

Roles must be enforced when necessary.

Example:

[Authorize(Roles = "Admin")]

---

# Input Validation

All incoming data must be validated.

Validation must occur in:

Application layer using Validators.

Never trust client input.

---

# Sensitive Data

Never expose:

- passwords
- security tokens
- internal IDs if unnecessary
- private system data

DTOs must filter sensitive properties.

---

# SQL Injection

Always use:

Entity Framework LINQ queries.

Never build raw SQL strings from user input.

Bad example:

string query = "SELECT * FROM Users WHERE name = '" + name + "'"

Good example:

context.Users.Where(x => x.Name == name)

---

# Error Handling

API errors must not expose internal stack traces.

Use controlled exception handling.

Return safe responses such as:

400 BadRequest  
401 Unauthorized  
403 Forbidden  
500 Internal Server Error

---

# Logging

Sensitive information must never appear in logs.

Do not log:

- passwords
- tokens
- personal data