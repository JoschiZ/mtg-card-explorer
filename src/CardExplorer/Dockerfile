FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CardExplorer/CardExplorer.csproj", "src/CardExplorer/"]
COPY ["src/CardExplorer.Client/CardExplorer.Client.csproj", "src/CardExplorer.Client/"]
RUN dotnet restore "src/CardExplorer/CardExplorer.csproj"
COPY . .
WORKDIR "/src/src/CardExplorer"
RUN dotnet build "./CardExplorer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CardExplorer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardExplorer.dll"]
