FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY /app/* .csproj ./app/
COPY /app /app
WORKDIR /app
RUN dotnet restore
RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT [ "dotnet", "PresentationCreatorAPI.dll" ]