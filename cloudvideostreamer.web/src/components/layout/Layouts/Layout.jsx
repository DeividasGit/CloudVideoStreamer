import { Outlet } from "react-router-dom";
import Nav from "../Navigation/Nav";

export default function Layout() {
    return (
        <div className="flex min-h-screen w-full">
            <Nav></Nav>
            <main className="flex-1 flex flex-col p-6 bg-transparent">
                <Outlet></Outlet>
            </main>
        </div>
    )
}