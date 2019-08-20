using System;

namespace Entities
{
    public class Vista_Rol : Entity
    {
        public string IdVista { get; set; }
        public string IdRol { get; set; }

        public Vista_Rol()
        {

        }

        public Vista_Rol(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 2)
            {
                IdVista = infoArray[0];
                IdRol = infoArray[1];
            }
            else
            {
                throw new Exception("Todos los valores son requeridos");
            }
        }
    }
}
