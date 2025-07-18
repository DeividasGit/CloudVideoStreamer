// src/services/axiosInstance.ts
import axios, { AxiosError } from "axios";
import { UserLoginResponseDto } from "../../models/dto/UserLoginResponseDto";
import { environment } from "../../environments/env";
import { ToastAndroid } from "react-native";
import { navigationRef } from "../../navigation/RootNavigation";

const API_URL = environment.apiUrl;

const axiosInstance = axios.create({
  baseURL: API_URL,
  //withCredentials: true, // include cookies
  timeout: 10_000,
  headers: { "Content-Type": "application/json" },
});

axiosInstance.interceptors.request.use((config) => {
  const token = ""; //localStorage.getItem("accessToken");
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

async function logout() {
  const response = await axiosInstance.post("auth/logout");
  
  //localStorage.removeItem("accessToken");
}

async function refresh(): Promise<UserLoginResponseDto> {
  const response = await axiosInstance.post<UserLoginResponseDto>("auth/refresh");

  return response.data;
}

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error: AxiosError) => {
    if (error.response && error.response.status === 401) {
      try {
        const newTokens = await refresh();
        //localStorage.setItem("accessToken", newTokens.token);

        if (error.config) {
          error.config.headers = error.config.headers || {};
          error.config.headers.Authorization = `Bearer ${newTokens.token}`;
          return axiosInstance(error.config); // retry request
        }
      } catch {
        logout();
        ToastAndroid.show("Your session expired. Please log in again.", ToastAndroid.SHORT);
        window.location.href = "/login";
      }
    }

    // ⚠️ Handle other errors
    if (error.code === "ECONNABORTED") {
      ToastAndroid.show("Request timed out. Check your connection.", ToastAndroid.SHORT);
    } else if (error.response) {
      const errors = error.response.data?.errors;
      if (errors && typeof errors === 'object') {
        const messages = Object.values(errors).flat(); //turns into one-level array
        
      if (Array.isArray(messages)) {
        messages.forEach((msg) => {
          if (typeof msg === 'string') {
            ToastAndroid.show(msg, ToastAndroid.SHORT);
          }
        });
      }

      } else {
        ToastAndroid.show(`Error ${error.response.status}: ${error.response.statusText}`, ToastAndroid.SHORT);
      }
    } else {
      ToastAndroid.show(error.message || "An unknown error occurred", ToastAndroid.SHORT);
    }

    return Promise.reject(error); // propagate so you can catch manually too
  }
);

export default axiosInstance;