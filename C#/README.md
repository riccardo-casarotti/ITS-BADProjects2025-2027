# C# — Final Exam Projects

Three ASP.NET Core solutions covering MVC with Entity Framework Core and a Web API.

## BikeStoresProject

ASP.NET Core MVC application for browsing the classic "BikeStores" sample database (sales and production data), using Entity Framework Core with Database First scaffolding.

### Data Model

The `BikeStoresContext` maps 8 entities, split across two database schemas:

**`production` schema**
- `Brand` — bicycle brand, one-to-many with `Product`
- `Category` — product category, one-to-many with `Product`
- `Product` — belongs to a `Brand` and a `Category`; linked to `OrderItem` and `Stock`

**`sales` schema**
- `Customer` — one-to-many with `Order`
- `Store` — one-to-many with `Order`, `Staff`, `Stock`
- `Staff` — belongs to a `Store`; self-referencing relationship (`Manager` / `InverseManager`) for the reporting hierarchy
- `Order` — belongs to a `Customer`, a `Staff` and a `Store`; one-to-many with `OrderItem`
- `OrderItem` — composite key (`OrderId`, `ItemId`); links an `Order` to a `Product` with quantity, list price and discount
- `Stock` — composite key (`StoreId`, `ProductId`); tracks quantity of a product at a store

### Controllers

Each entity has a dedicated controller exposing read-only `Index` (list) and `Details` actions:

| Controller | `Index` behavior |
|---|---|
| `CustomersController` | Search by first/last name |
| `OrdersController` | Search by order id, customer name or store name; date-range filter (`dataDa` / `dataA`) on order date; eager-loads `Customer` and `Store` |
| `ProductsController` | Search by product name, brand or category; sortable ascending/descending by product name; eager-loads `Brand` and `Category` |
| `StaffsController` | Search by first/last name |
| `StoresController` | Search by city or state |
| `HomeController` | Standard landing page + error page (default MVC scaffolding) |

All search/sort state is passed back to the view through `ViewBag` so the filters persist in the UI.

## Istat2

ASP.NET Core MVC project working with Italian geographic data (ISTAT).

> Notes from earlier work: resolved an Entity Framework table name mismatch, and redesigned the Razor layout with a travel-themed aesthetic (including an `@keyframes`/`@media` escaping fix for Razor's `@` interpretation). **Full Models/Controllers not yet reviewed** — upload them for a complete breakdown.

## PrestitiBibliotecaWebAPI

ASP.NET Core Web API for managing library loans ("prestiti biblioteca").

> Only the default project scaffolding has been reviewed so far (`Program.cs`, default `WeatherForecast.cs`, empty `appsettings.json`) — no custom Models, Controllers or DbContext yet available. **Needs the actual Models/Controllers/DbContext files** for a real description of what it does.
