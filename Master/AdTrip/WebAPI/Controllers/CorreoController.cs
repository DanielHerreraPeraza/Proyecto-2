using CoreAPI;
using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class CorreoController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            apiResp.Data = "get correo";

            return Ok(apiResp);
        }

        // GET api/correo/correoelectrónico
        // Retrieve by id
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mngS = new SolicitudHotelCrudFactory();

                var solicitudHotel = new SolicitudHotel
                {
                    CodigoSolicitud = Convert.ToInt32(id)

                };
                SolicitudHotel c = null;
                c = mngS.Retrieve<SolicitudHotel>(solicitudHotel);

                if (c != null)
                {
                    //solicitudHotel = mngS.RetrieveById(solicitudHotel);
                    var respuesta = "correo está comentado";
                    //var respuesta = EnviarCorreoManager.GetInstance().Enviar(c.CorreoUsuario, "RegistroHotel", c);

                    apiResp.Data = respuesta;
                    return Ok(apiResp);
                }
                else
                {
                    apiResp.Data = "solicitud no existe";
                    return Ok(apiResp);
                }

            }
            catch (Exception ex)
            {
                apiResp.Data = ex.Message;
                return Ok(apiResp);
            }
        }
        // POST 
        // CREATE
        public IHttpActionResult Post()
        {
            try
            {

                apiResp = new ApiResponse();
                apiResp.Message = "post hecho";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }
        // POST 
        // CREATE
        [HttpPost]
        public async Task<IHttpActionResult> EnviarQR(LlaveQR llave)
        {
            try
            {
                var mng = new LlaveQRManager();

                LlaveQR c = null;
                c = mng.RetrieveById(llave);

                if (c != null)
                {
                    await EnviarCorreoManager.GetInstance().ExecuteCorreoCodigoQR(llave.IdUsuario, llave);

                    apiResp = new ApiResponse();
                    apiResp.Message = "Llave enviada, Por favor verifique el correo electrónico";

                }
                else
                {
                    throw new BussinessException(52);
                }
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }

            return Ok(apiResp);
        }
        // PUT
        // UPDATE
        public IHttpActionResult Put()
        {
            try
            {

                apiResp = new ApiResponse();
                apiResp.Message = "put hecho";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // DELETE ==
        public IHttpActionResult Delete()
        {
            try
            {

                apiResp = new ApiResponse();
                apiResp.Message = "delete hecho";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }
    }
}