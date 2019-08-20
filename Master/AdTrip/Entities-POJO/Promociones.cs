using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Promociones : Entity
    {
        public string Codigo { get; set; }
        public int CantDisponible { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Descuento { get; set; }
        public string TipoPromocion { get; set; }
        public string Estado { get; set; }
        public string IdHotel { get; set; }
        public string ValorEstado { get; set; }
        public string ValorTipoPromocion { get; set; }

        public Promociones()
        {

        }
    }
}
