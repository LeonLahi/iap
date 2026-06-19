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
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;

        public TrackService(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<Result<IEnumerable<TrackDto>>> GetAllAsync()
        {
            var tracks = await _trackRepository.GetAllAsync();
            var dtos = tracks.Select(t => t.ToTrackDto());
            
            return Result<IEnumerable<TrackDto>>.Success(dtos);
        }

        public async Task<Result<TrackDto>> GetByIdAsync(int id)
        {
            var track = await _trackRepository.GetByIdAsync(id);

            if (track is null)
                return Result<TrackDto>.NotFound("Track not found");

            return Result<TrackDto>.Success(track.ToTrackDto());
        }

        public async Task<Result<TrackDto>> CreateAsync(CreateTrackRequestDto trackDto)
        {

            // Check for duplicate track name
            var existing = await _trackRepository
                .GetByTitleAndUserAsync(trackDto.Title, trackDto.UserId);

            if (existing is not null)
                return Result<TrackDto>.Conflict("Track with this title already exists");

            // Get model columns from dto to populate for new object
            var trackModel = trackDto.ToTrackFromCreateDto();
            trackModel.UserId = 1;
            trackModel.UploadedAt = DateTime.UtcNow;
            trackModel.IsDeleted = false;

            // Call repo to create track object
            var created = await _trackRepository.CreateAsync(trackModel);

            return Result<TrackDto>.Success(created.ToTrackDto());
            
        }

        public async Task<TrackDto?> DeleteTrackAsync(int trackId)
        {
            // Get track from repo
            var track = await _trackRepository.GetByIdAsync(trackId);

            // TODO: Set global query to filter out deleted records

            // Call repo to delete track from playlist
            var updated = await _trackRepository.DeleteTrackAsync(track);
            return updated?.ToTrackDto();
            
        }
    }
}