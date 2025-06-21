using TodoList.Database.Models;

namespace TodoList.Database.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<TodoWithCName>> GetAllAsync();
        Task<TodoWithCName?> GetByIdAsync(int id);
        Task<int> CreateAsync(Todo todo);
        Task<TodoWithCName?> UpdateAsync(Todo categoryWithId);
        Task<bool> DeleteAsync(Todo todo);
    }
}
