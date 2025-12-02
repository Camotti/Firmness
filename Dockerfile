# 1) Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj y restaurar primero para aprovechar caching
COPY *.sln ./
COPY */*.csproj ./
RUN for proj in $(ls -d */); do echo "restoring $proj"; done
RUN dotnet restore

# Copiar todo y publicar
COPY . .
RUN dotnet publish -c Release -o /app/publish

# 2) Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar archivos publicados
COPY --from=build /app/publish .

# Exponer puerto
EXPOSE 8080

ENTRYPOINT ["dotnet", "firmness.dll"]