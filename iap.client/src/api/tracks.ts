import client from './client'
import { type Track } from '../types/api'

export const getTracks = async (): Promise<Track[]> => {
    const response = await client.get<Track[]>('/tracks')
    return response.data
}