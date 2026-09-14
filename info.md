# Blazor Crud App

## Setup

### Day 1
- Create solution, client, api and shared project
- Install & Setup Radzen Component
- Create appsettings: base, development, and production
- Create & Register ApiSettings class
- Use RadzenGrid, RadzenBadge, RadzenButton
- Install Microsoft.Extensions.Http
- Create Extensions in Shared Project: Enum and Number Extensions
- Install & Register Scalar.ApstNetCore
- Install Microsoft.EntityFrameworkCore.Sqlite, and Microsoft.EntityFrameworkCore.Design
- Create first entity Product.cs
- Create AppDbContext.cs and set Product
- Register AppDbContext in Program.cs
- Create first migration `dotnet ef migrations add InitialCreate`
- Apply migration and create database `dotnet ef database update`
- Add Cors Allowed Origins in appsettings
- Setup Cors Allowed Origins in Program.cs
- Create ProductEndpoints to segregate concern of endpoints mapping
- Create ProductService to abstract business logic and database operation
- Call products endpoints in Client


## Tables

1. Product
   - id (primary key)
   - name
   - sku
   - price
   - category_id
   - creator_id
   - modifier_id
   - created_at
   - modified_at
2. Inventory
   - id (primary key)
   - product_id (foreign key)
   - reorder_level
   - quantity
   - creator_id
   - modifier_id
   - created_at
   - modified_at
3. Stock Movement
   - id
   - inventory_id
   - delta
   - unit_cost
   - creator_id
   - created_at