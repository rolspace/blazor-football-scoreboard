FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

COPY src/Hosts/Worker/cert.pem /https/cert.pem
COPY src/Hosts/Worker/key.pem /https/key.pem
COPY src/Hosts/Api/cert.pem /etc/ssl/certs/FootballApi-cert.pem

RUN update-ca-certificates --verbose --fresh

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Domain/Football.Domain.csproj", "Domain/"]
COPY ["src/Application/Football.Application.csproj", "Application/"]
COPY ["src/Infrastructure/Football.Infrastructure.csproj", "Infrastructure/"]
COPY ["src/Hosts/Worker/Football.Worker.csproj", "Hosts/Worker/"]
RUN dotnet restore "/src/Hosts/Worker/Football.Worker.csproj"

COPY src/Domain/ "Domain/"
COPY src/Application/ "Application/"
COPY src/Infrastructure/ "Infrastructure/"
COPY src/Hosts/Worker/ "Hosts/Worker/"
WORKDIR "/src/Hosts/Worker"
RUN dotnet build "Football.Worker.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Football.Worker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Football.Worker.dll"]
