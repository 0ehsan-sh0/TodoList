using TodoList.Database.Models;

namespace TodoList.Database.Interfaces
{
    public interface IUserRepository
    {
        //Task<List<TodoWithCName>> GetAllAsync();
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task<User?> LoginAsync(string username, string password);
        Task<User?> RegisterAsync(User user);
    }
}
