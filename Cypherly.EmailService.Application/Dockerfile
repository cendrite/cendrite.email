FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Cypherly.EmailService.Application/Cypherly.EmailService.Application.csproj", "Cypherly.EmailService.Application/"]
RUN dotnet restore "Cypherly.EmailService.Application/Cypherly.EmailService.Application.csproj"
COPY . .
WORKDIR "/src/Cypherly.EmailService.Application"
RUN dotnet build "./Cypherly.EmailService.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cypherly.EmailService.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cypherly.EmailService.Application.dll"]
