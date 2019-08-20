using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class HabitacionesManager : BaseManager
    {
        private HabitacionesCrudFactory crudHabitaciones;

        public HabitacionesManager()
        {
            crudHabitaciones = new HabitacionesCrudFactory();
        }

        public void Create(Habitaciones habitaciones)
        {
            crudHabitaciones.Create(habitaciones);
            /*
            try
            {
                var h = crudHabitaciones.Retrieve<Habitaciones>(habitaciones);
               
                if (h != null)
                {
                    throw new BussinessException(72);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            */
        }

        public List<Habitaciones> RetrieveAll()
        {
            return crudHabitaciones.RetrieveAll<Habitaciones>();
        }

        public List<Habitaciones> RetrieveByHotelId(string IdHotel)
        {
            return crudHabitaciones.RetrieveByHotelId<Habitaciones>(IdHotel);
        }

        public Habitaciones RetrieveById(Habitaciones habitaciones)
        {
            Habitaciones h = null;
            try
            {
                h = crudHabitaciones.Retrieve<Habitaciones>(habitaciones);
                if (h == null)
                {
                    throw new BussinessException(73);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return h;
        }

        public void Update(Habitaciones habitaciones)
        {
            crudHabitaciones.Update(habitaciones);
        }

        public void Delete(Habitaciones habitaciones)
        {
            crudHabitaciones.Delete(habitaciones);
        }
    }
}
