#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Authorization/AuthAPI/AuthAPI.csproj", "Services/Authorization/AuthAPI/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
RUN dotnet restore "Services/Authorization/AuthAPI/AuthAPI.csproj"
COPY . .
WORKDIR "/src/Services/Authorization/AuthAPI"
RUN dotnet build "AuthAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthAPI.dll"]