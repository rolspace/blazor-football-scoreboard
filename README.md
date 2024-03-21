# Blazor Football Scoreboard

This repository contains libraries and web applications that are used to run a simulation of the 2019 football season.

The system is split into three main elements:
- **Web API**: exposes HTTP endpoints to get data and statistics for football games from the 2019 season. It includes a SignalR Hub used to send messages to connected clients.
- **Blazor UI**: displays the scores and the play-by-play for each game. The user can also browse to a dedicated page for each game, with additional data.
- **Web Worker**: reads play data from an existing database. This process sends the play data to the SignalR Hub, which communicates with the Blazor client.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker Desktop 4.30+

## How to run locally

In order to run the system, the applications must be launched in the following order:
1. Web API ([Football.Api](/src/Hosts/Api/))
2. Blazor UI ([Football.Blazor](/src/Hosts/Blazor/))
3. Web Worker ([Football.Worker](/src/Hosts/Worker/))

For details on how to launch each application, refer to its README file: [Web API](/src/Hosts/Api/README.md), [Blazor UI](/src/Hosts/Blazor/README.md), and the [Web Worker](/src/Hosts/Worker/README.md).

## How to run the tests

The repository includes both unit tests and integration tests.

The integration tests require a database in order to run successfully.

### Preparing the test database

The Docker Compose file, [docker-compose.testdb.yml](/docker-compose.testdb.yml), provides a test database and a database management tool, [Adminer](https://www.adminer.org/).

The test database runs from a MySQL 8.0.28 Docker image. The Docker Compose configuration expects a `.env.testdb` file at the root of the repository, which must include the following variables:
- **MYSQL_ROOT_PASSWORD**
- **MYSQL_USER**
- **MYSQL_PASSWORD**

The variables are required to launch the database container.
The contents of the `.env.testdb` file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

> [!IMPORTANT]
> The test database is configured to run on a different port (3307) than the default port used by MySQL databases (3306).

### Starting the test database

Once the `.env.testdb` file has been set up, the test database is ready for launch.

The test database can be launched in two ways:
1. If you have the Docker extension for VSCode, right-click the [docker-compose.testdb.yml](/docker-compose.testdb.yml) file and select `Compose Up`.
2. Run the command, `docker-compose -f docker-compose.testdb.yml up -d`, from a terminal set at the root of the repository.

The Docker Compose file will start the containers for the test database and Adminer.

For the initial launch of the database container, there is an automated seeding process to generate the tables and data, based on the [footballscoreboard_testdb.sql](/scripts/testdb/footballscoreboard_testdb.sql) file.

Due to the size of the test database, the container startup will take a few minutes. Once the container is ready, the database will be persisted locally with a Docker Volume in the *.docker/volumes/testdb* folder.

Adminer will be available at the following URL: *http&ZeroWidthSpace;://localhost:8081*.

### Running the tests

To run the tests, perform the following steps:

1. In a terminal, set the current working directory to the root of the repository.
2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the [dotnet coverage](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage) tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the [dotnet report generator](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the root of the repository.

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests";`.

The HTML coverage report will be found in the *coverage* folder at the root of the repository, open the *index.html* file on your browser of choice to view the results.
