FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Domain/Football.Domain.csproj", "Domain/"]
COPY ["src/Application/Football.Application.csproj", "Application/"]
COPY ["src/Infrastructure/Football.Infrastructure.csproj", "Infrastructure/"]
COPY ["src/Hosts/Api/Football.Api.csproj", "Hosts/Api/"]
RUN dotnet restore "/src/Hosts/Api/Football.Api.csproj"
COPY src/Domain/ "Domain/"
COPY src/Application/ "Application/"
COPY src/Infrastructure/ "Infrastructure/"
COPY src/Hosts/Api/ "Hosts/Api/"
WORKDIR "/src/Hosts/Api"
RUN dotnet build "Football.Api.csproj" -c "$configuration" -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Football.Api.csproj" -c "$configuration" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Football.Api.dll"]
