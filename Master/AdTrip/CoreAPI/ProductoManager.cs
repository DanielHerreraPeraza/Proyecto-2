using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class ProductoManager : BaseManager
    {
        private ProductoCrudFactory crudProducto;

        public ProductoManager()
        {
            crudProducto = new ProductoCrudFactory();
        }

        public void Create(Producto producto)
        {
            try
            {
                var p = crudProducto.Retrieve<Producto>(producto);

                if (p != null)
                {
                    throw new BussinessException(17);
                }
                else
                {
                    crudProducto.Create(producto);
                }



            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Producto> RetrieveAll()
        {
            return crudProducto.RetrieveAll<Producto>();
        }

        public List<Producto> RetrieveByHotelId(string IdHotel)
        {
            return crudProducto.RetrieveByHotelId<Producto>(IdHotel);
        }

        public Producto RetrieveById(Producto producto)
        {
            Producto p = null;
            try
            {
                p = crudProducto.Retrieve<Producto>(producto);
                if (p == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return p;
        }


        public List<Producto> RetrieveAllById(Producto producto)
        {

            return crudProducto.RetrieveAllById<Producto>(producto);
        }


        public void Update(Producto producto)
        {
            crudProducto.Update(producto);
        }

        public void Delete(Producto producto)
        {
            crudProducto.Delete(producto);
        }
    }
}
