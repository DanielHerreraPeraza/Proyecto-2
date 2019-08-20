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
    public class TipoHabitacionesController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/customer
        // Retrieve
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new TipoHabitacionesManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        [HttpGet]
        public IHttpActionResult GetStatistic(string type)
        {

            apiResp = new ApiResponse();
            var mng = new TipoHabitacionesManager();

            return Ok(apiResp);
        }


        // GET api/tipoHabitaciones/5
        // Retrieve by id
        [HttpGet]
        [Route("api/tipoHabitaciones/{id}")]
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new TipoHabitacionesManager();
                var tipoHabitaciones = new TipoHabitaciones
                {
                    Codigo = id
                };

                tipoHabitaciones = mng.RetrieveById(tipoHabitaciones);
                apiResp = new ApiResponse();
                apiResp.Data = tipoHabitaciones;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // GET api/tipoHabitaciones/hotel/5
        [HttpGet]
        [Route("api/tipoHabitaciones/hotel/{idHotel}")]
        public IHttpActionResult Hotel(string idHotel)
        {
            try
            {
                var mng = new TipoHabitacionesManager();

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
        public IHttpActionResult Post(TipoHabitaciones tipoHabitaciones)
        {

            try
            {
                var mng = new TipoHabitacionesManager();
                mng.Create(tipoHabitaciones);

                apiResp = new ApiResponse();
                apiResp.Message = "El tipo de habitación fue creado";

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
        public IHttpActionResult Put(TipoHabitaciones tipoHabitaciones)
        {
            try
            {
                var mng = new TipoHabitacionesManager();
                mng.Update(tipoHabitaciones);

                apiResp = new ApiResponse();
                apiResp.Message = "El tipo de habitación fue modificado";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        // DELETE ==
        [BitacoraFilter]
        public IHttpActionResult Delete(TipoHabitaciones tipoHabitaciones)
        {
            try
            {
                var mng = new TipoHabitacionesManager();
                mng.Delete(tipoHabitaciones);

                apiResp = new ApiResponse();
                apiResp.Message = "El tipo de habitacion fue eliminado";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }
    }
}
