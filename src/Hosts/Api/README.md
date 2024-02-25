# Blazor Football Scoreboard Football.Api

This .NET 6 web application exposes HTTP endpoints and a SignalR Hub that are used to provide game data and statistics from the 2019 football season for connected clients.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

The application needs a database and the environment variables to run locally.

### Local - Database configuration

The Docker Compose file found at the root of the repository, [docker-compose.localdb.yml](/docker-compose.app.yml), launches A MySQL database and [Adminer](https://www.adminer.org/).

The local database runs from a MySQL 8.0.28 image. The Docker Compose settings expects a file, named `.env.localdb`, which must include the following environment variables:
- **MYSQL_ROOT_PASSWORD**
- **MYSQL_USER**
- **MYSQL_PASSWORD**

The contents of the `.env.localdb` file should be similar to the example below:

```
MYSQL_ROOT_PASSWORD={MYSQL ROOT USER PASSWORD}
MYSQL_USER={MYSQL USER IDENTIFIER}
MYSQL_PASSWORD={MYSQL USER PASSWORD}
```

Once the `.env.localdb` file has been set up, it is time to start the local database.

This can be done in two ways:

1. If you have the Docker extension for VSCode, right-click the [docker-compose.localdb.yml](/docker-compose.localdb.yml) file and select `Compose Up`.
2. Run the command, `docker-compose -f docker-compose.localdb.yml up -d`, from a terminal set at the root of the repository.

The Compose file references an SQL file, [football_db.sql](/scripts/appdb/football_db.sql), which seeds data to the database. The very first time the Compose file runs, the startup will take a bit longer due to the seeding process.

If the local database is launched for the first time, there is an automated seeding process that uses the [footballscoreboard_localdb.sql](/scripts/localdb/footballscoreboard_localdb.sql) file to generate the tables and data.

Due to the size of the database, the local database container startup will take a bit longer.
The database will be persisted locally in the /data/localdb folder for subsequent runs.

Adminer will be available at the following URL: http&ZeroWidthSpace;://localhost:8080.

### Local - Application settings

When running locally, the application will set *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Api/appsettings.Localhost.json) configuration file.

The application settings and the keys required are the following:
- **Cors:PolicyName**: CORS policy name defined for the web application.
- **Cors:AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to https&ZeroWidthSpace;://localhost:5002 to allow calls from the Blazor UI application (Football.Blazor project).
- **Cors:AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
- **Scoreboard:Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

Separately from the application settings, it is required to use the .NET user secrets to store settings that should not be committed to the repo:
- **ConnectionStrings:FootballDbConnection**: database connection string.

### Local - Launch the application locally

The application can be launched in two ways:
1. From a terminal set at the root of the project, */src/Hosts/Api*, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard API* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5001.

## How to run locally via Docker

The Docker application needs a database and the environment variables to run locally.

### Docker - Database configuration

Follow the same instructions for the [database configuration provided earlier](#local---database-configuration) for the local setup (no Docker).

### Docker - Application settings

When running via Docker, the application will set *Development* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Api/appsettings.Localhost.json) configuration file, if it exists.

The rest of the configuration values are provided as environment variables in the [tasks.json](/.vscode/tasks.json) file.

The application settings and the keys required are the following:
- **Cors__PolicyName**: CORS policy name defined for the web application.
- **Cors__AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to https&ZeroWidthSpace;://localhost:5002 to allow calls from the Blazor UI application (Football.Blazor project).
- **Cors__AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
- **Scoreboard__Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

Separately from the application settings, it is required to use a `.env` file, named `.env.api`, to store settings that should not be committed to the repo:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the custom certificate used by the application.
- **MYSQLCONNSTR_FootballDbConnection**: database connection string.

### Docker - Custom certificate

The Football.Api web application requires a custom certificate for HTTPS when running via Docker.

The certificate and the key can be created with the following command:

```
openssl req -x509 -newkey rsa:4096 -keyout certs/api/Api_CertKey.pem -out certs/api/Api_Cert.pem -sha256 -days 3650 -subj "/CN=Football Scoreboard API" -addext "subjectAltName = DNS:localhost, DNS:footballscoreboard_api"
```

This command will prompt for a certificate password. This password is the same value that needs to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** enviroment setting in the `.env.api` file mentioned [earlier](#docker---application-settings).

After the creation of the certificate is done, all the configuration necessary is provided in the [tasks.json](/.vscode/tasks.json) file.

### Docker - Launch the application via Docker

The application can be launched from VSCode via the *Run & Debug* menu. Select the *Launch Docker: Football Scoreboard API* launch config.

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

#### GET /api/v1/games/today

Retrieves all the games scheduled for today.
`today` is understood to represent the current week in the schedule (defined by the Scoreboard:Week application setting).

#### GET /api/v1/games/{id}

Retrieves the game data for the specified game id.

Returns a 400 if no game id parameter is provided.

Returns a 404 if the game id cannot be found.

#### GET /api/v1/games/{id}/stats

Retrieves the game statistics for the specified game id.

Returns a 400 if no game id parameter is provided.

Returns a 404 if the game id cannot be found.

### SignalR Hub

#### /hub/plays

The hub endpoint exposes the method called by the [Web
Worker](/src/Hosts/Football.Worker), which sends play data to the [Blazor](./src/Hosts/Football.Blazor) web clients.
