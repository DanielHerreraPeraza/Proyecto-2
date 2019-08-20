using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/cuenta")]
    public class CuentaController : ApiController
    {


        ApiResponse apiResp = new ApiResponse();


        [Route("")]
        public IHttpActionResult Post(Cuenta cuenta)
        {
            try
            {
                var mng = new CuentaManager();
                mng.Create(cuenta);
                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("generarGanancia")]
        public IHttpActionResult PostGanancia(Cuenta cuenta)
        {
            try
            {
                var mng = new CuentaManager();
                mng.GenerarGanancia(cuenta);

                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }



        [HttpPost]
        [Route("pagarMembresia")]
        public IHttpActionResult PostPagoMembresia(Cuenta cuenta)
        {
            try
            {
                var mng = new CuentaManager();
                mng.PagarMembresia(cuenta);

                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }


        [HttpPost]
        [Route("pagoReserva")]
        public IHttpActionResult PostPagoReserva(Cuenta cuenta)
        {
            try
            {
                var mng = new CuentaManager();
                mng.PagarReserva(cuenta);

                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-"
                    + bex.AppMessage.Message));
            }
        }




        [Route("actualizarGanancia")]

        public IHttpActionResult Put(Cuenta cuenta)
        {
            try
            {

                var mng = new CuentaManager();
                mng.Update(cuenta);

                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}