using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RolHotel : Entity
    {
        public string IdRol { get; set; }
        public string IdHotel { get; set; }

        public RolHotel()
        {

        }

        public RolHotel(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 5)
            {
                IdRol = infoArray[0];
                IdHotel = infoArray[1];
            }
            else
            {
                throw new Exception("Todos los valores son requeridos");
            }

        }
    }
}
