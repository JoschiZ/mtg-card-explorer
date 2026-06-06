# CardExplorer Guidelines

## Tech Stack
- **Frontend:** Blazor WASM with **MudBlazor** component library.
- **Backend:** ASP.NET Core with **FastEndpoints** for API design.
- **Database:** Entity Framework Core (EF Core).
- **Strong Typing:** Use **Vogen** for Value Objects and IDs (e.g., `ScryfallCardId`, `UserId`).

## Project Structure
- `src/CardExplorer`: Server-side logic, Endpoints, Data access, Migrations.
- `src/CardExplorer.Client`: Frontend logic, Blazor components, Shared DTOs.
- `src/CardExplorer.Client/Features`: Group frontend components by feature.
- `src/CardExplorer/Endpoints`: Group API endpoints by resource.

## Coding Standards
- **Strongly Typed IDs:** Always use Vogen-generated ID types instead of raw `Guid`, 'int' or `string` where applicable.
- **API Endpoints:** Use `FastEndpoints` classes. Inherit from `Endpoint<TRequest, TResponse>` or `EndpointWithoutRequest`.
- **UI Components:** Leverage MudBlazor components.
- **Data Access:** Use `ApplicationDbContext` for database operations. Follow the established pattern of using `ApplyConfigurationsFromAssembly`.
- **Naming Conventions:** Follow standard .NET PascalCase for classes and methods. Use camelCase for private fields (with `_` prefix).


## Testing
- Ensure that logic in both Client and Server is covered by unit/integration tests when appropriate.

## General Principles
- Keep the separation between Client and Server clean. 
- Use DTOs for data transfer between Client and Server.
- Prefer explicit over implicit logic.
- Follow existing patterns for dependency injection and configuration.
- The application should be usable for users that are not logged in
