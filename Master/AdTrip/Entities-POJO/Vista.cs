using System;

namespace Entities
{
    public class Vista : Entity
    {
        public string Id { get; set; }
        public string Definicion { get; set; }
        public string Grupo { get; set; }

        public Vista()
        {

        }

        public Vista(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 3)
            {
                Id = infoArray[0];
                Definicion = infoArray[1];
                Grupo = infoArray[2];
            }
            else
            {
                throw new Exception("Todos los valores son requeridos");
            }

        }
    }
}
