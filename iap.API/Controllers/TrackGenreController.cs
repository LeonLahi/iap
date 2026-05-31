// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using iap.API.Data;
// using iap.API.Dtos;
// using iap.API.Interfaces;
// using iap.API.Mappers;
// using iap.API.Models;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace iap.API.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]

//     public class TrackGenreController : ControllerBase
//     {
//         private readonly IapDbContext _context;
//         private readonly ITrackGenreRepository _trackGenreRepo;
//         public TrackGenreController(IapDbContext context, ITrackGenreRepository trackGenreRepo)
//         {
//             _trackGenreRepo = trackGenreRepo;
//             _context = context;
//         }

//         [HttpGet]

//         public async Task<IActionResult> GetAll()
//         {
//             var trackGenres = await _trackGenreRepo.GetAllAsync();
//             var trackGenreDto = trackGenres.Select(tg => tg.ToTrackGenreDto());

//             return Ok(trackGenreDto);
//         }

//         [HttpGet("{id}")]

//         public async Task<IActionResult> GetById([FromRoute] int id)
//         {
//             var trackGenre = await _trackGenreRepo.GetByIdAsync(id);

//             if (trackGenre == null)
//             {
//                 return NotFound();
//             }

//             return Ok(trackGenre.ToTrackGenreDto());
//         }
//     }

// }