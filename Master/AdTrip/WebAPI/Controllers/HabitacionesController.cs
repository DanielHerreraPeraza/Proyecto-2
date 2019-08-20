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
    public class HabitacionesController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/habitaciones
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new HabitacionesManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        [HttpGet]
        public IHttpActionResult GetStatistic(string type)
        {

            apiResp = new ApiResponse();
            var mng = new HabitacionesManager();

            return Ok(apiResp);
        }


        // GET api/habitaciones/5
        // Retrieve by id
        public IHttpActionResult Get(int id)
        {
            try
            {
                var mng = new HabitacionesManager();
                var habitaciones = new Habitaciones
                {
                    Codigo = id
                };

                habitaciones = mng.RetrieveById(habitaciones);
                apiResp = new ApiResponse();
                apiResp.Data = habitaciones;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // GET api/habitaciones/hotel/5
        [HttpGet]
        [Route("api/habitaciones/hotel/{idHotel}")]
        public IHttpActionResult Hotel(string idHotel)
        {
            try
            {
                var mng = new HabitacionesManager();

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
        public IHttpActionResult Post(Habitaciones habitaciones)
        {

            try
            {
                var mng = new HabitacionesManager();
                mng.Create(habitaciones);

                apiResp = new ApiResponse();
                apiResp.Message = "La habitación fue creada.";

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
        public IHttpActionResult Put(Habitaciones habitaciones)
        {
            try
            {
                var mng = new HabitacionesManager();
                mng.Update(habitaciones);

                apiResp = new ApiResponse();
                apiResp.Message = "La habitación fue modificada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [BitacoraFilter]
        public IHttpActionResult Delete(Habitaciones habitaciones)
        {
            try
            {
                var mng = new HabitacionesManager();
                mng.Delete(habitaciones);

                apiResp = new ApiResponse();
                apiResp.Message = "La habitación fue eliminada.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
