# Blazor Football Scoreboard Football.Api

This web application project runs an HTTP API endpoint and a SignalR Hub that are used to allow communication between the Worker process and the Blazor UI.

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

The Compose file requires an env file with the name *db.env*. This file should be at the root of the repository. The file must define the following environment variables, as they are required by the [MySQL Docker image](https://hub.docker.com/_/mysql/):
- MYSQL_ROOT_PASSWORD
- MYSQL_USER
- MYSQL_PASSWORD

### Application launch

Once the database is up and running, the Football.Api web application can be launched from:

1. A terminal set to the root of the project and executing the command, `dotnet run`.

    The application will run using *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Api/appsettings.Localhost.json) configuration file. The application settings require the following values:
    - **Cors:PolicyName**: CORS policy name defined for the web application.
    - **Cors:AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to *https&#65279;://localhost:5002* to allow calls from the Blazor application.
    - **Cors:AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to *GET* to allow calls from the Blazor application.
    - **Scoreboard:Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

    It is necessary to set the value of the **ConnectionStrings:FootballDbConnection** setting separately. This is a sensitive value and it is not included in the [appsettings.Localhost.json](/src/Hosts/Api/appsettings.Localhost.json) file. This can be done via the user secrets.

    For HTTPS, set up the developer certificate with the `dotnet dev-certs` command.

2. Select the *Launch Web: Football Scoreboard API* launch config from the VSCode *Run & Debug* menu.

    The application will run using the same approach as option #1, this is just a convenient way to launch directly from VSCode.

3. Select the *Launch Docker: Football Scoreboard API* launch config from the VSCode *Run and Debug* menu.

    In order to setup the SSL certificate for the application, before launching the container, make sure that certificate and key files are created by running this command at the root of the project folder:

    ```
    openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -sha256 -days 3650 -subj "/CN=footballscoreboard_api"
    ```

    The *CN (Common Name)* value is set to `footballscoreboard_api`. This is needed to allow container to container calls within the container network when running all the applications together. This is also the reason why the `dev-certs` PFX certificate is not used, as it does not support setting the CN value.

    The command will prompt you to create a password for the certificate. It is important to remember this password as it will be used later. It is also possible to use an empty password. Once this is done, there should be a *cert.pem* and a *key.pem* file at the root of the project.

    Once the launch config starts, the application will run using *Development* as the **ASPNETCORE_ENVIRONMENT**.
    The container image will be built using the [Dockerfile](/src/Hosts/Api/Dockerfile) at the root of the project. The variables required for the application to run are defined in the  *docker-run-api: debug* task, in the [tasks.json](/.vscode/tasks.json) file:

    - [**ASPNETCORE_URLS**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0#server-urls)
    - [**ASPNETCORE_HTTPS_PORT**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0#https-port)
    - [**ASPNETCORE_Kestrel__Certificates__Default__Path**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-8.0#certificate-sources)
    - [**ASPNETCORE_Kestrel__Certificates__Default__KeyPath**](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-8.0#certificate-sources)
    - **Cors__PolicyName**: CORS policy name defined for the Football.Api application.
    - **Cors__AllowedOrigins**: comma-separated list of CORS allowed origins. This value should be set to *https&#65279;://localhost:5002* to allow calls from the Blazor application.
    - **Cors__AllowedMethods**: comma-separated list of CORS allowed methods.  This value should be set to `GET` to allow calls from the Blazor application.
    - **Scoreboard__Week**: week number for the scheduled games, should be a value between 1 and 17. When running all the applications together, this value should match in both the Football.Api and Football.Worker applications.

    There is also a task definition with the label *docker-run-api: release*, where **ASPNETCORE_ENVIRONMENT** is set to *Production*.

Regardless of the method used to run the application locally, it will be available at *https&#65279;://localhost:5001*.

## How to run the tests

### Running the tests

1. Set the current working directory in your terminal of choice to the root of the repository, where the [Scoreboard.sln](/Scoreboard.sln) solution file is located.

2. Run the tests with the `dotnet test` command.

### Running the tests with coverage

1. Install the dotnet coverage tool globally with the command, `dotnet tool install --global dotnet-coverage`.

2. Install the dotnet report generator tool globally with the command, `dotnet tool install dotnet-reportgenerator-globaltool`.

3. Set the current working directory in your terminal of choice to the root of the repository, where the [Scoreboard.sln](/Scoreboard.sln) solution file is located.

4. Run the tests with the command, `dotnet-coverage collect 'dotnet test --no-restore' -f cobertura  -o 'coverage.xml'`. It is possible to change the report output by [changing the `-f` and `-o` parameters from the collect command](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/dotnet-coverage#dotnet-coverage-collect).

5. Generate the coverage report using the reportgenerator tool with the command, `reportgenerator "-reports:coverage.xml" "-reporttypes:Html" "-targetdir:./coveragereport" "-assemblyfilters:+Football.*;-Football.*Tests";`. The HTML coverage report will be found in the `coveragereport` folder at the root of the repository, open the `index.html` file on your browser of choice.

## HTTP API

The API exposes the following endpoints:

### GET /api/v1/games?week={week}

Retrieves all the games for a given week parameter, where the value of week is a number between 1 and 17.

Returns a 400 if no week parameter is provided.

Returns a 404 if no games are found for the given week.

### GET /api/v1/games/today

Retrieves all the games scheduled for today.

### GET /api/v1/games/{id}

Retrieves the game data for the specified id.

Returns a 400 if no id parameter is provided.

Returns a 404 if the id cannot be found.

### GET /api/v1/games/{id}/stats

Retrieves the game statistics for the specified id.

Returns a 400 if no id parameter is provided.

Returns a 404 if the id cannot be found.

## SignalR Hub

### /hub/plays

The hub endpoint exposes the method used by the [Worker](./src/Hosts/Football.Worker) game simulation that sends play data to the [Blazor](./src/Hosts/Football.Blazor) web clients.
