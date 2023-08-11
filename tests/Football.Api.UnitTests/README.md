# Football.Api Unit Tests

## Run the tests

1. Set the current working directory in your terminal of choice to the root of the project, where the `Football.Api.UnitTests.csproj` file is located.

2. Run the tests with the `dotnet test` command.

## Run the tests with coverage

1. Install the dotCover global tool, `dotnet tool install JetBrains.dotCover.GlobalTool -g`

2. Set the current working directory in your terminal of choice to the root of the project, where the `Football.Api.UnitTests.csproj` file is located.

3. Run the tests with the `dotnet dotcover test --dcReportType=HTML --dcFilters="-:MySqlConnector"` command.

4. The coverage report can be viewed by opening the `dotCover.Output.html` file in a browser window.
