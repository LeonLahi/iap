import { Routes, Route, Navigate } from 'react-router-dom'
import { useAuth } from './context/AuthContext'
import LoginPage from './pages/LoginPage'
import RegisterPage from './pages/RegisterPage'
import TracksPage from './pages/TracksPage'
import ProtectedRoute from './components/ProtectedRoute'

function App() {
    const { isAuthenticated } = useAuth()

    return (
        <Routes>
            <Route
                path="/login"
                element={isAuthenticated ? <Navigate to="/" /> : <LoginPage />}
            />
            <Route
                path="/register"
                element={isAuthenticated ? <Navigate to="/" /> : <RegisterPage />}
            />
            <Route
                path="/"
                element={
                    <ProtectedRoute>
                        <TracksPage />
                    </ProtectedRoute>
                }
            />
        </Routes>
    )
}

export default App