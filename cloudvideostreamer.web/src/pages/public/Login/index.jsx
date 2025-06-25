import { useState, useCallback, useEffect } from "react"
import LoginForm from "./form"
import { authService } from "../../../services/auth/authService";
import { useNavigate } from "react-router-dom";
import './login.css';

export default function Login () {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    
    const handleLogin = async (e) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        try {
            const data = await authService.login(email, password);
            navigate("/");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="login">
            <LoginForm handleLogin={handleLogin} setEmail={setEmail} setPassword={setPassword} loading={loading}/>
        </div>
    )
}