using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoList.Database.Interfaces;
using TodoList.RequestHandler.Mappers;
using TodoList.RequestHandler.QueryObjects;
using TodoList.RequestHandler.Requests.Category;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryRepository categoryRepository) : ControllerBase
    {
        private string GetUsername()
        {
            return User.FindFirstValue(JwtRegisteredClaimNames.Name)!;
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] QCategoryGetAll query)
        {
            var categories = await categoryRepository.GetAllAsync(GetUsername(), query);

            var rCategories = categories.Select(c => c.ToRCategory()).ToList();
            return Ok(rCategories);
        }

        [Authorize(Roles = "user")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, [FromQuery] QCategoryGetOne query)
        {
            var category = await categoryRepository.GetByIdAsync(id, GetUsername());
            if (category is null) return NotFound();

            if (query.Todos)
            {
                var todos = await categoryRepository.GetByIdAsync(id, GetUsername(), query);
                var rTodos = todos.Select(t => t.ToRTodo()).ToList();
                return Ok(rTodos);
            }
            else
                return Ok(category.ToRCategory());
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest createRequestCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var category = createRequestCategory.ToCategory();
            category.Username = GetUsername();
            int id = await categoryRepository.CreateAsync(category);

            return Created($"api/category/{id}", category.ToRCategory());
        }

        [Authorize(Roles = "user")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryRequest UCategory)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCategory = await categoryRepository.GetByIdAsync(id, GetUsername());
            if (existingCategory is null)
                return NotFound();

            var category = await categoryRepository.UpdateAsync(UCategory.ToCategory(id, existingCategory.Username));

            return Ok(category!.ToRCategory());
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCategory = await categoryRepository.GetByIdAsync(id, GetUsername());
            if (existingCategory is null)
                return NotFound();

            var category = await categoryRepository.DeleteAsync(id);
            if (!category) return NotFound();

            return NoContent();
        }
    }
}
