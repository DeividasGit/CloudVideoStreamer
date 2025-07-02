import { Outlet } from "react-router-dom";
import Nav from "../Navigation/Nav";
import UserMenu from "../Menu/UserMenu";
import { useAuth } from "../../../hooks/useAuth"
import { authService } from "../../../services/auth/authService";
import { useState } from "react";

export default function Layout() {
    const {user, login, logout} = useAuth();
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const handleLogout = async (e) => {
        e.preventDefault();
        setError(null);
        setLoading(true);
        try {
            await authService.logout(user.id);
            logout();
            navigate("/login");
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };
    
    return (
        <div className="flex min-h-screen w-full">
            <Nav></Nav>
            <div className="flex-1 flex flex-col">
                <header className="flex justify-end items-center p-4 border-b border-gray-300">
                    <UserMenu user={user} onLogout={handleLogout} />
                </header>
                <main className="flex-1 flex flex-col p-6 bg-transparent">
                    <Outlet></Outlet>
                </main>
            </div>
        </div>
    )
}