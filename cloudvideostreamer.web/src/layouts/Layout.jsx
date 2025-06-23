import { Outlet } from "react-router-dom";
import Nav from "../pages/Nav";

export default function Layout() {
    return (
             <div className="flex min-h-screen bg-neutral-100">
                <Nav></Nav>
                <main className="flex-1 p-6">
                    <Outlet></Outlet>
                </main>
             </div>
    )
}