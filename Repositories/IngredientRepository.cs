using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalСertificationRecipeBook.Repositories
{
    public class IngredientRepository : IGenericRepository<Ingredient>
    {
        private readonly RecipeBookContext _recipeBookContext;

        public IngredientRepository(RecipeBookContext recipeBookContext)
        {
            _recipeBookContext = recipeBookContext;
        }
        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _recipeBookContext.Ingredients.ToListAsync();
        }
        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await _recipeBookContext.Ingredients.FindAsync(id);
        }

        public async Task AddAsync(Ingredient ingredient)
        {
            _recipeBookContext.Ingredients.Add(ingredient);
            await _recipeBookContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Ingredient ingredient)
        {
            _recipeBookContext.Ingredients.Update(ingredient);
            await _recipeBookContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            Ingredient? ingredient = await _recipeBookContext.Ingredients.FindAsync(id);

            if (ingredient != null)
            {
                _recipeBookContext.Ingredients.Remove(ingredient);
                await _recipeBookContext.SaveChangesAsync();
            }
        }



    }
}
