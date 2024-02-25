# Blazor Football Scoreboard Football.Worker

This .NET6 web application runs a Background Service that reads the play data from games in the 2019 football season.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

The application needs a database and the environment variables to run locally.
Optionally, the background service connects to a SignalR Hub endpoint to send data to Hub clients.

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

Adminer will be available at the following URL: http://localhost:8080.

### Local - Application settings

When running locally, the application will set *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Worker/appsettings.Localhost.json) configuration file.

The application settings and the keys required are the following:
- **Hub:HubUrl**: URL for the Signal Hub.
- **Scoreboard:Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

Separately from the application settings, it is required to use the .NET user secrets to store settings that should not be committed to the repo:
- **ConnectionStrings:FootballDbConnection**: database connection string.

### Local - Launch the application locally

The application can be launched in two ways:
1. From a terminal set at the root of the project, */src/Hosts/Worker*, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard Worker* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5003.

## How to run locally via Docker

The Docker application needs a database and the environment variables to run locally.

### Docker - Database configuration

Follow the same instructions for the [database configuration provided earlier](#local---database-configuration) for the local setup (no Docker).

### Docker - Application settings

When running via Docker, the application will set *Development* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Worker/appsettings.Localhost.json) configuration file, if it exists.

The rest of the configuration values are provided as environment variables in the [tasks.json](/.vscode/tasks.json) file.

The application settings and the keys required are the following:
- **Hub:HubUrl**: URL for the Signal Hub.
- **Scoreboard:Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

Separately from the application settings, it is required to use a `.env` file, named `.env.worker`, to store settings that should not be committed to the repo:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the custom certificate used by the application.
- **MYSQLCONNSTR_FootballDbConnection**: database connection string.

### Docker - Custom certificate

The Football.Worker web application requires a custom certificate for HTTPS when running via Docker.

The certificate and the key can be created with the following commands:

```
dotnet dev-certs https -ep ~/.aspnet/https/Football.Worker.pfx -p {PASSWORD VALUE}
dotnet dev-certs https --trust
```

The {PASSWORD VALUE} is the same value that needs to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** enviroment setting in the `.env.worker` file mentioned [earlier](#docker---application-settings).

### Docker - Launch the application via Docker

The application can be launched from VSCode via the *Run & Debug* menu. Select the *Launch Docker: Football Scoreboard Worker* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5003.
