FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cypherly.EmaiService.Application/Cypherly.EmaiService.Application.csproj", "Cypherly.EmaiService.Application/"]
RUN dotnet restore "Cypherly.EmaiService.Application/Cypherly.EmaiService.Application.csproj"
COPY . .
WORKDIR "/src/Cypherly.EmaiService.Application"
RUN dotnet build "./Cypherly.EmaiService.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cypherly.EmaiService.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cypherly.EmaiService.Application.dll"]
