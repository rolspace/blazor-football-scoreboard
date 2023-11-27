# Blazor Football Scoreboard

This C# source code repository provides a system to simulate and display real-time game data from the 2019 football season.

The system is split into three main elements:
- HTTP API: this application exposes HTTP endpoints to get game data for football games from the 2019 NFL season. The API app also exposes a SignalR Hub to send messages to connected clients about plays in ongoing games.
- Web Worker: the worker is used to read play data from games. The Worker sends play data to the SignalR Hub which communicates with the Blazor clients.
- Blazor UI: this application displays the scores and current plays for games in a given week. The user can also view a page for a specific game which provides game statistics for both teams playing.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

For details on how to run each application separately, go to the README file for each of the host C# projects: the API, the Blazor UI, and the Worker. Each application can be started individually using the `dotnet cli` or with the VSCode launch config.

The preferred option to run the entire system is to launch all the applications together with a single click (or command) using Docker Compose. The Docker Compose file can be found at the root of the repository, `docker-compose.app.yml`

The Compose file references an SQL file, `football_db.sql`, which seeds data into the database. The SQL file can be found in the `data` folder at the root of the repository. The very first time the Compose file starts, the container startup will take a bit longer due to the seeding process.

### Certificates

Test

### DB configuration

The Compose file expects a `db.env` file at the root of the repository, with the secrets required to run the database. These values are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/):

- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

The contents of the file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

### Application configuration

The Compose file expects an `app.env` file at the root of the repository, with the secrets required to run the Football.Api and Football.Worker applications:

- ASPNETCORE_Kestrel__Certificates__Default__Password: password for the certificates
- MYSQLCONNSTR_FootballDbConnection: MySQL database connection string

The contents of the file should be similar to the example below:

```
ASPNETCORE_Kestrel__Certificates__Default__Password={CERTIFICATE PASSWORD VALUE}
MYSQLCONNSTR_FootballDbConnection={MYSQL CONNECTION STRING VALUE}
```

> There are additional settings needed for each application, these settings are not sensitive, so they are defined in the `docker-compose.app.yml` file. For more info on these settings, check the README file for each application.

Starting the Docker Compose file will run the applications in the following order:

1. MySQL Database
2. Football.Api
3. Football.Blazor
4. Football.Worker

If you have the Docker extension for VSCode, the containers in the Compose file can be started by right-clicking the `docker-compose.app.yml` file and selecting `Compose Up`.

If the extension is not installed, the command, `docker-compose -f docker-compose.app.yml up -d`, can be executed from a terminal set at the root of the repository.

## Tests

### Preparing the tests

The solution contains both unit tests and integration tests.

In order to run the integration tests successfully, a connection to a MySQL database is required.

A test database is provided with the Docker Compose file found in the integration tests project directory, `tests/Football.Application.IntegrationTests/docker-compose.testdb.yml`.

The Compose file references an SQL file, `football_testdb.sql`, which is used to seed data to the test database. The SQL file can be found in the `data` folder in the integration tests project directory. The very first time the Compose file runs, the startup will take a bit longer due to the seeing process.

The Compose file expects a `db.env` file with the secrets required to run the test database. These values are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/):

- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

The contents of the file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

If you have the Docker extension for VSCode, the containers in the Compose file can be started by right-clicking the `docker-compose.testdb.yml` file and selecting `Compose Up`.

If the extension is not installed, the command, `docker-compose -f docker-compose.testdb.yml up -d` can be executed from a terminal set to the root of the integration tests project.

It is important to note that the test database runs on a different port (3307) than the default used by MySQL databases (3306).

[Adminer](https://www.adminer.org/) is included in the Docker Compose file and it can be opened in the browser at http://localhost:8081.

### Running the tests

1. Set the current working directory in your terminal of choice to the root of the repository, where the `Scoreboard.sln` solution file is located.

2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotnet coverage tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the dotnet report generator tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the root of the repository, where the `Scoreboard.sln` solution file is located.

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:./coveragereport" "-assemblyfilters:+Football.*;-Football.*Tests";`. The HTML coverage report will be found in the `coveragereport` folder at the root of the repository, open the `index.html` file on your browser of choice.
