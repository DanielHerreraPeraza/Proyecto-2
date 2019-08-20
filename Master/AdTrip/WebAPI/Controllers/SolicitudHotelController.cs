using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class SolicitudHotelController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        // GET api/solicitudHotel
        // Retrieve
        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mng = new SolicitudHotelManager();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }



        // GET api/solicitudHotel/5
        // Retrieve by id
        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new SolicitudHotelManager();
                var solicitudHotel = new SolicitudHotel();
                apiResp = new ApiResponse();

                var numCodigo = 0;
                if (Int32.TryParse(id, out numCodigo))
                {

                    solicitudHotel = new SolicitudHotel
                    {
                        CodigoSolicitud = numCodigo
                    };
                }
                else
                {
                    apiResp.Data = null;
                    apiResp.Message = "codigo de solcitud incorrecto";
                    return Ok(apiResp);
                };




                solicitudHotel = mng.RetrieveById(solicitudHotel);
                
                if (solicitudHotel != null)
                {
                    apiResp.Data = solicitudHotel;
                    apiResp.Message = "Solicitud retornado";
                }
                else
                {
                    apiResp.Message = "No existe";
                }


                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        // POST 
        // CREATE
        public IHttpActionResult Post(SolicitudHotel solicitudHotel)
        {

            try
            {
                var mng = new SolicitudHotelManager();

                
                mng.Create(solicitudHotel);
                apiResp = new ApiResponse();
                apiResp.Message = "Solicitud enviada con éxito, recibirá un correo electrónico cuando la solicitud haya sido aprobada";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        // PUT
        // UPDATE
        public async Task<IHttpActionResult> PutAsync(SolicitudHotel solicitudHotel)
        {
            try
            {
                var mng = new SolicitudHotelManager();
                await mng.UpdateAsync(solicitudHotel);

                apiResp = new ApiResponse();
                apiResp.Message = "Solicitud resuelta con éxito";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }

        //[HttpPut]
        //public IHttpActionResult PutEstado(string id)
        //{
        //    try
        //    {
        //        apiResp = new ApiResponse();
        //        var mng = new SolicitudHotelManager();

        //        mng.UpdateEstado(id);

        //        apiResp.Message = "Estado modificado";

        //        return Ok(apiResp);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex.Data);
        //    }
        //}

        // DELETE ==
        public IHttpActionResult Delete(SolicitudHotel solicitudHotel)
        {
            try
            {
                var mng = new SolicitudHotelManager();
                mng.Delete(solicitudHotel);

                apiResp = new ApiResponse();
                apiResp.Message = "Acción ejecucada con éxito";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception( bex.AppMessage.Message));
            }
        }
    }
}
