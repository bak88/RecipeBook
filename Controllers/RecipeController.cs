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

        // Метод для обновления существующего рецепта
        // PUT: api/Recipe/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            // Проверка на соответствие ID в параметре и теле запроса
            if (id != recipe.Id)
                return BadRequest("Recipe ID mismatch.");

            // Помечаем сущность как измененную
            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Проверка, существует ли рецепт с заданным ID
                if (!_context.Recipes.Any(r => r.Id == id))
                    return NotFound();

                // Повторное выбрасывание исключения
                throw;
            }

            // Возвращаем статус 204 (No Content) в случае успешного обновления
            return NoContent();
        }

        // Метод для удаления рецепта по ID 
        // DELETE: api/Recipe/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            // Асинхронный поиск рецепта по ID
            Recipe? recipe = await _context.Recipes.FindAsync(id);

            // Если рецепт не найден, возвращаем статус 404 (Not Found)
            if (recipe == null)
                return NotFound();

            // Удаляем найденный рецепт из контекста базы данных
            _context.Recipes.Remove(recipe);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            // Возвращаем статус 204 (No Content) в случае успешного удаления
            return NoContent();
        }



    }
}
