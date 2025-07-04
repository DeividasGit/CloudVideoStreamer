import { Loader2 } from "lucide-react"
import { useNavigate } from "react-router-dom";
import './login.css';

export default function LoginForm({ handleLogin, setEmail, setPassword, loading }) {
    const navigate = useNavigate();
    
    return (
        <form onSubmit={handleLogin}>
            <h2 className="text-2xl font-bold text-white mb-2 text-center">Login</h2>
            <div>
                <label className="block text-neutral-200 mb-1" htmlFor="email">
                    Email
                </label>
                <input id="email" type="email" required onChange={(event) => setEmail(event.target.value)}
                className="w-full px-3 py-2 rounded-md bg-neutral-700 text-white focus:outline-none focus:ring-2 focus:ring-blue-500"></input>
            </div>
            <div>
                <label className="block text-neutral-200 mb-1" htmlFor="password">
                    Password
                </label>
                <input id="password" type="password" required onChange={(event) => setPassword(event.target.value)}
                className="w-full px-3 py-2 rounded-md bg-neutral-700 text-white focus:outline-none focus:ring-2 focus:ring-blue-500"></input>
            </div>
            <div>
                <button type="submit" disabled={loading}
                    className="btn-submit"
                    >
                    {loading ? (
                        <Loader2  className="btn-submit-loading"></Loader2 >
                    ) : (
                        "Login"
                    )}
                </button>
            </div>
            <div>
                <button className="btn-submit" disabled={loading}
                onClick={() => navigate("/register")}>
                    Don't have an account?
                </button>
            </div>
        </form>
    )
}