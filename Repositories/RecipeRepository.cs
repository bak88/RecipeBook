using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalСertificationRecipeBook.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly RecipeBookContext _context;
        public RecipeRepository(RecipeBookContext context) 
        { 
            _context = context; 
        }
        public async Task AddRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Add(recipe); 
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRecipeAsync(int id)
        {
            Recipe? recipe = await _context.Recipes.FindAsync(id); 

            if (recipe != null) 
            { 
                _context.Recipes.Remove(recipe); 
                await _context.SaveChangesAsync(); 
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.FindAsync(id);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe); 
            await _context.SaveChangesAsync();
        }
    }
}
