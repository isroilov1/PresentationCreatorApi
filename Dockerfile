# .NET SDK image asosida boshlaymiz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image uchun .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PresentationCreatorAPI/PresentationCreatorAPI.csproj", "PresentationCreatorAPI/"]
COPY ["PresentationCreatorAPI.Domain/PresentationCreatorAPI.Domain.csproj", "PresentationCreatorAPI.Domain/"]
COPY ["PresentationCreatorAPI.Data/PresentationCreatorAPI.Data.csproj", "PresentationCreatorAPI.Data/"]
COPY ["PresentationCreatorAPI.Application/PresentationCreatorAPI.Application.csproj", "PresentationCreatorAPI.Application/"]
RUN dotnet restore "PresentationCreatorAPI/PresentationCreatorAPI.csproj"
COPY . .
WORKDIR "/src/PresentationCreatorAPI"
RUN dotnet build "PresentationCreatorAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PresentationCreatorAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# Yaratilgan image asosida runtime image yaratamiz
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PresentationCreatorAPI.dll"]