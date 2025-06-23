using TodoList.Database.Models;
using TodoList.RequestHandler.QueryObjects;

namespace TodoList.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(string username);
        Task<Category?> GetByIdAsync(int id, string username);
        Task<List<Todo>> GetByIdAsync(int id, string username, QCategoryGetOne query);
        Task<int> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category categoryWithIdAndUsername);
        Task<bool> DeleteAsync(int id);
    }
}
