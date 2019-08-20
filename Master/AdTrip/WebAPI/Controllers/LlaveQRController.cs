using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/llaveQR")]
    public class LlaveQRController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new LlaveQRManager();
                apiResp.Data = mng.RetrieveAll();
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
            return Ok(apiResp);
        }

        [HttpGet]
        [Route("llavesReserva/{id}")]
        public IHttpActionResult GetAllByReserva(string id)
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new LlaveQRManager();

                var num = 0;
                var idR = 0;
                if (Int32.TryParse(id, out num))
                    idR = num;
                else
                    throw new BussinessException(22);

                var QR = new LlaveQR
                {
                    IdReserva = idR
                };

                apiResp.Data = mng.RetrieveAllByReserva(QR);

            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
            return Ok(apiResp);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new LlaveQRManager();

                var QR = new LlaveQR
                {
                    CodigoQR = id
                };

                QR = mng.RetrieveById(QR);
                apiResp = new ApiResponse();
                apiResp.Data = QR;
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
        public async Task<IHttpActionResult> PostAsync(LlaveQR llave)
        {
            try
            {
                var mng = new LlaveQRManager();

                await mng.Create(llave);

                apiResp = new ApiResponse();
                apiResp.Message = "Llave generada correctamente";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }


        [HttpPost]
        [Route("conCorreo")]
        [BitacoraFilter]
        public async Task<IHttpActionResult> PostYEnviarCorreo(LlaveQR llave)
        {
            try
            {
                var mng = new LlaveQRManager();
                var usuario = new Usuario
                {
                    Correo = llave.IdUsuario
                };
                await mng.CreateYEnviar(llave, usuario);

                apiResp = new ApiResponse();
                apiResp.Message = "Llave generada correctamente";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPut]
        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Put(LlaveQR llave)
        {
            try
            {
                var mng = new LlaveQRManager();

                mng.Update(llave);

                apiResp = new ApiResponse();
                apiResp.Message = "Llave modificada correctamente";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpDelete]
        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Delete(LlaveQR llave)
        {
            try
            {
                var mng = new LlaveQRManager();

                mng.Delete(llave);

                apiResp = new ApiResponse();
                apiResp.Message = "Llave Eliminada correctamente";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("checkIn")]
        public IHttpActionResult CheckIn(Reserva reserva)
        {
            try
            {
                var mng = new LlaveQRManager();

                apiResp = new ApiResponse
                {
                    Message = mng.CheckIn(reserva)
                };

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("checkOut")]
        public IHttpActionResult CheckOut(Reserva reserva)
        {
            try
            {
                var mng = new LlaveQRManager();

                apiResp = new ApiResponse
                {
                    Message = mng.CheckOut(reserva)
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