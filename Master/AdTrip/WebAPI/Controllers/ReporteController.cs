using CoreAPI;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using WebAPI.Models;

namespace WebAPI.Controllers
{



    [ExceptionFilter]
    [RoutePrefix("api/reporte")]
    public class ReporteController : ApiController
    {


        ApiResponse apiResp = new ApiResponse();

        //GetGananciasTotales
       
        [Route("{tipoReporte}")]
        public IHttpActionResult Get(string tipoReporte)
        {
            apiResp = new ApiResponse();
            var mng = new ReporteManager();


            switch (tipoReporte)
            {
                case "1":

                    apiResp.Data = mng.RetrieveGananciasTotalesAdmin();
                    break;
                case "2":

                    apiResp.Data = mng.RetrieveGananciasXMesAdmin();

                    break;
                case "3":
                    apiResp.Data = mng.RetrieveGananciasComisionXDiaAdmin();

                    break;
                case "4": //
                    apiResp.Data = mng.RetrieveGananciaMembresiaXMesAdmin();

                    break;
                
            }



            return Ok(apiResp);
        }

        [HttpGet]
        [Route("{tipoReporte}/{idUsuario}")]
        public IHttpActionResult Get(string tipoReporte, string idUsuario)
        {
            apiResp = new ApiResponse();
            var mng = new ReporteManager();

           
            var reporte = new Reporte
            {
                IdUsuario = idUsuario
            };

         
            switch (tipoReporte)
            {
             
                case "5": //total gerente 
                    apiResp.Data = mng.RetrieveGananciasTotalesGerente(reporte);

                    break;
                case "6": //ganancia por dia gerente
                    apiResp.Data = mng.RetrieveGananciaXDiaGerente(reporte);

                    break;
                case "7": //ganancia por dia gerente
                    apiResp.Data = mng.RetrieveCantHabHotel(reporte);
                    break;

            }


            return Ok(apiResp);
        }


        [HttpGet]
        [Route("reportehotel/{tipoReporte}/{idHotel}")]
        public IHttpActionResult GetReporteHotel(string tipoReporte, string idHotel)
        {
            apiResp = new ApiResponse();
            var mng = new ReporteManager();


            var reporte = new Reporte
            {
                IdHotel = idHotel
            };


            switch (tipoReporte)
            {

                case "8": //total hotel 
                    apiResp.Data = mng.RetrieveGananciasTotalHotel(reporte);

                    break;
                case "9": //ganancia por mes hotel
                    apiResp.Data = mng.RetrieveGananciaXMesHotel(reporte);

                    break;
                case "10": //cantidad de habitaciones por tipo
                    apiResp.Data = mng.RetrieveCantHabTipo(reporte);
                    break;
                case "11": //disponibilidad de habitaciones
                    apiResp.Data = mng.RetrieveDisponibilidadHab(reporte);
                    break;
            }


            return Ok(apiResp);
        }
    }
}