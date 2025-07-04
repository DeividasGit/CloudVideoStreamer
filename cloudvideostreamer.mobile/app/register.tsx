import { useRouter } from "expo-router";
import { Button, StyleSheet, Text, TextInput, View } from "react-native";

export default function Register() {
    const router = useRouter();

    return (
        <View>
            <Text style={styles.text}>Email</Text>
            <TextInput style={styles.input}></TextInput>
            <Text style={styles.text}>Password</Text>
            <TextInput style={styles.input}></TextInput>
            <Text style={styles.text}>Confirm Password</Text>
            <TextInput style={styles.input}></TextInput>
            <Button title="Register" ></Button>
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