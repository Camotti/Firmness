# 1️⃣ Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo los proyectos necesarios
COPY ../firmness.Domain/ firmness.Domain/
COPY ../firmness.Application/ firmness.Application/
COPY ../firmness.Infrastructure/ firmness.Infrastructure/
COPY ../firmness.Api/ firmness.Api/
COPY ../firmness.sln .

# Restaurar solo los proyectos que sí existen
RUN dotnet restore firmness.Api/firmness.Api.csproj

# Build y publish solo la API
WORKDIR /src/firmness.Api
RUN dotnet publish "firmness.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2️⃣ Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "firmness.Api.dll"]