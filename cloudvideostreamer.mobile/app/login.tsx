import { useRouter } from "expo-router";
import { Button, StyleSheet, Text, TextInput, View } from "react-native";

export default function Login() {
    const router = useRouter();

    return (
        <View>
            <Text style={styles.text}>Email</Text>
            <TextInput style={styles.input}></TextInput>
            <Text style={styles.text}>Password</Text>
            <TextInput style={styles.input}></TextInput>
            <Button title="Login" ></Button>
            <Button title="Don't have an account?" onPress={() => router.navigate("/register")}></Button>
        </View>
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