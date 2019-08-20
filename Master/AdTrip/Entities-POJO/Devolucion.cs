using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Devolucion : Entity
    { 
        public int Codigo { get; set; }
        public string Politica { get; set; }
        public double Porciento { get; set; }
        public string Estado { get; set; }
        public string Hotel { get; set; }

        public Devolucion() { }

        public Devolucion(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 5)
            {
                var codigo = 0;
                if (Int32.TryParse(infoArray[0], out codigo))
                    Codigo = codigo;
                else
                    throw new Exception("Code must be a number");

                Politica = infoArray[1];

                double porce = 0;
                if (Double.TryParse(infoArray[2], out porce))
                    Porciento = porce;
                else
                    throw new Exception("Code must be a number");
                Estado = infoArray[3];
                Hotel = infoArray[4];
            }
            else
            {
                throw new Exception("All values are require");
            }

        }
    }
}
