using TodoList.Database.Models;
using TodoList.RequestHandler.QueryObjects;

namespace TodoList.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<List<Todo>?> GetByIdAsync(int id, QCategoryGetOne query);
        Task<int> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category categoryWithId);
        Task<bool> DeleteAsync(int id);
    }
}
