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
    public class ServicioManager : BaseManager
    {

        private ServicioCrudFactory crudServicio;

        public ServicioManager()
        {
            crudServicio = new ServicioCrudFactory();
        }

        public void Create(Servicio servicio)
        {
            try
            {
                var s = crudServicio.Retrieve<Servicio>(servicio);

                if (s != null)
                {
                    throw new BussinessException(16);
                }
                else
                {
                    crudServicio.Create(servicio);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

       


        public List<Servicio> RetrieveAll()
        {
            return crudServicio.RetrieveAll<Servicio>();
        }

        public Servicio RetrieveById(Servicio servicio)
        {
            Servicio s = null;
            try
            {
                s = crudServicio.Retrieve<Servicio>(servicio);
                if (s == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return s;
        }


        public List<Servicio> RetrieveAllById(Servicio servicio)
        {

            return crudServicio.RetrieveAllById<Servicio>(servicio);
        }


        public void Update(Servicio servicio)
        {
            crudServicio.Update(servicio);
        }

        public void Delete(Servicio servicio)
        {
            crudServicio.Delete(servicio);
        }
    }
}
