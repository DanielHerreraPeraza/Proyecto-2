using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/vista")]
    public class VistaController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [Route("")]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new VistaManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new VistaManager();
                var usuario = new Usuario
                {
                    Identificacion = id
                };

                var lstVistas = mng.RetrieveById(usuario);
                return Ok(lstVistas);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        public IHttpActionResult Post(Vista vista)
        {
            try
            {
                var mng = new VistaManager();
                mng.Create(vista);

                apiResp = new ApiResponse();
                apiResp.Message = "Vista registrada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        public IHttpActionResult Put(Vista vista)
        {
            try
            {
                var mng = new VistaManager();
                mng.Update(vista);

                apiResp = new ApiResponse();
                apiResp.Message = "Vista modificada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        public IHttpActionResult Delete(Vista vista)
        {
            try
            {
                var mng = new VistaManager();
                mng.Delete(vista);

                apiResp = new ApiResponse();
                apiResp.Message = "Vista eliminada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
