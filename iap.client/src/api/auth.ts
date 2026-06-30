import client from './client'
import { type AuthResponse } from '../types/api'

export interface LoginRequest {
    email: string
    password: string
}

export interface RegisterRequest {
    username: string
    email: string
    password: string
    displayName?: string
}

export const login = async (data: LoginRequest): Promise<AuthResponse> => {
    const response = await client.post<AuthResponse>('/auth/login', data)
    return response.data
}

export const register = async (data: RegisterRequest): Promise<AuthResponse> => {
    const response = await client.post<AuthResponse>('/auth/register', data)
    return response.data
}