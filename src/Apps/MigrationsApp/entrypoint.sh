#!/bin/bash

set -e
run_cmd="dotnet run --project ../WebApiApp/ChefsBook.WebApiApp.csproj"

until dotnet ef database update -c CoreDbContext; do
>&2 echo "Applying database migrations..."
sleep 1
done

>&2 echo "Database migrations applied."
exec $run_cmd