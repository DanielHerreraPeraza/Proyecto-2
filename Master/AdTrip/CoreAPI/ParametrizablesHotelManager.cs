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
    public class ParametrizablesHotelManager : BaseManager
    {
        private ParametrizablesHotelCrudFactory crudParam;


        public ParametrizablesHotelManager()
        {
            crudParam = new ParametrizablesHotelCrudFactory();

        }

        public void Create(ParametrizablesHotel param)
        {
            try
            {

                //por default el hotel tendrá las siguientes politicas:
                //5% de comision por cada item
                param.Comision = 5;
                //El 50% de devolución del costo de la reserva 
                param.Porciento = 50;

                param.Politica = "El hotel permitirá la devolución únicamente del 50% del costo de la reserva de una habitación," +
                    " únicamente si el cliente canceló la reserva en un plazo de al menos siete días antes de la activación de la reserva.";
                param.Dias = 7;


                //este es el mensaje que se mostrará al usuario un día antes de que se active su reservación
                param.Mensaje = @"Bienvenido, le recordamos que usted tiene una reservación para mañana en nuestras instalaciones. Lo esperamos";

                crudParam.Create(param);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<ParametrizablesHotel> RetrieveAll()
        {
            return crudParam.RetrieveAll<ParametrizablesHotel>();
        }

        public ParametrizablesHotel RetrieveById(ParametrizablesHotel param)
        {
            ParametrizablesHotel p = null;
            try
            {
                p = crudParam.Retrieve<ParametrizablesHotel>(param);
                if (p == null)
                {
                    throw new BussinessException(53);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return p;
        }


        public List<ParametrizablesHotel> RetrieveAllById(ParametrizablesHotel param)
        {

            return crudParam.RetrieveAllById<ParametrizablesHotel>(param);
        }


        public void Update(ParametrizablesHotel param)
        {
            crudParam.Update(param);
        }

    }
}
