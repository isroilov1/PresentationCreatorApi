# .NET SDK image asosida boshlaymiz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image uchun .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PresentationCreatorAPI/PresentationCreatorAPI.csproj", "PresentationCreatorAPI/"]
COPY ["PresentationCreatorAPI.Domain/PresentationCreatorAPI.Domain.csproj", "PresentationCreatorAPI.Domain/"]
COPY ["PresentationCreatorAPI.Data/PresentationCreatorAPI.Data.csproj", "PresentationCreatorAPI.Data/"]
COPY ["PresentationCreatorAPI.Application/PresentationCreatorAPI.Application.csproj", "PresentationCreatorAPI.Application/"]
RUN dotnet restore "PresentationCreatorAPI/PresentationCreatorAPI.csproj"
COPY . .
WORKDIR "/src/PresentationCreatorAPI"
RUN dotnet build "PresentationCreatorAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PresentationCreatorAPI.csproj" -c Release -o /app/publish

# Yaratilgan image asosida runtime image yaratamiz
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PresentationCreatorAPI.dll"]