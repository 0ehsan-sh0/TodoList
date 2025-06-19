using Microsoft.AspNetCore.Mvc;
using TodoList.Database.Interfaces;
using TodoList.RequestHandler.Mappers;
using TodoList.RequestHandler.Requests;

namespace TodoList.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var rCategories = categories.Select(c => c.ToRCategory()).ToList();
            return Ok(rCategories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) return NoContent();
            return Ok(category.ToRCategory());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest createRequestCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = createRequestCategory.ToCategory();
            int id = await _categoryRepository.CreateAsync(category);

            return Created($"api/category/{id}", category.ToRCategory());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequest UCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryRepository.UpdateAsync(UCategory.ToCategory(id));
            if (category == null) return NotFound();

            return Ok(category.ToRCategory());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _categoryRepository.DeleteAsync(id);
            if (!category) return NotFound();

            return NoContent();
        }
    }
}
