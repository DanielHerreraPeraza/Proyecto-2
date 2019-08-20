using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TipoImpuesto : Entity
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Porcentaje { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }

        public string IdEstado { get; set; } //NUEVO

        public TipoImpuesto() { }

        public TipoImpuesto(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 5)
            {
                var codigo = 0;
                if (Int32.TryParse(infoArray[0], out codigo))
                    Codigo = codigo;
                else
                    throw new Exception("El código debe ser un número.");

                Nombre = infoArray[1];

                decimal porce = 0;
                if (decimal.TryParse(infoArray[2], out porce))
                    Porcentaje = porce;
                else
                    throw new Exception("El porcentaje debe ser un número");
                Descripcion = infoArray[3];
                Estado = infoArray[4];
            }
            else
            {
                throw new Exception("Todos los campos son necesarios.");
            }

        }

    }
}
