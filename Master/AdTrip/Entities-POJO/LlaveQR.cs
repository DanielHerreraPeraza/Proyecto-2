using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LlaveQR : Entity
    {
        public string CodigoQR { get; set; }
        public string ImagenQR { get; set; }
        public string EstadoQR { get; set; }
        public string ValorEstado { get; set; }
        public string IdUsuario { get; set; }//este es el correo
        public int IdReserva { get; set; }

        public LlaveQR() { }

        public LlaveQR(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 7)
            {
                //var co = 0;
                //if (Int32.TryParse(infoArray[0], out co))
                //    Id = co;
                //else
                //    throw new Exception("Valor debe ser int");
                //Fecha = Convert.ToDateTime(infoArray[1]);
                //TipoAction = infoArray[2];
                //Controller = infoArray[3];
                //RolUsuario = infoArray[4];
                //CorreoUsuario = infoArray[5];
                //IdHotel = infoArray[6];

            }
            else
            {
                throw new Exception("Falatan datos");
            }

        }

    }
}