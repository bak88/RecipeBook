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
        private readonly IGenericRepository<Ingredient> _ingredientRepository;

        public IngredientController(IGenericRepository<Ingredient> ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        
        [HttpGet]
        public async Task<ActionResult> GetAllIngredients()
        {
            IEnumerable<Ingredient> ingredients = await _ingredientRepository.GetAllAsync();
            return Ok(ingredients);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);

            if (ingredient == null)
                return NotFound();

            return Ok(ingredient);
        }

       
        [HttpPost]
        public async Task<ActionResult> AddIngredient(Ingredient ingredient)
        {
            _ingredientRepository.AddAsync(ingredient);

            return CreatedAtAction(nameof(GetIngredientById), new { id = ingredient.Id }, ingredient);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
                return BadRequest();

            await _ingredientRepository.UpdateAsync(ingredient);
            return NoContent();

        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIngredient(int id)
        {
            await _ingredientRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}

