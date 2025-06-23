import React from "react";
import { Navigate } from "react-router-dom";

export default function PrivateRoute({ children }) {
  //const { user } = useAuth(); // Get current user (null if not logged in)
  //const location = useLocation();

  const user = false;

  if (!user) {
    // TODO: save current location
    return <Navigate to="/login" />;
  }

  // Logged in, render the protected component
  return children;
}
