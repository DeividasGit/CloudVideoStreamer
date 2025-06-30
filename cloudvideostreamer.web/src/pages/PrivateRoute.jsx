import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "../hooks/useAuth"

export default function PrivateRoute({ children }) {
  //const location = useLocation();
  const {user, login, logout} = useAuth();

  console.log(user);

  //const user = localStorage.getItem("accessToken");

  if (!user) {
    // TODO: save current location
    return <Navigate to="/login" />;
  }

  // Logged in, render the protected component
  return children;
}
