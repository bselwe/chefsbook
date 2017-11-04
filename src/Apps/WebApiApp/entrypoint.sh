#!/bin/bash

set -e
run_cmd="dotnet run"

cd "../MigrationsApp"
until dotnet ef database update -c CoreDbContext; do
>&2 echo "Applying database migrations..."
sleep 1
done

>&2 echo "Database migrations applied."
cd "../WebApiApp"
exec $run_cmd