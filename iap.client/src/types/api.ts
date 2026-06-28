export interface Track {
    id: number
    title: string
    artist: string | null
    albumName: string | null
    durationSeconds: number
    blobUrl: string
    coverArtUrl: string | null
    uploadedAt: string
}

export interface Playlist {
    id: number
    name: string
    description: string | null
    type: 'Album' | 'Mix' | 'Playlist' | 'Folder'
    coverArtUrl: string | null
    isDefault: boolean
    parentId: number | null
    tracks: PlaylistTrack[]
}

export interface PlaylistTrack {
    trackId: number
    order: number
    title: string
    artist: string | null
    durationSeconds: number
    coverArtUrl: string | null
}

export interface AuthResponse {
    token: string
    username: string
    displayName: string | null
    email: string
}