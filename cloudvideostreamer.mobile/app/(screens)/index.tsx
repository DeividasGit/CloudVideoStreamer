import useMediaContent from "@/hooks/useMediaContent";
import { Text, View } from "react-native";

export default function Home() {
    const { mediaContent, loading, refresh } = useMediaContent();

    if (loading) return <View><Text>Loading media...</Text></View>;

    return (
        <View>
            {mediaContent.map((item) => (
        <Text key={item.id}>{item.title}</Text>
      ))}
        </View>
    )
}