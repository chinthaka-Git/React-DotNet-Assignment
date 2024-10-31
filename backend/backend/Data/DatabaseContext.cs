using System.Data;
using System.Data.SqlClient;

namespace backend.Data
{
    public class DatabaseContext : IDisposable
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            var _connection = new SqlConnection(_configuration.GetConnectionString("ContactDB"));
            if (_connection.State != ConnectionState.Open)
            {

                _connection.Open();
            }
            return _connection;
        }

        public void Dispose()
        {
            var _connection = new SqlConnection(_configuration.GetConnectionString("ContactDB"));
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            _connection.Dispose();
        }
    }
}
