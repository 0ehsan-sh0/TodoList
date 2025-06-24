using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;
using TodoList.Services;

namespace TodoList.Database.Repositories
{
    public class UserRepository(DapperUtility dapperUtility) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(int id)
        {
            string sql = "Get_User_By_Id";
            using var connection = dapperUtility.GetConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { id }, commandType: CommandType.StoredProcedure);
            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            string sql = "Get_User_By_Username";
            using var connection = dapperUtility.GetConnection();
            var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { username }, commandType: CommandType.StoredProcedure);
            return user;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            if (user is null || !PasswordHasher.VerifyPassword(password, user.Password)) return null;
            return user;
        }

        public async Task<User?> RegisterAsync(User user)
        {
            var databaseUser = await GetByUsernameAsync(user.Username);
            if (databaseUser is not null) return null;
            user = new User()
            {
                Username = user.Username,
                Password = PasswordHasher.HashPassword(user.Password),
            };
            using var connection = dapperUtility.GetConnection();
            var result = await connection.InsertAsync<User>(user);
            var createdUser = await GetByIdAsync(result);
            return createdUser;
        }
    }
}
