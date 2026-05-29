FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SetExplorer/SetExplorer.csproj", "src/SetExplorer/"]
COPY ["src/SetExplorer.Client/SetExplorer.Client.csproj", "src/SetExplorer.Client/"]
RUN dotnet restore "src/SetExplorer/SetExplorer.csproj"
COPY . .
WORKDIR "/src/src/SetExplorer"
RUN dotnet build "./SetExplorer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SetExplorer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SetExplorer.dll"]
