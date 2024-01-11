# Blazor Football Scoreboard Football.Blazor

This Blazor web application run a website that displays the current scores and latest plays from games in the 2019 football season.
The index page of the website shows all the games in a given week. There is also a page for each game with additional information related to that game.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

Before starting the Blazor web application, it is important to know that it relies on both the SignalR Hub and HTTP API from the Football.Api application. The SignalR Hub relies on the BackgroundService from the Football.Worker application to receive the play data that is shown on the game pages. Follow the documentation to [launch the Football.Api](/src/Hosts/Api/README.md#application-launch) and [Football.Worker applications](/src/Hosts/Worker/README.md#application-launch).

### Application launch

The Football.Blazor application can be launched from:

1. A terminal set to the root of the project and executing the command, `dotnet run`.

    The application will run using *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Blazor/appsettings.Localhost.json) configuration file. The application settings require the following values:
        - **Api_ApiBaseUrl**: Base URL for the HTTP API that will be called by the Blazor application.
        - **Hub:HubUrl**: URL for the SignalR Hub that will receive play data.

    For HTTPS, set up the developer certificate with the `dotnet dev-certs` command. Once the application is running, it will be available at *https&#65279;://localhost:5002*

2. Select the *Launch Web: Football Scoreboard Blazor WebAssembly* launch config from the VSCode Run & Debug menu.

    The application will run using the same approach as option #1, this is just a convenient way to launch directly from VSCode.

3. Select the *Launch Docker: Football Scoreboard Blazor WebAssembly* launch config from the VSCode *Run and Debug* menu.

    The Docker container for the Blazor application runs a little differently than the previous options.
    The application runs via Nginx using the setup provided in the [tasks.json](/.vscode/tasks.json) file.

    Once the launch config starts, the application will run using the *Development* configuration via the `add_header` directive in the [nginx.conf](/src/Hosts/Blazor/nginx.conf) file. Application settings will be loaded from the APP SEETING DEVELOPMENT FILE
    The container image will be built using the [Dockerfile](/src/Hosts/Worker/Dockerfile) at the root of the project.

    In order to setup the SSL certificate for the Blazor application, before launching the container, run the following command at the root of the project folder:

    ```
    openssl req -x509 -newkey rsa:4096 -keyout key.pem -out cert.pem -nodes -days 3650 -subj "/CN=localhost"
    ```
    Pleas note that this certificate will be created without a passsword.

    Once the application is running, it will be available at *https&#65279;://localhost*

