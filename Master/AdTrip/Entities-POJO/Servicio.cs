using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Servicio : Entity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
        public string FotoPerfil { get; set; }
        public string IdHotel { get; set; }

        public Servicio()
        {

        }


        public Servicio(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 4)
            {
                Codigo = infoArray[0];
                Nombre = infoArray[1];
                Descripcion = infoArray[2];
                IdEstado = infoArray[3];
                Estado = infoArray[4];
                FotoPerfil = infoArray[5];
                IdHotel = infoArray[6];
            }
            else
            {
                throw new Exception("Los siguientes datos son requeridos para su registro: Código, Nombre, Descripción");
            }

        }

    }
}