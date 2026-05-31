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

    public class GenreController : ControllerBase
    {
        private readonly IapDbContext _context;
        private readonly IGenreRepository _genreRepo;
        public GenreController(IapDbContext context, IGenreRepository genreRepo)
        {
            _genreRepo = genreRepo;
            _context = context;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreRepo.GetAllAsync();
            var genreDto = genres.Select(g => g.ToGenreDto());

            return Ok(genreDto);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var genre = await _genreRepo.GetByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre.ToGenreDto());
        }
    }

}