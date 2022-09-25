#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/PriceList/Discount.API/PriceList.API.csproj", "Services/PriceList/Discount.API/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/PriceList/Discount.API/PriceList.API.csproj"
COPY . .
WORKDIR "/src/Services/PriceList/Discount.API"
RUN dotnet build "PriceList.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PriceList.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PriceList.API.dll"]