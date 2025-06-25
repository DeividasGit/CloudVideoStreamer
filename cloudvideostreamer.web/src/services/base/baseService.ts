import { environment } from "../../environments/env";

// src/services/BaseService.ts
export default class BaseService<T> {
  protected baseUrl: string;

  constructor(endpoint: string) {
    this.baseUrl = `${environment.apiUrl}${endpoint}`; 
  }

  async getAll(): Promise<T[]> {
    const response = await fetch(this.baseUrl);
    if (!response.ok) throw new Error(`Error fetching all: ${response.statusText}`);
    return response.json();
  }

  async getById(id: number | string): Promise<T> {
    const response = await fetch(`${this.baseUrl}${id}`);
    if (!response.ok) throw new Error(`Error fetching by id: ${response.statusText}`);
    return response.json();
  }

  async create(model: T): Promise<T> {
    const response = await fetch(this.baseUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(model),
    });
    if (!response.ok) throw new Error(`Error creating: ${response.statusText}`);
    return response.json();
  }

  async update(id: number | string, model: Partial<T>): Promise<T> {
    const response = await fetch(`${this.baseUrl}${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(model),
    });
    if (!response.ok) throw new Error(`Error updating: ${response.statusText}`);
    return response.json();
  }

  async delete(id: number | string): Promise<void> {
    const response = await fetch(`${this.baseUrl}${id}`, { method: "DELETE" });
    if (!response.ok) throw new Error(`Error deleting: ${response.statusText}`);
  }
}
