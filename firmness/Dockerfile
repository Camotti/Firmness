# --- Stage 1: Build ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todos los proyectos para restaurar correctamente
COPY firmness/firmness.csproj firmness/
COPY firmness.Api/firmness.Api.csproj firmness.Api/
COPY firmness.Application/firmness.Application.csproj firmness.Application/
COPY firmness.Domain/firmness.Domain.csproj firmness.Domain/
COPY firmness.Infrastructure/firmness.Infrastructure.csproj firmness.Infrastructure/

RUN dotnet restore firmness/firmness.csproj

# Copiar todo el código
COPY . .

# Build en modo Release
WORKDIR /src/firmness
RUN dotnet publish -c Release -o /app/publish --no-restore

# --- Stage 2: Runtime ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "firmness.dll"]