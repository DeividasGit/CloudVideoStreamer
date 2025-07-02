import { Loader2 } from "lucide-react"
import './register.css';

export default function RegisterForm({ handleRegister, setName, setEmail, setPassword, setConfirmPassword, loading }) {
    return (
        <form onSubmit={handleRegister}>
            <h2 className="text-2xl font-bold text-white mb-2 text-center">Register</h2>
            <div>
                <label className="block text-neutral-200 mb-1" htmlFor="name">
                    Name
                </label>
                <input id="name" type="text" required onChange={(event) => setName(event.target.value)}
                className="w-full px-3 py-2 rounded-md bg-neutral-700 text-white focus:outline-none focus:ring-2 focus:ring-blue-500"></input>
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
                <label className="block text-neutral-200 mb-1" htmlFor="confirmPassword">
                    Confirm Password
                </label>
                <input id="confirmPassword" type="password" required onChange={(event) => setConfirmPassword(event.target.value)}
                className="w-full px-3 py-2 rounded-md bg-neutral-700 text-white focus:outline-none focus:ring-2 focus:ring-blue-500"></input>
            </div>
            <div>
                <button type="submit" disabled={loading}
                    className="btn-submit"
                    >
                    {loading ? (
                        <Loader2  className="btn-submit-loading"></Loader2 >
                    ) : (
                        "Register"
                    )}
                </button>
            </div>
        </form>
    )
}