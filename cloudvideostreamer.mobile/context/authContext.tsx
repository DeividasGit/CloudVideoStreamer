// app/context/AuthContext.tsx
import { createContext, ReactNode, useState } from 'react';
// import * as SecureStore from 'expo-secure-store'; // optional for storing token securely

export interface AuthContextType {
  user: boolean
  login: () => void;
}

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [user, setUser] = useState(false);
  
    const login = () => setUser(true);

  return (
    <AuthContext.Provider value={{ user, login }}>
      {children}
    </AuthContext.Provider>
  );
};
