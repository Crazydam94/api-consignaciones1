using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ConsignacionesApi.Models;
using ConsignacionesApi.Services;

namespace ConsignacionesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsignacionesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConsignacionesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IEnumerable<Consignacion>> Get()
        {
            var resultado = new List<Consignacion>();

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await con.OpenAsync();
                var cmd = new SqlCommand(@"
                    SELECT 
                        Cons.ID,
                        C.Nombre AS Cliente,
                        Cons.Total,
                        ISNULL((SELECT SUM(Monto) FROM AbonosConsignacion WHERE ConsignacionID = Cons.ID), 0) AS MontoAbonado,
                        Cons.EstadoPago,
                        Cons.Fecha
                    FROM Consignaciones Cons
                    INNER JOIN Clientes C ON Cons.ClienteID = C.ID
                    ORDER BY Cons.Fecha DESC", con);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        resultado.Add(new Consignacion
                        {
                            Id = (int)reader["ID"],
                            Cliente = reader["Cliente"].ToString(),
                            Total = (decimal)reader["Total"],
                            Monto_Abonado = (decimal)reader["MontoAbonado"],
                            Estado_Pago = reader["EstadoPago"].ToString(),
                            Fecha = (DateTime)reader["Fecha"]
                        });
                    }
                }
            }

            return resultado;
        }
    }
}