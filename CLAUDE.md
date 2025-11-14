# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This repository simulates the 2019 NFL football season through three interconnected applications:
- **Football.Api**: ASP.NET Core Web API with SignalR Hub for real-time play-by-play communication
- **Football.Blazor**: Blazor WebAssembly UI displaying live game scores and statistics
- **Football.Worker**: Background service that replays game data from the database and sends it to the SignalR Hub

The applications must be launched in this specific order: API → Blazor → Worker.

## Architecture

The solution follows Clean Architecture principles with clear separation of concerns:

### Layer Structure

```
src/
├── Domain/           # Entities and enums (Game, Play, Stat, GameState)
├── Application/      # Business logic using CQRS pattern
├── Infrastructure/   # Persistence, external services, SignalR
└── Hosts/           # Entry points
    ├── Api/         # REST API + SignalR Hub
    ├── Blazor/      # WebAssembly UI
    └── Worker/      # Background service
```

### Key Patterns

**CQRS with MediatR**: All application logic is implemented as queries and commands (e.g., `GetGamesQuery`, `SaveGameStatsCommand`) with corresponding handlers. Each query/command has a FluentValidation validator.

**Dependency Injection Setup**: Each layer exposes a `ConfigureServices` extension method:
- `AddApplicationServices()` - Registers AutoMapper, FluentValidation, MediatR
- `AddInfrastructureServices(IConfiguration)` - Registers EF Core DbContext, options

**SignalR Communication**: The Worker sends `PlayDto` objects to the `PlayHub` (/hub/plays), which broadcasts to all connected Blazor clients. The Hub implements `Hub<IPlayClient>` for strongly-typed client methods.

**Environment Handling**: Uses custom environment extension `IsLocalhost()` in addition to standard `IsDevelopment()`. Localhost environment loads user secrets and enables Swagger.

**Database**: MySQL 8.0.28 accessed through Entity Framework Core with explicit configurations in `Infrastructure/Persistence/Configurations/`.

## Development Commands

### Tool Setup
```bash
# Restore local dotnet tools (required once after cloning)
dotnet tool restore
```

### Building
```bash
dotnet restore
dotnet build --configuration Release --no-restore
```

### Running Tests
```bash
# All tests (requires test database running on port 3307)
dotnet test --no-restore

# With coverage
dotnet dotnet-coverage collect 'dotnet test --no-restore' -f cobertura -o 'coverage.xml'

# Generate HTML coverage report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"
```

### Running Individual Tests
```bash
# Run specific test project
dotnet test tests/Football.Application.UnitTests/Football.Application.UnitTests.csproj

# Run specific test
dotnet test --filter "FullyQualifiedName~MapGameDtoFromEntityProfileTest"
```

### Database Setup

**Test Database** (port 3307, required for integration tests):
```bash
docker-compose -f docker-compose.testdb.yml up -d
```
Requires `.env.testdb` file with MYSQL_ROOT_PASSWORD, MYSQL_USER, MYSQL_PASSWORD.

**Local Database** (port 3306, required for running applications):
```bash
docker-compose -f docker-compose.localdb.yml up -d
```
Requires `.env.localdb` file with MYSQL_ROOT_PASSWORD, MYSQL_USER, MYSQL_PASSWORD.

### Running Applications Locally

**API** (https://localhost:5001):
```bash
cd src/Hosts/Api
dotnet run
```
- Uses `appsettings.Localhost.json` when ASPNETCORE_ENVIRONMENT=Localhost
- Connection string must be in user secrets: `ConnectionStrings:FootballDbConnection`
- Set `Scoreboard:Week` (1-17) to match Worker

**Blazor** (https://localhost:5002):
```bash
cd src/Hosts/Blazor
dotnet run
```
- Configure `Hub:HubUrl` and `Api:ApiBaseUrl` in `wwwroot/appsettings.Localhost.json`

**Worker** (https://localhost:5003):
```bash
cd src/Hosts/Worker
dotnet run
```
- Set `Hub:HubUrl` and `Scoreboard:Week` in `appsettings.Localhost.json`
- Connection string must be in user secrets

## Testing

**Unit Tests**: `Football.Application.UnitTests`, `Football.Api.UnitTests`, `Football.Blazor.UnitTests`
- Focus on AutoMapper profiles and validation logic
- Use xUnit framework

**Integration Tests**: `Football.Application.IntegrationTests`
- Test database operations end-to-end
- Require test database container running on port 3307
- Use connection string environment variable: `MYSQLCONNSTR_FootballDbConnection`

## Important Configuration Notes

**Week Synchronization**: The `Scoreboard:Week` setting MUST match between Football.Api and Football.Worker for the simulation to work correctly.

**CORS Configuration**: API must allow origin `https://localhost:5002` for Blazor client to connect.

**SignalR Hub URL**: All applications reference `/hub/plays` endpoint on the API server.

**Docker Certificates**: When running via Docker, API and Worker require custom SSL certificates. See individual README files in src/Hosts/ for certificate generation commands.

## CI/CD

GitHub Actions workflow (`.github/workflows/ci.yml`) runs on all pushes:
1. Spins up MySQL test database container
2. Restores and builds in Release mode
3. Runs all tests with coverage
4. Sends coverage to SonarCloud

Uses .NET 8.0.407 SDK (see `global.json`).

## Key Files for Understanding System Flow

- `src/Hosts/Api/Hubs/PlayHub.cs` - SignalR Hub receiving plays from Worker
- `src/Application/Features/Games/GetGamesQuery.cs` - Example CQRS query pattern
- `src/Infrastructure/Persistence/FootballDbContext.cs` - EF Core context
- `src/Hosts/Blazor/Components/GameCard.razor` - Example Blazor component
