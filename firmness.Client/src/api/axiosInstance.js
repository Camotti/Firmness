import axios from "axios";

const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL, // Esta es la URL desde el .env
});

// Interceptor para agregar el Token automáticamente 
api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default api;