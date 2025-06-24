using TodoList.Database.Models;
using TodoList.RequestHandler.Requests.Todo;
using TodoList.RequestHandler.Responces.Todo;

namespace TodoList.RequestHandler.Mappers
{
    public static class TodoMapper
    {
        public static Todo ToTodo(this CreateTodoRequest todo)
        {
            return new Todo
            {
                Title = todo.Title,
                Description = todo.Description,
                Category_id = todo.Category_id,
            };
        }

        public static Todo ToTodo(this UpdateTodoRequest todo, int id)
        {
            return new Todo
            {
                Id = id,
                Title = todo.Title,
                Description = todo.Description,
                Category_id = todo.Category_id,
                Is_done = todo.Is_done,
            };
        }

        public static RTodo ToRTodo(this Todo todo)
        {
            return new RTodo
            {
                id = todo.Id,
                title = todo.Title,
                description = todo.Description,
                is_done = todo.Is_done,
                category_id = todo.Category_id,

            };
        }

        public static RTodo ToRTodo(this TodoWithCName todo)
        {
            return new RTodo
            {
                id = todo.Id,
                title = todo.Title,
                description = todo.Description,
                is_done = todo.Is_done,
                category_id = todo.Category_id,
                category_name = todo.Category_name,

            };
        }


    }
}
