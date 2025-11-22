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
    }
    
};