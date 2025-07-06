FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG NUGET_TOKEN

WORKDIR /src

COPY NuGet.config ./
RUN sed -i "s|%NUGET_TOKEN%|${NUGET_TOKEN}|g" nuget.config

COPY ["Cypherly.EmailService.API/Cypherly.EmailService.API.csproj", "Cypherly.EmailService.API/"]
RUN dotnet restore "Cypherly.EmailService.API/Cypherly.EmailService.API.csproj" --configfile ./NuGet.config

COPY . .

WORKDIR "/src/Cypherly.EmailService.API"
RUN dotnet build "./Cypherly.EmailService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/Cypherly.EmailService.API"
RUN dotnet publish "./Cypherly.EmailService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cypherly.EmailService.API.dll"]
