using System;
using System.Collections.Generic;

namespace Entities
{
    public class Usuario : Entity
    {
        public string Identificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public decimal Calificacion { get; set; }
        public string ValorEstado { get; set; }
        public string Estado { get; set; }
        public string Contrasenna { get; set; }
        public string[] Roles { get; set; }
        public string ContrasennaNueva { get; set; }

        public Usuario()
        {
            Calificacion = 0;
            SegundoNombre = "";
        }

        public Usuario(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 15)
            {
                Identificacion = infoArray[0];
                PrimerNombre = infoArray[1];
                SegundoNombre = infoArray[2];
                PrimerApellido = infoArray[3];
                SegundoApellido = infoArray[4];
                Provincia = infoArray[5];
                Canton = infoArray[6];
                Distrito = infoArray[7];
                DireccionExacta = infoArray[8];
                Telefono = infoArray[9];
                Correo = infoArray[10];
                decimal calificacion = 0;
                Estado = infoArray[12];
                Contrasenna = infoArray[13];
                
                if(decimal.TryParse(infoArray[11], out calificacion))
                {
                    Calificacion = calificacion;
                }
                else
                {
                    throw new Exception("La calificación debe ser un número");
                }

            }
            else
            {
                throw new Exception("Todos los valores son requeridos");
            }

        }

    }
}
