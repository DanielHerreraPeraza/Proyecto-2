using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reporte : Entity
    {
        public string IdUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public decimal MontoGanancia { get; set; }
        public string FechaRealizado { get; set; }
        public string TipoPago { get; set; }
        public string LugarGanancia { get; set; }
        public int CantHab { get; set; }
        public string IdHotel { get; set; }
        public string NomHotel { get; set; }
    }
}
