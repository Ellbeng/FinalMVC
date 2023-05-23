using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace MVCWithDapper.DBContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DapperContextConnection");
            providerName = "System.Data.SqlClient";
        }


        public string providerName { get; }


        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
