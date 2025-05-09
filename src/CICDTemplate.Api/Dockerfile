FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["Nuget.config", "."]
COPY ["src/CICDTemplate.Api/CICDTemplate.Api.csproj", "src/CICDTemplate.Api/"]
COPY ["src/CICDTemplate.Application/CICDTemplate.Application.csproj", "src/CICDTemplate.Application/"]
COPY ["src/CICDTemplate.Domain/CICDTemplate.Domain.csproj", "src/CICDTemplate.Domain/"]
COPY ["src/CICDTemplate.Infrastructure/CICDTemplate.Infrastructure.csproj", "src/CICDTemplate.Infrastructure/"]
COPY ["src/CICDTemplate.ServiceDefaults/CICDTemplate.ServiceDefaults.csproj", "src/CICDTemplate.ServiceDefaults/"]
RUN dotnet restore "./src/CICDTemplate.Api/CICDTemplate.Api.csproj"
COPY . .
WORKDIR "/src/src/CICDTemplate.Api"
RUN dotnet build "./CICDTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CICDTemplate.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CICDTemplate.Api.dll"]