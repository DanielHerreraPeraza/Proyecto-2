using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class SolicitudHotelManager : BaseManager
    {
        private SolicitudHotelCrudFactory crudSolicitudHotel;
        private HotelCrudFactory crudHotel;

        public SolicitudHotelManager()
        {
            crudSolicitudHotel = new SolicitudHotelCrudFactory();
            crudHotel = new HotelCrudFactory();
        }

        public void Create(SolicitudHotel solicitudHotel)
        {
            try
            {
                solicitudHotel.Estado = "Pendiente";

                if (solicitudHotel.Cadena == null)
                {
                    solicitudHotel.Cadena = "";
                }

                var c = crudSolicitudHotel.Retrieve<SolicitudHotel>(solicitudHotel);
                if (c != null)
                {
                    //solicitud already exist
                    throw new BussinessException(54);
                }
                else
                {
                    Hotel hotel = new Hotel
                    {
                        CedulaJuridica = solicitudHotel.CedulaJuridica
                    };

                    var d = crudHotel.Retrieve<Hotel>(hotel);
                    if (d != null)
                    {
                        throw new BussinessException(52);
                    }
                    else
                    {
                        crudSolicitudHotel.Create(solicitudHotel);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<SolicitudHotel> RetrieveAll()
        {
            return crudSolicitudHotel.RetrieveAll<SolicitudHotel>();
        }

        public SolicitudHotel RetrieveById(SolicitudHotel solicitudHotel)
        {
            SolicitudHotel c = null;
            try
            {
                c = crudSolicitudHotel.Retrieve<SolicitudHotel>(solicitudHotel);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public async Task UpdateAsync(SolicitudHotel solicitudHotel)
        {
            //valores a modificar
            var estadoCambio = solicitudHotel.Estado;
            decimal membrecia = 0;
            if (solicitudHotel.Membrecia > 0)
            {
                membrecia = solicitudHotel.Membrecia;
            }

            var hotelEnviado = new SolicitudHotel
            {
                CodigoSolicitud = solicitudHotel.CodigoSolicitud
            };



            SolicitudHotel c = null;
            c = crudSolicitudHotel.Retrieve<SolicitudHotel>(hotelEnviado);



            if (c != null)//si la solicitud del id existe quiero que le cambie el estado
            {
                if (estadoCambio.Equals("Aprobada"))
                {
                    if (c.Estado.Equals("Pendiente"))
                    {
                        c.Estado = "Aprobada";
                        c.Membrecia = membrecia;
                        var respuesta = await EnviarCorreoManager.GetInstance().ExecuteCorreoSolicitudAprobada(c.CorreoUsuario, c);
                        
                    }
                }
                else if (estadoCambio.Equals("Rechazada"))
                {
                    if (c.Estado.Equals("Pendiente"))
                    {
                        c.Estado = "Rechazada";
                        c.Membrecia = 0;

                        var respuesta = await EnviarCorreoManager.GetInstance().ExecuteCorreoSolicitudRechazada(c.CorreoUsuario, c);
                    }

                }
                else
                {
                }
                crudSolicitudHotel.Update(c);
            }
            else
            {
                throw new BussinessException(55);
            }
        }

        public void UpdateEstado(string IdSoliYEstado)
        {
            var datos = IdSoliYEstado.Split(',');
            var id = 0;
            int numCodigo = -1;
            if (Int32.TryParse(datos[0], out numCodigo))

                id = numCodigo;
            else
                throw new BussinessException(56);

            var estadoCambio = datos[1];

            var hotelEnviado = new SolicitudHotel
            {
                CodigoSolicitud = id
            };


            SolicitudHotel c = null;
            c = crudSolicitudHotel.Retrieve<SolicitudHotel>(hotelEnviado);


            if (c != null)//si la solicitud del id existe quiero que le cambie el estado
            {
                if (estadoCambio.Equals("Aprobado"))
                {
                    if (c.Estado.Equals("Pendiente"))
                    {
                        c.Estado = "Aprobado";
                    }
                }
                else if (estadoCambio.Equals("Rechazado"))
                {
                    if (c.Estado.Equals("Pendiente"))
                    {
                        c.Estado = "Rechazado";
                    }

                }
                else
                {
                }
                crudSolicitudHotel.Update(c);
            }
            else
            {
                throw new BussinessException(55);
            }

        }
        public void Delete(SolicitudHotel solicitudHotel)
        {
            crudSolicitudHotel.Delete(solicitudHotel);
        }
    }
}