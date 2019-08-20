using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/servicio")]
    public class ServicioController : ApiController
    {

      
        ApiResponse apiResp = new ApiResponse();

        [Route("")]
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new ServicioManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [Route("{idHotel}")]

        // GET api/servicio/5/idServicio
        public IHttpActionResult Get(string idHotel)
        {
            try
            {
                var mng = new ServicioManager();
                var servicio = new Servicio
                {
                    IdHotel = idHotel
                };

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllById(servicio); 
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("traerUno/{id}")]
        [HttpGet]
        // GET api/servicio/5/idServicio
        public IHttpActionResult GetServicio(string id)
        {
            try
            {
                var mng = new ServicioManager();
                var servicio = new Servicio
                {
                    Codigo = id
                };

                servicio = mng.RetrieveById(servicio);
                apiResp = new ApiResponse();
                apiResp.Data = servicio;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Post(Servicio servicio)
        {

            try
            {
                var mng = new ServicioManager();
                mng.Create(servicio);

                apiResp = new ApiResponse();
                apiResp.Message = "Servicio registrado.";

                return Ok(apiResp);
            }
             catch (BussinessException bex)
            {
                   return InternalServerError(new Exception(bex.ExceptionId + "-"
                   + bex.AppMessage.Message));
             }
        }

        

        // PUT
        [Route("")]
        // UPDATE
        [BitacoraFilter]
        public IHttpActionResult Put(Servicio servicio)
        {
            try
            {
                var mng = new ServicioManager();
                mng.Update(servicio);

                apiResp = new ApiResponse();
                apiResp.Message = "La información ha sido modificada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Delete(Servicio servicio)
        {
            try
            {
                var mng = new ServicioManager();
                mng.Delete(servicio);

                apiResp = new ApiResponse();
                apiResp.Message = "El servicio se ha eliminado correctamente.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}