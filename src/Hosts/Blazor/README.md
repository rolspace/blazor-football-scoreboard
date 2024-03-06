# Blazor Football Scoreboard Football.Blazor

This .NET 6 Blazor web application run a website that allows a user to view the current scores and latest plays from games in the 2019 football season.
The website has a page for all the games for a given week, where the user can select any of the games to show additional information related to that game.

## Requirements

- .NET 6+ SDK
- Visual Studio Code 1.83+
- Docker 4.30+

## How to run locally

The application can be launched locally with the dotnet CLI.

The Blazor web application relies on both the Football.Api and Football.Worker applications for the data shown to the user. Follow the documentation to launch the [Football.Api](/src/Hosts/Api/README.md) and the [Football.Worker](/src/Hosts/Worker/README.md) applications.

### Application settings

Running via the dotnet CLI, the application will set *Localhost* as the **ASPNETCORE_ENVIRONMENT** and use the [appsettings.Localhost.json](/src/Hosts/Blazor/wwwroot/appsettings.Localhost.json) configuration file.

The keys required for the application settings are the following:
- **Hub:HubUrl**: URL for the Signal Hub.
- **Api:ApiBaseUrl**: Base URL for the HTTP API.

### Launch the application

The application can be launched in two ways:
1. From a terminal set at the root of the project, `./src/Hosts/Blazor`, with the the command: `dotnet run`.
2. From VSCode via the *Run & Debug* menu. Select the *Launch Web: Football Scoreboard Blazor* launch config.

Once the application starts, it will be available at https&ZeroWidthSpace;://localhost:5002.
