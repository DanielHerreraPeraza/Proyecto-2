using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SolicitudHotel : Entity
    {
        public int CodigoSolicitud { get; set; }
        public string Nombre { get; set; }
        public string CedulaJuridica { get; set; }
        public string EmpresaDuenna { get; set; }
        public string Direccion { get; set; }
        public string Descripcion { get; set; }
        public string Cadena { get; set; }
        public int Clasificacion { get; set; }
        public string Estado { get; set; }
        public string IdUsuario { get; set; }
        public string CorreoUsuario { get; set; }
        public string NombreUsuario  { get; set; }
        public decimal Membrecia { get; set; }

        public SolicitudHotel()
        {

        }

        public SolicitudHotel(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 13)
            {

                var numCodigo = 0;
                if (Int32.TryParse(infoArray[3], out numCodigo))

                    CodigoSolicitud = numCodigo;
                else
                    throw new Exception("Codigo debe ser un numero");

                Nombre = infoArray[1];
                CedulaJuridica = infoArray[2];
                EmpresaDuenna = infoArray[3];
                Direccion = infoArray[4];
                Descripcion = infoArray[5];
                Cadena = infoArray[6];

                var numClasi = 0;
                if (Int32.TryParse(infoArray[7], out numClasi))

                    Clasificacion = numClasi;
                else
                    throw new Exception("La clasificacion debe ser un número");

                Estado = infoArray[8];
                IdUsuario = infoArray[9];
                CorreoUsuario = infoArray[10];
                NombreUsuario = infoArray[11];

                decimal numMem = 0;
                if (decimal.TryParse(infoArray[12], out numMem))

                    Membrecia = numMem;
                else
                    throw new Exception("La mebrecia debe ser un número");

                 


            }
            else
            {
                throw new Exception("Faltan valores");
            }

        }
    }
}
