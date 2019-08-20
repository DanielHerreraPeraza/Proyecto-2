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
    public class CarritoController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/Carrito
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new CarritoManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        [HttpGet]
        public IHttpActionResult GetStatistic(string type)
        {

            apiResp = new ApiResponse();
            var mng = new CarritoManager();

            return Ok(apiResp);
        }


        // GET api/Carrito/5
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new CarritoManager();
                var carrito = new Carrito
                {
                    IdCarrito = id
                };

                carrito = mng.RetrieveById(carrito);
                apiResp = new ApiResponse();
                apiResp.Data = carrito;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // GET api/carrito/reserva/5
        [HttpPost]
        [Route("api/carrito/llaveQR")]
        public IHttpActionResult Reserva(Carrito carrito)
        {
            try
            {
                var mng = new CarritoManager();

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveByReservaId(carrito);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // POST 
        // CREATE
        public IHttpActionResult Post(Carrito carrito)
        {

            try
            {
                var mng = new CarritoManager();
                mng.Create(carrito);

                apiResp = new ApiResponse();
                apiResp.Message = "El producto fue comprado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // PUT
        // UPDATE 
        [HttpPut]
        public IHttpActionResult Put(Carrito carrito)
        {
            try
            {
                var mng = new CarritoManager();
                mng.Update(carrito);

                apiResp = new ApiResponse();
                apiResp.Message = "El carrito fue modificado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // DELETE ==
        public IHttpActionResult Delete(Carrito carrito)
        {
            try
            {
                var mng = new CarritoManager();
                mng.Delete(carrito);

                apiResp = new ApiResponse();
                apiResp.Message = "El carrito fue eliminado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
