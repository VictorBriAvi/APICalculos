# Database Rules

The database layer uses **Entity Framework Core**.

All database logic must follow clean repository patterns.

---

# DbContext Usage

DbContext must exist only inside:

Infrastructure/Data

Controllers must never access DbContext.

Only repositories may use DbContext.

---

# Entity Configuration

Entity configuration must use:

Fluent API configurations.

Location:

Infrastructure/Data/Configurations

Example:

ClientConfig.cs

---

# Migrations

Migrations must be created using:

dotnet ef migrations add MigrationName

Migrations must never be edited manually unless necessary.

---

# Table Design Rules

Tables must include:

Primary Key  
Proper relationships  
Indexes when needed  

Use:

Guid or int IDs depending on context.

---

# Relationships

Relationships must be defined in:

Entity configuration classes.

Use:

HasOne  
HasMany  
WithMany  
WithOne

Example:

builder
    .HasMany(x => x.Sales)
    .WithOne(x => x.Client)

---

# Soft Deletes

Prefer soft deletes when data must be preserved.

Use:

IsActive flag or DeletedAt field.

Avoid hard deletes for critical data.

---

# Transactions

Complex operations must use:

UnitOfWork transactions.

Example:

Sale creation with multiple SaleDetails.

This ensures consistency.