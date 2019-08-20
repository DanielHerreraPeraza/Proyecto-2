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
    [RoutePrefix("api/foto")]
    public class FotoController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();


        [Route("{idEntidad}")]
        public IHttpActionResult Get(string idEntidad)
        {
            try
            {
                var mng = new FotoManager();
                var foto = new Foto
                {
                    IdEntidad = idEntidad
                };
                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllById(foto);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("{entidad}/{idEntidad}")]
        public IHttpActionResult Get(string entidad, string idEntidad)
        {
            try
            {
                var mng = new FotoManager();
                var foto = new Foto
                {
                    Entidad = entidad,
                    IdEntidad = idEntidad
                };
                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllById(foto);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        [Route("")]
        public IHttpActionResult Post(Foto foto)
        {
            try
            {
                var mng = new FotoManager();
                mng.Create(foto);

                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

        }



        // PUT
        // UPDATE
        [Route("")]

        public IHttpActionResult Put(Foto foto)
        {
            try
            {
                var mng = new FotoManager();
                mng.Update(foto);
                apiResp = new ApiResponse();
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [Route("")]

        public IHttpActionResult Delete(Foto foto)
        {
            try
            {
                var mng = new FotoManager();
                mng.Delete(foto);
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