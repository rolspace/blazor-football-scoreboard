# Blazor Football Scoreboard Football.Application Integration Tests

This test project executes the integration tests for the [Football.Application](/src/Application/) assembly project.

## Requirements

- .NET 6+ SDK
- Docker 4.30+

## How to run the tests

### Preparing the test database

The integration tests require a database connection in order to run successfully. The Compose file, [docker-compose.testdb.yml](/docker-compose.testdb.yml), provides a test database via Docker.

The Compose file references an SQL file, [football_testdb.sql](/data/testdb/football_testdb.sql), which is used to seed data to the test database. The very first time the Compose file runs, the startup will take a bit longer due to the seeding process.

The Compose file expects a file with the name *.env.testdb* at the root of the repository. The following values are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/) and should be included in the env file:

- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

The test database for the integration tests that can be launched from:

- A terminal set to the [root of the repository](/). On this terminal, run the command, `docker-compose -f docker-compose.testdb.yml up -d`

- The VSCode Explorer, assuming the Docker extension is installed. Simply right click on the [docker-compose.testdb.yml](/docker-compose.testdb.yml) file and select `Compose Up`.

### Running the tests

Once the test database is ready, run the tests by:

1. Opening a terminal and setting the working directory to [the root of the integration test project](/tests/Football.Application.IntegrationTests/).

2. In the terminal, run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotCover tool globally with the command, `dotnet tool install JetBrains.dotCover.GlobalTool -g`

2. In a terminal, set the working directory to [the root of the integration test project](/tests/Football.Application.IntegrationTests/).

3. Run the command, `dotnet dotcover test --dcReportType=HTML --dcFilters="-:MySqlConnector"`.

The coverage report can be viewed by opening the `dotCover.Output.html` file in a browser window.
