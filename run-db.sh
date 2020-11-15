#!/bin/sh

# Execute docker-compose only for db specific images
cd docker && docker-compose -f compose-db.yml up -d
