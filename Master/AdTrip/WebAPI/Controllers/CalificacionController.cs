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
    [RoutePrefix("api/calificacion")]
    public class CalificarController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();
        [HttpPost]
        [Route("usuarioHotel")]
        public IHttpActionResult Post(Calificacion calificacion)
        {
            try
            {
                var mngCal = new CalificacionManager();


                mngCal.Create(calificacion);

                apiResp = new ApiResponse();
                apiResp.Message = "Gracias por calificar.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("hotelUsuario")]
        public IHttpActionResult PostCalificar(Calificacion calificacion)
        {
            try
            {
                var mngCal = new CalificacionManager();


                mngCal.CreateCalificacion(calificacion);

                apiResp = new ApiResponse();
                apiResp.Message = "Gracias por calificar.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }



    }
}