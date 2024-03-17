# Blazor Football Scoreboard Football.Application Integration Tests

This test project runs the integration tests for the [Football.Application](/src/Application/) assembly project.

## Requirements

- .NET 6+ SDK
- Docker Desktop 4.30+

## How to run the tests

### Preparing the test database

The integration tests require a database in order to run successfully.

The Docker Compose file, [docker-compose.testdb.yml](/docker-compose.testdb.yml), provides a test database and a database management tool, [Adminer](https://www.adminer.org/).

The test database runs from a MySQL 8.0.28 Docker image. The Docker Compose configuration expects a `.env.testdb` file at the root of the repository, which must include the following variables:
- **MYSQL_ROOT_PASSWORD**
- **MYSQL_USER**
- **MYSQL_PASSWORD**

The variables are required to launch the database container.
The contents of the `.env.testdb` file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

> [!IMPORTANT]
> The test database is configured to run on a different port (3307) than the default port used by MySQL databases (3306).

### Starting the test database

The test database can be launched in two ways:
1. If you have the Docker extension for VSCode, right-click the [docker-compose.testdb.yml](/docker-compose.testdb.yml) file and select *Compose Up*.
2. Run the command, `docker-compose -f docker-compose.testdb.yml up -d`, from a terminal set at the root of the repository.

The Compose file will start containers for the test MySQL database and Adminer.

For the initial launch of the database container, there is an automated seeding process to generate the tables and data, based on the [footballscoreboard_testdb.sql](/scripts/testdb/footballscoreboard_testdb.sql) file.

Due to the size of the test database, the container startup will take a few minutes. Once the container is ready, the database will be persisted locally with a Docker Volume in the *.docker/volumes/testdb* folder.

Adminer will be available at the following URL, *http&ZeroWidthSpace;://localhost:8081*.

### Running the tests

Once the test database is ready, run the tests by:

1. Opening a terminal and setting the working directory to [the root of the integration test project](/tests/Football.Application.IntegrationTests/).

2. In the terminal, run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the [dotnet coverage](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage) tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the [dotnet report generator](https://www.nuget.org/packages/dotnet-reportgenerator-globaltool) tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the root of the repository.

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the -f and -o parameters](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect) from the `collect` command.

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:.coverage" "-assemblyfilters:+Football.*;-Football.*Tests";`.

The HTML coverage report will be found in the *.coverage* folder at the root of the repository, open the *index.html* file on your browser of choice to view the results.
