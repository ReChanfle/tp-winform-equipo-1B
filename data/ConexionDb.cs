using System;
using Microsoft.Data.SqlClient;

namespace data
{
    public class ConexionDb
    {
        private readonly string _connectionString;

        public ConexionDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
