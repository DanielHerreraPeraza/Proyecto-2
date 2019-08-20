
using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/bitacora")]
    public class BitacoraController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();
        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mngBit = new BitacoraManager();
            apiResp.Data = mngBit.RetrieveAll();

            return Ok(apiResp);
        }
        [Route("bitacoraHotel/{id}")]
        [HttpGet]
        public IHttpActionResult GetBitacoraHotel(string id)
        {
            apiResp = new ApiResponse();
            var mngBit = new BitacoraManager();
            apiResp.Data = mngBit.RetrieveAllBiHotel(id);

            return Ok(apiResp);
        }
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetById(string id)
        {
            try
            {
                var mngBit = new BitacoraManager();
                var bitacora = new Bitacora
                {
                    Id = Convert.ToInt32(id)
                };

                bitacora = mngBit.RetrieveById(bitacora);
                apiResp = new ApiResponse();
                apiResp.Data = bitacora;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(Bitacora bitacora)
        {
            try
            {
                var mng= new BitacoraManager();


                mng.Create(bitacora);

                apiResp = new ApiResponse();
                apiResp.Message = "Bitacora fue creada con éxito";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }
    }
}