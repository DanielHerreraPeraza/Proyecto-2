using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class TipoHabitacionesManager : BaseManager
    {
        private TipoHabitacionesCrudFactory crudTipoHabitaciones;

        public TipoHabitacionesManager()
        {
            crudTipoHabitaciones = new TipoHabitacionesCrudFactory();
        }

        public void Create(TipoHabitaciones tipoHabitaciones)
        {
            try
            {
                var t = crudTipoHabitaciones.Retrieve<TipoHabitaciones>(tipoHabitaciones);

                if (t != null)
                {
                    throw new BussinessException(70);
                }
                crudTipoHabitaciones.Create(tipoHabitaciones);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<TipoHabitaciones> RetrieveAll()
        {
            return crudTipoHabitaciones.RetrieveAll<TipoHabitaciones>();
        }

        public List<TipoHabitaciones> RetrieveByHotelId(string IdHotel)
        {
            return crudTipoHabitaciones.RetrieveByHotelId<TipoHabitaciones>(IdHotel);
        }

        public TipoHabitaciones RetrieveById(TipoHabitaciones tipoHabitaciones)
        {
            TipoHabitaciones t = null;
            try
            {
                t = crudTipoHabitaciones.Retrieve<TipoHabitaciones>(tipoHabitaciones);
                if (t == null)
                {
                    throw new BussinessException(71);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return t;
        }

        public void Update(TipoHabitaciones tipoHabitaciones)
        {
            crudTipoHabitaciones.Update(tipoHabitaciones);
        }

        public void Delete(TipoHabitaciones tipoHabitaciones)
        {
            crudTipoHabitaciones.Delete(tipoHabitaciones);
        }
    }
}
