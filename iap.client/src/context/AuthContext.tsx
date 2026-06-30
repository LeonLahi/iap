import { createContext, useContext, useState, type ReactNode } from 'react'
import { type AuthResponse } from '../types/api'

interface AuthContextType {
    user: AuthResponse | null
    token: string | null
    loginUser: (data: AuthResponse) => void
    logoutUser: () => void
    isAuthenticated: boolean
}

const AuthContext = createContext<AuthContextType | null>(null)

export function AuthProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<AuthResponse | null>(() => {
        const stored = localStorage.getItem('user')
        return stored ? JSON.parse(stored) : null
    })

    const [token, setToken] = useState<string | null>(() => {
        return localStorage.getItem('token')
    })

    const loginUser = (data: AuthResponse) => {
        setUser(data)
        setToken(data.token)
        localStorage.setItem('token', data.token)
        localStorage.setItem('user', JSON.stringify(data))
    }

    const logoutUser = () => {
        setUser(null)
        setToken(null)
        localStorage.removeItem('token')
        localStorage.removeItem('user')
    }

    return (
        <AuthContext.Provider value={{
            user,
            token,
            loginUser,
            logoutUser,
            isAuthenticated: !!token
        }}>
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth() {
    const context = useContext(AuthContext)
    if (!context) throw new Error('useAuth must be used within AuthProvider')
    return context
}