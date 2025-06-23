using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;
using TodoList.RequestHandler.Mappers;
using TodoList.RequestHandler.Requests.Todo;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController(ITodoRepository todoRepository) : ControllerBase
    {
        private string GetUsername()
        {
            return User.FindFirstValue(JwtRegisteredClaimNames.Name)!;
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var todos = await todoRepository.GetAllAsync();
            var rTodos = todos.Select(t => t.ToRTodo()).ToList();
            return Ok(rTodos);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var todo = await todoRepository.GetByIdAsync(id);
            if (todo is null) return NotFound();
            return Ok(todo.ToRTodo());
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest CreateTodoRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = CreateTodoRequest.ToTodo();
            int id = await todoRepository.CreateAsync(todo, GetUsername());
            if (id == 0) return BadRequest("دسته بندی مورد نظر یافت نشد.");

            return Created($"api/category/{id}", todo.ToRTodo());
        }

        [Authorize(Roles = "user")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoRequest UTodo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = await todoRepository.UpdateAsync(UTodo.ToTodo(id), GetUsername());
            if (todo is null) return NotFound();

            return Ok(todo.ToRTodo());
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await todoRepository.DeleteAsync(new Todo { id = id }, GetUsername());
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
