FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 2500
EXPOSE 11517

ENV ASPNETCORE_URLS=http://+:2500

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["src/Services/GameService/Football.Services.GameService.csproj", "src/Services/GameService/"]
RUN dotnet restore "src/Services/GameService/Football.Services.GameService.csproj"
COPY . .
WORKDIR "/src/src/Services/GameService"
RUN dotnet build "Football.Services.GameService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Football.Services.GameService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
HEALTHCHECK CMD curl --fail http://localhost:5000/health || exit
ENTRYPOINT ["dotnet", "Football.Services.GameService.dll"]
