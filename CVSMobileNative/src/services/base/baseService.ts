import { environment } from "../../environments/env"; 
import axiosInstance from "./axiosInstance";

// src/services/BaseService.ts
export default class BaseService<T> {
  protected baseUrl: string;

  constructor(endpoint: string) {
    this.baseUrl = `${environment.apiUrl}${endpoint}`; 
  }

  async getAll(): Promise<T[]> {
    console.log(this.baseUrl);
    const response = await axiosInstance.get(this.baseUrl);
    return response.data;
  }

  async getById(id: number | string): Promise<T> {
    const response = await axiosInstance.get(`${this.baseUrl}/${id}`);
    return response.data;
  }

  async create(model: T): Promise<T> {
    const response = await axiosInstance.post(this.baseUrl, model);
    return response.data;
  }

  async update(id: number | string, model: Partial<T>): Promise<T> {
    const response = await axiosInstance.put(`${this.baseUrl}/${id}`, model);
    return response.data;
  }

  async delete(id: number | string): Promise<void> {
    const response = await axiosInstance.delete(`${this.baseUrl}${id}`);
    //return response.data;
  }
}