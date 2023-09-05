FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Domain/Football.Domain.csproj", "Domain/"]
COPY ["Application/Football.Application.csproj", "Application/"]
COPY ["Infrastructure/Football.Infrastructure.csproj", "Infrastructure/"]
COPY ["Hosts/Worker/Football.Worker.csproj", "Hosts/Worker/"]
RUN dotnet restore "/src/Hosts/Worker/Football.Worker.csproj"

COPY Domain/ "Domain/"
COPY Application/ "Application/"
COPY Infrastructure/ "Infrastructure/"
COPY Hosts/Worker "Hosts/Worker"
WORKDIR "/src/Hosts/Worker"
RUN dotnet build "Football.Worker.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Football.Worker.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Football.Worker.dll"]
