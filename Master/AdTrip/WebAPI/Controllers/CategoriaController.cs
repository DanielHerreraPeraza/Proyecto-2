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
    public class CategoriaController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();


        public IHttpActionResult Get()
        {

            apiResp = new ApiResponse();
            var mngCat = new CategoriaManager();
            apiResp.Data = mngCat.RetrieveAll();

            return Ok(apiResp);
        }

        [HttpGet]
        public IHttpActionResult Get(int codigo)
        {
            try
            {
                var mngCat = new CategoriaManager();
                var categoria = new Categoria
                {
                    Codigo = codigo
                };

                    categoria = mngCat.RetrieveById(categoria);
                    apiResp = new ApiResponse();
                    apiResp.Data = categoria;
                    return Ok(apiResp);
                }
                catch (BussinessException bex)
                {
                    return InternalServerError(new Exception(bex.AppMessage.Message));
                }
            }

        [BitacoraFilter]
        public IHttpActionResult Post(Categoria categoria)
        {
            try
            {
                var mngCat = new CategoriaManager();


                mngCat.Create(categoria);

                    apiResp = new ApiResponse();
                    apiResp.Message = "La categoría se registró con éxito.";
                    return Ok(apiResp);
                }
                catch (BussinessException bex)
                {
                    return InternalServerError(new Exception(bex.AppMessage.Message));
                }
            }

        [BitacoraFilter]
        public IHttpActionResult Put(Categoria categoria)
        {
            try
            {
                var mngCat = new CategoriaManager();
                mngCat.Update(categoria);

                    apiResp = new ApiResponse();
                    apiResp.Message = "La información se modificó correctamente.";

                    return Ok(apiResp);
                }
                catch (BussinessException bex)
                {
                    return InternalServerError(new Exception(bex.AppMessage.Message));
                }
            }

        [BitacoraFilter]
        public IHttpActionResult Delete(Categoria categoria)
        {
            try
            {
                var mngCat = new CategoriaManager();
                mngCat.Delete(categoria);

                    apiResp = new ApiResponse();
                    apiResp.Message = "La categoría se eliminó correctamente.";

                    return Ok(apiResp);
                }
                catch (BussinessException bex)
                {
                    return InternalServerError(new Exception(bex.AppMessage.Message));
                }
            }
    }
}