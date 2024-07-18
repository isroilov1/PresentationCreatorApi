FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PresentationCreatorAPI/PresentationCreatorAPI.csproj", "PresentationCreatorAPI/"]
RUN dotnet restore "PresentationCreatorAPI/PresentationCreatorAPI.csproj"
COPY . .
WORKDIR "/src/PresentationCreatorAPI"
RUN dotnet build "PresentationCreatorAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PresentationCreatorAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PresentationCreatorAPI.dll"]
