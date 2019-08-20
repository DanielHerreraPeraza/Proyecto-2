using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Calificacion : Entity
    {
        public int Reserva { get; set; }
        public int Valor { get; set; }
        public string Usuario { get; set; }
        public string Hotel { get; set; }


        public Calificacion()
        {

        }

        public Calificacion(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 21)
            {

                int val = 0;
                if (int.TryParse(infoArray[1], out val))
                    Valor = val;
                else
                    throw new Exception("El porcentaje debe ser un número");

                Usuario = infoArray[2];
                Hotel = infoArray[3];

                var idreserva = 0;
                if (Int32.TryParse(infoArray[4], out idreserva))
                    Reserva = idreserva;
                else
                    throw new Exception("El código debe ser un número.");
            }

        }
    }
}