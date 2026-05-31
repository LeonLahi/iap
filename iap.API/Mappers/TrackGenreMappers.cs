using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Dtos;
using iap.API.Models;

namespace iap.API.Mappers
{
    public static class TrackGenreMapper
    {
        public static TrackGenreDto ToTrackGenreDto(this TrackGenre trackgenre)
        {
            return new TrackGenreDto
            {
                TrackId = trackgenre.TrackId,
                GenreId = trackgenre.GenreId
            };
        }

        public static TrackGenre ToTrackGenreFromCreateDto(this CreateTrackGenreRequestDto dto)
        {
            return new TrackGenre
            {
                TrackId = dto.TrackId,
                GenreId = dto.GenreId
            };
        }
    }
}