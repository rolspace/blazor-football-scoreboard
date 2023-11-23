# Blazor Football Scoreboard

This C# source code contains three main elements:
- HTTP API: the API exposes methods to get game data for football games from the 2019 NFL season. The API app also exposes a SignalR Hub to notify connected clients that a new play has occurred in an ongoing game.
- Worker: the worker is used to simulate plays from games in a given week from the 2019 football season. The worker sends play data to the SignalR Hub clients.
- Blazor UI: the web application displays the scores for the ongoing games in a given week. The user can also select to view a page for a specific game which also has statistics for both teams.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

For details on how to run each application, go to the README file for each of the host C# projects: the Api README, the Blazor README, and the Worker README.

Each application can be run locally with the dotnet cli or with Visual Studio Code launch configs. Using this approach, the applications must be started individually.

There is also an option to launch them all together with a single click (or command) using Docker Compose.
For the system to work together, the applications should be started in a specific order:
1. Football.Api
2. Football.Blazor
3. Football.Worker

## Tests

### Preparing the tests

The solution contains both unit tests and integration tests.

In order to run the integration tests successfully, a connection to a MySQL database is required.

A test database is provided with the Docker Compose file found in the integration tests project directory, `./tests/Football.Application.IntegrationTests/docker-compose.testdb.yml`.

The containers in the Compose file can be run in Visual Studio Code by right-clicking the file and selecting `Compose Up`, assuming the Docker VSCode extension is installed. Otherwise, the command, `docker-compose -f docker-compose.testdb.yml up -d` can be executed from a terminal set to the root of the integration tests project.

The Compose file references an SQL file, `football_testdb.sql`, which is used to seed data to the test database. The SQL file can be found in the `data` folder in the integration tests project directory. The very first time the Compose file runs, the startup will take a bit longer due to the seeing process.

The Compose file expects a `db.env` file to exist with the secrets required to run the test database. These values are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/):
- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

It is important to know that the test database runs on a different port (3307) than the application database (3306).

[Adminer](https://www.adminer.org/) is included in the Docker Compose file and it can be opened in the browser at http://localhost:8081.

### Running the tests

1. Set the current working directory in your terminal of choice to the root of the repository, where the `Scoreboard.sln` solution file is located.

2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotnet coverage tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the dotnet report generator tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the root of the repository, where the `Scoreboard.sln` solution file is located.

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by changing [the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:./coveragereport" "-assemblyfilters:+Football.*;-Football.*Tests";`. The HTML coverage report will be found in the ./coveragereport folder at the root of the repository, open the `index.html` file on your browser of choice.
