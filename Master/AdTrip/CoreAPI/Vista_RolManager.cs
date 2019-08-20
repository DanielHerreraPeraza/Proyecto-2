using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class Vista_RolManager : BaseManager
    {
        private Vista_RolCrudFactory crudManager;

        public Vista_RolManager()
        {
            crudManager = new Vista_RolCrudFactory();
        }

        public void Create(Vista_Rol vistaRol)
        {
            try
            {
                crudManager.Create(vistaRol);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Vista_Rol> RetrieveAll()
        {
            return crudManager.RetrieveAll<Vista_Rol>();
        }

        public List<Vista_Rol> RetrieveAllVistasByRolId(Vista_Rol vistaRol)
        {
            return crudManager.RetrieveAllById<Vista_Rol>(vistaRol);
        }

        public List<Vista_Rol> RetrieveAllRolesByVistaId(Vista_Rol vistaRol)
        {
            return crudManager.RetrieveAllById<Vista_Rol>(vistaRol);
        }

        public void Delete(Vista_Rol vistaRol)
        {
            crudManager.Delete(vistaRol);
        }

    }
}
