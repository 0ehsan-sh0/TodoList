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
                title = todo.title,
                description = todo.description,
                category_id = todo.category_id,
            };
        }

        public static Todo ToTodo(this UpdateTodoRequest todo, int id)
        {
            return new Todo
            {
                id = id,
                title = todo.title,
                description = todo.description,
                category_id = todo.category_id,
                is_done = todo.is_done,
            };
        }

        public static RTodo ToRTodo(this Todo todo)
        {
            return new RTodo
            {
                id = todo.id,
                title = todo.title,
                description = todo.description,
                is_done = todo.is_done,
                category_id = todo.category_id,

            };
        }

        public static RTodo ToRTodo(this TodoWithCName todo)
        {
            return new RTodo
            {
                id = todo.id,
                title = todo.title,
                description = todo.description,
                is_done = todo.is_done,
                category_id = todo.category_id,
                category_name = todo.category_name,

            };
        }


    }
}
