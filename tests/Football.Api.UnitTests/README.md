# Blazor Football Scoreboard Football.Api Unit Tests

This test project executes the unit tests for the [Football.Api](/src/Hosts/Api/) web application project.

## Requirements

- .NET 6+ SDK

## How to run the tests

### Running the tests

1. Set the current working directory a terminal to the [root of the unit test project](/tests/Football.Api.UnitTests/).

2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotnet coverage tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the dotnet report generator tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the [root of the unit test project](/tests/Football.Api.UnitTests/).

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:./coveragereport" "-assemblyfilters:+Football.*;-Football.*Tests";`. The HTML coverage report will be found in the `coveragereport` folder at the [root of the unit test project](/tests/Football.Api.UnitTests/), open the `index.html` file on your browser of choice to view the results.
