Blazor Football Scoreboard

## Run the tests

Set the current working directory in your terminal of choice to the root of the source code, where the `Scoreboard.sln` solution file is located.

Run the tests with the `dotnet test` command.

## Run the tests with coverage

Set the current working directory in your terminal of choice to the root of the source code, where the `Scoreboard.sln` solution file is located.

In order to avoid issues with previous test results and coverage files, clean any existing directories with the `rm -rf ./**/coveragereport ./**/TestResults` command.

Run the tests and the default test data collector with the `dotnet test --collect:"XPlat Code Coverage"` command.

Move the generated test result file to another location where it is easier to be read by the report generator with the following commands:

`mv -v ./tests/Football.Api.UnitTests/TestResults/*/*.* ./tests/Football.Api.UnitTests/TestResults/`

`mv -v ./tests/Football.Application.IntegrationTests/TestResults/*/*.* ./tests/Football.Application.IntegrationTests/TestResults/`

Make sure the [ReportGenerator .NET tool](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) is installed as a global package with the `dotnet tool install -g dotnet-reportgenerator-globaltool` command.

Generate the test coverage report with the following command,
`reportgenerator -reports:"./tests/*/TestResults/coverage.cobertura.xml;" -targetdir:"coveragereport" -reporttypes:html`
