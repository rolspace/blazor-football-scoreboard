FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY bin/Release/netcoreapp3.1 .
ENTRYPOINT [ "dotnet", "Games.Worker.dll" ]
