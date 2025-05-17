FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
# Silo-to-Silo communication (default)
EXPOSE 11111
# Client-to-Silo communication (default)
EXPOSE 30000
# Orleans Dashboard
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TheCodeKitchen.Infrastructure.OrleansSilo/TheCodeKitchen.Infrastructure.OrleansSilo.csproj", "TheCodeKitchen.Infrastructure.OrleansSilo/"]
COPY ["TheCodeKitchen.Application.Business/TheCodeKitchen.Application.Business.csproj", "TheCodeKitchen.Application.Business/"]
COPY ["TheCodeKitchen.Application.Contracts/TheCodeKitchen.Application.Contracts.csproj", "TheCodeKitchen.Application.Contracts/"]
COPY ["TheCodeKitchen.Core.Domain/TheCodeKitchen.Core.Domain.csproj", "TheCodeKitchen.Core.Domain/"]
COPY ["TheCodeKitchen.Core.Enums/TheCodeKitchen.Core.Enums.csproj", "TheCodeKitchen.Core.Enums/"]
COPY ["TheCodeKitchen.Infrastructure/TheCodeKitchen.Infrastructure.csproj", "TheCodeKitchen.Infrastructure/"]
RUN dotnet restore "TheCodeKitchen.Infrastructure.OrleansSilo/TheCodeKitchen.Infrastructure.OrleansSilo.csproj"
COPY . .
WORKDIR "/src/TheCodeKitchen.Infrastructure.OrleansSilo"
RUN dotnet build "TheCodeKitchen.Infrastructure.OrleansSilo.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TheCodeKitchen.Infrastructure.OrleansSilo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheCodeKitchen.Infrastructure.OrleansSilo.dll"]
