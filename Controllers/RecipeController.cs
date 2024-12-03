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
        private readonly IGenericRepository<Recipe> _recipeRepository;
        public RecipeController(IGenericRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
         

        [HttpGet]
        public async Task<ActionResult> GetAllRecipes()
        {
            IEnumerable<Recipe> recipes = await _recipeRepository.GetAllAsync();

            return Ok(recipes);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRecipeById(int id)
        {            
            Recipe? recipe = await _recipeRepository.GetByIdAsync(id);
                        
            if (recipe == null)
                return NotFound();
            
            return Ok(recipe);
        }

        
        [HttpPost]
        public async Task<ActionResult> AddRecipe([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _recipeRepository.AddAsync(recipe);

            return CreatedAtAction(nameof(GetRecipeById), new {id = recipe.Id, recipe});
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipe(int id, [FromBody] Recipe recipe)
        {
            
            if (id != recipe.Id)
                return BadRequest("Recipe ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Recipe existingRecipe = await _recipeRepository.GetByIdAsync(id);

            if (existingRecipe == null)
                return NotFound();

            await _recipeRepository.UpdateAsync(recipe);

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe(int id)
        {            
            Recipe? recipe = await _recipeRepository.GetByIdAsync(id);
                        
            if (recipe == null)
                return NotFound();

            await _recipeRepository.DeleteAsync(id);
            return NoContent();
        }



    }
}
