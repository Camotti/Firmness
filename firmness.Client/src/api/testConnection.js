import api from "./axiosConfig.js";

export async function testConnection(){
    try {
        const response = await api.get("/Products"); // cualquier endpoint que haya
        console.log("Connection Successful:", response.data);
    } catch (error){
        console.error("Error:", error);
    }
}