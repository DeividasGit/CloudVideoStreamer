import PrimaryButton from "@/components/Buttons/PrimaryButton";
import AuthScreenLayout from "@/components/Layout/AuthScreenLayout";
import { useRouter } from "expo-router";
import { StyleSheet, Text, TextInput } from "react-native";

export default function Register() {
    const router = useRouter();

    return (
        <AuthScreenLayout>
            <Text style={styles.text}>Email</Text>
            <TextInput style={styles.input}></TextInput>
            <Text style={styles.text}>Password</Text>
            <TextInput style={styles.input}></TextInput>
            <Text style={styles.text}>Confirm Password</Text>
            <TextInput style={styles.input}></TextInput>
            <PrimaryButton title="Register"/>
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