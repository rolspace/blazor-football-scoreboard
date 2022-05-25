FROM mcr.microsoft.com/dotnet/runtime:6.0-focal AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["src/Workers/GameWorker/Football.Workers.GameWorker.csproj", "src/Workers/GameWorker/"]
RUN dotnet restore "src/Workers/GameWorker/Football.Workers.GameWorker.csproj"
COPY . .
WORKDIR "/src/src/Workers/GameWorker"
RUN dotnet build "Football.Workers.GameWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Football.Workers.GameWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Football.Workers.GameWorker.dll"]
