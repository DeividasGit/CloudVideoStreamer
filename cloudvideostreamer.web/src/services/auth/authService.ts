// src/services/authService.ts
import BaseService  from "../base/baseService"
import { User } from "../../models/User"
import { UserLoginResponseDto } from "../../models/dto/UserLoginResponseDto";

class AuthService extends BaseService<User> {
  constructor() {
    super("auth/"); // Base path to auth endpoint
  }

  async login(email: string, password: string): Promise<UserLoginResponseDto> {
    const response = await fetch(`${this.baseUrl}login`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.message || "Login failed");
    }

    // Parse JSON **once** and strongly type the result
    const userLoginDto = await response.json() as UserLoginResponseDto;

    // Save token
    localStorage.setItem("token", userLoginDto.token);

    return userLoginDto;
  }

  logout() {
    localStorage.removeItem("token");
  }
}

export const authService = new AuthService();
