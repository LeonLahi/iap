import { useEffect, useState } from 'react'
import { getTracks } from '../api/tracks'
import { type Track } from '../types/api'

export default function TracksPage() {
    const [tracks, setTracks] = useState<Track[]>([])
    const [loading, setLoading] = useState(true)

    useEffect(() => {
        getTracks()
            .then(setTracks)
            .finally(() => setLoading(false))
    }, [])

    if (loading) return <p>Loading...</p>

    return (
        <div className="p-4">
            {tracks.map(track => (
                <div key={track.id} className="p-2 border-b border-gray-700">
                    <p className="font-bold">{track.title}</p>
                    <p className="text-gray-400">{track.artist}</p>
                </div>
            ))}
        </div>
    )
}