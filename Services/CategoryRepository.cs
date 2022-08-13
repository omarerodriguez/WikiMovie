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
        //GET ALL CATEGORIES
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _AppDbcontext.Categories.OrderBy(c => c.NameCategory).ToListAsync();
        }
        //GET CATEGORY BY ID
        public async Task<Category> GetCategoryById(int categoryid)
        {
            return await _AppDbcontext.Categories.FirstOrDefaultAsync(c => c.Id == categoryid);
        }
        //CREATE CATEGORY
        public async Task<Category> AddCategory(Category category)
        {
           var categoryAdd = await _AppDbcontext.Categories.AddAsync(category);
            await _AppDbcontext.SaveChangesAsync();
            return categoryAdd.Entity;
        }
        //UPDATE CATEGORY
        public async Task<Category> UpdateCategory(Category category)
        {
            var categoryUpdate = await _AppDbcontext.Categories.FirstOrDefaultAsync(
                e => e.Id == category.Id);
            if(categoryUpdate != null)
            {
                categoryUpdate.NameCategory = category.NameCategory;
                categoryUpdate.Status = category.Status;
            }
            await _AppDbcontext.SaveChangesAsync();
            return categoryUpdate;
        }
        //DELETE CATEGORY
        public async Task DeleteCategory(int categoryid)
        {
            var categoryDelete = await _AppDbcontext.Categories.FirstOrDefaultAsync
                (e => e.Id == categoryid);
            if(categoryDelete != null)
            {
                _AppDbcontext.Categories.Remove(categoryDelete);
                await _AppDbcontext.SaveChangesAsync();
            }
        }
      
    }
}
