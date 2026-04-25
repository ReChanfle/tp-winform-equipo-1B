using System;
using System.Data.SqlClient;

namespace infraestructura
{
    public class ConexionDb
    {
        private readonly string _connectionString;

        public ConexionDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            try
            {
                return new SqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo conectar a la base de datos.", ex);
            }
        }
    }
}
