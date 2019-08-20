using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cuenta : Entity
    {
        public string IdUsuario { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaRealizado { get; set; }
        public string TipoCuenta { get; set; }
        public string TipoUsuario { get; set; }
        public string IdHotel { get; set; }
        public string TipoPago  { get; set; }


        public Cuenta()
        {

        }

    }
}
