import { Link } from "react-router-dom";
import './Nav.css';
import { useState } from "react";
import { SlHome  } from "react-icons/sl";
import { CiCircleInfo } from "react-icons/ci";
import { Menu } from "lucide-react";

export default function Nav(){
    const [isOpen, setIsOpen] = useState(true);

    return (
    <nav
      className={`bg-neutral-800 text-white min-h-screen p-4 flex flex-col transition-all duration-300 bg-gradient-to-r from-neutral-800 to-transparent ${
        isOpen ? "w-48" : "w-20"
      }`}
    >
      {/* Logo and Toggle */}
      <div className="flex items-center justify-between mb-4">
        <span className={`text-xl font-bold ${!isOpen && "hidden"}`}>
          Streamer
        </span>
        <button
            onClick={() => setIsOpen(!isOpen)}
            className="button"
            >
            <Menu className="h-4 w-4" />
        </button>

      </div>

      {/* Navigation Links */}
      <ul className="space-y-2 flex-1">
        <li>
          <Link
            to="/"
            className={`menu-item ${
              isOpen ? "justify-start" : "justify-center"
            }`}
          >
            <SlHome/>
            {isOpen && <span className="ml-2">Home</span>}
          </Link>
        </li>
        <li>
          <Link
            to="/about"
            className={`menu-item ${
              isOpen ? "justify-start" : "justify-center"
            }`}
          >
            <CiCircleInfo/>
            {isOpen && <span className="ml-2">About</span>}
          </Link>
        </li>
      </ul>
    </nav>
  );
}