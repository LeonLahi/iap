import axios from 'axios'

const client = axios.create({
    baseURL: 'http://localhost:5048/api', // your API URL from launchSettings.json
})

// Automatically attach JWT token to every request
client.interceptors.request.use((config) => {
    const token = localStorage.getItem('token')
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

export default client