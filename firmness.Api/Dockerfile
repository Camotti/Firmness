# 1️⃣ Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo los .csproj de cada proyecto (esto acelera y evita errores)
COPY firmness/firmness.csproj firmness/
COPY firmness.Api/firmness.Api.csproj firmness.Api/
COPY firmness.Application/firmness.Application.csproj firmness.Application/
COPY firmness.Domain/firmness.Domain.csproj firmness.Domain/
COPY firmness.Infrastructure/firmness.Infrastructure.csproj firmness.Infrastructure/

# Restaurar dependencias de la solución .
RUN dotnet restore firmness.Api/firmness.Api.csproj

# Copiar el resto del código
COPY . .

# Publicar la API
WORKDIR /src/firmness.Api
RUN dotnet publish -c Release -o /app/publish --no-restore /p:UseAppHost=false /p:PublishReadyToRun=false

# 2️⃣ Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "firmness.Api.dll"]