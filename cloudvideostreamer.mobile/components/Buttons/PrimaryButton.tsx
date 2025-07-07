// components/PrimaryButton.tsx
import { StyleSheet, Text, TouchableOpacity, ViewStyle } from 'react-native';

interface PrimaryButtonProps {
  title: string;
  onPress: () => void;
  style?: ViewStyle;
}

export default function PrimaryButton({ title, onPress, style }: PrimaryButtonProps) {
  return (
    <TouchableOpacity style={[styles.button, style]} onPress={onPress}>
      <Text style={styles.text}>{title}</Text>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
  button: {
    backgroundColor: '#28a745',
    padding: 14,
    borderRadius: 10,
    alignItems: 'center',
    margin: 10
  },
  text: {
    color: '#fff',
    fontSize: 16,
    fontWeight: "bold"
  },
});
