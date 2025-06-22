using Microsoft.AspNetCore.Mvc;
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
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var todos = await todoRepository.GetAllAsync();
            var rTodos = todos.Select(t => t.ToRTodo()).ToList();
            return Ok(rTodos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var todo = await todoRepository.GetByIdAsync(id);
            if (todo is null) return NotFound();
            return Ok(todo.ToRTodo());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest CreateTodoRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = CreateTodoRequest.ToTodo();
            int id = await todoRepository.CreateAsync(todo);
            if (id == 0) return BadRequest("دسته بندی مورد نظر یافت نشد.");

            return Created($"api/category/{id}", todo.ToRTodo());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoRequest UTodo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var todo = await todoRepository.UpdateAsync(UTodo.ToTodo(id));
            if (todo is null) return NotFound();

            return Ok(todo.ToRTodo());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await todoRepository.DeleteAsync(new Todo { id = id });
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
