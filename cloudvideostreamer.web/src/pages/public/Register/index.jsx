import { useState } from "react";
import { authService } from "../../../services/auth/authService";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../hooks/useAuth";
import RegisterForm from "./form";

export default function Register() {
    const [name, setName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();
    const {user, login, logout} = useAuth();

    const handleRegister = async (e) => {
        e.preventDefault();
        setError(null);
        setLoading(true);

        try {
            const data = await authService.register(name, email, password, confirmPassword);
            login(data);
            navigate("/");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return (
    <div className="login">
        <RegisterForm handleRegister={handleRegister} setName={setName} setEmail={setEmail} setPassword={setPassword} setConfirmPassword={setConfirmPassword} loading={loading}/>
    </div>
    )
}