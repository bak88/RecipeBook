using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
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
        private readonly RecipeBookContext _context;

        public IngredientController(RecipeBookContext context)
        {
            _context = context;
        }

        // GET: api/ingredient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetAllIngredients()
        {
            return await _context.Ingredients.ToListAsync();
        }

        // GET: api/ingredient/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingredient>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            return ingredient;
        }

        // POST: api/ingredient
        [HttpPost]
        public async Task<ActionResult<Ingredient>> PostIngredient(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // PUT: api/ingredient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, Ingredient ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ingredient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return _context.Ingredients.Any(e => e.Id == id);
        }
    }
}

