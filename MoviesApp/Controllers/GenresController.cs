using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL;
using MoviesApp.DTOs.GenreDTOs;
using MoviesApp.Entities;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        public GenresController(MoviesDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task <IActionResult> GetAll()
        {
            List<GenreGetDto> genreGetDtos = new List<GenreGetDto>();
            var genres=await _context.Genres.ToListAsync();
            foreach (var genre in genres)
            {
                GenreGetDto genreGetDto = new GenreGetDto() 
                {
                    Id = genre.Id,
                    Name = genre.Name,
                };
                genreGetDtos.Add(genreGetDto);
            }
            return Ok(genreGetDtos);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre is null) return NotFound();
            GenreGetDto GenreGetdto = new GenreGetDto()
            {
                Id = id,
                Name = genre.Name,  
            };
            return Ok(GenreGetdto);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(GenrePostDto genrePostDto)
        {
            Genre genre = new Genre()
            {
                Name = genrePostDto.Name,
                IsDeleted = genrePostDto.IsDeleted,
                CreatedDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return Created();
        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] GenrePostDto genrePostDto)
        {
            var currentGenre = await _context.Genres.FindAsync(id);
            if (currentGenre is null) return NotFound();
            currentGenre.Name = genrePostDto.Name;
            currentGenre.IsDeleted = genrePostDto.IsDeleted;
            await _context.SaveChangesAsync();
            return Ok(currentGenre);
        }
        [HttpDelete("[action]/{id}")]
        public async Task <IActionResult> Delete([FromRoute]int id)
        {
            var currentGenre = await _context.Genres.FindAsync(id);
            if(currentGenre is null) return NotFound(); 
            _context.Genres.Remove(currentGenre);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
