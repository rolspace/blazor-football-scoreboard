# Blazor Football Scoreboard Football.Blazor

This .NET 6 Blazor web application runs a website with the current scores and latest plays from games in the 2019 football season.
The website has an index page for all the games in a given week in the season schedule, where the user can select any of the games to show additional information related to that game.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker Desktop 4.30+

## How to run locally

The application can be launched locally with the dotnet CLI.

It relies on both the [Football.Api](/src/Hosts/Api/README.md) and [Football.Worker](/src/Hosts/Worker/README.md) applications to retrieve the data shown to the user. Follow the documentation for each application to launch it.

### Application settings

Running via the dotnet CLI, the startup will set the the **ASPNETCORE_ENVIRONMENT** to *Localhost* and use the [appsettings.Localhost.json](/src/Hosts/Blazor/wwwroot/appsettings.Localhost.json) configuration file.

The keys required for the application settings are the following:
- **Hub:HubUrl**: URL for the Signal Hub, this hub is provided by the Football.Api application.
- **Api:ApiBaseUrl**: Base URL for the HTTP API, this API is provided by the Football.Api application.

### Launch the application

The application can be launched in two ways:
1. From a terminal set at the root of the project, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard Blazor* launch config.

Once the application starts, it will be available at *https&ZeroWidthSpace;://localhost:5002*.
