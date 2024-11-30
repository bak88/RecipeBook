using FinalСertificationRecipeBook.Models;

namespace FinalСertificationRecipeBook.Repositories
{
    public interface IRecipeRepository
    {
        Task<Recipe> GetRecipeByIdAsync(int id); 
        Task<IEnumerable<Recipe>> GetAllRecipesAsync(); 
        Task AddRecipeAsync(Recipe recipe); 
        Task UpdateRecipeAsync(Recipe recipe); 
        Task DeleteRecipeAsync(int id);
    }
}
