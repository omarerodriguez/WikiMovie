using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WikiMovies.Data;
using WikiMovies.Interfaces;
using WikiMovies.Models;

namespace WikiMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository moviRespository;

        public MoviesController(IMovieRepository moviRespository)
        {
            this.moviRespository = moviRespository;
        }
        //READ ALL MOVIES
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await moviRespository.GetAllMovies();
        }
        //READ MOVIE BY ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            try
            {
                var result = await moviRespository.GetMovie(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
           
        }
        //CREATE MOVIE
        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest();
                }
                var result = await moviRespository.AddMovie(movie);
                return CreatedAtAction(nameof(AddMovie), new {Id = result.Id},result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new movie record");
            }

        }
        //UPDATE MOVIE
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Movie>> UpdateMovie(int id, Movie movie)
        {
            try
            {
                if(id != movie.Id)
                {
                    return BadRequest("Movie ID mismatch");
                }
                var result = await moviRespository.UpdateMovie(movie);
                if(result == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }
                return await moviRespository.UpdateMovie(movie);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update Movie record");
            }
        }
        //DELETE MOVIE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            try
            {
                var movieDelete = await moviRespository.GetMovie(id);
                if(movieDelete == null)
                {
                    return NotFound($"Movie with Id = {id} not found");
                }
                await moviRespository.DeleteMovie(id);
                return Ok($"Movie with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Delete movie record");
            }
        }
    }
   
}
