using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Carrito : Entity
    {
        public int IdCarrito { get; set; }
        public int Cantidad { get; set; }
        public string IdProducto { get; set; }
        public int IdReserva { get; set; }
        public string IdLLaveQR { get; set; }
        public string Correo { get; set; }


        public Carrito()
        {

        }
    }
}
