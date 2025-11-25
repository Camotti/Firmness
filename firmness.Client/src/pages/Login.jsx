import {useState} from "react";
import {loginRequest} from "../api/auth";
import {useNavigate} from "react-router-dom";

export default function Login(){
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    
    const handleSubmit = async (error) => {
        error.preventDefault();
        
        const {data} = await loginRequest(email, password)
        localStorage.setItem("token", data.token);
        navigate("/products"); // redirige a products despues de login 
    };
    
    return (
        <form onSubmit={handleSubmit}>
            <h2>Login</h2>
            <input
            type="email"
            placeholder="Email"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            />
            <button>login</button>
        </form>
    );
}