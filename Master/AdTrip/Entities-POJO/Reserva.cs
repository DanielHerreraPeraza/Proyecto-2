using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reserva : Entity
    {
        public int Codigo { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Precio { get; set; }
        public string Promocion { get; set; }
        public string IdUsuario { get; set; }
        public string IdHotel { get; set; }
        public string Estado { get; set; }
        public string IdEstado { get; set; } 
        public string [] TipoHabitaciones { get; set; } 
        public int [] CantHabitaciones { get; set; }
        public string NombreTipoHab { get; set; }
        public string TipoHab { get; set; }
        public int CantHab { get; set; }
        public int Respuesta { get; set; }
        public int IdHabitacion { get; set; }
        public int CantPersonas { get; set; }
        public string LlaveQR { get; set; }
        public string idQR { get; set; }
        public int NumFacturacion { get; set; }


        public Reserva()
        {

        }


    }
}
