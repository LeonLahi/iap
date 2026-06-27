using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iap.API.Interfaces;
using iap.API.Models;
using iap.API.Data;
using iap.API.Dtos;
using System.Formats.Tar;
using iap.API.Mappers;
using iap.API.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using iap.API.Common;
using iap.API.Repository;

namespace iap.API.Services
{
    public class ListeningSessionService : IListeningSessionService
    {
        private readonly IListeningSessionRepository _listeningSessionRepository;
        private readonly ITrackRepository _trackRepository;

        public ListeningSessionService(IListeningSessionRepository listeningSessionRepository, ITrackRepository trackRepository)
        {
            _listeningSessionRepository = listeningSessionRepository;
            _trackRepository = trackRepository;
        }

        public async Task<Result<IEnumerable<ListeningSessionDto>>> GetAllAsync()
        {
            var listeningSessions = await _listeningSessionRepository.GetAllAsync();
            var dtos = listeningSessions.Select(ls => ls.ToListeningSessionDto());
            
            return Result<IEnumerable<ListeningSessionDto>>.Success(dtos);
        }

        public async Task<Result<ListeningSessionDto>> GetByIdAsync(int id)
        {
            var listeningSession = await _listeningSessionRepository.GetByIdAsync(id);

            if (listeningSession is null)
                return Result<ListeningSessionDto>.NotFound("Listening session not found");

            return Result<ListeningSessionDto>.Success(listeningSession.ToListeningSessionDto());
        }

        public async Task<Result<IEnumerable<ListeningSessionDto>>> GetRecentlyPlayedAsync()
        {
            var listeningSessions = await _listeningSessionRepository.GetRecentlyPlayedAsync();
            var dtos = listeningSessions.Select(ls => ls.ToListeningSessionDto());
            
            return Result<IEnumerable<ListeningSessionDto>>.Success(dtos);
        }

        public async Task<Result<IEnumerable<MostPlayedDto>>> GetMostPlayedAsync()
        {
            var dtos = await _listeningSessionRepository.GetMostPlayedAsync();
            return Result<IEnumerable<MostPlayedDto>>.Success(dtos);
        }

        public async Task<Result<ListeningSessionDto>> CreateAsync(CreateListeningSessionDto dto, int userId)
        {
            var track = await _trackRepository.GetByIdAsync(dto.TrackId);
            if (track is null)
                return Result<ListeningSessionDto>.NotFound("Track not found");

            if (dto.SecondsListened < 30)
                return Result<ListeningSessionDto>
                    .ValidationError("Track must be listened to for at least 30 seconds to record a play.");

            if (dto.SecondsListened > track.DurationSeconds)
                return Result<ListeningSessionDto>
                    .ValidationError("Seconds listened cannot exceed track duration.");

            var session = new ListeningSession
            {
                UserId = userId, // TODO: Change to get userId from JWT token
                PlayedAt = DateTimeOffset.Now,
                SecondsListened = dto.SecondsListened,
                TrackId = dto.TrackId
            };

            // Call repo to create listening session object
            var created = await _listeningSessionRepository.CreateAsync(session);

            var withTrack = await _listeningSessionRepository
                .GetByIdAsync(created.Id);

            return Result<ListeningSessionDto>.Success(withTrack!.ToListeningSessionDto());
            
        }
    }
}