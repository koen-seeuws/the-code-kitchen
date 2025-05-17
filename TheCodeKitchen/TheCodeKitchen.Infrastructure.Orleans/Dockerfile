FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 11111  # Silo-to-Silo communication (default)
EXPOSE 30000  # Client-to-Silo communication (default)
EXPOSE 8080   # Orleans Dashboard

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TheCodeKitchen.Infrastructure.Orleans/TheCodeKitchen.Infrastructure.Orleans.csproj", "TheCodeKitchen.Infrastructure.Orleans/"]
COPY ["TheCodeKitchen.Application.Business/TheCodeKitchen.Application.Business.csproj", "TheCodeKitchen.Application.Business/"]
COPY ["TheCodeKitchen.Application.Contracts/TheCodeKitchen.Application.Contracts.csproj", "TheCodeKitchen.Application.Contracts/"]
COPY ["TheCodeKitchen.Core.Domain/TheCodeKitchen.Core.Domain.csproj", "TheCodeKitchen.Core.Domain/"]
COPY ["TheCodeKitchen.Core.Enums/TheCodeKitchen.Core.Enums.csproj", "TheCodeKitchen.Core.Enums/"]
COPY ["TheCodeKitchen.Infrastructure/TheCodeKitchen.Infrastructure.csproj", "TheCodeKitchen.Infrastructure/"]
RUN dotnet restore "TheCodeKitchen.Infrastructure.Orleans/TheCodeKitchen.Infrastructure.Orleans.csproj"
COPY . .
WORKDIR "/src/TheCodeKitchen.Infrastructure.Orleans"
RUN dotnet build "TheCodeKitchen.Infrastructure.Orleans.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TheCodeKitchen.Infrastructure.Orleans.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheCodeKitchen.Infrastructure.Orleans.dll"]
