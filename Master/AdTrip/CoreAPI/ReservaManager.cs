using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CoreAPI
{
    public class ReservaManager : BaseManager
    {
        private ReservaCrudFactory crudReserva;

        public ReservaManager()
        {
            crudReserva = new ReservaCrudFactory();
        }

        public async Task Create(Reserva reserva)
        {
            try
            {

                FacturaCrudFactory crudFactura = new FacturaCrudFactory();
                LlaveQRManager mngLlave = new LlaveQRManager();
                UsuarioManager mngUsuario = new UsuarioManager();
                HotelCrudFactory crudHotel = new HotelCrudFactory();


                var resReserva = crudReserva.Retrieve<Reserva>(reserva);
                Factura factura = new Factura();


                while (resReserva != null)
                {
                    var guid = Guid.NewGuid();
                    var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
                    reserva.Codigo = int.Parse(justNumbers.Substring(0, 9));
                    resReserva = crudReserva.Retrieve<Reserva>(reserva);
                };

                var resFactura = new Factura();
                do 
                {
                    var guid = Guid.NewGuid();
                    var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());

                    factura.NumFacturacion = int.Parse(justNumbers.Substring(0, 9));
                    resFactura = crudFactura.Retrieve<Factura>(factura);
                } while (resFactura != null);



                int[] cH = reserva.CantHabitaciones;
                int cont = 0;
                var r = new Reserva
                {
                    Codigo = reserva.Codigo,
                    FechaInicio = reserva.FechaInicio,
                    FechaFin = reserva.FechaFin,
                    Precio = reserva.Precio,
                    Promocion = reserva.Promocion,
                    IdUsuario = reserva.IdUsuario,
                    IdHotel = reserva.IdHotel,  
                    CantPersonas = reserva.CantPersonas, 
                    NumFacturacion = factura.NumFacturacion
                };


                crudReserva.Create(r);

               
                cont = 0;

                foreach (string tipoHab in reserva.TipoHabitaciones)
                {
                    var r3 = new Reserva
                    {
                        TipoHab = tipoHab,
                        CantHab = cH[cont],
                        Codigo = reserva.Codigo,
                        IdHotel = reserva.IdHotel,
                        FechaInicio = reserva.FechaInicio,
                        FechaFin = reserva.FechaFin
                    };
                    crudReserva.AsignarHabitacionReserva(r3);
                    cont++;


                }


                Usuario usuarioEncontrado = null;

                var u = new Usuario
                {
                    Identificacion = reserva.IdUsuario
                };

                usuarioEncontrado = mngUsuario.RetrieveById(u);


                LlaveQR llave = new LlaveQR();
                llave.EstadoQR = "1";
                llave.IdReserva = reserva.Codigo;
                llave.IdUsuario = usuarioEncontrado.Correo;

                await mngLlave.CreateYEnviar(llave, usuarioEncontrado);

                var rQR = new LlaveQR
                {
                    IdReserva = reserva.Codigo
                };

                var respuesta = mngLlave.RetrieveAllByReserva(rQR);


                var llaveReserva = new Reserva
                {
                    Codigo = reserva.Codigo,
                    LlaveQR = respuesta[0].ImagenQR
                };

                crudReserva.AsignarQRReservaReserva(llaveReserva);

                               
                //string numeroFactura, string descripcion, ArrayList detalles, Hotel hotel, Usuario cliente




                var detalleFactura = new LineaDetalleFactura
                {
                    Nombre = "Reservación",
                    Precio = (float)reserva.Precio,
                    Cantidad = 1,
                    SubTotal = (float)reserva.Precio,
                    Impuesto = 0,
                    Total = (float)reserva.Precio
                };

                ArrayList detalle = new ArrayList();
                detalle.Add(detalleFactura);


                var h = new Hotel
                {
                    CedulaJuridica = reserva.IdHotel
                };

                Hotel hotel = crudHotel.Retrieve<Hotel>(h) ;


                string numFactura = Convert.ToString(factura.NumFacturacion);

                await PdfManager.Create(numFactura, 
                    "el pago de una reservación en AdTrip", detalle, hotel,
                    (Usuario)usuarioEncontrado);


            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Reserva> RetrieveAll()
        {
            return crudReserva.RetrieveAll<Reserva>();
        }

        public Reserva RetrieveById(Reserva reserva)
        {
            Reserva r = null;
            try
            {
                r = crudReserva.Retrieve<Reserva>(reserva);
                if (r == null)
                {
                    throw new BussinessException(2);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return r;
        }

        public List<Reserva> RetrieveAllById(Reserva reserva)
        {
            return crudReserva.RetrieveAllById<Reserva>(reserva);
        }

        public List<TipoHabitaciones> RetrieveAllHabsReserva(Reserva reserva)
        {

            List<TipoHabitaciones> listaTipoHabitaciones = new List<TipoHabitaciones>();

            HabitacionesCrudFactory habCrudFactory = new HabitacionesCrudFactory();
            TipoHabitacionesCrudFactory tipoCrudFactory = new TipoHabitacionesCrudFactory();

            var listaHabitaciones = crudReserva.RetrieveAllHabsReserva<Reserva>(reserva);

            foreach (var i in crudReserva.RetrieveAllHabsReserva<Reserva>(reserva))
            {
                var codigoHab = i.IdHabitacion;

                var h = new Habitaciones
                {
                    Codigo = codigoHab
                };

                // var habitacionObtenida = habCrudFactory.Retrieve<Habitaciones>(h);

//                var th = new TipoHabitaciones
  //              {
    //                Codigo = habitacionObtenida.IdTipoHab
       //         };


      //        var infoTipo = tipoCrudFactory.Retrieve<TipoHabitaciones>(th);

       //         listaTipoHabitaciones.Add(infoTipo);

            }


            return listaTipoHabitaciones;
        }

        public void ValidarHabitaciones(Reserva reserva)
        {
            try
            {
                int[] cH = reserva.CantHabitaciones;
                int cont = 0;

                foreach (string tipoHab in reserva.TipoHabitaciones)
                {
                    var r1 = new Reserva
                    {
                        TipoHab = tipoHab,
                        CantHab = cH[cont],
                        IdHotel = reserva.IdHotel
                    };
                    cont++;

                    var respuesta = crudReserva.VerificarCantHabXTipo<Reserva>(r1);

                    if (respuesta.Respuesta == 0)
                    {
                        throw new BussinessException(98);
                    }

                }

                cont = 0;
                foreach (string tipoHab in reserva.TipoHabitaciones)
                {
                    var r2 = new Reserva
                    {
                        TipoHab = tipoHab,
                        CantHab = cH[cont],
                        IdHotel = reserva.IdHotel,
                        FechaInicio = reserva.FechaInicio,
                        FechaFin = reserva.FechaFin
                    };
                    cont++;

                    var respuesta = crudReserva.VerificarDisponibilidadHabitaciones<Reserva>(r2);

                    if (respuesta.Respuesta == 0)
                    {
                        throw new BussinessException (99);
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

        }

        public void ValidarCantPersonas(Reserva reserva)
        {
            TipoHabitacionesCrudFactory crudTipos = new TipoHabitacionesCrudFactory();
            try
            {
                int[] cH = reserva.CantHabitaciones;
                int cont = 0;
                double capacidadMaxima = 0;

                foreach (string tipoHab in reserva.TipoHabitaciones)
                {
                    var r1 = new TipoHabitaciones
                    {
                        Codigo = tipoHab
                    };
                    
                    var respuesta = crudTipos.Retrieve<TipoHabitaciones>(r1);
                    capacidadMaxima = capacidadMaxima + Math.Pow(Convert.ToDouble(respuesta.CupoMax), Convert.ToDouble(cH[cont]));
                    cont++;
                }

                if (capacidadMaxima >= Convert.ToDouble(reserva.CantPersonas))
                {
                    
                }
                else
                {
                    throw new BussinessException(97);
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(Reserva reserva)
        {
            crudReserva.Update(reserva);
        }

        public void Delete(Reserva reserva)
        {
            crudReserva.Delete(reserva);
        }

        public List<Reserva> ObtenerInfoHabitacionesReserva(Entity entity)
        {
            return crudReserva.ObtenerCodigoHabitacionesReserva<Reserva>(entity);
        }
    }
}
