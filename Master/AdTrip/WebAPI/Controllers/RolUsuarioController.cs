using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/rolUsuario")]
    public class RolUsuarioController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [Route("")]
        public IHttpActionResult Post(Rol_Usuario rolUsuario)
        {
            try
            {
                var mng = new Rol_UsuarioManager();
                mng.Create(rolUsuario);

                apiResp = new ApiResponse();
                apiResp.Message = "Rol de usuario registrado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        public IHttpActionResult Delete(Rol_Usuario rolUsuario)
        {
            try
            {
                var mng = new Rol_UsuarioManager();
                mng.Delete(rolUsuario);

                apiResp = new ApiResponse();
                apiResp.Message = "Rol de usuario eliminado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new Rol_UsuarioManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAllRoles(string id)
        {
            try
            {
                var mng = new Rol_UsuarioManager();
                var rolUsuario = new Rol_Usuario
                {
                    IdUsuario = id
                };

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllById(rolUsuario);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
