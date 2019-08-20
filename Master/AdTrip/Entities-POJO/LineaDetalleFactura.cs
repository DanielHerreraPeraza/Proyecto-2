using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LineaDetalleFactura
    {
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public float SubTotal { get; set; }
        public float Impuesto { get; set; }
        public float Total { get; set; }
    }
}
