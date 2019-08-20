using CoreAPI;
using Entities;
using Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebAPI
{
    public class BitacoraFilter : ActionFilterAttribute
    {
        private BitacoraManager mngB = new BitacoraManager();

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var controller = context.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = context.ActionContext.ActionDescriptor.ActionName;

            if (context.Response.IsSuccessStatusCode)
            {
                registrarBitacora(controller, action, context);
            }
            else
            {
                //string hola = "";
            }
            //registrarBitacora(controller, action, context);

        }

        private void registrarBitacora(string controller, string action, HttpActionExecutedContext context)
        {

            try
            {
                var Jdatos = new JObject();
                //TryParse(String input, JsonObject result)
                var roles = "";
                try
                {
                    Jdatos = JObject.Parse(GetBodyFromRequest(context));
                    roles = (string)Jdatos["RolB"][0];
                }
                catch (Exception ex)
                {
                    Jdatos = JObject.Parse(parseUrlToJson(GetBodyFromRequest(context)));
                    roles = (string)Jdatos["RolB%5B%5D"];
                }
               
                //JToken jUser = Jdatos["categoria"];
                var correo = (string)Jdatos["CorreoUB"];
                var idHotel = (string)Jdatos["IdHotelB"];
                var rol = roles;

                //var correo = Resquest.QueryString["CorreoUB"].ToString();
                //var idHotel = Resquest.QueryString["IdHotelB"].ToString();
                //var rol = Resquest.QueryString["RolB"].ToString();

                var correoG = correo.Replace("%40", "@");
                if (action.Equals("Post"))
                {
                    action = "Creó";
                }
                else if (action.Equals("Put"))
                {
                    action = "Modificó";
                }
                else if (action.Equals("Delete"))
                {
                    action = "Eliminó";
                }
                else
                {
                    //action = "Indefinida";
                }
                DateTime dateTime = DateTime.Now;
                string sqlFormattedDate = dateTime.ToString("dd-MM-yyyy HH:mm:ss tt");

                if (idHotel==null)
                {
                    idHotel = "nulo";
                }

                Bitacora bitacora = new Bitacora
                {
                    Id = -1,
                    Fecha = DateTime.ParseExact(sqlFormattedDate, "dd-MM-yyyy HH:mm:ss tt", null),
                    TipoAction = action,
                    Controller = controller,
                    RolUsuario = rol,
                    CorreoUsuario = correoG,
                    IdHotel = idHotel
                };
                mngB.Create(bitacora);
            }
            catch (Exception ex)
            {
                
            }
            

        }
        private string GetBodyFromRequest(HttpActionExecutedContext context)
        {
            string data;
            using (var stream = context.Request.Content.ReadAsStreamAsync().Result)
            {
                if (stream.CanSeek)
                {
                    stream.Position = 0;
                }
                data = context.Request.Content.ReadAsStringAsync().Result;
            }
            return data;
        }
        // Returns an object with elements "name: value" with data ftom URL (the "name=value" pairs)
        private string parseUrlToJson(string url)
        {

            var ArrayData = url.Split('&');

            string datosJson = "{ ";
            for (var i = 0; i < ArrayData.Length; i++)
            {
                var valor = ArrayData[i].Split('=');

                datosJson = datosJson + '"' + valor[0] + '"' + ":" + '"' + valor[1] + '"';

                if (i + 1 < ArrayData.Length)
                {
                    datosJson = datosJson + ",";
                }
            }

            datosJson = datosJson + " }";
            return datosJson;


        }
    }
}