FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CustomerSupportManagement/CustomerSupportManagement.csproj", "CustomerSupportManagement/"]
COPY ["CustomerSupportManagement.DomainServices/CustomerSupportManagement.DomainServices.csproj", "CustomerSupportManagement.DomainServices/"]
COPY ["CustomerSupportManagement.Domain/CustomerSupportManagement.Domain.csproj", "CustomerSupportManagement.Domain/"]
COPY ["CustomerSupportManagement.Infrastructure/CustomerSupportManagement.Infrastructure.csproj", "CustomerSupportManagement.Infrastructure/"]
RUN dotnet restore "CustomerSupportManagement/CustomerSupportManagement.csproj"
COPY . .
WORKDIR "/src/CustomerSupportManagement"
RUN dotnet build "CustomerSupportManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CustomerSupportManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerSupportManagement.dll"]
