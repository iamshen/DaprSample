#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Ordering/Presentation/Ordering.Api/Ordering.Api.csproj", "src/Services/Ordering/Presentation/Ordering.Api/"]
COPY ["src/BuildingBlocks/ApiExtensions/ApiExtensions.csproj", "src/BuildingBlocks/ApiExtensions/"]
COPY ["src/BuildingBlocks/CommonUtility/CommonUtility.csproj", "src/BuildingBlocks/CommonUtility/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/BuildingBlocks/Healthchecks/Healthchecks.csproj", "src/BuildingBlocks/Healthchecks/"]
COPY ["src/Services/Ordering/Application/Ordering.Application/Ordering.Application.csproj", "src/Services/Ordering/Application/Ordering.Application/"]
COPY ["src/Services/Ordering/Domain/Ordering.Domain.Interfaces/Ordering.Domain.Interfaces.csproj", "src/Services/Ordering/Domain/Ordering.Domain.Interfaces/"]
COPY ["src/Services/Ordering/Infrastructure/Ordering.Infrastructure.Shared/Ordering.Infrastructure.Shared.csproj", "src/Services/Ordering/Infrastructure/Ordering.Infrastructure.Shared/"]
COPY ["src/Services/Ordering/Infrastructure/Ordering.Infrastructure.Linq2Db/Ordering.Infrastructure.Linq2Db.csproj", "src/Services/Ordering/Infrastructure/Ordering.Infrastructure.Linq2Db/"]
COPY ["src/BuildingBlocks/Linq2DbAccessor/Linq2DbAccessor.csproj", "src/BuildingBlocks/Linq2DbAccessor/"]
RUN dotnet restore "src/Services/Ordering/Presentation/Ordering.Api/Ordering.Api.csproj"

COPY . .
WORKDIR "/src/src/Services/Ordering/Presentation/Ordering.Api"
RUN dotnet build "Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]