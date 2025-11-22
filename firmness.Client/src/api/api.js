import api from "./axiosConfig.js";

//Auth endpoints 
export const login = async (email, password) => {
    const response = await api.post("/auth/login", null, {
        params: {email, password} // AuthController recibe parametros y no json por eso se usa params
    });
    return response.data;
};

export const register = async (email, password) => {
    const response = await api.post("/auth/register", null, {
        params: {email, password}
    });
};


// products endpoints 

export const getProducts = async () => {
    const response = await api.post("/products");
    return response.data;
};



