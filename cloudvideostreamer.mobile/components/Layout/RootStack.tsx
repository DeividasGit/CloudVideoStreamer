import useAuth from "@/hooks/useAuth";
import { Stack } from "expo-router";

export default function RootStack() {
  const authcontextType = useAuth();
  const user = authcontextType?.user === null || authcontextType?.user === undefined ? false : authcontextType?.user;
  
  return (
    <Stack>
      <Stack.Protected guard={user}>
        <Stack.Screen name="(screens)" options={{ headerShown: false }}/>
      </Stack.Protected>

      <Stack.Protected guard={!user}>
        <Stack.Screen name="login" options={{ headerShown: false }}/>
        <Stack.Screen name="register" options={{ headerShown: false }}/>
      </Stack.Protected>
    </Stack>
  );
}
