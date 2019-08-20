using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class PromocionesManager : BaseManager
    {
        private PromocionesCrudFactory crudPromociones;

        public PromocionesManager()
        {
            crudPromociones = new PromocionesCrudFactory();
        }

        public void Create(Promociones promociones)
        {

            crudPromociones.Create(promociones);

            /*      
      try
      {
          var p = crudPromociones.Retrieve<Promociones>(promociones);

          if (p != null)
          {
              throw new BussinessException(74);
          }
      }
      catch (Exception ex)
      {
          ExceptionManager.GetInstance().Process(ex);
      }*/

        }

        public List<Promociones> RetrieveAll()
        {
            return crudPromociones.RetrieveAll<Promociones>();
        }

        public List<Promociones> RetrieveByHotelId(string IdHotel)
        {
            return crudPromociones.RetrieveByHotelId<Promociones>(IdHotel);
        }

        public Promociones RetrieveById(Promociones promociones)
        {
            Promociones p = null;
            try
            {
                p = crudPromociones.Retrieve<Promociones>(promociones);
                if (p == null)
                {
                    throw new BussinessException(75);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return p;
        }

        public void Update(Promociones promociones)
        {
            crudPromociones.Update(promociones);
        }

        public void Delete(Promociones promociones)
        {
            crudPromociones.Delete(promociones);
        }
    }
}
