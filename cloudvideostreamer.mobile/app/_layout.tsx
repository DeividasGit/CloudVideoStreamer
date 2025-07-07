import RootStack from "@/components/Layout/RootStack";
import { AuthProvider } from "@/context/authContext";

export default function RootLayout() {  
  return (
    <AuthProvider>
      <RootStack/>
    </AuthProvider>
  );
}
