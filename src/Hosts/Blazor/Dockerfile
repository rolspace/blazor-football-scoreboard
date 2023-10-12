FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release

WORKDIR /src
COPY ["src/Domain/Football.Domain.csproj", "Domain/"]
COPY ["src/Application/Football.Application.csproj", "Application/"]
COPY ["src/Infrastructure/Football.Infrastructure.csproj", "Infrastructure/"]
COPY ["src/Hosts/Blazor/Football.Blazor.csproj", "Hosts/Blazor/"]
RUN dotnet restore "/src/Hosts/Blazor/Football.Blazor.csproj"

COPY src/Domain/ "Domain/"
COPY src/Application/ "Application/"
COPY src/Infrastructure/ "Infrastructure/"
COPY src/Hosts/Blazor/ "Hosts/Blazor/"
WORKDIR "/src/Hosts/Blazor"
RUN dotnet build "Football.Blazor.csproj" -c $configuration -o /app/build \
    && dotnet publish "Football.Blazor.csproj" -c $configuration -o /app/publish

FROM nginx AS final
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
COPY src/Hosts/Blazor/nginx.conf /etc/nginx/nginx.conf
COPY src/Hosts/Blazor/cert.pem /etc/nginx/certs/cert.pem
COPY src/Hosts/Blazor/key.pem /etc/nginx/certs/key.pem
