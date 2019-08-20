using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Bitacora : Entity
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoAction { get; set; }
        public string Controller { get; set; }
        public string RolUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string IdHotel { get; set; }

        public Bitacora() { }

        public Bitacora(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 7)
            {
                var co = 0;
                if (Int32.TryParse(infoArray[0], out co))
                    Id = co;
                else
                    throw new Exception("Valor debe ser int");
                Fecha = Convert.ToDateTime(infoArray[1]);
                TipoAction = infoArray[2];
                Controller = infoArray[3];
                RolUsuario = infoArray[4];
                CorreoUsuario = infoArray[5];
                IdHotel = infoArray[6];

            }
            else
            {
                throw new Exception("Falatan datos");
            }

        }

    }
}
