// src/services/authService.ts
import BaseService from "../base/baseService";
import User from "../../models/User"
import { UserLoginResponseDto } from "../../models/dto/UserLoginResponseDto";
import axiosInstance from "../base/axiosInstance";

class AuthService extends BaseService<User> {  
  constructor() {
    super("auth"); // Base path to auth endpoint
  }

  async login(email: string, password: string): Promise<UserLoginResponseDto> {
    const response = await axiosInstance.post<UserLoginResponseDto>("auth/login", { email, password });

    const userLoginDto = response.data;
    localStorage.setItem("accessToken", userLoginDto.token); // Save token or userLoginDto.token depending on your DTO

    return userLoginDto;
  }

  async logout(id: number) {
    const response = await axiosInstance.post(`auth/logout/${id}`);
    
    localStorage.removeItem("accessToken");
  }

  async refresh(id: number): Promise<UserLoginResponseDto> {
    const response = await axiosInstance.post<UserLoginResponseDto>(`auth/refresh/${id}`);

    return response.data;
  }

  async register(name: string, email: string, password: string, confirmPassword: string) {
    const response = await axiosInstance.post<UserLoginResponseDto>("auth/register", {name, email, password, confirmPassword});

    const userLoginDto = response.data;
    localStorage.setItem("accessToken", userLoginDto.token);

    return userLoginDto;
  }
}

export const authService = new AuthService();
