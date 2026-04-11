# Requirements Document

## Introduction

The existing integration tests in `tests/Football.Application.IntegrationTests/` test the Application layer directly by instantiating query handlers and commands with a real database context. This approach bypasses the API layer (routing, validation middleware, response codes, serialization) and requires manual user secrets or environment variables to connect to a pre-existing external database.

The goal is to replace these tests with API-level integration tests that exercise the full request/response pipeline of `Football.Api`, using Testcontainers to spin up an isolated PostgreSQL instance per test run. The new project should live in a new folder under `tests/` and preserve all test scenarios from the original project.

## Requirements

### Requirement 1 — Create a new API integration test project

**User Story:** As a developer, I want a new `Football.Api.IntegrationTests` project under `tests/`, so that integration tests exercise the full HTTP pipeline instead of only the Application layer.

#### Acceptance Criteria

1. WHEN the repository is opened THEN there SHALL be a new project at `tests/Football.Api.IntegrationTests/`.
2. WHEN the new project is built THEN it SHALL reference `Football.Api` and use `Microsoft.AspNetCore.Mvc.Testing` with `WebApplicationFactory`.
3. WHEN the new project is added to the solution THEN the old `tests/Football.Application.IntegrationTests/` project SHALL be removed from the solution and its folder deleted.

---

### Requirement 2 — Use Testcontainers for database lifecycle management

**User Story:** As a developer, I want the test database to be created and destroyed automatically, so that tests can run in any environment without manual setup.

#### Acceptance Criteria

1. WHEN the test suite starts THEN the system SHALL spin up a PostgreSQL 17 container using the `Testcontainers.PostgreSql` NuGet package.
2. WHEN the container is ready THEN the system SHALL seed the database by executing the SQL script at `scripts/testdb/footballscoreboard_testdb.sql`.
3. WHEN the test suite finishes THEN the system SHALL automatically stop and remove the PostgreSQL container.
4. IF a test run is started without Docker available THEN the test suite SHALL fail with a clear error message indicating that Docker is required.
5. WHEN the `WebApplicationFactory` is configured THEN it SHALL override the `FootballDbConnection` connection string with the Testcontainer's connection string so the API connects to the ephemeral database.

---

### Requirement 3 — Reuse `GET /api/v1/games?week={week}` test cases

**User Story:** As a developer, I want tests for the `GetGamesByWeek` endpoint, so that query handling and validation are verified end-to-end.

#### Acceptance Criteria

1. WHEN `GET /api/v1/games?week=1` is called THEN the system SHALL return `200 OK` with a JSON array containing the seeded game(s) for week 1. *(Replaces `GetGamesQueryHandlerTest.Handle_GamesFoundForWeek_ReturnsGameDtoCollection`)*
2. WHEN `GET /api/v1/games?week=99` is called for a week with no seeded games THEN the system SHALL return `200 OK` with an empty JSON array. *(Replaces `GetGamesQueryHandlerTest.Handle_GamesNotFoundForWeek_ReturnsEmptyCollection`)*
3. WHEN `GET /api/v1/games?week=0` is called THEN the system SHALL return `400 Bad Request`. *(Replaces `GetGamesQueryValidatorTest.Validate_WeekIsZero_Throw`)*
4. WHEN `GET /api/v1/games?week=20` is called THEN the system SHALL return `400 Bad Request`. *(Replaces `GetGamesQueryValidatorTest.Validate_WeekIsTwenty_Throw`)*
5. WHEN `GET /api/v1/games?week=10` is called THEN the system SHALL return `200 OK`. *(Replaces `GetGamesQueryValidatorTest.Validate_WeekIsTen_ValidationSuccess`)*

---

### Requirement 4 — Reuse `GET /api/v1/games/{id}` test cases

**User Story:** As a developer, I want tests for the `GetGameById` endpoint, so that lookup by ID and validation are verified end-to-end.

#### Acceptance Criteria

1. WHEN `GET /api/v1/games/2019090500` is called THEN the system SHALL return `200 OK` with a JSON body matching the seeded game with ID `2019090500`. *(Replaces `GetGameQueryHandlerTest.Handle_GameIdExists_ReturnsGameDto`)*
2. WHEN `GET /api/v1/games/999999999` is called for an ID not in the database THEN the system SHALL return `404 Not Found`. *(Replaces `GetGameQueryHandlerTest.Handle_GameIdNotFound_ReturnsNull`)*
3. WHEN `GET /api/v1/games/0` is called THEN the system SHALL return `400 Bad Request`. *(Replaces `GetGameQueryValidatorTest.Validate_GameIdIsZero_Throw`)*
4. WHEN `GET /api/v1/games/1` is called THEN the system SHALL return either `200 OK` or `404 Not Found` (not `400`). *(Replaces `GetGameQueryValidatorTest.Validate_GameIdIsPositive_ValidationSuccess`)*

---

### Requirement 5 — Reuse `GET /api/v1/games/{id}/stats` test cases

**User Story:** As a developer, I want tests for the `GetStatsById` endpoint, so that stat retrieval and validation are verified end-to-end.

#### Acceptance Criteria

1. WHEN `GET /api/v1/games/2019090500/stats` is called THEN the system SHALL return `200 OK` with a JSON body matching the seeded stats for game `2019090500`. *(Replaces `GetGameStatsQueryHandlerTest.Handle_GameStatsFoundForGameId_ReturnsGameStatsDto`)*
2. WHEN `GET /api/v1/games/1/stats` is called for a game not in the database THEN the system SHALL return `404 Not Found`. *(Replaces `GetGameStatsQueryHandlerTest.Handle_GameNotFoundForGameId_ReturnsNull`)*
3. WHEN `GET /api/v1/games/0/stats` is called THEN the system SHALL return `400 Bad Request`. *(Replaces `GetGameStatsQueryValidatorTest.Validate_GameIdIsZero_Throw`)*
4. WHEN `GET /api/v1/games/1/stats` is called THEN the system SHALL return either `200 OK` or `404 Not Found` (not `400`). *(Replaces `GetGameStatsQueryValidatorTest.Validate_GameIdIsPositive_ValidationSuccess`)*

---

### Requirement 6 — Migrate plays and save-stats test cases

**User Story:** As a developer, I want the test scenarios from `GetPlaysQueryHandlerTest` and `SaveGameStatsCommandHandlerTest` to be preserved, so that no coverage is lost during the migration.

#### Acceptance Criteria

1. WHEN the new project is created THEN the `GetPlaysQuery` scenarios SHALL be tested at the application layer within the `WebApplicationFactory` context (i.e., resolved from the DI container) because plays are not exposed as an HTTP endpoint — they are consumed internally by the Worker via SignalR. *(Replaces `GetPlaysQueryHandlerTest.Handle_PlaysFoundForSearchParams_ReturnsPlayDtoCollection` and `Handle_PlaysNotFoundForSearchParams_ReturnsEmptyCollection`)*
2. WHEN the new project is created THEN the `GetPlaysQueryValidator` scenarios SHALL be moved to `Football.Application.UnitTests` because they test only in-memory validation logic and do not require a database or HTTP layer. *(Replaces all `GetPlaysQueryValidatorTest` cases)*
3. WHEN the new project is created THEN the `SaveGameStatsCommand` handler scenario SHALL be tested at the application layer within the `WebApplicationFactory` context because there is no HTTP endpoint that triggers this command. *(Replaces `SaveGameStatsCommandHandlerTest.Handle_ValidGameStats_SaveSuccessful`)*
4. WHEN the new project is created THEN the `SaveGameStatsCommandValidator` scenarios SHALL be moved to `Football.Application.UnitTests` because they test only in-memory validation logic. *(Replaces all `SaveGameStatsCommandValidatorTest` cases)*

---

## Non-Functional Requirements

### Code Architecture and Modularity

- The test project SHALL use a single shared `ApiIntegrationTestFixture` (implementing `IAsyncLifetime`) that manages the Testcontainer and `WebApplicationFactory` lifecycle for the entire test suite, avoiding per-test container spin-up overhead.
- Test classes SHALL use `IClassFixture<ApiIntegrationTestFixture>` to share the fixture.
- The fixture SHALL be the single source of truth for the `HttpClient` used across all test classes.

### Performance

- The PostgreSQL Testcontainer SHALL be started once per test suite run (not once per test class or per test), keeping total test run time acceptable.

### Security

- The Testcontainer database credentials are ephemeral and test-only; they SHALL NOT be committed to any configuration file used outside of test execution.

### Reliability

- Each test SHALL be independent and SHALL NOT rely on execution order.
- Tests that write to the database (e.g., the `SaveGameStatsCommand` test) SHALL use a database transaction rolled back at the end of the test, or otherwise restore state, so subsequent tests see a clean database.

### Usability

- The new project SHALL be runnable with `dotnet test tests/Football.Api.IntegrationTests/` with no additional manual setup beyond having Docker available.
- The project SHALL be included in the solution file so it appears in IDEs alongside the other test projects.
