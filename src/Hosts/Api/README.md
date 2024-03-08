# Blazor Football Scoreboard Football.Api

This .NET 6 web application exposes HTTP endpoints and a SignalR Hub that are used to provide game data and statistics from the 2019 football season for connected clients.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker Desktop 4.30+

## How to run locally

The application can be launched locally with the dotnet CLI or with Docker.
In order for the application to run successfully a database and environment variables are required.

### Database configuration

The Docker Compose file, [docker-compose.localdb.yml](/docker-compose.localdb.yml), provides a MySQL database and a database management tool, [Adminer](https://www.adminer.org/).

The local database runs from a MySQL 8.0.28 Docker image. The Docker Compose configuration expects a file to exist at the root of the repository with the name `.env.localdb`, which must include the following variables:
- **MYSQL_ROOT_PASSWORD**
- **MYSQL_USER**
- **MYSQL_PASSWORD**

The variables are required to launch the database container.
The contents of the `.env.localdb` file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

The local database can be launched in two ways:

1. If you have the Docker extension for VSCode, right-click the [docker-compose.localdb.yml](/docker-compose.localdb.yml) file and select `Compose Up`.
2. Run the command, `docker-compose -f docker-compose.localdb.yml up -d`, from a terminal set at the root of the repository.

The Compose file will start containers for the local MySQL database and Adminer.

If the local database is launched for the first time, there is an automated seeding process that uses the [footballscoreboard_localdb.sql](/scripts/localdb/footballscoreboard_localdb.sql) file to generate the tables and data.

Due to the size of the database, the local database container startup will take a bit longer.
The database will be persisted locally in the `./data/localdb` folder for subsequent runs.

Adminer will be available at the following URL: http&ZeroWidthSpace;://localhost:8080.

### Application settings

#### dotnet CLI

Running via the dotnet CLI, the application will set *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Api/appsettings.Localhost.json) configuration file.

The keys required for the application settings are the following:
- **Cors:PolicyName**: CORS policy name defined for the web application.
- **Cors:AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to https&ZeroWidthSpace;://localhost:5002 to allow calls from the Blazor UI application.
- **Cors:AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
- **Scoreboard:Week**: number for the week in the season schedule.

> [!IMPORTANT]
> The **Scoreboard:Week** setting is used to set the week of the schedule for the games that will simulated.
> It is necessary that both the Football.Api and the Football.Worker applications have the same value for this setting.

Separately from the application settings, it is required to use the .NET user secrets to store settings that should not be committed to the repo:
- **ConnectionStrings:FootballDbConnection**: database connection string.

---

#### Docker

Running via Docker, the application will set *Development* as the **ASPNETCORE_ENVIRONMENT** and use environment variables defined in the `docker-run-api: debug` task in the [tasks.json](/.vscode/tasks.json) file.

The keys required for the application settings are the following:
- **Cors__PolicyName**: CORS policy name defined for the web application.
- **Cors__AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to https&ZeroWidthSpace;://localhost:5002 to allow calls from the Blazor UI application.
- **Cors__AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
- **Scoreboard__Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

> [!IMPORTANT]
> The **Scoreboard__Week** setting is used to set the week of the schedule for the games that will simulated.
> It is necessary that both the Football.Api and the Football.Worker applications have the same value for this setting.

Separately from the application settings, it is required to use a `.env` file, named `.env.api`, to store settings that should not be committed to the repo:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the custom certificate used by the application.
- **MYSQLCONNSTR_FootballDbConnection**: database connection string.

### Launch the application

#### dotnet CLI

The application can be launched in two ways:
1. From a terminal set at the root of the project, `src/Hosts/Api`, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard API* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5001.

---

#### Docker

Before starting the application via Docker, a custom certificate is required to allow calls to the SignalR Hub from inside the Docker network with SSL.

The certificate and the key can be created with the following command:

```
openssl req -x509 -newkey rsa:4096 -keyout certs/api/Api_CertKey.pem -out certs/api/Api_Cert.pem -sha256 -days 3650 -subj "/CN=Football Scoreboard API" -addext "subjectAltName = DNS:localhost, DNS:footballscoreboard_api"
```

This command will prompt for a certificate password. This password is the same value that needs to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** enviroment setting in the `.env.api` file mentioned earlier in the [application settings](#application-settings) section.

Once the certificate is ready, the application can be launched from VSCode via the *Run and Debug* menu, simply select the *Launch Docker: Football Scoreboard API* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5001.

## How to run the unit tests

Refer to the [Football.Api.UnitTests README](/tests/Football.Api.UnitTests/README.md) for details.

## Additional Details

### HTTP API

The API exposes the following endpoints:

#### GET /api/v1/games?week={week}

Retrieves all the games for a given week in the schedule.

Returns a 400 if no week parameter is provided.

Returns a 404 if no games are found for the given week.

---

#### GET /api/v1/games/today

Retrieves all the games scheduled for today.
`today` is understood to represent the current week in the schedule (defined by the Scoreboard:Week application setting).

---

#### GET /api/v1/games/{id}

Retrieves the game data for the specified game id.

Returns a 400 if no game id parameter is provided.

Returns a 404 if the game id cannot be found.

---

#### GET /api/v1/games/{id}/stats

Retrieves the game statistics for the specified game id.

Returns a 400 if no game id parameter is provided.

Returns a 404 if the game id cannot be found.

### SignalR Hub

#### /hub/plays

The hub endpoint exposes the method called by the [Web
Worker](/src/Hosts/Football.Worker), which sends play data to the [Blazor](/src/Hosts/Football.Blazor) web clients.
