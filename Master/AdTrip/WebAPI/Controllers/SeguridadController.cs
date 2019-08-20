using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/seguridad")]
    public class SeguridadController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                Usuario usuarioValidado = mng.ValidarUsuario(usuario);
                apiResp = new ApiResponse();
                if (usuarioValidado != null)
                {
                    apiResp.Message = "Bienvenido!";
                    apiResp.Data = usuarioValidado;
                    return Ok(apiResp);
                }
                else
                {
                    apiResp.Message = "No se pudo validar las credenciales ingresadas, favor verifique los datos.";
                    return BadRequest(apiResp.Message);
                }
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("googleLogin")]
        public async Task<IHttpActionResult> GoogleLoginAsync(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();
                Usuario usuarioValidado = await mng.ValidarUsuarioGoogleAsync(usuario.Contrasenna);

                apiResp = new ApiResponse
                {
                    Message = "Bienvenido!",
                    Data = usuarioValidado
                };
                return Ok(apiResp);

            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("ConfirmarUsuario/{id}")]
        public IHttpActionResult ConfirmarUsuarioAsync(string id)
        {
            try
            {
                var mng = new UsuarioManager();

                if (mng.VerificarCorreo(id))
                {
                    apiResp = new ApiResponse();
                    apiResp.Message = "Su cuenta ha sido activada con éxito.";

                    return Redirect("https://adtripapp.azurewebsites.net/Home/vLogIn");
                }
                else
                {
                    return BadRequest("No ha sido posible activar su cuenta.");
                }
                
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPut]
        [Route("RestablecerContrasenna")]
        [BitacoraFilter]
        public async Task<IHttpActionResult> RestablecerContrasennaAsync(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();

                await mng.RestablecerContrasennaAsync(usuario);
                apiResp = new ApiResponse
                {
                    Message = "Instrucciones para restablecer su contraseña han sido enviadas al correo asociado a su cuenta."
                };

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPut]
        [Route("CambiarContrasenna")]
        [BitacoraFilter]
        public IHttpActionResult CambiarContrasenna(Usuario usuario)
        {
            try
            {
                var mng = new UsuarioManager();

                mng.ModificarContrasenna(usuario);
                apiResp = new ApiResponse
                {
                    Message = "La contraseña ha sido cambiada con éxito."
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
