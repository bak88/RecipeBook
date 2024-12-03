using FinalСertificationRecipeBook.Data;
using FinalСertificationRecipeBook.Models;
using FinalСertificationRecipeBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalСertificationRecipeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        public CategoryController(IGenericRepository<Category> repository)
        {
            _categoryRepository = repository;
        }

        
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

       
        [HttpPost]
        public async Task<ActionResult> AddCategory(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest("Category ID mismatch.");

            await _categoryRepository.UpdateAsync(category); 
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteAsync(id); 
            return NoContent();
        }


    }
}
