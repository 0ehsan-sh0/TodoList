using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;

namespace TodoList.Database.Repositories
{
    public class TodoRepository(DapperUtility dapperUtility, ICategoryRepository categoryRepository) : ITodoRepository
    {
        public async Task<List<TodoWithCName>> GetAllAsync()
        {
            string sql = "Todo_Get_All";
            using var connection = dapperUtility.GetConnection();
            var result = await connection.QueryAsync<TodoWithCName>(sql, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task<TodoWithCName?> GetByIdAsync(int id)
        {
            string sql = "Todo_Get_One";
            using var connection = dapperUtility.GetConnection();
            var result = await connection.QueryFirstOrDefaultAsync<TodoWithCName>(sql, new { id }, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<int> CreateAsync(Todo todo, string username)
        {
            using var connection = dapperUtility.GetConnection();
            var Category = await categoryRepository.GetByIdAsync(todo.Category_id, username);
            if (Category is null) return 0;
            int result = await connection.InsertAsync<Todo>(todo);
            return result;
        }

        public async Task<TodoWithCName?> UpdateAsync(Todo TodoWithId, string username)
        {
            using var connection = dapperUtility.GetConnection();
            var Category = await categoryRepository.GetByIdAsync(TodoWithId.Category_id, username);
            if (Category is null) return null;
            bool result = await connection.UpdateAsync<Todo>(TodoWithId);
            if (result) return await GetByIdAsync(TodoWithId.Id);
            return null;
        }

        public async Task<bool> DeleteAsync(Todo todo, string username)
        {
            var eTodo = await GetByIdAsync(todo.Id);
            if (eTodo is null) return false;

            var Category = await categoryRepository.GetByIdAsync(eTodo.Category_id, username);
            if (Category is null) return false;

            using var connection = dapperUtility.GetConnection();
            bool result = await connection.DeleteAsync<Todo>(todo);
            return result;
        }
    }
}
