import Home from '@/app/(screens)';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import React from 'react';

const Tab = createBottomTabNavigator();

export default function TabNavigator() {
  return (
    <Tab.Navigator
      screenOptions={{
        headerShown: false,
      }}
    >
    <Tab.Screen name="Home" component={Home} />
      {/* Add more tabs here */}
    </Tab.Navigator>
  );
}
