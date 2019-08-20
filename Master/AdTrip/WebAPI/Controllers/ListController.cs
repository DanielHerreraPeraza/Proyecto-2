using CoreAPI;
using Entities;
using Exceptions;
using System;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ListController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Get(string id)
        {
            try
            {
                var mng = new ListManager();
                var option = new OptionList
                {
                    ListId = id
                };

                var lstOptions = mng.RetrieveById(option);
                return Ok(lstOptions);
            }
            catch (BussinessException bex)
            {
                return InternalServerError(new Exception(bex.AppMessage.Message));
            }
        }

    }
}
