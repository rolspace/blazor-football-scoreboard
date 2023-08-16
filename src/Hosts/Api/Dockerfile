#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Hosts/Api/Football.Api.csproj", "src/Hosts/Api/"]
COPY ["src/Application/Football.Application.csproj", "src/Application/"]
COPY ["src/Domain/Football.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Football.Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/Hosts/Api/Football.Api.csproj"
COPY . .
WORKDIR "/src/src/Hosts/Api"
RUN dotnet build "Football.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Football.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Football.Api.dll"]