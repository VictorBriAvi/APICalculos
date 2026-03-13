# Project Map

Root structure:

API/
Application/
Domain/
Infrastructure/

---

# API

API/Controllers

Contains REST controllers.

Each controller calls a corresponding Application service.

Example:

ClientController → IClientService

---

# Application

Contains:

DTOs
Interfaces
Services
Mapping
Validators

Structure:

Application/
  DTOs/
  Interfaces/
  Services/
  Validators/

Services implement business logic.

---

# Domain

Contains core business models.

Structure:

Domain/
  Entities/
  Enums/
  Exceptions/
  ValueObjects/

Domain contains no external dependencies.

---

# Infrastructure

Contains technical implementations.

Structure:

Infrastructure/
  Data/
  Repositories/
  UnitOfWork/
  Extensions/

Repositories implement Application interfaces.