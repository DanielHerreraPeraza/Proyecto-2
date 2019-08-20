using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Foto : Entity
    {
        public string UrlFoto { get; set; }
        public string Entidad { get; set; }
        public string IdEntidad { get; set; }
        public string TipoFoto { get; set; }



        public Foto()
        {

        }


        public Foto(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 4)
            {
                Entidad = infoArray[0];
                IdEntidad = infoArray[1];
                UrlFoto = infoArray[2];
                TipoFoto = infoArray[3];
            }
            else
            {
                throw new Exception("Los siguientes datos son requeridos para su registro: Id del servicio y la url de la foto");
            }

        }
    }
}
