# Football.Application Integration Tests

## Test Preparation

The integration tests require a connection to a MySQL database in order to run successfully.

The database can be started with the Compose file found in the project directory, `docker-compose.testdb.yml`, with the command, `docker-compose -f docker-compose.testdb.yml up -d`

The Compose file references an SQL file, `football_testdb.sql`, to seed data into the test database. The sql file can be found in the `data` folder in the project directory.

## Run the tests

1. Set the current working directory in your terminal of choice to the root of the project, where the `Football.Application.IntegrationTests.csproj` file is located.

2. Run the tests with the `dotnet test` command.

## Run the tests with coverage

1. Install the dotCover global tool, `dotnet tool install JetBrains.dotCover.GlobalTool -g`

2. Set the current working directory in your terminal of choice to the root of the project, where the `Football.Application.IntegrationTests.csproj` file is located.

3. Run the tests with the `dotnet dotcover test --dcReportType=HTML --dcFilters="-:MySqlConnector"` command.

4. The coverage report can be viewed by opening the `dotCover.Output.html` file in a browser window.
