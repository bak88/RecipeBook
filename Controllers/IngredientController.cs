using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
using FinalСertificationRecipeBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/ingredient
        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            IEnumerable<Ingredient> ingredients = await _ingredientRepository.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        // GET: api/ingredient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(id);

            if (ingredient == null)
                return NotFound();

            return Ok(ingredient);
        }

        // POST: api/ingredient
        [HttpPost]
        public async Task<IActionResult> AddIngredient(Ingredient ingredient)
        {
            _ingredientRepository.AddIngredientAsync(ingredient);

            return CreatedAtAction(nameof(GetIngredientById), new { id = ingredient.Id }, ingredient);
        }

        // PUT: api/ingredient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return BadRequest();

            await _ingredientRepository.UpdateIngredientAsync(ingredient);
            return NoContent();

        }

        // DELETE: api/ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientRepository.DeleteIngredientByIdAsync(id);
            return NoContent();
        }


    }
}

