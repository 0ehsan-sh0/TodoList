using Microsoft.AspNetCore.Mvc;
using TodoList.Database.Interfaces;
using TodoList.RequestHandler.Mappers;
using TodoList.RequestHandler.QueryObjects;
using TodoList.RequestHandler.Requests.Category;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _repository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            var rCategories = categories.Select(c => c.ToRCategory()).ToList();
            return Ok(rCategories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromQuery] QCategoryGetOne query)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) return NotFound();
            if (query.todos)
            {
                var todos = await _repository.GetByIdAsync(id, query);
                var rTodos = todos.Select(t => t.ToRTodo()).ToList();
                return Ok(rTodos);
            }
            else
            {

                return Ok(category.ToRCategory());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest createRequestCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = createRequestCategory.ToCategory();
            int id = await _repository.CreateAsync(category);

            return Created($"api/category/{id}", category.ToRCategory());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequest UCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _repository.UpdateAsync(UCategory.ToCategory(id));
            if (category is null) return NotFound();

            return Ok(category.ToRCategory());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = await _repository.DeleteAsync(id);
            if (!category) return NotFound();

            return NoContent();
        }
    }
}
