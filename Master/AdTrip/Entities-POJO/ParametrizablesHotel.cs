using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ParametrizablesHotel : Entity
    {
        public string IdHotel { get; set; }
        public decimal Comision { get; set; }
        public decimal Porciento { get; set; }
        public string Politica { get; set; }
        public int Dias { get; set; }
        public string Mensaje { get; set; }

        public ParametrizablesHotel() { }

        public ParametrizablesHotel(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 5)
            {
                IdHotel = infoArray[0];
                decimal comi = 0;
                if (decimal.TryParse(infoArray[1], out comi))
                    Comision = comi;
                else
                    throw new Exception("Code must be a number");
                decimal porce = 0;
                if (decimal.TryParse(infoArray[2], out porce))
                    Porciento = porce;
                else
                    throw new Exception("Code must be a number");

                Politica = infoArray[3];

                var days = 0;
                if (Int32.TryParse(infoArray[4], out days))
                    Dias = days;
                else
                    throw new Exception("Code must be a number");
                
                Mensaje = infoArray[5];
            }
            else
            {
                throw new Exception("All values are require");
            }

        }
    }
}
