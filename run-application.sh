#!/bin/sh

cd src/Dashboard/Dashboard.Server
dotnet publish -c Release

cd ../../Games/Games.Worker
dotnet publish -c Release

cd ../../../docker
pwd

docker-compose -f docker-compose-app.yml up --build
