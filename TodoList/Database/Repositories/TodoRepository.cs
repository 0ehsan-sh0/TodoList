using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;

namespace TodoList.Database.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DapperUtility _dapperUtility;
        private readonly ICategoryRepository _categoryRepository;
        public TodoRepository(DapperUtility dapperUtility, ICategoryRepository categoryRepository)
        {
            _dapperUtility = dapperUtility;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<TodoWithCName>> GetAllAsync()
        {
            string sql = "Todo_Get_All";
            using (var connection = _dapperUtility.GetConnection())
            {
                var result = await connection.QueryAsync<TodoWithCName>(sql, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<TodoWithCName?> GetByIdAsync(int id)
        {
            string sql = "Todo_Get_One";
            using (var connection = _dapperUtility.GetConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<TodoWithCName>(sql, new { id = id }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> CreateAsync(Todo todo)
        {
            using (var connection = _dapperUtility.GetConnection())
            {
                var Category = await _categoryRepository.GetByIdAsync(todo.category_id);
                if (Category == null) return 0;
                int result = await connection.InsertAsync<Todo>(todo);
                return result;
            }
        }

        public async Task<TodoWithCName?> UpdateAsync(Todo TodoWithId)
        {
            using (var connection = _dapperUtility.GetConnection())
            {
                var Category = await _categoryRepository.GetByIdAsync(TodoWithId.category_id);
                if (Category == null) return null;
                bool result = await connection.UpdateAsync<Todo>(TodoWithId);
                if (result) return await GetByIdAsync(TodoWithId.id);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Todo todo)
        {
            using (var connection = _dapperUtility.GetConnection())
            {
                bool result = await connection.DeleteAsync<Todo>(todo);
                return result;
            }
        }
    }
}
