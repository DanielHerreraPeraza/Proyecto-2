using CoreAPI;
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
    [RoutePrefix("api/hotel")]
    public class HotelController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/solicitudHotel
        // Retrieve
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new HotelManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [Route("usuario/{id}")]
        public IHttpActionResult GetHotelesUsuario(string id)
        {
            apiResp = new ApiResponse();
            var mng = new HotelManager();
            
            apiResp.Data = mng.GetHotelesPorUsuario(id);
            return Ok(apiResp);


        }

        // GET api/solicitudHotel/5
        // Retrieve by id
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new HotelManager();
                var hotel = new Hotel
                {
                    CedulaJuridica = id
                };

                hotel = mng.RetrieveById(hotel);
                apiResp = new ApiResponse();
                apiResp.Data = hotel;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        // POST 
        // CREATE
        [BitacoraFilter]
        public async Task<IHttpActionResult> Post(Hotel hotel)
        {

            try
            {
                var mng = new HotelManager();

                await mng.Create(hotel);

                apiResp = new ApiResponse();
                apiResp.Message = "Hotel registrado con éxito";

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
        public IHttpActionResult Put(Hotel hotel)
        {
            try
            {
                var mng = new HotelManager();
              
                mng.Update(hotel);

                apiResp = new ApiResponse();
                apiResp.Message = "Hotel modificado con éxito";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        [Route("cambioEstado/{id}")]
        public IHttpActionResult GetcambioEstado(string id)

        {
            try
            {
                var mng = new HotelManager();
                var hotel = new Hotel
                {
                    CedulaJuridica = id
                };

                mng.CambioEstado(hotel);
                hotel = mng.RetrieveById(hotel);
                apiResp = new ApiResponse();
                apiResp.Data = hotel;
                apiResp.Message = "Se a cambiado el estado";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }
        // DELETE ==
        [BitacoraFilter]
        public IHttpActionResult Delete(Hotel hotel)
        {
            try
            {
                var mng = new HotelManager();
                mng.Delete(hotel);

                apiResp = new ApiResponse();
                apiResp.Message = "Hotel eliminado con éxito";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("GetListaHoteles")]
        public IHttpActionResult GetListaHoteles()
        {
            var mng = new HotelManager();

            return Ok(mng.RetrieveAll());
        }
    }
}