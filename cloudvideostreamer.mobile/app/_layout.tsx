// app/_layout.tsx
import TabNavigator from '@/components/Navigation/TabNavigator';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { StatusBar } from 'react-native';
import { SafeAreaProvider } from 'react-native-safe-area-context';

const Tab = createBottomTabNavigator();

export default function RootLayout() {
  return (
    <>
    <StatusBar barStyle="dark-content" backgroundColor="#fff"></StatusBar>
    <SafeAreaProvider>
      <TabNavigator/>
    </SafeAreaProvider>
    </>
  );
}
