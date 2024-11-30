using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
using FinalСertificationRecipeBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalСertificationRecipeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // Метод для получения всех рецептов
        // GET: api/Recipe
        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            IEnumerable<Recipe> recipes = await _recipeRepository.GetAllRecipesAsync();

            return Ok(recipes);
        }

        // Метод для получения рецепта по ID 
        // GET: api/Recipe/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(int id)
        {
            // Ищем рецепт по ID и включаем ингредиенты
            Recipe? recipe = await _recipeRepository.GetRecipeByIdAsync(id);

            // Если рецепт не найден, возвращаем статус 404
            if (recipe == null)
                return NotFound();

            // Если рецепт найден, возвращаем его со статусом 200
            return Ok(recipe);
        }

        // Метод для добавления нового рецепта
        // POST: api/Recipe
        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _recipeRepository.AddRecipeAsync(recipe);

            return CreatedAtAction(nameof(GetRecipeById), new {id = recipe.Id, recipe});
        }

        // Метод для обновления существующего рецепта
        // PUT: api/Recipe/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] Recipe recipe)
        {
            
            if (id != recipe.Id)
                return BadRequest("Recipe ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Recipe existingRecipe = await _recipeRepository.GetRecipeByIdAsync(id);

            if (existingRecipe == null)
                return NotFound();

            await _recipeRepository.UpdateRecipeAsync(recipe);

            return NoContent();

        }

        // Метод для удаления рецепта по ID 
        // DELETE: api/Recipe/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            // Асинхронный поиск рецепта по ID
            Recipe? recipe = await _recipeRepository.GetRecipeByIdAsync(id);

            // Если рецепт не найден, возвращаем статус 404 (Not Found)
            if (recipe == null)
                return NotFound();

            await _recipeRepository.DeleteRecipeAsync(id);
            return NoContent();
        }



    }
}
