# 1️⃣ Imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS testrunner

# 2️⃣ Definir directorio de trabajo dentro del contenedor
WORKDIR /app

# 3️⃣ Copiar toda la solución
COPY . .

# 4️⃣ Restaurar dependencias
RUN dotnet restore

# 5️⃣ Construir solo el proyecto de tests
RUN dotnet build firmness.Tests/firmness.Tests.csproj --configuration Release

# 6️⃣ Ejecutar las pruebas del proyecto de tests
CMD ["dotnet", "test", "firmness.Tests/firmness.Tests.csproj", "--configuration", "Release", "--verbosity", "normal"]
