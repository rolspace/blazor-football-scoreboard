Blazor Football Scoreboard

## Test Preparation

The solution contains both unit tests and integration tests.

In order to run the integration tests successfully, a connection to a MySQL database is required.

The database can be started with the Compose file found in the integration test project directory, `./tests/Football.Application.IntegrationTests/docker-compose.testdb.yml`, with the command, `docker-compose -f docker-compose.testdb.yml up -d`

The Compose file references an SQL file, `football_testdb.sql`, to seed data into the test database. The sql file can be found in the `data` folder in the integration tests project directory.

## Run the tests

1. Set the current working directory in your terminal of choice to the root of the repository, where the `Scoreboard.sln` solution file is located.

2. Run the tests with the `dotnet test` command.

## Run the tests with coverage

1. 1. Instal the dotCover global tool, `dotnet tool install JetBrains.dotCover.GlobalTool -g`

2. Set the current working directory in your terminal of choice to the root of the repository, where the `Scoreboard.sln` solution file is located.

3. Run all the tests with the `dotnet dotcover test --dcReportType=HTML --dcFilters="-:MySqlConnector"` command.

4. The coverage report can be viewed by opening the `dotCover.Output.html` file in a browser window.
