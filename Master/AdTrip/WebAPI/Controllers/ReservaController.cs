using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/reserva")]
    public class ReservaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new ReservaManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("{codigo}")]
        public IHttpActionResult Get(int codigo)
        {
            try
            {
                var mng = new ReservaManager();
                var reserva = new Reserva
                {
                    Codigo = codigo
                };

                reserva = mng.RetrieveById(reserva);
                apiResp = new ApiResponse();
                apiResp.Data = reserva;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("misReservas/{idUsuario}")]
        public IHttpActionResult GetReservaByUsuario(string idUsuario)
        {
            try
            {
                var mng = new ReservaManager();
                var reserva = new Reserva
                {
                    IdUsuario = idUsuario
                };
               
                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllById(reserva); ;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("obtenertipos/{codigo}")]
        public IHttpActionResult GetHabitacionesReserva(int codigo)
        {
            try
            {
                var mng = new ReservaManager();
                var reserva = new Reserva
                {
                    Codigo = codigo
                };

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllHabsReserva(reserva); ;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("")]
        [BitacoraFilter]
        public async Task<IHttpActionResult> Post(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                await mng.Create(reserva);
                apiResp = new ApiResponse();
                apiResp.Message = "La reserva se registró con éxito. Se le enviará a su correo electrónico una llave para su ingreso.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("validarhabs")]
        public IHttpActionResult PostValidar(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                mng.ValidarHabitaciones(reserva);
                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPut]
        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Put(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                mng.Update(reserva);
                apiResp = new ApiResponse();
                apiResp.Message = "La información se modificó correctamente.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpDelete]
        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Delete(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                mng.Delete(reserva);
                apiResp = new ApiResponse();
                apiResp.Message = "La reserva se eliminó correctamente.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("valcantpersonas")]
        public IHttpActionResult PostValidarCantPersonas(Reserva reserva)
        {
            try
            {
                var mng = new ReservaManager();
                mng.ValidarCantPersonas(reserva);
                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("obtenerInfoHabs/{idReserva}")]
        public IHttpActionResult GetObtenerCodigosHabitacion(int idReserva)
        {
            try
            {
                var mng = new ReservaManager();

                var reserva = new Reserva
                {
                    Codigo = idReserva
                };

                apiResp = new ApiResponse();
                apiResp.Data = mng.ObtenerInfoHabitacionesReserva(reserva);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("GetListaReservas/{id}")]
        public IHttpActionResult GetListaReservas(string id)
        {
            var mng = new ReservaManager();
            var reserva = new Reserva()
            {
                IdUsuario = id
            };

            return Ok(mng.RetrieveAllById(reserva));
        }

        [HttpGet]
        [Route("GetReservasList")]
        public IHttpActionResult GetReservasList()
        {
            var mng = new ReservaManager();
            return Ok(mng.RetrieveAll());

        }
    }
}