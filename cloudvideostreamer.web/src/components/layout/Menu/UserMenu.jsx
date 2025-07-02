import { useState, useRef, useEffect } from "react";
import defaultAvatar from "../../../assets/default-avatar.png"

export default function UserMenu({ user, onLogout }) {
    const [isOpen, setIsOpen] = useState(false);
    const dropdownRef = useRef();
    
    useEffect(() => {
        function handleClickOutside(event) {
        if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
            setIsOpen(false);
        }
        }
        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);

    return (
        <div className="relative" ref={dropdownRef}>
        <button
            onClick={() => setIsOpen(!isOpen)}
            className="flex items-center space-x-2 rounded-full hover:bg-gray-200 p-2"
        >
            <img
            src={defaultAvatar}
            alt="User avatar"
            className="w-8 h-8 rounded-full"
            />
            <span className="hidden md:inline font-medium">{user.name}</span>
            <svg
            className={`w-4 h-4 transition-transform ${isOpen ? "rotate-180" : ""}`}
            fill="none"
            stroke="currentColor"
            strokeWidth={2}
            viewBox="0 0 24 24"
            xmlns="http://www.w3.org/2000/svg"
            >
            <path strokeLinecap="round" strokeLinejoin="round" d="M19 9l-7 7-7-7" />
            </svg>
        </button>

        {isOpen && (
            <div className="absolute right-0 mt-2 w-48 bg-white border rounded shadow-lg z-50">
            <ul>
                <li>
                <button
                    onClick={() => alert("Go to profile")}
                    className="block w-full text-left px-4 py-2 hover:bg-gray-100"
                >
                    Profile
                </button>
                </li>
                <li>
                <button
                    onClick={() => alert("Go to settings")}
                    className="block w-full text-left px-4 py-2 hover:bg-gray-100"
                >
                    Settings
                </button>
                </li>
                <li>
                <button
                    onClick={onLogout}
                    className="block w-full text-left px-4 py-2 text-red-600 hover:bg-red-100"
                >
                    Log Out
                </button>
                </li>
            </ul>
            </div>
        )}
        </div>
    )
}