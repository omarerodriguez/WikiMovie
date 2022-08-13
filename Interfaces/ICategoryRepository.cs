using WikiMovies.Models;

namespace WikiMovies.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryid);
    }
}
