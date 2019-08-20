using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Habitaciones : Entity
    {
        public int Codigo { get; set; }
        public string Estado { get; set; }
        public string IdTipoHab { get; set; }
        public string NombreTipoHab { get; set; }
        public string IdHotel { get; set; }
        public string ValorEstado { get; set; }


        public Habitaciones()
        {

        }
    }
}
