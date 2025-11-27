# Blazor Football Scoreboard

[![ci](https://github.com/rolspace/blazor-football-scoreboard/actions/workflows/ci.yml/badge.svg)](https://github.com/rolspace/blazor-football-scoreboard/actions/workflows/ci.yml) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=rolspace_blazor-football-scoreboard&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=rolspace_blazor-football-scoreboard) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=rolspace_blazor-football-scoreboard&metric=coverage)](https://sonarcloud.io/summary/new_code?id=rolspace_blazor-football-scoreboard)

This repository contains libraries and web applications that are used to run a simulation of the 2019 football season.

The system is split into three main elements:
- **Web API**: exposes HTTP endpoints to get data and statistics for football games from the 2019 regular season. It includes a SignalR Hub used to send messages to connected clients.
- **Blazor UI**: displays the scores and the play-by-play for each game. The user can also browse to a dedicated page for each game, with additional data.
- **Web Worker**: reads play data from an existing database. This process sends the play data to the SignalR Hub, which communicates with the Blazor client.

> [!NOTE]
> The data for the 2019 regular season games was obtained from https://github.com/ryurko/nflscrapR-data.

## Requirements

- .NET 10+ SDK
- Visual Studio Code 1.83+
- Docker Desktop 4.53+

## How to run locally

In order to run the system, the applications must be launched in the following order:
1. Web API ([Football.Api](/src/Hosts/Api/))
2. Blazor UI ([Football.Blazor](/src/Hosts/Blazor/))
3. Web Worker ([Football.Worker](/src/Hosts/Worker/))

For details on how to launch each application, refer to its README file: [Web API](/src/Hosts/Api/README.md), [Blazor UI](/src/Hosts/Blazor/README.md), and the [Web Worker](/src/Hosts/Worker/README.md).

## How to run the tests

The repository includes unit tests and integration tests. Integration tests require a PostgreSQL test database.

### Preparing the test database

The Docker Compose file, [docker-compose.testdb.yml](/docker-compose.testdb.yml), provides a test database and a database management tool, [Adminer](https://www.adminer.org/).

The test database runs from a PostgreSQL 17 Docker image. Create a `.env.testdb` file at the root of the repository:

```env
POSTGRES_PASSWORD=your_password
POSTGRES_USER=postgres
```

> [!IMPORTANT]
> The test database runs on port **5433** and the database name is `footballscoreboard_db`.

### Starting the test database

Start the database and Adminer:

```bash
docker-compose -f docker-compose.testdb.yml up -d
```

The database is automatically seeded from [footballscoreboard_testdb.sql](/scripts/testdb/footballscoreboard_testdb.sql). Initial startup takes a few minutes. Adminer is available at http://localhost:8081.

For integration tests, configure the connection string using user secrets or the `CUSTOMCONNSTR_FootballDbConnection` environment variable. See [integration tests README](/tests/Football.Application.IntegrationTests/README.md) for details.

### Running the tests

Basic test run:

```bash
dotnet test
```

With coverage:

```bash
# Restore tools
dotnet tool restore

# Run tests with coverage
dotnet dotnet-coverage collect 'dotnet test --no-restore' -f xml -o 'coverage.xml'

# Generate HTML report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"
```

Open `coverage/index.html` to view results.
