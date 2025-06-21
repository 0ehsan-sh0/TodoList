using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;
using TodoList.RequestHandler.QueryObjects;

namespace TodoList.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperUtility _dapperUtility;
        public CategoryRepository(DapperUtility dapperUtility)
        {
            _dapperUtility = dapperUtility;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            string sql = "Category_Get_All";
            using (var connection = _dapperUtility.GetConnection())
            {
                var result = await connection.QueryAsync<Category>(sql, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }

        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            string sql = "Category_Get_One";
            using (var connection = _dapperUtility.GetConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync<Category>(sql, new { id = id }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<List<Todo>?> GetByIdAsync(int id, QCategoryGetOne query)
        {
            string sql = "Category_Get_Todos";
            using (var connection = _dapperUtility.GetConnection())
            {
                var result = await connection.QueryAsync<Todo>(sql, new { id = id }, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> CreateAsync(Category category)
        {
            using (var connection = _dapperUtility.GetConnection())
            {
                int result = await connection.InsertAsync<Category>(category);
                return result;
            }
        }

        public async Task<Category?> UpdateAsync(Category categoryWithId)
        {
            using (var connection = _dapperUtility.GetConnection())
            {
                bool result = await connection.UpdateAsync<Category>(categoryWithId);
                if (result) return await GetByIdAsync(categoryWithId.id);
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string sql = "Category_Delete";
            using (var connection = _dapperUtility.GetConnection())
            {
                int result = await connection.ExecuteAsync(sql, new { id = id });
                if (result == 1) return true;
                return false;
            }
        }
    }
}
