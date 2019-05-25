using Dapper;
using DddInPractice.QueryHandlers.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DddInPractice.QueryHandlers
{
    public class SnackTypesQueries : ISnackTypesQueries
    {
        private readonly string _connectionString;

        public SnackTypesQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SnackTypes[]> GetSnackTypes()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<SnackTypes>(
                    "SELECT * FROM dbo.SnakType")).ToArray();
            }
        }
    }
}
