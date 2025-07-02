import { createContext, useState, useEffect } from "react";

export const AuthContext = createContext(null);

export default function AuthProvider({ children }) {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    const login = (userData) => setUser(userData);
    const logout = () => setUser(null);

    const getPayloadFromToken = token => {
        const encodedPayload = token.split('.')[1];
        return JSON.parse(atob(encodedPayload));
    }

  useEffect(() => {
    const token = localStorage.getItem("accessToken");
    if (token) {
      const payload = getPayloadFromToken(token);
      if (payload) {
        // Map payload claims to user object as needed
        const user = {
          id: Number(payload.userid || payload.nameid || payload.sub || 0), // fallback if claim name differs
          name: payload.name || "",
          email: payload.email || "",
          roleName: payload.role || "",
          token: token,
        };

        setUser(user);
      }
    }
    setLoading(false);
  }, []);

    return (
        <AuthContext.Provider value={{ user, login, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
}