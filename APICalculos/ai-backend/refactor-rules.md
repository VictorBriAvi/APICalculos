# Refactoring Rules

When refactoring code:

1. Preserve layer boundaries
2. Do not move business logic to controllers
3. Avoid duplication
4. Extract reusable services
5. Improve naming clarity

---

# Code Smells

The AI must refactor when detecting:

- Fat controllers
- Repeated repository calls
- Duplicate DTOs
- Large services (>300 lines)
- Mixed domain and infrastructure logic

---

# Preferred Improvements

Prefer:

- smaller services
- clear method names
- dependency injection