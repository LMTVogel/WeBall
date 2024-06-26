FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PaymentManagement/PaymentManagement.csproj", "PaymentManagement/"]
COPY ["PaymentManagement.Infrastructure/PaymentManagement.Infrastructure.csproj", "PaymentManagement.Infrastructure/"]
COPY ["PaymentManagement.Domain/PaymentManagement.Domain.csproj", "PaymentManagement.Domain/"]
COPY ["PaymentManagement.DomainServices/PaymentManagement.DomainServices.csproj", "PaymentManagement.DomainServices/"]
RUN dotnet restore "PaymentManagement/PaymentManagement.csproj"
COPY . .
RUN ls
WORKDIR "/src/PaymentManagement"
RUN dotnet build "PaymentManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PaymentManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentManagement.dll"]
