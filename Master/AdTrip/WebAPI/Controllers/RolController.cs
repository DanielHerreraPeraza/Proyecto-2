using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/rol")]
    public class RolController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [Route("")]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new RolManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new RolManager();
                var rol = new Rol
                {
                    Codigo = id
                };

                rol = mng.RetrieveById(rol);
                apiResp = new ApiResponse
                {
                    Data = rol
                };
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
        public IHttpActionResult Post(Rol rol)
        {
            try
            {
                var mng = new RolManager();
                mng.Create(rol);

                apiResp = new ApiResponse();
                apiResp.Message = "Rol registrado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Put(Rol rol)
        {
            try
            {
                var mng = new RolManager();
                mng.Update(rol);

                apiResp = new ApiResponse();
                apiResp.Message = "Rol modificado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Delete(Rol rol)
        {
            try
            {
                var mng = new RolManager();
                mng.Delete(rol);

                apiResp = new ApiResponse();
                apiResp.Message = "Rol eliminado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("rolesGerente/{id}")]
        public IHttpActionResult GetRolesByGerente(string id)
        {
            try
            {
                var mng = new RolManager();
                apiResp.Data = mng.RetrieveByGerenteId(id);

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}