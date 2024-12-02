using FinalСertificationRecipeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalСertificationRecipeBook.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }

}
