# Blazor Football Scoreboard Football.Worker

This web application project runs a Background Service that reads the play log data from games in the 2019 football season. The service:

- Takes the play data and passes it off to a separate module which generates statistics for each game.
- Send the play data via a SignalR hub to the Blazor client.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

### Database configuration

In order to run the web application, a connection to a MySQL database is required.

A database is provided with the Docker Compose file found at the root of the repository, [docker-compose.db.yml](/docker-compose.db.yml).

The containers in the Compose file can be run in VSCode by right-clicking the file and selecting *Compose Up*, assuming the Docker extension is installed in VSCode. Otherwise, the command, `docker-compose -f docker-compose.db.yml up -d`, can be executed from a terminal at the root of the repository.

The Compose file references an SQL file, [football_db.sql](/data/football_db.sql), which seeds data to the database. The very first time the Compose file runs, the startup will take a bit longer due to the seeding process.

The Compose file requires a secrets file with the name *db.env*. This file should be at the rooot of the repository. The file must define the following environment variables, as they are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/):
- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

### Application launch

Once the database is up and running, the Football.Worker web application can be launched from:

1. A terminal set to the root of the project and executing the command, `dotnet run`.

    The application will run using *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Worker/appsettings.Localhost.json) configuration file. The application settings require the following values:
    - **Hub:HubUrl**: URL for the SignalR Hub that will receive play data.
    - **Scoreboard:Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

    It is necessary to set the value of the **ConnectionStrings:FootballDbConnection** setting separately. This is a sensitive value and it is not included in the [appsettings.Localhost.json](/src/Hosts/Worker/appsettings.Localhost.json) file. This can be done via the user secrets.

    For HTTPS, set up the developer certificate with the `dotnet dev-certs` command.

2. Select the *Launch Web: Football Scoreboard Worker* launch config from the VSCode Run & Debug menu.

    The application will run using the same approach as option #1, this is just a convenient way to launch directly from VSCode.

3. Select the *Launch Docker: Football Scoreboard Worker* launch config from the VSCode *Run and Debug* menu.

    The Docker container needs a certificate and its key in order to run with HTTPS. Before starting the container make sure that these files are created by running the command at the root of the project folder:

    ```
    openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 3650 -subj "/CN=localhost"
    ```

    The command will prompt you to create a password for the certificate. It is important to remember this password as it will be used later. It is also possible to use an empty password. Once this is done, there should be a *cert.pem* and a *key.pem* file at the root of the project.

    > [!IMPORTANT]
    > Keep in mind that that when running the whole system together, the Football.Worker application needs to call the Football.Api application. In order for this to work properly, make sure that the [Football.Api certificate](https://github.com/rolspace/blazor-football-scoreboard/tree/main/src/Hosts/Api#application-launch) is also created before the container is built.

    Once the launch config starts, the application will run using *Development* as the **ASPNETCORE_ENVIRONMENT**.
    The container image will be built using the [Dockerfile](/src/Hosts/Worker/Dockerfile) at the root of the project. The variables required for the application to run are defined in the  *docker-run-worker: debug* task, in the [tasks.json](/tasks.json) file:

    - [**ASPNETCORE_URLS**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0#server-urls)
    - [**ASPNETCORE_HTTPS_PORT**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0#https-port)
    - [**ASPNETCORE_Kestrel__Certificates__Default__Path**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-8.0#certificate-sources)
    - [**ASPNETCORE_Kestrel__Certificates__Default__KeyPath**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-8.0#certificate-sources)
    - **Hub__HubUrl**: SignalR Hub URL.
    - **Scoreboard__Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

    There is also a task definition with the label *docker-run-worker: release*, where **ASPNETCORE_ENVIRONMENT** is set to *Production*.

Regardless of the method used to run the application locally, it will be available at *https&#65279;://localhost:5003*.
