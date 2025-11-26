# 1️⃣ Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar toda la solución
COPY . .

# Restaurar usando el .sln real
RUN dotnet restore firmness.sln

# Publicar la API
RUN dotnet publish firmness.Api/firmness.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# 2️⃣ Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "firmness.Api.dll"]
