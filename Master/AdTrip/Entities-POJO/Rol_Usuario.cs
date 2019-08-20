using System;

namespace Entities
{
    public class Rol_Usuario : Entity
    {
        public string IdRol { get; set; }
        public string IdUsuario { get; set; }

        public Rol_Usuario()
        {

        }

        public Rol_Usuario(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 2)
            {
                IdRol = infoArray[0];
                IdUsuario = infoArray[1];
            }
            else
            {
                throw new Exception("Todos los valores son requeridos");
            }

        }
    }
}
