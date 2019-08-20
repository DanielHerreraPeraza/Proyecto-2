using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{

    [ExceptionFilter]
    public class ImpuestoController : ApiController

    {
        ApiResponse apiResp = new ApiResponse();


        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mngImp = new ImpuestoManager();
            apiResp.Data = mngImp.RetrieveAll();

            return Ok(apiResp);
        }
        [HttpGet]


        public IHttpActionResult Get(int codigo)
        {
            try
            {
                var mngImp = new ImpuestoManager();
                var impuesto = new TipoImpuesto

                {
                    Codigo = codigo
                };

                impuesto = mngImp.RetrieveById(impuesto);
                apiResp = new ApiResponse();
                apiResp.Data = impuesto;
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [BitacoraFilter]
        public IHttpActionResult Post(TipoImpuesto impuesto)
        {
            try
            {
                var mngImp = new ImpuestoManager();

               
                mngImp.Create(impuesto);

                apiResp = new ApiResponse();
                apiResp.Message = "El impuesto se registró exitosamente.";
                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [BitacoraFilter]
        public IHttpActionResult Put(TipoImpuesto impuesto)
        {
            try
            {
                var mngImp = new ImpuestoManager();
                mngImp.Update(impuesto);

                apiResp = new ApiResponse();
                apiResp.Message = "La información ha sido modificada correctamente.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

        [BitacoraFilter]
        public IHttpActionResult Delete(TipoImpuesto impuesto)
        {
            try
            {
                var mngImp = new ImpuestoManager();
                mngImp.Delete(impuesto);

                apiResp = new ApiResponse();
                apiResp.Message = "El impuesto se eliminó correctamente.";

                return Ok(apiResp);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

    }
}