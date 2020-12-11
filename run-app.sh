#!/bin/sh

projectRoot=$(pwd)

cd $projectRoot/src/Scoreboard/Scoreboard.Server
dotnet publish -c Release

cd $projectRoot/src/Worker/Worker.Game
dotnet publish -c Release

cd $projectRoot/docker \
  && docker-compose -f compose-db.yml down \
  && docker-compose -f compose-app.yml up -d --build
