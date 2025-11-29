import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../api/axiosInstance";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await api.post("/auth/login", { email, password });
            localStorage.setItem("token", res.data.token);
            navigate("/products");
        } catch (error) {
            alert("Login failed: " + error.response?.data?.message || error.message);
        }
    };

    return (
        <div className="max-w-md mx-auto mt-20 p-6 border rounded shadow">
            <h2 className="text-2xl font-bold mb-4">Login</h2>
            <form onSubmit={handleSubmit} className="flex flex-col gap-4">
                <input
                    type="email"
                    placeholder="Email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                    required
                    className="p-2 border rounded"
                />
                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                    required
                    className="p-2 border rounded"
                />
                <button type="submit" className="bg-green-600 text-white p-2 rounded hover:bg-green-700">
                    Login
                </button>
            </form>
        </div>
    );
}