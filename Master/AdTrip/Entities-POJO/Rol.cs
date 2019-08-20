using System;

namespace Entities
{
    public class Rol : Entity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public string ValorEstado { get; set; }
        public string[] Vistas { get; set; }
        public string[] Hoteles { get; set; }

        public Rol()
        {

        }

        public Rol(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 5)
            {
                Codigo = infoArray[0];
                Nombre = infoArray[1];
                Descripcion = infoArray[2];
                Estado = infoArray[3];
                ValorEstado = infoArray[4];
            }
            else
            {
                throw new Exception("Todos los valores son requeridos");
            }

        }
    }
}
