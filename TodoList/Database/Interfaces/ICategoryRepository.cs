using TodoList.Database.Models;

namespace TodoList.Database.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<int> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category categoryWithId);
        Task<bool> DeleteAsync(int id);
    }
}
