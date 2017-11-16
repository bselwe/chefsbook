FROM microsoft/dotnet:latest

RUN mkdir /app
COPY . /app
WORKDIR /app/Apps/WebApiApp

RUN dotnet restore
RUN dotnet build

EXPOSE 5000/tcp

WORKDIR /app/Apps/MigrationsApp

RUN dotnet restore
RUN dotnet build

WORKDIR /app/Apps/WebApiApp

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
