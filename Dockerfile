# ============================================
# Stage 1: Build
# ============================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y todos los archivos csproj para aprovechar el cache de Docker
COPY *.sln ./
COPY firmness.Api/*.csproj ./firmness.Api/
COPY firmness.Application/*.csproj ./firmness.Application/
COPY firmness.Domain/*.csproj ./firmness.Domain/
COPY firmness.Infrastructure/*.csproj ./firmness.Infrastructure/
COPY firmness.Tests/*.csproj ./firmness.Tests/

# Restaurar dependencias (esta capa se cachea si no cambian los .csproj)
RUN dotnet restore "firmness.Api/firmness.Api.csproj"

# Copiar el resto del código fuente
COPY . .

# Compilar y publicar la aplicación
# - Self-contained=false: usa el runtime compartido (imagen más pequeña)
# - PublishReadyToRun=true: optimización AOT parcial para inicio más rápido
# - PublishSingleFile=false: mejor para contenedores
WORKDIR /src/firmness.Api
RUN dotnet publish "firmness.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:PublishReadyToRun=true

# ============================================
# Stage 2: Runtime
# ============================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Crear usuario no-root para mayor seguridad
RUN groupadd -r appuser && useradd -r -g appuser appuser

WORKDIR /app

# Copiar archivos publicados desde la etapa de build
COPY --from=build --chown=appuser:appuser /app/publish .

# Variables de entorno para optimización
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_EnableDiagnostics=0 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Exponer puerto
EXPOSE 8080

# Usar usuario no-root
USER appuser

# Health check para verificar el estado de la aplicación
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Punto de entrada
ENTRYPOINT ["dotnet", "firmness.Api.dll"]