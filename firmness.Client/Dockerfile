# 1️⃣ Build stage (Node)
FROM node:18 AS build
WORKDIR /app

# Copiar dependencias
COPY package*.json ./

# Instalar dependencias
RUN npm install

# Copiar todo el código del cliente
COPY . .

# Compilar la app para producción
RUN npm run build

# 2️⃣ Production stage (Nginx)
FROM nginx:alpine AS production

# Copiar el build generado por React
COPY --from=build /app/dist /usr/share/nginx/html

# Exponer el puerto
EXPOSE 80

# Arrancar NGINX
CMD ["nginx", "-g", "daemon off;"]
