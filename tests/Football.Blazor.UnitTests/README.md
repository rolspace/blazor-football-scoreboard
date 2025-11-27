# Football.Blazor Unit Tests

Unit tests for the [Football.Blazor](/src/Hosts/Blazor/) WebAssembly application project.

## Requirements

- .NET 10+ SDK

## Running Tests

### Basic Test Run

From the repository root:

```bash
dotnet test tests/Football.Blazor.UnitTests/Football.Blazor.UnitTests.csproj
```

### With Coverage

From the repository root:

```bash
# Restore tools
dotnet tool restore

# Run tests with coverage
dotnet dotnet-coverage collect 'dotnet test tests/Football.Blazor.UnitTests/Football.Blazor.UnitTests.csproj --no-restore' -f xml -o 'coverage.xml'

# Generate HTML report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"
```

Open `coverage/index.html` to view results.
