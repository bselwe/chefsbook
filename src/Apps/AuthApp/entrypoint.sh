#!/bin/bash

set -e
run_cmd="dotnet run"

cd "../MigrationsApp"
until dotnet ef database update -c AuthDbContext; do
>&2 echo "Applying auth migrations..."
sleep 1
done

>&2 echo "Auth migrations applied."
cd "../AuthApp"
exec $run_cmd