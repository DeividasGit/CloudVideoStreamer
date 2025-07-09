import PrimaryButton from "@/components/Buttons/PrimaryButton";
import AuthScreenLayout from "@/components/Layout/AuthScreenLayout";
import useAuth from "@/hooks/useAuth";
import { useRouter } from "expo-router";
import { StyleSheet, Text, TextInput } from "react-native";

export default function Login() {
    const router = useRouter();
    const authcontextType = useAuth();

    const handleLogin = () => {
        authcontextType?.login();
        router.replace("/(screens)");
    };

    return (
        <AuthScreenLayout>
            <Text style={styles.text}>Email</Text>
            <TextInput style={styles.input}></TextInput>
            <Text style={styles.text}>Password</Text>
            <TextInput style={styles.input}></TextInput>
            <PrimaryButton title="Login" onPress={handleLogin}/>
            <PrimaryButton title="Don't have an account?" onPress={() => router.navigate("/register")}/>
        </AuthScreenLayout>
    )
}

const styles = StyleSheet.create({
    input: {
        borderWidth: 0.5,
        margin: 10
    },
    text: {
        margin: 10
    }
})