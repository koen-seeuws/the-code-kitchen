FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TheCodeKitchen.Infrastructure.Orleans.Dashboard/TheCodeKitchen.Infrastructure.Orleans.Dashboard.csproj", "TheCodeKitchen.Infrastructure.Orleans.Dashboard/"]
COPY ["TheCodeKitchen.Infrastructure.Orleans/TheCodeKitchen.Infrastructure.Orleans.csproj", "TheCodeKitchen.Infrastructure.Orleans/"]
COPY ["TheCodeKitchen.Infrastructure/TheCodeKitchen.Infrastructure.csproj", "TheCodeKitchen.Infrastructure/"]
COPY ["TheCodeKitchen.Application.Constants/TheCodeKitchen.Application.Constants.csproj", "TheCodeKitchen.Application.Constants/"]
COPY ["TheCodeKitchen.Application.Contracts/TheCodeKitchen.Application.Contracts.csproj", "TheCodeKitchen.Application.Contracts/"]
COPY ["TheCodeKitchen.Core.Domain/TheCodeKitchen.Core.Domain.csproj", "TheCodeKitchen.Core.Domain/"]
COPY ["TheCodeKitchen.Core.Enums/TheCodeKitchen.Core.Enums.csproj", "TheCodeKitchen.Core.Enums/"]
RUN dotnet restore "TheCodeKitchen.Infrastructure.Orleans.Dashboard/TheCodeKitchen.Infrastructure.Orleans.Dashboard.csproj"
COPY . .
WORKDIR "/src/TheCodeKitchen.Infrastructure.Orleans.Dashboard"
RUN dotnet build "./TheCodeKitchen.Infrastructure.Orleans.Dashboard.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TheCodeKitchen.Infrastructure.Orleans.Dashboard.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TheCodeKitchen.Infrastructure.Orleans.Dashboard.dll"]
