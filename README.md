# Blazor Football Scoreboard

This source code repository contains libraries and applications that are used to run a system that simulates real-time game data from the 2019 football season.

The system is split into three main elements:
- HTTP API: this application exposes HTTP endpoints to get data and statistics for football games from the 2019 NFL season. This application also exposes a SignalR Hub used to send messages to connected clients.
- Web Worker: the worker is used to read play data from an existing game database. The Worker sends this play data to the SignalR Hub which communicates with the Blazor client.
- Blazor UI: this application displays the scores and current plays for games in a given week in the schedule. The user can also view a page for a specific game which provides game statistics.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

In order to run the system, it is important to keep in mind the following details:
1. The HTTP API must be launched first, as it provides the game data and statistics.
2. The Blazor UI should be launched second, it will load the current game data and receive "real-time" updates and stats via the HTTP API and SignalR Hub.
3. The Web Worker should be launched third, it will begin the process of simulating the games.

> [!IMPORTANT]
> The HTTP API and Web Worker use a configuration value to set the week in the season schedule.
> It is necessary that both applications have the same week set in their respective application settings.
> Current, the value provided in the application settings for both applications is set to Week = 1 (first week of the season schedule).

To start the applications, refer to the specific README file for each of them: [HTTP API](/src/Hosts/Api/README.md), [Blazor UI](/src/Hosts/Blazor/README.md), and the [Web Worker](/src/Hosts/Worker/README.md).

The settings for the `Localhost` **ASPNETCORE_ENVIRONMENT** in each application are defined to allow the system to run as expected without any changes.

## How to run locally with Docker Compose

The Docker Compose file found at the root of the repository, [docker-compose.app.yml](/docker-compose.app.yml), launches the system following the startup order provided in the previous section. The settings for the `Development` **ASPNETCORE_ENVIRONMENT** in each application are defined to allow the system to run as expected without any changes.

The Docker Compose file requires the following items to be setup before launch:
- .env files
- Certificates

### .env files

The Docker Compose file starts four services:
- The application database
- HTTP API
- Blazor UI
- Web Worker

---

The application database runs from a MySQL 8.0.28 image. The Docker Compose settings expects a file, named `.env.localdb`, which must include the following environment variables:
- **MYSQL_ROOT_PASSWORD**
- **MYSQL_USER**
- **MYSQL_PASSWORD**

The contents of the `.env.localdb` file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

---

The HTTP API Compose settings expects a file, named `.env.api`, which must include the following environment variables:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the certificate
- **MYSQLCONNSTR_FootballDbConnection**: MySQL database connection string

The contents of the `.env.api` file should be similar to the example below:

```
ASPNETCORE_Kestrel__Certificates__Default__Password={CERTIFICATE PASSWORD VALUE}
MYSQLCONNSTR_FootballDbConnection={MYSQL CONNECTION STRING VALUE}
```

---

The Web Worker Compose settings expects a file, named `.env.worker`, which must include the following environment variables:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the certificate
- **MYSQLCONNSTR_FootballDbConnection**: MySQL database connection string

The contents of the `.env.worker` file should be similar to the example below:

```
ASPNETCORE_Kestrel__Certificates__Default__Password={CERTIFICATE PASSWORD VALUE}
MYSQLCONNSTR_FootballDbConnection={MYSQL CONNECTION STRING VALUE}
```

### Certificates

All services in the Docker Compose file run with SSL. These certificates need to be created manually.

#### Football.Api Certificate

The HTTP API will be called by the Web Worker application when sending data via the SignalR Hub. This call will be made inside the container network, which will use the name of the container set in the Compose file. For this reason, a custom certificate is needed.

The certificate can be created by running the following command from the repository root:

```
openssl req -x509 -newkey rsa:4096 -keyout certs/api/Api_CertKey.pem -out certs/api/Api_Cert.pem -sha256 -days 3650 -subj "/CN=Football Scoreboard API" -addext "subjectAltName = DNS:localhost, DNS:footballscoreboard_api"
```

The command will request a password, the password value is the same value that must to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** environment setting in the `.env.api` file used by the Docker Compose configuration.

The certificate and key will be created in the `./certs/api` folder.

#### Football.Blazor Certificate

The Blazor UI runs as a static web application served by NGINX. Therefore, it is necessary to create a custom certificate abd key. The files can be created by running the following command from the repository root:

```
openssl req -x509 -newkey rsa:4096 -keyout certs/blazor/Blazor_CertKey.pem -out certs/blazor/Blazor_Cert.pem -nodes -days 3650 -subj "/CN=localhost"
```

The command will not request a password.

The certificate and key will be created in the `./certs/blazor` folder.

#### Football.Worker Certificate

The Web Worker application uses a development certificate provided by .NET. The development certificate can be created by running the following command:

```
dotnet dev-certs https -ep ~/.aspnet/https/Football.Worker.pfx -p {PASSWORD VALUE}
dotnet dev-certs https --trust
```

The {PASSWORD VALUE} is the same value that must to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** environment setting in the `.env.worker` file used by the Docker Compose configuration.

### Compose Up

Once the .env files and the certificates have been set up, it is time to start the system.

This can be done in two ways:

1. If you have the Docker extension for VSCode, right-click the [docker-compose.app.yml](/docker-compose.app.yml) file and select `Compose Up`.
2. Run the command, `docker-compose -f docker-compose.app.yml up -d`, from a terminal set at the root of the repository.

Running `docker-compose up` on the Docker Compose file will start the services in the following order:

1. MySQL Database
2. Football.Api
3. Football.Blazor
4. Football.Worker

The Compose file will start containers for the MySQL database and all three applications.

If the database is launched for the first time, there is an automated seeding process that uses the [footballscoreboard_localdb.sql](/scripts/localdb/footballscoreboard_localdb.sql) file to generate the tables and data. Due to the size of the database, the database container startup will take longer.
The database will be persisted locally in the /data/localdb folder for subsequent runs.

After startup, the HTTP API application will be available at the following URL: https&ZeroWidthSpace;://localhost:5001.
After startup, Blazor UI application will be available at the following URL: https&ZeroWidthSpace;://localhost.

## How to run the tests

The repository includes both unit tests and integration tests.

The integration tests require a database in order to run successfully.

### Preparing the test database

The Docker Compose file found at the root of the repository, [docker-compose.testdb.yml](/docker-compose.testdb.yml), provides a test database and a database viewer via [Adminer](https://www.adminer.org/).

The test database runs from a MySQL 8.0.28 image. The Docker Compose settings expects a file, named `.env.testdb`, which must include the following environment variables:
- **MYSQL_ROOT_PASSWORD**
- **MYSQL_USER**
- **MYSQL_PASSWORD**

The contents of the `.env.testdb` file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

> [!IMPORTANT]
> The test database is configured to run on a different port (3307) than the default port used by MySQL databases (3306).

### Start the test database

Once the .env file has been set up, it is time to start the test database.

This can be done in two ways:

1. If you have the Docker extension for VSCode, right-click the [docker-compose.testdb.yml](/docker-compose.testdb.yml) file and select `Compose Up`.
2. Run the command, `docker-compose -f docker-compose.testdb.yml up -d`, from a terminal set at the root of the repository.

The Compose file will start containers for the test MySQL database and Adminer.

If the test database is launched for the first time, there is an automated seeding process that uses the [footballscoreboard_testdb.sql](/scripts/localdb/footballscoreboard_testdb.sql) file to generate the tables and data.

Due to the size of the database, the test database container startup will take a bit longer.
The database will be persisted locally in the /data/testdb folder for subsequent runs.

Adminer will be available at the following URL: http&ZeroWidthSpace;://localhost:8081.

### Running the tests

1. Set the current working directory in your terminal of choice to the root of the repository.

2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotnet coverage tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the dotnet report generator tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the root of the repository.

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:./coveragereport" "-assemblyfilters:+Football.*;-Football.*Tests";`.

The HTML coverage report will be found in the `coveragereport` folder at the root of the repository, open the `index.html` file on your browser of choice to view the results.
