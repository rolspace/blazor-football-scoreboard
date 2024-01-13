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

For details on how to run each application separately, go to the README file for each of the host C# projects: the API, the Blazor UI, and the Worker. Each application can be started individually using the `dotnet cli`, with the VSCode launch config, or with a Docker container.

The preferred option to run the entire system together is to launch all the applications with a single click, or command, using Docker Compose. The Docker Compose file can be found at the root of the repository, [docker-compose.app.yml](/docker-compose.app.yml)

The Compose file will start containers for all three applications, including the MySQL database where the game data is stored. In order to seed the database, the MySQL container references an SQL file, [football_db.sql](/data/football_db.sql).
It is important to be aware that the very first time the Compose file starts, the container startup will take a bit longer due to the seeding process.

### Database configuration

The Compose file expects a *db.env* file at the root of the repository, with the secrets required to run the database. These values are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/):

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

The Compose file expects an *app.env* file at the root of the repository, with the secrets required to run the Football.Api and Football.Worker applications:

- ASPNETCORE_Kestrel__Certificates__Default__Password: password for the certificates
- MYSQLCONNSTR_FootballDbConnection: MySQL database connection string

The contents of the file should be similar to the example below:

```
ASPNETCORE_Kestrel__Certificates__Default__Password={CERTIFICATE PASSWORD VALUE}
MYSQLCONNSTR_FootballDbConnection={MYSQL CONNECTION STRING VALUE}
```

> [!IMPORTANT]
> There are additional settings needed for each application, these settings are not sensitive, so they can be listed in the [docker-compose.app.yml](/docker-compose.app.yml) file. For more info on these settings, check the README file for each application.

### Certificates

All applications running via the Compose file are configured to use HTTPS. The certificates required for each application can be created by using the `openssl` utility.

For the Blazor UI application, we will create a certificate with no password by running the following command at the root of the project folder:

```
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -nodes -days 3650 -subj "/CN=localhost"
```

For the Football.Worker application, we will create a certificate, with a password, with the following command at the root of the project folder:

```
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 3650 -subj "/CN=localhost
```

> [!IMPORTANT]
> The command will prompt you to enter a password...this is the same password that should be used for the ASPNETCORE_Kestrel__Certificates__Default__Password secret. As a convenience, use the same password for the local certificates (that's ok in this case, this is a project for playing around).

There is a special case with the certificate for the Football.Api container. Inside the container network, the Football.Worker hosted service will call the HTTP API provided by the Football.Api container. This call requires HTTPS, so it is necessary to create the certificate for the Football.Api application with the name of the domain inside the container network by running the command at the root of the project:

```
openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 3650 -subj "/CN=footballscoreboard_api"
```

Each of these commands will create a cert.pem file and a key.pem file at the root of the respective project. The Docker Compose setup will take care of placing all the files in the correct locations, once it starts.

### Compose Up

Starting the Docker Compose file will run the applications in the following order:

1. MySQL Database
2. Football.Api
3. Football.Blazor
4. Football.Worker

If you have the Docker extension for VSCode, the containers in the Compose file can be started by right-clicking the [docker-compose.app.yml](/docker-compose.app.yml) file and selecting `Compose Up`.

If the extension is not installed, the command, `docker-compose -f docker-compose.app.yml up -d`, can be executed from a terminal set at the root of the repository.

After startup, the Football.Api application will be available at https://localhost:5001.
The Football.Blazor application will be available at https://localhost.

## How to run the tests

### Preparing the tests

The solution contains both unit tests and integration tests.

In order to run the integration tests successfully, a connection to a MySQL database is required.

The test database can be run via the Docker Compose file found in the integration tests project directory, [docker-compose.testdb.yml](/tests/Football.Application.IntegrationTests/docker-compose.testdb.yml).

The Compose file references an SQL file, [football_testdb.sql](/tests/Football.Application.IntegrationTests/data/football_testdb.sql), which is used to seed data to the test database. The very first time the Compose file runs, the startup will take a bit longer due to the seeding process.

The Compose file requires an env file with the name *db.env*. This file should be at the [root of the integration tests project](/tests/Football.Application.IntegrationTests/). The following values are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/) and should be included in the env file:

- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

The contents of the env file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

If you have the Docker extension for VSCode, the containers in the Compose file can be started by right-clicking the [docker-compose.testdb.yml](/tests/Football.Application.IntegrationTests/docker-compose.testdb.yml) file and selecting `Compose Up`.

If the extension is not installed, the command, `docker-compose -f docker-compose.testdb.yml up -d` can be executed from a terminal set to the root of the integration tests project.

It is important to note that the test database runs on a different port (3307) than the default used by MySQL databases (3306).

[Adminer](https://www.adminer.org/) is included in the Docker Compose file and it can be opened in the browser at http://localhost:8081.

### Running the tests

1. Set the current working directory in your terminal of choice to the [root of the repository](/).

2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotnet coverage tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the dotnet report generator tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the [root of the repository](/).

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:./coveragereport" "-assemblyfilters:+Football.*;-Football.*Tests";`. The HTML coverage report will be found in the `coveragereport` folder at the [root of the repository](/), open the `index.html` file on your browser of choice to view the results.
