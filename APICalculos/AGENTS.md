# Backend AI Agent Rules

This repository contains a **.NET Clean Architecture API**.

The AI must follow the architectural rules defined in `/ai-backend`.

Primary language: Spanish

All explanations must be written in Spanish.

Code must follow C# conventions.

---

# Architecture

This project follows **Clean Architecture**:

API → Application → Domain  
Infrastructure → Domain

Rules:

- Controllers must contain no business logic.
- Business logic belongs to Application Services.
- Domain must be independent of frameworks.
- Infrastructure implements repository interfaces.

---

# Development Flow

When implementing new features:

1. Define Domain entities if needed
2. Define Application DTOs
3. Define Application interfaces
4. Implement Infrastructure repositories
5. Implement Application services
6. Expose endpoints in API controllers

Never skip layers.

---

# Forbidden

The AI must NEVER:

- Access DbContext inside Controllers
- Add business logic in Controllers
- Return Entities directly from API
- Use Infrastructure inside Domain
- Mix DTOs with Entities

Always maintain strict layer separation.