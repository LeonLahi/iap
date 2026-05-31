using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iap.API.Data;
using iap.API.Dtos;
using iap.API.Interfaces;
using iap.API.Mappers;
using iap.API.Models;
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
        public ListeningSessionController(IapDbContext context, IListeningSessionRepository listeningSessionRepo)
        {
            _listeningSessionRepo = listeningSessionRepo;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var listeningSessions = await _listeningSessionRepo.GetAllAsync();
            var listeningSessionDto = listeningSessions.Select(ls => ls.ToListeningSessionDto());

            return Ok(listeningSessionDto);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var listeningSession = await _listeningSessionRepo.GetByIdAsync(id);

            if (listeningSession == null)
            {
                return NotFound();
            }

            return Ok(listeningSession.ToListeningSessionDto());
        }
    }

}