import { useState, useCallback, useEffect } from "react"
import LoginForm from "./form"
import { login } from "../../../services/auth/authService"
import { useNavigate } from "react-router-dom";

export default function Login () {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    
    const handleLogin = async (e) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        try {
            const data = await login(email, password);
            localStorage.setItem("token", data.token);
            navigate("/home");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="flex min-h-screen items-center justify-center bg-neutral-900 p-4">
            <LoginForm handleLogin={handleLogin} setEmail={setEmail} setPassword={setPassword} loading={loading}/>
        </div>
    )
}