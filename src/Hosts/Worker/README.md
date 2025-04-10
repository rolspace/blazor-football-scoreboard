# Blazor Football Scoreboard Football.Worker

This .NET 6 web application runs a background service that replays game logs from games in the 2019 football season.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker Desktop 4.30+

## How to run locally

The application can be launched locally with the dotnet CLI or with Docker.
In order for the application to run successfully a local database and the application settings are required.

### Database configuration

The Docker Compose file, [docker-compose.localdb.yml](/docker-compose.app.yml), provides a MySQL database and a database management tool, [Adminer](https://www.adminer.org/).

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

The Docker Compose file will start containers for the local database and Adminer.

For the initial launch of the database container, there is an automated seeding process to generate the tables and data, based on the [footballscoreboard_localdb.sql](/scripts/localdb/footballscoreboard_localdb.sql) file.

Due to the size of the local database, the container startup will take a few minutes. Once the container is ready, the database will be persisted locally with a Docker Volume in the *.docker/volumes/localdb* folder.

Adminer will be available at the following URL: *http&ZeroWidthSpace;://localhost:8080*.

### Application settings

#### dotnet CLI

Running via the dotnet CLI, the startup will set the **ASPNETCORE_ENVIRONMENT** to *Localhost* and use the [appsettings.Localhost.json](/src/Hosts/Worker/appsettings.Localhost.json) configuration file.

The keys required for the application settings are the following:
- **Hub:HubUrl**: URL of the SignalR Hub.
- **Scoreboard:Week**: number for the week in the season schedule.

> [!IMPORTANT]
> The **Scoreboard:Week** setting is used to set the week in the season schedule that will be simulated.
> It is necessary that both the [Football.Api](/src/Hosts/Api/) and the [Football.Worker](/src/Hosts/Worker/) applications have the same value for this setting.

Separately from the application settings file, an additional setting should be stored in the .NET user secrets:
- **ConnectionStrings:FootballDbConnection**: database connection string.

---

#### Docker

Running via Docker, the application will set the **ASPNETCORE_ENVIRONMENT** variable to *Development* and use the environment variables defined in the `docker-run-worker: debug` task in the [tasks.json](/.vscode/tasks.json) file.

The keys required for the application settings are the following:
- **Hub:HubUrl**: URL for the Signal Hub.
- **Scoreboard:Week**: number for the week in the season schedule.

> [!IMPORTANT]
> The **Scoreboard:Week** setting is used to set the week in the season schedule that will be simulated.
> It is necessary that both the [Football.Api](/src/Hosts/Api/) and the [Football.Worker](/src/Hosts/Worker/) applications have the same value for this setting.

Separately from the environment variables, sensitive settings should be stored in a `.env.worker` file, at the root of the repository:
- **ASPNETCORE_Kestrel__Certificates__Default__Password**: password for the custom certificate used by the application.
- **MYSQLCONNSTR_FootballDbConnection**: database connection string.

### Launch the application

#### dotnet CLI

The application can be launched in two ways:
1. From a terminal set at the root of the project, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard Worker* launch config.

Once the application starts, it will be available at *https&ZeroWidthSpace;://localhost:5003*, however, there are no relevant endpoints exposed. The main output of the application will be output to the console from the background service.

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

Once the application starts, it will be available at *https&ZeroWidthSpace;://localhost:5003*, however, there are no other endpoints available. The main output of the application will be sent to the console from the running background service.
