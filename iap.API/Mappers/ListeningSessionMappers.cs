using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Mappers
{
    public static class ListeningSessionMapper
    {
        public static ListeningSessionDto ToListeningSessionDto(this ListeningSession listeningSession)
        {
            return new ListeningSessionDto
            {
                Id = listeningSession.Id,
                PlayedAt = listeningSession.PlayedAt,
                SecondsListened = listeningSession.SecondsListened,
                UserId = listeningSession.UserId,
                TrackId = listeningSession.TrackId,
                Track = listeningSession.Track.ToTrackDto()
            };
        }
    }
}