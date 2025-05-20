using System;

namespace ConsignacionesApi.Models
{
    public class Consignacion
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal Total { get; set; }
        public decimal Monto_Abonado { get; set; }
        public string Estado_Pago { get; set; }
        public DateTime Fecha { get; set; }
    }
}