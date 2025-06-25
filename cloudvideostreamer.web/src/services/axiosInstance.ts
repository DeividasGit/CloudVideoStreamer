// src/services/axiosInstance.ts
import axios, { AxiosError } from "axios";
import toast from "react-hot-toast";
import { environment } from "../environments/env";
import { UserLoginResponseDto } from "../models/dto/UserLoginResponseDto";

const API_URL = environment.apiUrl;

const axiosInstance = axios.create({
  baseURL: API_URL,
  withCredentials: true, // include cookies
  timeout: 10_000,
  headers: { "Content-Type": "application/json" },
});

axiosInstance.interceptors.request.use((config) => {
  const token = localStorage.getItem("accessToken");
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

async function logout() {
  const response = await axiosInstance.post("auth/logout");
  
  localStorage.removeItem("accessToken");
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
        localStorage.setItem("accessToken", newTokens.token);

        if (error.config) {
          error.config.headers = error.config.headers || {};
          error.config.headers.Authorization = `Bearer ${newTokens.token}`;
          return axiosInstance(error.config); // retry request
        }
      } catch {
        logout();
        toast.error("Your session expired. Please log in again.");
        window.location.href = "/login";
      }
    }

    // ⚠️ Handle other errors
    if (error.code === "ECONNABORTED") {
      toast.error("Request timed out. Check your connection.");
    } else if (error.response) {
      toast.error(
        (error.response.data as any)?.message ||
        `Error ${error.response.status}: ${error.response.statusText}`
      );
    } else {
      toast.error(error.message || "An unknown error occurred");
    }

    return Promise.reject(error); // propagate so you can catch manually too
  }
);

export default axiosInstance;
