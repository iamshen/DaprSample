#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Ordering/Presentation/Ordering.Host/Ordering.Host.csproj", "src/Services/Ordering/Presentation/Ordering.Host/"]
COPY ["src/Services/Ordering/Domain/Ordering.Domain/Ordering.Domain.csproj", "src/Services/Ordering/Domain/Ordering.Domain/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/BuildingBlocks/CommonUtility/CommonUtility.csproj", "src/BuildingBlocks/CommonUtility/"]
COPY ["src/Services/Ordering/Domain/Ordering.Domain.Interfaces/Ordering.Domain.Core.csproj", "src/Services/Ordering/Domain/Ordering.Domain.Interfaces/"]
COPY ["src/Services/Ordering/Infrastructure/Ordering.Infrastructure.Shared/Ordering.Infrastructure.Shared.csproj", "src/Services/Ordering/Infrastructure/Ordering.Infrastructure.Shared/"]
RUN dotnet restore "src/Services/Ordering/Presentation/Ordering.Host/Ordering.Host.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/Presentation/Ordering.Host"
RUN dotnet build "Ordering.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Host.dll"]