# Football.Application Integration Tests

Integration tests for the [Football.Application](/src/Application/) project.

## Requirements

- .NET 10+ SDK
- Docker Desktop 4.53+

## Setup

### 1. Configure Environment Variables

Create a `.env.testdb` file at the repository root:

```env
POSTGRES_PASSWORD=your_password
POSTGRES_USER=postgres
```

### 2. Start the Test Database

From the repository root, run:

```bash
docker-compose -f docker-compose.testdb.yml up -d
```

This starts:
- PostgreSQL 17 test database on port **5433**
- Adminer (database viewer) at http://localhost:8081

The database (`footballscoreboard_db`) is automatically seeded from [footballscoreboard_testdb.sql](/scripts/testdb/footballscoreboard_testdb.sql). Initial startup takes a few minutes.

### 3. Configure Connection String

Set the connection string using one of these methods:

**User Secrets** (recommended):
```bash
dotnet user-secrets set "ConnectionStrings:FootballDbConnection" "Host=localhost;Port=5433;Database=footballscoreboard_db;Username=postgres;Password=your_password"
```

**Environment Variable**:
```bash
export CUSTOMCONNSTR_FootballDbConnection="Host=localhost;Port=5433;Database=footballscoreboard_db;Username=postgres;Password=your_password"
```

## Running Tests

### Basic Test Run

```bash
dotnet test
```

### With Coverage

From the repository root:

```bash
# Restore tools
dotnet tool restore

# Run tests with coverage
dotnet dotnet-coverage collect 'dotnet test --no-restore' -f xml -o 'coverage.xml'

# Generate HTML report
dotnet reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:coverage" "-assemblyfilters:+Football.*;-Football.*Tests"
```

Open `coverage/index.html` to view results.
