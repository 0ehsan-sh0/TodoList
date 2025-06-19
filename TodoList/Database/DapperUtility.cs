using Microsoft.Data.SqlClient;

namespace TodoList.Database
{
    public class DapperUtility
    {
        private IConfiguration _configuration;
        public DapperUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("Default"));
        }
    }
}
