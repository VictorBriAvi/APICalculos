# Performance Rules

The API must be optimized for scalability and performance.

---

# Query Optimization

Avoid loading unnecessary data.

Use:

Select projections when possible.

Bad example:

context.Clients.ToList()

Good example:

context.Clients
    .Select(x => new ClientDTO { ... })
    .ToList()

---

# Avoid N+1 Queries

When loading related data use:

Include()

Example:

context.Sales
    .Include(x => x.SaleDetails)

Avoid loops that trigger database queries.

---

# Pagination

Large lists must always support pagination.

Never return thousands of records.

Use:

page
pageSize

Example response:

{
  "data": [],
  "total": 120,
  "page": 1
}

---

# Async Operations

All database operations must be async.

Use:

ToListAsync()  
FirstOrDefaultAsync()  
SaveChangesAsync()

Avoid synchronous calls.

---

# Caching

Frequently requested data should use caching.

Examples:

Payment types  
Service types  
Configuration tables

Use:

MemoryCache or Redis.

---

# DTO Projections

Always return DTOs instead of entities.

DTO projections improve performance and security.

---

# API Response Size

API responses must remain lightweight.

Avoid returning nested objects with unnecessary depth.

Prefer flattened DTO models.