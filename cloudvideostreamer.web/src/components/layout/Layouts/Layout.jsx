import { Outlet } from "react-router-dom";
import Nav from "../Navigation/Nav";
import UserMenu from "../Menu/UserMenu";
import { useAuth } from "../../../hooks/useAuth"

export default function Layout() {
    const {user, login, logout, loading} = useAuth();
    
    return (
        <div className="flex min-h-screen w-full">
            <Nav></Nav>
            <div className="flex-1 flex flex-col">
                <header className="flex justify-end items-center p-4 border-b border-gray-300">
                    <UserMenu user={user} onLogout={logout} />
                </header>
                <main className="flex-1 flex flex-col p-6 bg-transparent">
                    <Outlet></Outlet>
                </main>
            </div>
        </div>
    )
}