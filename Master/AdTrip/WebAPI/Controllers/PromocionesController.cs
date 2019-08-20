using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    public class PromocionesController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/promociones
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new PromocionesManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        [HttpGet]
        public IHttpActionResult GetStatistic(string type)
        {

            apiResp = new ApiResponse();
            var mng = new PromocionesManager();

            return Ok(apiResp);
        }


        // GET api/promociones/5
        // Retrieve by id
          public IHttpActionResult Get(string id)
          {
              try
              {
                  var mng = new PromocionesManager();
                  var promociones = new Promociones
                  {
                      Codigo = id
                  };

                  promociones = mng.RetrieveById(promociones);
                  apiResp = new ApiResponse();
                  apiResp.Data = promociones;
                  return Ok(apiResp);
              }
              catch (BussinessException bex)
              {
                  return InternalServerError(new Exception(bex.AppMessage.Message));
              }
          }

        // GET api/promociones/hotel/5
        [HttpGet]
        [Route("api/promociones/hotel/{idHotel}")]
        public IHttpActionResult Hotel(string idHotel)
        {
            try
            {
                var mng = new PromocionesManager();

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveByHotelId(idHotel);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // POST 
        // CREATE
        [BitacoraFilter]
        public IHttpActionResult Post(Promociones promociones)
        {

            try
            {
                var mng = new PromocionesManager();
                mng.Create(promociones);

                apiResp = new ApiResponse();
                apiResp.Message = "La promoción fue creada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // PUT
        // UPDATE
        [BitacoraFilter]
        public IHttpActionResult Put(Promociones promociones)
        {
            try
            {
                var mng = new PromocionesManager();
                mng.Update(promociones);

                apiResp = new ApiResponse();
                apiResp.Message = "La promoción fue modificada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [BitacoraFilter]
        public IHttpActionResult Delete(Promociones promociones)
        {
            try
            {
                var mng = new PromocionesManager();
                mng.Delete(promociones);

                apiResp = new ApiResponse();
                apiResp.Message = "La promoción fue eliminada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
