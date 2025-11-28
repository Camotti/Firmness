# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y los .csproj de cada proyecto (aprovecha cache de Docker)
COPY *.sln ./
COPY firmness.Api/*.csproj ./firmness.Api/
COPY firmness.Application/*.csproj ./firmness.Application/
COPY firmness.Domain/*.csproj ./firmness.Domain/
COPY firmness.Infrastructure/*.csproj ./firmness.Infrastructure/
COPY firmness.Tests/*.csproj ./firmness.Tests/

# Restaurar dependencias del proyecto principal
RUN dotnet restore "firmness.Api/firmness.Api.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar y publicar la API
WORKDIR /src/firmness.Api
RUN dotnet publish "firmness.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false \
    /p:PublishReadyToRun=false

# ============================================
# Stage 2: Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Crear usuario no-root para seguridad
RUN groupadd -r appuser && useradd -r -g appuser appuser

WORKDIR /app

# Copiar los archivos publicados desde la etapa de build
COPY --from=build --chown=appuser:appuser /app/publish .

# Variables de entorno recomendadas
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Exponer puerto
EXPOSE 8080

# Cambiar a usuario no-root
USER appuser

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Punto de entrada
ENTRYPOINT ["dotnet", "firmness.Api.dll"]
