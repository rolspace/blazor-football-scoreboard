# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A .NET 10 system that simulates the 2019 NFL football season with real-time play-by-play using SignalR. Three main components communicate to replay game data:

- **Football.Api**: Web API with HTTP endpoints and SignalR Hub (port 5001)
- **Football.Blazor**: Blazor WebAssembly UI displaying live scores and plays (port 5002)
- **Football.Worker**: Background service that reads and sends play data via SignalR (port 5003)

See [ARCHITECTURE.md](ARCHITECTURE.md) for the full technical design, layer structure, key patterns, API endpoints, and technology stack.

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
dotnet test tests/Football.Api.IntegrationTests/
dotnet test tests/Football.Api.UnitTests/
dotnet test tests/Football.Application.UnitTests/
dotnet test tests/Football.Blazor.UnitTests/
dotnet test tests/Football.Worker.UnitTests/
```

* The integration tests require Docker to run.

### Test Coverage
```bash
# Restore tools first
dotnet tool restore

# Collect coverage
dotnet dotnet-coverage collect 'dotnet test --no-restore' -f cobertura -o 'coverage.xml'

# Generate HTML report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"

# View report
start coverage/index.html  # Windows
open coverage/index.html   # macOS
```

## Database Setup

### Test Database (PostgreSQL 17, Port 5432)

Required for running the integration tests.
Requires Docker.

The test database is launched automatically when running the Football.Api.IntegrationTests via Testcontainers.
Once the integration tests are finished, the database container is removed.

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
Database name: `footballscoreboard_db`
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

## Development Notes

**VSCode Launch Configs**: Available in `.vscode/launch.json` for all three applications
