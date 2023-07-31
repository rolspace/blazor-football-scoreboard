# Football.Application Integration Tests

## Running the tests

Set the current working directory in your terminal of choice to the root of the `Football.Application.IntegrationTests.csproj` file.

Run the tests with the `dotnet test` command.

Run the tests and the default test data collector with the `dotnet test --collect:"XPlat Code Coverage"` command.

## Test Coverage

Make sure the [ReportGenerator .NET tool](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) is installed as a global package with `dotnet tool install -g dotnet-reportgenerator-globaltool`.

Generate the test coverage report with the following command:
````
reportgenerator
    -reports:"TestResults\{guid}\coverage.cobertura.xml"
    -targetdir:"coveragereport"
    -reporttypes:Html
````
