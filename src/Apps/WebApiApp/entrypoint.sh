#!/bin/bash

set -e
run_cmd="dotnet run"

cd "../MigrationsApp"
until dotnet ef database update -c CoreDbContext; do
>&2 echo "Applying core migrations..."
sleep 1
done

>&2 echo "Core migrations applied."
cd "../WebApiApp"
exec $run_cmd