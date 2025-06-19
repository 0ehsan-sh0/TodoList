using Dapper.Contrib.Extensions;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;

namespace TodoList.Database.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DapperUtility _dapperUtility;
        public TodoRepository(DapperUtility dapperUtility)
        {
            _dapperUtility = dapperUtility;
        }
        public async Task<int> CreateAsync(Todo todo)
        {
            using (var connection = _dapperUtility.GetConnection())
            {
                int result = await connection.InsertAsync<Todo>(todo);
                return result;
            }
        }
    }
}
