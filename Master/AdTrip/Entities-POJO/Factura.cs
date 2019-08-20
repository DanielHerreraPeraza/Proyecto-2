using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Factura : Entity
    {
        public int NumFacturacion { get; set; }
        public DateTime FechaFactura { get; set; }
        public string NomPlataforma { get; set; }
        public string NombreCliente { get; set; }
        public string IdCliente { get; set; }
        public string Hotel { get; set; }
        public string CedJuridica { get; set; }
        public string Direccion { get; set; }
        public decimal TotalPagar { get; set; }
        public string Estado { get; set; }
        public string IdUsuario { get; set; }

        public string ValorEstado { get; set; }

        public Factura()
        {

        }
    }

   
}
