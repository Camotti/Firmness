import {useState} from "react";
import {registerRequest} from "../api/auth.js";
import {useNavigate} from "react-router-dom";

export default function Register() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();
  
  const handleSubmit = async (e) => {
      e.preventDefault();
      await registerRequest(email, password);
      navigate("/login"); // redirige a login despues de registro
  }
    return (
      <form>
          <h2>Register</h2>
          <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          />
          
          <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          />
          <button>Register</button>
      </form>  
    );
};