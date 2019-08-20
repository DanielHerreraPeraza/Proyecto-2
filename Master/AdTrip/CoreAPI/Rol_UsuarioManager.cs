using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class Rol_UsuarioManager : BaseManager
    {
        private Rol_UsuarioCrudFactory crudManager;

        public Rol_UsuarioManager()
        {
            crudManager = new Rol_UsuarioCrudFactory();
        }

        public void Create(Rol_Usuario rolUsuario)
        {
            try
            {
                crudManager.Create(rolUsuario);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Rol_Usuario> RetrieveAll()
        {
            return crudManager.RetrieveAll<Rol_Usuario>();
        }

        public List<Rol_Usuario> RetrieveAllById(Rol_Usuario rolUsuario)
        {
            return crudManager.RetrieveAllById<Rol_Usuario>(rolUsuario);
        }

        public void Delete(Rol_Usuario rolUsuario)
        {
            crudManager.Delete(rolUsuario);
        }
    }
}
