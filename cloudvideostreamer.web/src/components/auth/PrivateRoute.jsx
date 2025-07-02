import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../../hooks/useAuth"

export default function PrivateRoute({ children }) {
  //const location = useLocation();
  const {user, login, logout, loading} = useAuth();
  const location = useLocation();

  console.log("user", user)

  //const user = localStorage.getItem("accessToken");

  // if (loading) {
  //   return <div>Loading...</div>;
  // }

  if (!user) {
    // TODO: save current location
    return <Navigate to="/login" state={{ from: location }} replace/>;
  }

  // Logged in, render the protected component
  return children;
}
