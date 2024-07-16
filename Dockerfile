# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj and restore as distinct layers
COPY *.sln .
COPY PresentationCreatorAPI/*.csproj ./PresentationCreatorAPI/
COPY PresentationCreatorAPI.Application/*.csproj ./PresentationCreatorAPI.Application/
COPY PresentationCreatorAPI.Data/*.csproj ./PresentationCreatorAPI.Data/
COPY PresentationCreatorAPI.Domain/*.csproj ./PresentationCreatorAPI.Domain/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/PresentationCreatorAPI
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/PresentationCreatorAPI/out .
ENTRYPOINT ["dotnet", "PresentationCreatorAPI.dll"]