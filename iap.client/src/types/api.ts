export interface Track {
    id: number
    title: string
    artist: string | null
    albumName: string | null
    durationSeconds: number
    blobUrl: string
    coverArtUrl: string | null
    uploadedAt: string
    updatedAt: string | null
    isDeleted: boolean
    deletedAt: string | null
    originalTitle: string | null
    originalArtist: string | null
    originalAlbumName: string | null
    originalCoverArtUrl: string | null
    // TODO: albumTrackNumber: number | null
}

export interface Playlist {
    id: number
    name: string
    description: string | null
    coverArtUrl: string | null
    isDefault: boolean
    type: 'Album' | 'Mix' | 'Playlist' | 'Folder'
    createdAt: string
    updatedAt: string | null
    isDeleted: boolean
    deletedAt: string | null
    parentId: number | null
    tracks: PlaylistTrack[]
    user: User
}

export interface PlaylistTrack {
    id: number
    title: string
    artist: string | null
    albumName: string | null
    durationSeconds: number
    coverArtUrl: string | null
    originalTitle: string | null
    originalArtist: string | null
    originalAlbumName: string | null
    originalCoverArtUrl: string | null
    order: number
}

export interface User {
    id: number
    username: string
    displayName: string | null
}

export interface AuthResponse {
    token: string
    username: string
    displayName: string | null
    email: string
}