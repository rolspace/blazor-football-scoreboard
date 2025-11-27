# Football.Worker Unit Tests

Unit tests for the [Football.Worker](/src/Hosts/Worker/) background service project.

## Requirements

- .NET 10+ SDK

## Running Tests

### Basic Test Run

From the repository root:

```bash
dotnet test tests/Football.Worker.UnitTests/Football.Worker.UnitTests.csproj
```

### With Coverage

From the repository root:

```bash
# Restore tools
dotnet tool restore

# Run tests with coverage
dotnet dotnet-coverage collect 'dotnet test tests/Football.Worker.UnitTests/Football.Worker.UnitTests.csproj --no-restore' -f xml -o 'coverage.xml'

# Generate HTML report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"
```

Open `coverage/index.html` to view results.
