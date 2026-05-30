# Renovation API

A .NET 7 REST API for a renovation-services platform. Personal full-stack project.

Companion frontend: [renovation.FE](https://github.com/AndrzejDar/renovation.FE)
(Next.js + TypeScript + shadcn/ui).

## Stack

- C# / .NET 7
- ASP.NET Core Web API
- Entity Framework Core (SQL Server provider)
- ASP.NET Core Identity + JWT bearer auth
- AutoMapper
- Swagger / OpenAPI

## Configuration

The JWT signing key is not committed; supply it via `dotnet user-secrets` or an
environment variable before running the API:

```bash
dotnet user-secrets init --project Renovation.API
dotnet user-secrets set "Jwt:Key" "<your-256-bit-secret>" --project Renovation.API
```

`ConnectionStrings:RenovationConnectionString` and
`ConnectionStrings:RenovationAuthConnectionString` can be overridden the same way.

## What's here

The solution is organised around the repository pattern with two DbContexts —
one for the business domain (projects, regions, rooms) and one for auth/identity:

```
Renovation.API/
  Controllers/         AuthController, ProjectsController, RegionsController, UsersController
  Models/
    Domain/            Project, Region, Room, RoomType, RenovationTask
    DTO/               request/response shapes
  Repositories/        IRegionRepository / SQLRegionRepository, etc.
  Data/
    RenovationDbContext.cs       business data
    RenovationAuthDbContext.cs   identity / auth
  Migrations/          EF Core migrations for both contexts
  Mappings/            AutoMapper profiles
  Program.cs           composition root
```

## Running locally

Requires .NET 7 SDK and a reachable SQL Server instance. Update the connection
strings in `appsettings.json` to point at your DB, then:

```bash
dotnet restore
dotnet ef database update --context RenovationDbContext
dotnet ef database update --context RenovationAuthDbContext
dotnet run --project Renovation.API
```

Swagger UI mounts at `https://localhost:<port>/swagger` (port from `launchSettings.json`).

## Status

Not deployed publicly. Built as a personal full-stack exercise covering the
backend half of the renovation services platform; the frontend lives in
[renovation.FE](https://github.com/AndrzejDar/renovation.FE).
