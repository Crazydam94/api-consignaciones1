using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsignacionesApi.Services
{
    public class ConexionBD
    {
        private readonly string _connectionString;

        public ConexionBD(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection AbrirConexion()
        {
            var con = new SqlConnection(_connectionString);
            con.Open();
            return con;
        }
    }
}