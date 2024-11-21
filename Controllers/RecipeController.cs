using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalСertificationRecipeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeBookContext _context;
        public RecipeController(RecipeBookContext context)
        {
            _context = context;
        }

        // Метод для получения всех рецептов
        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipe()
        {
            List<Recipe> recipes = await _context.Recipes
                                         .Include(r => r.Ingredients)
                                         .ToListAsync();
            return Ok(recipes);
        }

        // Метод для получения рецепта по ID 
        // GET: api/Recipe/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe(int id)
        {
            // Ищем рецепт по ID и включаем ингредиенты
            Recipe? recipe = await _context.Recipes
                                       .Include(r => r.Ingredients)
                                       .FirstOrDefaultAsync(r => r.Id == id);

            // Если рецепт не найден, возвращаем статус 404
            if (recipe == null)
                return NotFound();

            // Если рецепт найден, возвращаем его со статусом 200
            return Ok(recipe);
        }

        // Метод для добавления нового рецепта
        // POST: api/Recipe
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            // Проверка на валидность входных данных
            if (recipe == null)
                return BadRequest("Recipe can't be null");

            // Проверка на существование обязательных полей
            if (string.IsNullOrEmpty(recipe.Name))
                return BadRequest("Recipe name can't be empty");

            try
            {
                // Добавляем новый рецепт в контекст базы данных
                _context.Recipes.Add(recipe);

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving recipe: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

            return CreatedAtAction( nameof(GetRecipe),
                                    new { id = recipe.Id }, 
                                    recipe);
        }

    }
}
