# Blazor Football Scoreboard Football.Worker

This .NET 6 web application runs a background service that reads the existing data from football games in the 2019 season.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

The application can be launched locally with the dotnet CLI or with Docker.
In order for the application to run successfully a database and environment variables are required.

### Database configuration

The Docker Compose file, [docker-compose.localdb.yml](/docker-compose.app.yml), provides a MySQL database and a database management tool, [Adminer](https://www.adminer.org/).

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

#### dotnet CLI

Running via the dotnet CLI, the application will set *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Worker/appsettings.Localhost.json) configuration file.

The keys required for the application settings are the following:
- **Hub:HubUrl**: URL of the SignalR Hub.
- **Scoreboard:Week**: number for the week in the season schedule.

> [!IMPORTANT]
> The **Scoreboard:Week** setting is used to set the week of the schedule for the games that will simulated.
> It is necessary that both the Football.Api and the Football.Worker applications have the same value for this setting.

Separately from the application settings, it is required to use the .NET user secrets to store settings that should not be committed to the repo:
- **ConnectionStrings:FootballDbConnection**: database connection string.

---

#### Docker

Running via Docker, the application will set *Development* as the **ASPNETCORE_ENVIRONMENT** and use environment variables defined in the `docker-run-worker: debug` task in the [tasks.json](/.vscode/tasks.json) file.

The keys required for the application settings are the following:
- **Hub:HubUrl**: URL for the Signal Hub.
- **Scoreboard:Week**: number for the week in the season schedule.

Separately from the application settings, it is required to use a `.env` file, named `.env.worker`, to store settings that should not be committed to the repo:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the custom certificate used by the application.
- **MYSQLCONNSTR_FootballDbConnection**: database connection string.


### Launch the application

#### dotnet CLI

The application can be launched in two ways:
1. From a terminal set at the root of the project, `./src/Hosts/Worker`, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard Worker* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5003, however, there are no relevant endpoints exposed. The main output of the application will be output to the console from the background service.

---

#### Docker

The Football.Worker web application requires a custom certificate for HTTPS when running via Docker.

The certificate and the key can be created with the `dotnet dev-certs` tool with the following commands:

```
dotnet dev-certs https -ep ~/.aspnet/https/Football.Worker.pfx -p {PASSWORD VALUE}
dotnet dev-certs https --trust
```

The {PASSWORD VALUE} is the same value that needs to be set for the **ASPNETCORE_Kestrel__Certificates__Default__Password** enviroment setting in the `.env.worker` file mentioned earlier in the [application settings](#application-settings) section.

Once the certificate is ready, the application can be launched from VSCode via the *Run and Debug* menu, simply select the *Launch Docker: Football Scoreboard API* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5003, however, there are no relevant endpoints exposed. The main output of the application will be output to the console from the background service.








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
