import api from "./axiosInstance";

export const loginRequest = (email, password) => {
    return api.post("/Auth/login", { email, password });
};

export const registerRequest = (userData) => {
    return api.post("/Auth/register", userData);
};