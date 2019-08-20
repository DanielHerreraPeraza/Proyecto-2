using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class CarritoManager : BaseManager
    {
        private CarritoCrudFactory crudCarrito;

        public CarritoManager()
        {
            crudCarrito = new CarritoCrudFactory();
        }

        public void Create(Carrito Carrito)
        {
            crudCarrito.Create(Carrito);
            /*
            try
            {
                var c = crudCarrito.Retrieve<Carrito>(Carrito);
               
                if (c != null)
                {
                    throw new BussinessException(76);
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            */
        }

        public List<Carrito> RetrieveAll()
        {
            return crudCarrito.RetrieveAll<Carrito>();
        }

        public List<Carrito> RetrieveByReservaId(Carrito carrito)
        {
            return crudCarrito.RetrieveByReservaId<Carrito>(carrito);
        }

        public Carrito RetrieveById(Carrito Carrito)
        {
            Carrito c = null;
            try
            {
                c = crudCarrito.Retrieve<Carrito>(Carrito);
                if (c == null)
                {
                    throw new BussinessException(77);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public void Update(Carrito carrito)
        {
            crudCarrito.Update(carrito);
        }

        public void Delete(Carrito carrito)
        {
            crudCarrito.Delete(carrito);
        }
    }
}
