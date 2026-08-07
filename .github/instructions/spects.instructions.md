---
description: "Use when creating or modifying any code in this project. Enforces Clean Architecture, Angular standalone components, versioned REST APIs, and EF Core usage."
applyTo: "**"
---

# AcademicoApp — Project Requirements

## Architecture: Clean Architecture

Structure all backend code into four layers. Never cross layer boundaries in the wrong direction (outer layers depend on inner layers, never the reverse).

```
Domain/          → Entities, value objects, domain events, interfaces
Application/     → Use cases, DTOs, commands/queries, service interfaces
Infrastructure/  → EF Core DbContext, repositories, external services
Presentation/    → Controllers, middleware, request/response models
```

- Domain must have **zero dependencies** on other layers or NuGet packages (except primitives).
- Application depends only on Domain.
- Infrastructure depends on Application and Domain.
- Presentation depends on Application.
- Use interfaces defined in Application/Domain; implement them in Infrastructure.

## Backend: .NET / C#

- Use **EF Core** for all data access. No raw SQL, no ADO.NET, no Dapper.
  - Use strongly typed LINQ queries.
  - Define entity configurations with `IEntityTypeConfiguration<T>` in Infrastructure.
- Define repository interfaces in the **Domain** or **Application** layer; implement in **Infrastructure**.
- Register all dependencies via **Dependency Injection** in `Program.cs` or dedicated extension methods.
- Never use `static` classes or methods for business logic.
- Use `async/await` consistently — no `.Result` or `.Wait()` on Tasks.

## REST API: Versioning

- All API controllers must be versioned.
- Use URL-segment versioning: `/api/v1/resource`, `/api/v2/resource`.
- Annotate controllers with `[ApiVersion("x.x")]` and `[Route("api/v{version:apiVersion}/[controller]")]`.
- Do not expose domain entities directly — always map to DTOs in the Application layer.
- Return standard HTTP status codes (`200`, `201`, `400`, `404`, `409`, `500`).

## Frontend: Angular

- Use **standalone components** exclusively — no `NgModule` declarations.
  - Every component, directive, and pipe must use `standalone: true`.
  - Import dependencies directly in the component's `imports` array.
- Use the Angular **`inject()`** function instead of constructor injection where possible.
- Organize features into self-contained feature folders under `src/app/features/`.
- Use `HttpClient` via `provideHttpClient()` in `app.config.ts` — no `HttpClientModule`.
- Lazy-load all feature routes using `loadComponent` or `loadChildren`.

## TypeScript

- Enable `strict` mode in `tsconfig.json`.
- No `any` — use explicit types or generics.
- Prefer `interface` over `type` for object shapes.
- Use `readonly` on properties that must not change after initialization.

## General

- All public methods must have a single, clear responsibility.
- Validation belongs in the Application layer (e.g., FluentValidation or custom validators).
- Do not include business logic in controllers or components — delegate to services/use cases.
- Keep files small and focused; split when a file exceeds ~250 lines.
