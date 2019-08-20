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
    public class CalificacionManager : BaseManager
    {


        private CalificacionCrudFactory crudCalificacion;

        public CalificacionManager()
        {
            crudCalificacion = new CalificacionCrudFactory();
        }

        public void Create(Calificacion calificacion)
        {
            try
            {
                var cat = crudCalificacion.Retrieve<Calificacion>(calificacion);

                if (cat != null)
                {
                    //Ya has calificado el hotel de esta reserva
                    throw new BussinessException(50);
                }
                else
                {
                    crudCalificacion.Create(calificacion);
                }



            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }
        public void CreateCalificacion(Calificacion calificacion)
        {
            try
            {

                var cat = crudCalificacion.RetrieveCalificacion<Calificacion>(calificacion);

                if (cat != null)
                {
                    //Ya has calificado al usuario de esta reserva
                    throw new BussinessException(51);
                }
                else
                {
                    crudCalificacion.Create(calificacion);
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }
    }
}
