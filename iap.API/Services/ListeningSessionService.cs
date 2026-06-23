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

        public ListeningSessionService(IListeningSessionRepository listeningSessionRepository)
        {
            _listeningSessionRepository = listeningSessionRepository;
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
    }
}