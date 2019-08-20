using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Hotel : Entity
    {
        public string CedulaJuridica { get; set; }
        public string IdGerente { get; set; }
        public string Nombre { get; set; }
        public int Clasificacion { get; set; }
        public string UbicacionX { get; set; }
        public string UbicacionY { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string Direccion { get; set; }
        public string vProvincia { get; set; }
        public string vCanton { get; set; }
        public string vDistrito { get; set; }
        public string Descripcion { get; set; }
        public string Cadena { get; set; }
        public decimal PromCalificacion { get; set; }
        public string Estado { get; set; }
        public string Correo { get; set; }
        public string Telefonos { get; set; }
        public string URLSitio { get; set; }
        public decimal Membrecia { get; set; }
        public string EstadoMembrecia { get; set; }
        public DateTime FechaFin { get; set; }
        public string Logo { get; set; }
        public string FotoPerfil { get; set; }
        public int NumFacturacion { get; set; }

        public Hotel()
        {

        }

        public Hotel(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 21)
            {
                CedulaJuridica = infoArray[0];
                IdGerente = infoArray[1];
                Nombre = infoArray[2];

                var numClasi = 0;
                if (Int32.TryParse(infoArray[3], out numClasi))

                    Clasificacion = numClasi;
                else
                    throw new Exception("La clasificacion debe ser un número");

                UbicacionX = infoArray[4];
                UbicacionY = infoArray[5];
                Provincia = infoArray[6];
                Canton = infoArray[7];
                Distrito = infoArray[8];
                Direccion = infoArray[9];
                Descripcion = infoArray[10];
                Cadena = infoArray[11];

                decimal numCa = 0;
                if (decimal.TryParse(infoArray[12], out numCa))

                    PromCalificacion = numCa;
                else
                    throw new Exception("La membrecia debe ser un número");
                Estado = infoArray[13];
                Correo = infoArray[14];
                URLSitio = infoArray[15];
                decimal numMem = 0;
                if (decimal.TryParse(infoArray[16], out numMem))

                    Membrecia = numMem;
                else
                    throw new Exception("La membrecia debe ser un número");
                EstadoMembrecia = infoArray[17];
                DateTime fecha = DateTime.Now;
                if (DateTime.TryParse(infoArray[18], out fecha))

                    FechaFin = fecha;
                else
                    throw new Exception("La fecha de fin debe ser un fecha");
                Logo = infoArray[19];
                FotoPerfil = infoArray[20];



            }
            else
            {
                throw new Exception("Faltan valores");
            }

        }
    }
}
