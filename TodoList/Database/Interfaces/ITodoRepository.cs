using TodoList.Database.Models;

namespace TodoList.Database.Interfaces
{
    public interface ITodoRepository
    {
        //Task<List<Category>> GetAllAsync();
        //Task<Category?> GetByIdAsync(int id);
        Task<int> CreateAsync(Todo todo);
        //Task<Category?> UpdateAsync(Category categoryWithId);
        //Task<bool> DeleteAsync(int id);
    }
}
