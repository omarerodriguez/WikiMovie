using Microsoft.EntityFrameworkCore;
using WikiMovies.Data;
using WikiMovies.Interfaces;
using WikiMovies.Models;

namespace WikiMovies.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _AppDbContext;

        public MovieRepository(ApplicationDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }
        //GET ALL MOVIES
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _AppDbContext.Movies.OrderBy(e => e.Name).ToListAsync();
        }
        // GET MOVIE BY ID
        public async Task<Movie> GetMovie(int movieId)
        {
            return await _AppDbContext.Movies.Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == movieId);
        }
        //CREATE MOVIE
        public async Task<Movie> AddMovie(Movie movie)
        {
            if(movie.Category != null)
            {
                _AppDbContext.Entry(movie.Category).State = EntityState.Unchanged;
            }
            var result = await _AppDbContext.Movies.AddAsync(movie);
            await _AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        //UPDATE MOVIE
        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var result = await _AppDbContext.Movies.FirstOrDefaultAsync
                (e => e.Id == movie.Id);
            if (result != null)
            {
                result.Name = movie.Name;
                result.Age = movie.Age;
                result.Director = movie.Director;
                if (movie.CategoryId != 0)
                {
                    result.CategoryId = movie.CategoryId;
                }
                else if (movie.Category != null)
                {
                    result.CategoryId = movie.Category.Id;
                }
                await _AppDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
        //DELETE MOVIE
        public async Task DeleteMovie(int movieId)
        {
            var result = await _AppDbContext.Movies.FirstOrDefaultAsync(
                e=>e.Id==movieId);
            if (result != null)
            {
                _AppDbContext.Movies.Remove(result);
                await _AppDbContext.SaveChangesAsync();
            }
        }
    }
}
