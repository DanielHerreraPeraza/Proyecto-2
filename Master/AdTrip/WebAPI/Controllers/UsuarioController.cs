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
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        
        [Route("")]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new UsuarioManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
       
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new UsuarioManager();
                var usuario = new Usuario
                {
                    Identificacion = id
                };

                usuario = mng.RetrieveById(usuario);
                apiResp = new ApiResponse
                {
                    Data = usuario
                };
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("validarExiste")]
        public IHttpActionResult GetValidar(Usuario usuario)
        {
            try
            {

                var mng = new UsuarioManager();
                //var usuario = new Usuario
                //{
                //    Correo = correo,
                //    Identificacion = id
                //};

                usuario = mng.Validar(usuario);
                apiResp = new ApiResponse
                {
                    Data = usuario
                };
                if (usuario == null)
                {
                    apiResp.Message = "El usuario no existe";
                }
                else
                {
                    apiResp.Message = "El usuario si existe";
                }

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }


        [Route("")]
        [BitacoraFilter]
        public async Task<IHttpActionResult> PostAsync(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                await mng.CreateAsync(usuario);

                apiResp = new ApiResponse
                {
                    Message = "Usuario registrado."
                };

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Put(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                mng.Update(usuario);

                apiResp = new ApiResponse
                {
                    Message = "Usuario modificado."
                };

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Delete(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                mng.Delete(usuario);

                apiResp = new ApiResponse
                {
                    Message = "Usuario eliminado."
                };

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}