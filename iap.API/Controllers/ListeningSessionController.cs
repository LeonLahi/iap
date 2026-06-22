using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Data;
using iap.API.Dtos;
using iap.API.Interfaces;
using iap.API.Mappers;
using iap.API.Models;
using iap.API.Common;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iap.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListeningSessionController : ControllerBase
    {
        private readonly IapDbContext _context;
        private readonly IListeningSessionRepository _listeningSessionRepo;
        private readonly IListeningSessionService _listeningSessionService;
        public ListeningSessionController(IapDbContext context, IListeningSessionRepository listeningSessionRepo, IListeningSessionService listeningSessionService)
        {
            _listeningSessionRepo = listeningSessionRepo;
            _context = context;
            _listeningSessionService = listeningSessionService;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var result = await _listeningSessionService.GetAllAsync();
                
            return result.ToActionResult(this);
        }

        [HttpGet("recently-played")]
        public async Task<IActionResult> GetRecentyPlayed()
        {
            var result = await _listeningSessionService.GetRecentlyPlayedAsync();
                
            return result.ToActionResult(this);
        }
    }

}