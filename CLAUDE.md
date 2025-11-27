# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A .NET 10 system that simulates the 2019 NFL football season with real-time play-by-play using SignalR. Three main components communicate to replay game data:

- **Football.Api**: Web API with HTTP endpoints and SignalR Hub (port 5001)
- **Football.Blazor**: Blazor WebAssembly UI displaying live scores and plays (port 5002)
- **Football.Worker**: Background service that reads and sends play data via SignalR (port 5003)

## Architecture

### Clean Architecture Layers

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

### Key Patterns

- **CQRS with MediatR**: All queries in `Application/Features/` follow the pattern: Query → Validator → Handler
- **AutoMapper**: DTOs mapped in `Mappings` folders within each feature
- **FluentValidation**: Validators registered via DI in `ConfigureServices.cs`
- **Entity Framework Core**: PostgreSQL with Npgsql provider (migrated from MySQL)
- **SignalR**: Hub at `/hub/plays` for real-time communication

### Important Configuration

**Scoreboard:Week Setting**: Must match between Football.Api and Football.Worker (values 1-17). This determines which week's games are simulated.

## Build and Test Commands

### Build
```bash
dotnet build
```

### Run All Tests
```bash
dotnet test
```

### Run Specific Test Project
```bash
dotnet test tests/Football.Api.UnitTests/
dotnet test tests/Football.Application.UnitTests/
dotnet test tests/Football.Application.IntegrationTests/
dotnet test tests/Football.Blazor.UnitTests/
dotnet test tests/Football.Worker.UnitTests/
```

### Test Coverage
```bash
# Restore tools first
dotnet tool restore

# Collect coverage
dotnet dotnet-coverage collect 'dotnet test --no-restore' -f xml -o 'coverage.xml'

# Generate HTML report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"

# View report
start coverage/index.html  # Windows
open coverage/index.html   # macOS
```

## Database Setup

### Test Database (PostgreSQL 17, Port 5433)

Required for integration tests.

1. Create `.env.testdb` at repository root:
```env
POSTGRES_PASSWORD=your_password
POSTGRES_USER=postgres
```

2. Start database:
```bash
docker-compose -f docker-compose.testdb.yml up -d
```

Database name: `footballscoreboard_db`
Adminer UI: http://localhost:8081

3. Configure connection for integration tests:
```bash
dotnet user-secrets set "ConnectionStrings:FootballDbConnection" "Host=localhost;Port=5433;Database=footballscoreboard_db;Username=postgres;Password=your_password"
```

Or set environment variable:
```bash
export CUSTOMCONNSTR_FootballDbConnection="Host=localhost;Port=5433;Database=footballscoreboard_db;Username=postgres;Password=your_password"
```

### Local Development Database (PostgreSQL 17, Port 5432)

Required for running the applications locally.

1. Create `.env.localdb` at repository root:
```env
POSTGRES_PASSWORD=your_password
POSTGRES_USER=postgres
```

2. Start database:
```bash
docker-compose -f docker-compose.localdb.yml up -d
```

Database is seeded from `scripts/localdb/footballscoreboard_localdb.sql` (takes a few minutes on first start).
Adminer UI: http://localhost:8080

## Running Applications Locally

Applications must start in order: API → Blazor → Worker

### Football.Api

**Via dotnet CLI:**
```bash
cd src/Hosts/Api
dotnet run
```
Environment: `Localhost`, uses `appsettings.Localhost.json`

**Required user secrets:**
```bash
dotnet user-secrets set "ConnectionStrings:FootballDbConnection" "Host=localhost;Port=5432;Database=footballscoreboard_db;Username=postgres;Password=your_password"
```

**Required appsettings.Localhost.json:**
- `Cors:PolicyName`: CORS policy name
- `Cors:AllowedOrigins`: `https://localhost:5002`
- `Cors:AllowedMethods`: `GET`
- `Scoreboard:Week`: 1-17

URL: https://localhost:5001

**Via Docker:**
Requires custom certificate in `.docker/volumes/certs/api/`. See `src/Hosts/Api/README.md`.

### Football.Blazor

**Via dotnet CLI:**
```bash
cd src/Hosts/Blazor
dotnet run
```
Environment: `Localhost`, uses `wwwroot/appsettings.Localhost.json`

**Required appsettings.Localhost.json:**
- `Hub:HubUrl`: SignalR Hub URL (from Football.Api)
- `Api:ApiBaseUrl`: Base API URL (from Football.Api)

URL: https://localhost:5002

### Football.Worker

**Via dotnet CLI:**
```bash
cd src/Hosts/Worker
dotnet run
```
Environment: `Localhost`, uses `appsettings.Localhost.json`

**Required user secrets:**
```bash
dotnet user-secrets set "ConnectionStrings:FootballDbConnection" "Host=localhost;Port=5432;Database=footballscoreboard_db;Username=postgres;Password=your_password"
```

**Required appsettings.Localhost.json:**
- `Hub:HubUrl`: SignalR Hub URL (from Football.Api)
- `Scoreboard:Week`: 1-17 (must match Football.Api)

URL: https://localhost:5003 (no UI, console output only)

## API Endpoints

- `GET /api/v1/games?week={week}` - Get all games for a week
- `GET /api/v1/games/now` - Get current week's games (based on Scoreboard:Week setting)
- `GET /api/v1/games/{id}` - Get specific game
- `GET /api/v1/games/{id}/stats` - Get game statistics
- SignalR Hub: `/hub/plays` - Real-time play updates

## Development Notes

- **Target Framework**: .NET 10.0 (see `global.json`)
- **Database**: PostgreSQL 17 with Npgsql provider
- **Logging**: Serilog configured in all hosts
- **API Versioning**: Uses `Asp.Versioning` library
- **Data Source**: 2019 NFL season data from https://github.com/ryurko/nflscrapR-data
- **VSCode Launch Configs**: Available in `.vscode/launch.json` for all three applications
