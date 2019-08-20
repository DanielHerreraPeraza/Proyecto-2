using System;

namespace Entities
{
    public class TipoHabitaciones : Entity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int NumCamas { get; set; }
        public int CupoMax { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; }
        public DateTime HoraCheckIn { get; set; }
        public DateTime HoraCheckOut { get; set; }
        public string IdHotel { get; set; }
        public int NumHabitaciones { get; set; }
        public string ValorEstado { get; set; }

        public string FotoPrincipal { get; set; }

        public TipoHabitaciones()
        {

        }


    }
}
