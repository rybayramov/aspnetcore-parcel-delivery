#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Delivery.API/Ordering.API/Ordering.API.csproj", "Services/Delivery.API/Ordering.API/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Services/Delivery.API/Ordering.Application/Ordering.Application.csproj", "Services/Delivery.API/Ordering.Application/"]
COPY ["Services/Delivery.API/Ordering.Domain/Ordering.Domain.csproj", "Services/Delivery.API/Ordering.Domain/"]
COPY ["Services/Delivery.API/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Services/Delivery.API/Ordering.Infrastructure/"]
RUN dotnet restore "Services/Delivery.API/Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Services/Delivery.API/Ordering.API"
RUN dotnet build "Ordering.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]