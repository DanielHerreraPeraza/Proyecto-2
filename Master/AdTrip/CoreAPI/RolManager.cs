using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class RolManager : BaseManager
    {
        private RolCrudFactory crudRol;

        public RolManager()
        {
            crudRol = new RolCrudFactory();
        }

        public void Create(Rol rol)
        {
            try
            {
                var c = crudRol.Retrieve<Rol>(rol);

                if (c != null)
                {
                    throw new BussinessException(15);
                }

                crudRol.Create(rol);

                var mng = new Vista_RolManager();
                foreach (string vista in rol.Vistas)
                {
                    var vistaRol = new Vista_Rol
                    {
                        IdVista = vista,
                        IdRol = rol.Codigo
                    };

                    mng.Create(vistaRol);
                }

                var mngRolHotel = new RolHotelManager();
                foreach (string hotel in rol.Hoteles)
                {
                    var rolHotel = new RolHotel
                    {
                        IdRol = rol.Codigo,
                        IdHotel = hotel
                    };

                    mngRolHotel.Create(rolHotel);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Rol> RetrieveAll()
        {
            return crudRol.RetrieveAll<Rol>();
        }

        public Rol RetrieveById(Rol rol)
        {
            Rol c = null;
            try
            {
                c = crudRol.Retrieve<Rol>(rol);
                if (c == null)
                {
                    throw new BussinessException(13);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public List<Rol> RetrieveByGerenteId(String idGerente)
        {
            return crudRol.RetrieveByGerente<Rol>(idGerente);
        }

        public void Update(Rol rol)
        {
            crudRol.Update(rol);
        }

        public void Delete(Rol rol)
        {
            crudRol.Delete(rol);
        }
    }
}
