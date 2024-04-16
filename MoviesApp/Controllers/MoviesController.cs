using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.DAL;
using MoviesApp.DTOs.MovieDTOs;
using MoviesApp.Entities;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        public MoviesController(MoviesDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            List<MovieGetDto>movieGetDtos = new List<MovieGetDto>();
            var movies=await _context.Movies.ToListAsync();
            foreach (var movie in movies)
            {
                MovieGetDto movieDto = new MovieGetDto()
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Desc=movie.Desc,
                    GenreId=movie.GenreId,
                    SalePrice=movie.SalePrice,
                };
                movieGetDtos.Add(movieDto);
            }
            return Ok(movieGetDtos);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var currentMovie=await _context.Movies.FindAsync(id);
            if(currentMovie is null) return NotFound();
            MovieGetDto MovieDto = new MovieGetDto()
            {
                Id=currentMovie.Id,
                Name=currentMovie.Name,
                Desc=currentMovie.Desc,
                GenreId = currentMovie.GenreId,
                SalePrice=currentMovie.SalePrice,   
            };
            return Ok(MovieDto);
        }
        [HttpPost("[action]")]
        public async Task <IActionResult> Create(MoviePostDto moviePostDto)
        {
            Movie movie = new Movie()
            {
                Name = moviePostDto.Name,
                Desc = moviePostDto.Desc,
                GenreId = moviePostDto.GenreId,
                IsDeleted=moviePostDto.IsDeleted,
                SalePrice = moviePostDto.SalePrice,
                CostPrice = moviePostDto.CostPrice,
                CreatedDate=DateTime.UtcNow,
                UpdateDate=DateTime.UtcNow,
            };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return Created();
        }
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]MoviePostDto moviePostDto)
        {
            var currentMovie=await _context.Movies.FindAsync(id);
            if(currentMovie is null) return NotFound();
            currentMovie.Name = moviePostDto.Name;
            currentMovie.Desc = moviePostDto.Desc;
            currentMovie.GenreId = moviePostDto.GenreId;
            currentMovie.CostPrice = moviePostDto.CostPrice;    
            currentMovie.SalePrice = moviePostDto.SalePrice;
            currentMovie.IsDeleted = moviePostDto.IsDeleted;
            currentMovie.CreatedDate = DateTime.UtcNow;
            currentMovie.UpdateDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("[action]/{id}")]
        public async Task <IActionResult> Delete([FromRoute]int id)
        {
            var currentMovie= await _context.Movies.FindAsync(id);
            if(currentMovie is null) return NotFound();
            _context.Movies.Remove(currentMovie);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
