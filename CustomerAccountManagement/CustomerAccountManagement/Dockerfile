FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 6666 
EXPOSE 6667 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomerAccountManagement/CustomerAccountManagement.csproj", "CustomerAccountManagement/"]
COPY ["CustomerAccountManagement.DomainServices/CustomerAccountManagement.DomainServices.csproj", "CustomerAccountManagement.DomainServices/"]
COPY ["CustomerAccountManagement.Domain/CustomerAccountManagement.Domain.csproj", "CustomerAccountManagement.Domain/"]
COPY ["CustomerAccountManagement.Infrastructure/CustomerAccountManagement.Infrastructure.csproj", "CustomerAccountManagement.Infrastructure/"]
RUN dotnet restore "CustomerAccountManagement/CustomerAccountManagement.csproj"
COPY . .
WORKDIR "/src/CustomerAccountManagement"
RUN dotnet build "CustomerAccountManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CustomerAccountManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerAccountManagement.dll"]
