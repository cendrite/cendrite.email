FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY nuget.config ./nuget.config
RUN --mount=type=secret,id=nuget_token \
    sed -i "s|%NUGET_TOKEN%|$(cat /run/secrets/nuget_token)|g" nuget.config

COPY ["Cendrite.EmailService.API/Cendrite.EmailService.API.csproj", "Cendrite.EmailService.API/"]
RUN dotnet restore "Cendrite.EmailService.API/Cendrite.EmailService.API.csproj" --configfile ./nuget.config

COPY . .

WORKDIR "/src/Cendrite.EmailService.API"
RUN dotnet build "./Cendrite.EmailService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cendrite.EmailService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cendrite.EmailService.API.dll"]
