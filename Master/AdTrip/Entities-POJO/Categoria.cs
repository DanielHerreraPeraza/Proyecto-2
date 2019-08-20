
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categoria : Entity
    {

        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; } //NUEVO
        public Categoria() { }

        public Categoria(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 4)
            {
                var codigo = 0;
                if (Int32.TryParse(infoArray[0], out codigo))
                    Codigo = codigo;
                else
                    throw new Exception("El código debe ser un número.");
                Nombre = infoArray[1];
                Descripcion = infoArray[2];
                Estado = infoArray[3];
                IdEstado = infoArray[4];
            }
            else
            {
                throw new Exception("Todos los campos son requeridos.");
            }

        }



    }
}
