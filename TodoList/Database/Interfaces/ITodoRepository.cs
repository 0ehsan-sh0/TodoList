using TodoList.Database.Models;

namespace TodoList.Database.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<TodoWithCName>> GetAllAsync();
        Task<TodoWithCName?> GetByIdAsync(int id);
        Task<int> CreateAsync(Todo todo, string username);
        Task<TodoWithCName?> UpdateAsync(Todo categoryWithId, string username);
        Task<bool> DeleteAsync(Todo todo, string username);
    }
}
