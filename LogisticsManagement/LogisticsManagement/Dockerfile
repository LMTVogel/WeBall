FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 1337 
EXPOSE 1338 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LogisticsManagement/LogisticsManagement.csproj", "LogisticsManagement/"]
COPY ["LogisticsManagement.Infrastructure/LogisticsManagement.Infrastructure.csproj", "LogisticsManagement.Infrastructure/"]
COPY ["LogisticsManagement.DomainServices/LogisticsManagement.DomainServices.csproj", "LogisticsManagement.DomainServices/"]
COPY ["LogisticsManagement.Domain/LogisticsManagement.Domain.csproj", "LogisticsManagement.Domain/"]
RUN dotnet restore "LogisticsManagement/LogisticsManagement.csproj"
COPY . .
WORKDIR "/src/LogisticsManagement"
RUN dotnet build "LogisticsManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LogisticsManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogisticsManagement.dll"]
