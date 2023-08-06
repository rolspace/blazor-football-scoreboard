# Football.Application Integration Tests

## Test Preparation

The integration tests require a connection to a MySQL database in order to run successfully.

The database can be started with the Compose file found in the project directory, `docker-compose.testdb.yml`, with the command, `docker-compose -f docker-compose.testdb.yml up -d`

The Compose file references an SQL file, `football_testdb.sql`, to seed data into the test database. The sql file can be found in the `data` folder in the project directory.

## Run the tests

1. Set the current working directory in your terminal of choice to the root of the `Football.Application.IntegrationTests.csproj` file.

2. Run the tests with the `dotnet test` command.

## Run the tests with coverage

1. Set the current working directory in your terminal of choice to the root of the `Football.Application.IntegrationTests.csproj` file.

2. In order to avoid issues with previous test results and coverage files, clean any existing directories with the `rm -rf ./coveragereport ./TestResults` command.

3. Run the tests and the default test data collector with the `dotnet test --collect:"XPlat Code Coverage"` command.

4. Move the generated test result file to another location where it is easier to be read by the report generator with the `mv -v ./TestResults/*/*.* ./TestResults/` command.

5. Make sure the [ReportGenerator .NET tool](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) is installed as a global package with the `dotnet tool install -g dotnet-reportgenerator-globaltool` command.

6. Generate the test coverage report with the following command, `reportgenerator -reports:"./TestResults/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html`
