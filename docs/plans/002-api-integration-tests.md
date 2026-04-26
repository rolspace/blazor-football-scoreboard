# Plan: API-Level Integration Tests

## Context
The existing `Football.Application.IntegrationTests` project tests query handlers and commands by directly instantiating them with a real `FootballDbContext`. This bypasses the full API pipeline (routing, model binding, FluentValidation middleware, HTTP status codes, JSON serialization). It also requires a manually configured external database via user secrets or environment variables.

The goal is to replace this project with `Football.Api.IntegrationTests`, which sends real HTTP requests through `WebApplicationFactory<Program>` and uses a Testcontainers-managed PostgreSQL instance seeded from `scripts/testdb/footballscoreboard_testdb.sql`. All 23 original test cases must be preserved.

---

## Files to Create

### `tests/Football.Api.IntegrationTests/Football.Api.IntegrationTests.csproj`
- Target: `net10.0`
- Project reference: `src/Hosts/Api/Football.Api.csproj`
- NuGet packages:
  - `Microsoft.AspNetCore.Mvc.Testing` 10.0.0
  - `Testcontainers.PostgreSql` 4.4.0
  - `FluentAssertions` 8.2.0
  - `Microsoft.NET.Test.Sdk` 17.13.0
  - `xunit` 2.9.3
  - `xunit.runner.visualstudio` 3.0.2
  - `coverlet.collector` 6.0.4
- Include the seed SQL as content copied to output:
  ```xml
  <Content Include="..\..\..\scripts\testdb\footballscoreboard_testdb.sql">
    <CopyToOutputDirectory>Always</CopyToOutputDirectory>
  </Content>
  ```

### `tests/Football.Api.IntegrationTests/Fixtures/ApiIntegrationTestFixture.cs`
Single shared fixture implementing `IAsyncLifetime`:
1. **InitializeAsync**: Start a `PostgreSqlContainer` (image: `postgres:17`), then seed the database (see below), then build `WebApplicationFactory<Program>` with `WithWebHostBuilder` calling `builder.UseSetting("ConnectionStrings:FootballDbConnection", container.GetConnectionString())` and `builder.UseSetting("Scoreboard:Week", "1")`. Expose `HttpClient` (created once) and `IServiceProvider Services`.
2. **Seeding**: Read the SQL file bytes from `AppContext.BaseDirectory`, copy them into the container at `/tmp/seed.sql` via `CopyAsync` (using `DotNet.Testcontainers.Configurations.UnixFileModes`), then run `psql --username ... --dbname ... --file /tmp/seed.sql` via `ExecAsync`. The username and database name are parsed from the connection string using `NpgsqlConnectionStringBuilder`. **Do NOT use `NpgsqlCommand` to run the seed SQL** — the dump file uses `COPY ... FROM stdin` statements that require the PostgreSQL wire-level COPY protocol, which `NpgsqlCommand` does not support.
3. **DisposeAsync**: Dispose `HttpClient`, `WebApplicationFactory`, stop and dispose the container.

### `tests/Football.Api.IntegrationTests/Fixtures/ApiIntegrationTestCollection.cs`
```csharp
[CollectionDefinition(ApiIntegrationTestCollection.Name)]
public class ApiIntegrationTestCollection : ICollectionFixture<ApiIntegrationTestFixture>
{
    public const string Name = "ApiIntegrationTests";
}
```
This ensures the container and `WebApplicationFactory` are started **once** for all test classes.

### `tests/Football.Api.IntegrationTests/GetGamesByWeekTests.cs`
`[Collection(ApiIntegrationTestCollection.Name)]` — 5 test cases against `GET /api/v1/games?week={w}`:
| Test method | Input | Expected |
|---|---|---|
| `GetGamesByWeek_ValidWeek_Returns200WithGames` | week=1 | 200, array with game 2019090500 |
| `GetGamesByWeek_WeekZero_Returns400` | week=0 | 400 |
| `GetGamesByWeek_WeekTwenty_Returns400` | week=20 | 400 |
| `GetGamesByWeek_WeekTen_Returns200` | week=10 | 200 |

### `tests/Football.Api.IntegrationTests/GetGameByIdTests.cs`
`[Collection(ApiIntegrationTestCollection.Name)]` — 4 test cases against `GET /api/v1/games/{id}`:
| Test method | Input | Expected |
|---|---|---|
| `GetGameById_ExistingId_Returns200WithGame` | id=2019090500 | 200, game DTO |
| `GetGameById_NonExistentId_Returns404` | id=999999999 | 404 |
| `GetGameById_IdZero_Returns400` | id=0 | 400 |
| `GetGameById_PositiveId_DoesNotReturn400` | id=1 | 200 or 404 |

### `tests/Football.Api.IntegrationTests/GetStatsByIdTests.cs`
`[Collection(ApiIntegrationTestCollection.Name)]` — 4 test cases against `GET /api/v1/games/{id}/stats`:
| Test method | Input | Expected |
|---|---|---|
| `GetStatsById_ExistingId_Returns200WithStats` | id=2019090500 | 200, stats DTO with CHI and GB entries |
| `GetStatsById_NonExistentId_Returns404` | id=1 | 404 |
| `GetStatsById_IdZero_Returns400` | id=0 | 400 |
| `GetStatsById_PositiveId_DoesNotReturn400` | id=1 | 200 or 404 |

### `tests/Football.Api.IntegrationTests/GetPlaysQueryTests.cs`
`[Collection(ApiIntegrationTestCollection.Name)]` — 2 test cases using the DI-resolved handler (no HTTP endpoint exists for plays):
- Resolve `IMediator` from `fixture.Services.CreateScope()` and call `Send(new GetPlaysQuery {...})`.
- `GetPlays_MatchingParams_ReturnsPlayCollection` — Week=1, Quarter=1, QSR=900 → 2 plays
- `GetPlays_NoMatchingParams_ReturnsEmpty` — Week=1, Quarter=1, QSR=1000 → empty

### `tests/Football.Api.IntegrationTests/SaveGameStatsCommandTests.cs`
`[Collection(ApiIntegrationTestCollection.Name)]` — 1 test case using the DI-resolved handler:
- Resolve `IMediator` and `FootballDbContext` from a scope.
- Begin a transaction on the `DbContext`, call `Send(new SaveGameStatsCommand {...})`, assert the stat row was written, then roll back the transaction to leave the DB clean.

### `tests/Football.Api.IntegrationTests/Usings.cs`
Global usings for the project.

---

## Files to Modify

### `tests/Football.Application.UnitTests/Football.Application.UnitTests.csproj`
Add two NuGet packages required by the moved validator tests:
- `FluentValidation.TestHelper` (match version used by Application project, `11.11.0`)

### `tests/Football.Application.UnitTests/GetPlaysQueryValidatorTest.cs` *(new file)*
Copy from `Football.Application.IntegrationTests/GetPlaysQueryValidatorTest.cs` with namespace updated to `Football.Application.UnitTests`.

### `tests/Football.Application.UnitTests/SaveGameStatsCommandValidatorTest.cs` *(new file)*
Copy from `Football.Application.IntegrationTests/SaveGameStatsCommandValidatorTest.cs` with namespace updated.

### `Scoreboard.sln`
- Remove the `Football.Application.IntegrationTests` project entry (GUID `{A6FC206F-F3C4-41A1-96BD-D6BFC1BF522C}`) from the `Project(...)` block, `ProjectConfigurationPlatforms`, and `NestedProjects`.
- Add a new `Football.Api.IntegrationTests` project entry with a new GUID, nested under the `tests` solution folder (`{98905046-DCD0-49E1-B146-8B78F006F800}`).

Use `dotnet sln` CLI commands to handle solution edits cleanly rather than editing the `.sln` file by hand.

---

## Files to Delete
- `tests/Football.Application.IntegrationTests/` — entire folder

---

## Key Technical Decisions

- **`ICollectionFixture` over `IClassFixture`**: Ensures the Testcontainer and `WebApplicationFactory` are started once for the whole suite, not once per test class. All test classes are annotated with `[Collection(ApiIntegrationTestCollection.Name)]`.
- **`WebApplicationFactory<Program>` works** because `Program.cs` declares `public partial class Program {}` at the bottom.
- **Connection string override** via `builder.UseSetting("ConnectionStrings:FootballDbConnection", ...)` inside `WithWebHostBuilder` — this takes precedence over any `appsettings.json` or user secrets.
- **Environment**: `WebApplicationFactory` defaults to `"Development"`, which means `IsLocalhost()` returns false (user secrets not loaded), CORS is not configured (no `Cors` config section in test settings), and DI scope/build validation is enabled — all desirable.
- **Plays and SaveStats tests**: Resolved directly from `fixture.Services.CreateScope()` since there are no HTTP endpoints for these operations. This keeps the fixture as the single database authority and avoids needing a second `DbContext` fixture.
- **Transaction rollback** for `SaveGameStatsCommandTests` to keep the database clean between test runs.
- **Seed SQL path**: Copied to output directory via `<CopyToOutputDirectory>Always</CopyToOutputDirectory>` in the `.csproj`, read at runtime via `AppContext.BaseDirectory`.
- **Seeding via psql, not NpgsqlCommand**: The seed file is a `pg_dump` export that uses `COPY ... FROM stdin` statements. `NpgsqlCommand.ExecuteNonQueryAsync` cannot handle these — they require the PostgreSQL wire-level COPY protocol. The fix is to copy the SQL file into the running container and execute it with `psql` via `container.ExecAsync`. The username and database name are extracted from the connection string using `NpgsqlConnectionStringBuilder`. The file mode argument to `CopyAsync` must use `DotNet.Testcontainers.Configurations.UnixFileModes`, not `System.IO.UnixFileMode`.

---

## Verification

```bash
# 1. Build passes
dotnet build

# 2. New integration tests pass (requires Docker)
dotnet test tests/Football.Api.IntegrationTests/

# 3. Moved validator tests pass
dotnet test tests/Football.Application.UnitTests/

# 4. All other test projects still pass
dotnet test tests/Football.Api.UnitTests/
dotnet test tests/Football.Worker.UnitTests/
dotnet test tests/Football.Blazor.UnitTests/

# 5. Full suite
dotnet test
```
