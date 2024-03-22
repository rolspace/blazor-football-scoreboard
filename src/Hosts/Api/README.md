# Blazor Football Scoreboard Football.Api

This .NET 6 web application provides a collection of HTTP endpoints and a SignalR Hub that are used to request game data and statistics from the 2019 football season.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker Desktop 4.30+

## How to run locally

The application can be launched locally with the dotnet CLI or with Docker.
In order for the application to run successfully a local database and the application settings are required.

### Database configuration

The Docker Compose file, [docker-compose.localdb.yml](/docker-compose.localdb.yml), provides a MySQL database and a database management tool, [Adminer](https://www.adminer.org/).

The local database runs from a MySQL 8.0.28 Docker image. The Docker Compose configuration expects a `.env.localdb` file at the root of the repository, which must include the following variables:
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

The Docker Compose file will start the containers for the local database and Adminer.

For the initial launch of the database container, there is an automated seeding process to generate the tables and data, based on the [footballscoreboard_localdb.sql](/scripts/localdb/footballscoreboard_localdb.sql) file.

Due to the size of the local database, the container startup will take a few minutes. Once the container is ready, the database will be persisted locally with a Docker Volume in the *.docker/volumes/localdb* folder.

Adminer will be available at the following URL, *http&ZeroWidthSpace;://localhost:8080*.

### Application settings

#### dotnet CLI

Running via the dotnet CLI, the startup will set the **ASPNETCORE_ENVIRONMENT** to *Localhost* and use the [appsettings.Localhost.json](/src/Hosts/Api/appsettings.Localhost.json) configuration file.

The keys required for the application settings are the following:
- **Cors:PolicyName**: CORS policy name defined for the web application.
- **Cors:AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to *https&ZeroWidthSpace;://localhost:5002* to allow calls from the Blazor UI application.
- **Cors:AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
- **Scoreboard:Week**: number for the week in the season schedule.

> [!IMPORTANT]
> The **Scoreboard:Week** setting is used to set the week in the season schedule that will be simulated.
> It is necessary that both the [Football.Api](/src/Hosts/Api/) and the [Football.Worker](/src/Hosts/Worker/) applications have the same value for this setting.

Separately from the application settings file, an additional setting should be stored in the .NET user secrets:
- **ConnectionStrings:FootballDbConnection**: database connection string.

---

#### Docker

Running via Docker, the application will set the **ASPNETCORE_ENVIRONMENT** variable to *Development* and use the environment variables defined in the `docker-run-api: debug` task in the [tasks.json](/.vscode/tasks.json) file.

The keys required for the application settings are the following:
- **Cors__PolicyName**: CORS policy name defined for the web application.
- **Cors__AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to *https&ZeroWidthSpace;://localhost:5002* to allow calls from the Blazor UI application.
- **Cors__AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
- **Scoreboard__Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

> [!IMPORTANT]
> The **Scoreboard__Week** setting is used to set the week in the season schedule that will be simulated.
> It is necessary that both the [Football.Api](/src/Hosts/Api/) and the [Football.Worker](/src/Hosts/Worker/) applications have the same value for this setting.

Separately from the environment variables, sensitive settings should be stored in a `.env.api` file, at the root of the repository:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the custom certificate used by the application.
- **MYSQLCONNSTR_FootballDbConnection**: database connection string.

### Launch the application

#### dotnet CLI

The application can be launched in two ways:
1. From a terminal set at the root of the project, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard API* launch config.

Once the application starts, it will be available at *https&ZeroWidthSpace;://localhost:5001*.

---

#### Docker

Before starting the application via Docker, a custom certificate is required to allow calls to the SignalR Hub from inside the Docker network with SSL.

The certificate and the key can be created with the following command in a terminal set at the root of the repository:

```
openssl req -x509 -newkey rsa:4096 -sha256 -days 3650 \
    -out .docker/volumes/certs/api/Api_Cert.pem \
    -keyout .docker/volumes/certs/api/Api_CertKey.pem \
    -subj "/CN=Football Scoreboard API" \
    -addext "subjectAltName = DNS:localhost, DNS:footballscoreboard_api"
```

This command will prompt for a certificate password. This password is the same value that needs to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** enviroment variable in the `.env.api` file mentioned earlier in the [application settings](#application-settings) section.

The certificate and key created will be stored in a location known to the Docker setup, the *.docker/volumes/certs/api* folder.

Once the certificate is ready, the application can be launched from VSCode via the *Run and Debug* menu, simply select the *Launch Docker: Football Scoreboard API* launch config.

Once the application starts, it will be available at *https&ZeroWidthSpace;://localhost:5001*.

## How to run the unit tests

Refer to the [Football.Api.UnitTests README](/tests/Football.Api.UnitTests/README.md) for details.

## Additional Details

### HTTP API

The API exposes the following endpoints:

---

#### GET /api/v1/games?week={week}

Retrieves all the games for a given week in the schedule.

Returns a 400 if no week parameter is provided.

Returns a 404 if no games are found for the given week.

---

#### GET /api/v1/games/now

Retrieves all the games scheduled for this moment.
*now* represents the current week in the season schedule, which is determined by the *Scoreboard:Week* application setting.

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

The hub endpoint exposes the method called by the [Web Worker](/src/Hosts/Football.Worker), which sends play data to the [Blazor](/src/Hosts/Football.Blazor) web clients.
