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
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        // Метод для получения всех категорий 
        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // Метод для получения категории по ID 
        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            Category? category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // Метод для добавления новой категории
        // POST: api/Category
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);

        }

        // Метод для обновления существующей категории
        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest("Category ID mismatch.");

            await _categoryRepository.UpdateCategoryAsync(category); 
            return NoContent();
        }

        // Метод для удаления категории по ID 
        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id); 
            return NoContent();
        }


    }
}
