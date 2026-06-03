# Architecture

## Project Overview

A .NET 10 system that simulates the 2019 NFL football season with real-time play-by-play using SignalR. Three main components communicate to replay game data:

| Component | Description | Port |
|---|---|---|
| **Football.Api** | Web API with HTTP endpoints and SignalR Hub | 5001 |
| **Football.Blazor** | Blazor WebAssembly UI displaying live scores and plays | 5002 |
| **Football.Worker** | Background service that reads and sends play data via SignalR | 5003 |

## Clean Architecture Layers

```
src/
├── Domain/              # Entities and enums (no dependencies)
├── Application/         # Business logic, MediatR queries, AutoMapper, FluentValidation
│   └── Features/        # Organized by domain (Games, Plays, Stats)
├── Infrastructure/      # EF Core, SignalR client, Polly, Npgsql
│   ├── Persistence/     # DbContext and configurations
│   └── Services/        # External service implementations
└── Hosts/              # Entry points
    ├── Api/            # ASP.NET Core Web API + SignalR Hub
    ├── Blazor/         # Blazor WebAssembly
    └── Worker/         # Background worker service
```

## Key Patterns

- **CQRS with MediatR**: All queries in `Application/Features/` follow the pattern: Query → Validator → Handler
- **AutoMapper**: DTOs mapped in `Mappings` folders within each feature
- **FluentValidation**: Validators registered via DI in `ConfigureServices.cs`
- **Entity Framework Core**: PostgreSQL with Npgsql provider
- **SignalR**: Hub at `/hub/plays` for real-time communication

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| `GET` | `/api/v1/games?week={week}` | Get all games for a week |
| `GET` | `/api/v1/games/now` | Get current week's games (based on `Scoreboard:Week` setting) |
| `GET` | `/api/v1/games/{id}` | Get specific game |
| `GET` | `/api/v1/games/{id}/stats` | Get game statistics |
| SignalR | `/hub/plays` | Real-time play updates |

## Technology Stack

- **Target Framework**: .NET 10.0 (see `global.json`)
- **Database**: PostgreSQL 17 with Npgsql provider
- **Logging**: Serilog configured in all hosts
- **API Versioning**: `Asp.Versioning` library
- **Data Source**: 2019 NFL season data from https://github.com/ryurko/nflscrapR-data

## Important Configuration

**`Scoreboard:Week` Setting**: Must match between Football.Api and Football.Worker (values 1–17). This determines which week's games are simulated.
