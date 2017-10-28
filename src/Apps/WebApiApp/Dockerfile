FROM microsoft/dotnet:latest

RUN mkdir /app
COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet build

EXPOSE 5000/tcp

ENTRYPOINT [ "dotnet", "run" ]