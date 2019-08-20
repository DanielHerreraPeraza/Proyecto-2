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
    public class CuentaManager : BaseManager
    {

        private CuentaCrudFactory crudCuenta;

        public CuentaManager()
        {
            crudCuenta = new CuentaCrudFactory();
        }




        public void Create(Cuenta cuenta)
        {
            try
            {
                crudCuenta.Create(cuenta);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }


        public void GenerarGanancia(Cuenta cuenta)
        {
            try
            {
                crudCuenta.CreateGanancia(cuenta);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }



        public void PagarMembresia (Cuenta cuenta)
        {
            try
            {

                 crudCuenta.PagarMembresia(cuenta);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }


        public void Update(Cuenta cuenta)
        {
            try
            {
                crudCuenta.Update(cuenta);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void PagarReserva(Cuenta cuenta)
        {
            try
            {


                crudCuenta.PagarReserva(cuenta);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

    }

}
