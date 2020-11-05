#!/bin/sh

cd src/Dashboard/Dashboard.Server
dotnet publish -c Release

cd ../../Worker/Worker.Game
dotnet publish -c Release

cd ../../../docker
pwd

docker-compose -f docker-compose-app.yml up --build
