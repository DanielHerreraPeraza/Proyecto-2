using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    class RolHotelManager : BaseManager
    {
        private RolHotelCrudFactory crudManager;

        public RolHotelManager()
        {
            crudManager = new RolHotelCrudFactory();
        }

        public void Create(RolHotel rolHotel)
        {
            try
            {
                crudManager.Create(rolHotel);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<RolHotel> RetrieveAll()
        {
            return crudManager.RetrieveAll<RolHotel>();
        }

        public List<RolHotel> RetrieveAllById(RolHotel rolHotel)
        {
            return crudManager.RetrieveAllById<RolHotel>(rolHotel);
        }

        public void Delete(RolHotel rolHotel)
        {
            crudManager.Delete(rolHotel);
        }
    }
}
