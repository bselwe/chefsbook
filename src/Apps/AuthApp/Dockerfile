FROM microsoft/dotnet:latest

RUN mkdir /app
COPY . /app
WORKDIR /app/Apps/AuthApp

RUN dotnet restore
RUN dotnet build

EXPOSE 5000/tcp

WORKDIR /app/Apps/Data

RUN cp localhost.crt /usr/local/share/ca-certificates/
RUN update-ca-certificates

WORKDIR /app/Apps/MigrationsApp

RUN dotnet restore
RUN dotnet build

WORKDIR /app/Apps/AuthApp

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh
