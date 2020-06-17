FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY bin/Release/netcoreapp3.1 .
EXPOSE 80
ENTRYPOINT [ "dotnet", "Dashboard.Server.dll" ]
