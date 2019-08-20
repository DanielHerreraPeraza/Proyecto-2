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
    public class FacturaController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/factura
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new FacturaManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        [HttpGet]
        public IHttpActionResult GetStatistic(string type)
        {

            apiResp = new ApiResponse();
            var mng = new FacturaManager();

            return Ok(apiResp);
        }


        // GET api/factura/5
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new FacturaManager();
                var factura = new Factura
                {
                    NumFacturacion = id
                };

                factura = mng.RetrieveById(factura);
                apiResp = new ApiResponse();
                apiResp.Data = factura;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // GET api/factura/hotel/5
        [HttpGet]
        [Route("api/factura/hotel/{cedJuridica}")]
        public IHttpActionResult Hotel(string cedJuridica)
        {
            try
            {
                var mng = new FacturaManager();

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveByHotelId(cedJuridica);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // POST 
        // CREATE
        public IHttpActionResult Post(Factura factura)
        {

            try
            {
                var mng = new FacturaManager();
                mng.Create(factura);

                apiResp = new ApiResponse();
                apiResp.Message = "La factura fue creada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // PUT
        // UPDATE
        public IHttpActionResult Put(Factura factura)
        {
            try
            {
                var mng = new FacturaManager();
                mng.Update(factura);

                apiResp = new ApiResponse();
                apiResp.Message = "La factura fue modificada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // DELETE ==
        public IHttpActionResult Delete(Factura factura)
        {
            try
            {
                var mng = new FacturaManager();
                mng.Delete(factura);

                apiResp = new ApiResponse();
                apiResp.Message = "La factura fue eliminada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
