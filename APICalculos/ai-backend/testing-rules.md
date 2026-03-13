# Testing Rules

Testing priority:

1. Application services
2. Domain logic
3. Controllers

Repositories are tested with integration tests.

---

# Unit Testing

Unit tests must:

- mock repositories
- test business logic
- verify edge cases

---

# Integration Testing

Integration tests must:

- test API endpoints
- validate database operations
- ensure mappings are correct