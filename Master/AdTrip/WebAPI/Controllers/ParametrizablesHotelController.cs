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
    public class ParametrizablesHotelController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();


        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new ParametrizablesHotelManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }




        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new ParametrizablesHotelManager();
                var param = new ParametrizablesHotel
                {
                    IdHotel = id
                };


                apiResp = new ApiResponse();
                apiResp.Data = mng.RetrieveById(param);
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(ParametrizablesHotel parame)
        {
            var mng = new ParametrizablesHotelManager();
            mng.Create(parame);

            apiResp = new ApiResponse();
            apiResp.Message = "Acción ejecutada.";

            return Ok(apiResp);

        }

        [BitacoraFilter]
        public IHttpActionResult Put(ParametrizablesHotel param)
        {
            try
            {
                var mng = new ParametrizablesHotelManager();
                mng.Update(param);

                apiResp = new ApiResponse();
                apiResp.Message = "Los datos del hotel han sido modificados";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("GetParametrizablesList/{id}")]
        public IHttpActionResult GetParametrizablesList(string id)
        {
            var mng = new ParametrizablesHotelManager();

            var parametrizables = new ParametrizablesHotel
            {
                IdHotel = id
            };

            return Ok(mng.RetrieveById(parametrizables));
        }
    }
}