using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ExceptionFilter]
    [RoutePrefix("api/producto")]
    public class ProductoController : ApiController
    {


        ApiResponse apiResp = new ApiResponse();

        [Route("")]
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new ProductoManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [Route("{idServicio}")]
        // GET api/servicio/5/idServicio
        public IHttpActionResult Get(string idServicio)
        {
            try
            {
                var mng = new ProductoManager();
                var producto = new Producto
                {
                    IdServicio = idServicio
                };

               
                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveAllById(producto);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("idProducto/{id}")]
        // GET api/producto/idProducto
        public IHttpActionResult GetById(string id)
        {
            try
            {
                var mng = new ProductoManager();
                var producto = new Producto
                {
                    Codigo = id
                };


                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveById(producto);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // GET api/producto/hotel/5
        [HttpGet]
        [Route("hotel/{idHotel}")]
        public IHttpActionResult Hotel(string idHotel)
        {
            try
            {
                var mng = new ProductoManager();

                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveByHotelId(idHotel);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Post(Producto producto)
        {

            try
            {
                var mng = new ProductoManager();
                mng.Create(producto);

                apiResp = new ApiResponse();
                apiResp.Message = "Producto registrado.";

                return Ok(apiResp);
                    }
                   catch (BussinessException bex)
             {
                 return InternalServerError(new Exception(bex.ExceptionId + "-"
                  + bex.AppMessage.Message));
             }
        }





        // PUT
        [Route("")]
        // UPDATE
        [BitacoraFilter]
        public IHttpActionResult Put(Producto producto)
        {
            try
            {
                var mng = new ProductoManager();
                mng.Update(producto);

                apiResp = new ApiResponse();
                apiResp.Message = "Información del producto actualizada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [Route("")]
        [BitacoraFilter]
        public IHttpActionResult Delete(Producto producto)
        {
            try
            {
                var mng = new ProductoManager();
                mng.Delete(producto);

                apiResp = new ApiResponse();
                apiResp.Message = "Producto eliminado.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

    }
}