using Microsoft.EntityFrameworkCore;
using WikiMovies.Data;
using WikiMovies.Interfaces;
using WikiMovies.Models;

namespace WikiMovies.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _AppDbcontext;

        public CategoryRepository(ApplicationDbContext appDbcontext)
        {
            _AppDbcontext = appDbcontext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _AppDbcontext.Categories.OrderBy(c=>c.NameCategory).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int categoryid)
        {
            return await _AppDbcontext.Categories.FirstOrDefaultAsync(c=>c.Id == categoryid);
        }
    }
}
