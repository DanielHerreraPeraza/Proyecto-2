using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class VistaManager : BaseManager
    {
        private VistaCrudFactory crudVista;

        public VistaManager()
        {
            crudVista = new VistaCrudFactory();
        }

        public void Create(Vista vista)
        {
            try
            {
                var c = crudVista.Retrieve<Rol>(vista);

                if (c != null)
                {
                    throw new BussinessException(14);
                }

                crudVista.Create(vista);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Vista> RetrieveAll()
        {
            return crudVista.RetrieveAll<Vista>();
        }

        public List<Vista> RetrieveById(Usuario usuario)
        {
            return crudVista.RetrieveAllById<Vista>(usuario);
        }

        public void Update(Vista vista)
        {
            crudVista.Update(vista);
        }

        public void Delete(Vista vista)
        {
            crudVista.Delete(vista);
        }
    }
}
