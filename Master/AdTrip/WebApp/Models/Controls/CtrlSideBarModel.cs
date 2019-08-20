using Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlSideBarModel : CtrlBaseModel
    {
        public CtrlSideBarModel()
        {
            Options = "";
            ViewName = "";
        }

        public string Options { get; set; }
        public string OptionsActionName { get; set; }

        private string URL_API_LISTs = "https://adtripapi.azurewebsites.net/api/vista/";


        public int OptionsCount => Options.Split(',').Length;

        public string ActionLinks
        {
            get
            {
                Usuario usuario = (Usuario)HttpContext.Current.Session["usuario"];
                var vistas = (List<Vista>)HttpContext.Current.Session["permisos"];


                var links = "";
                if (usuario != null)
                {
                    //var vistas = GetVistasFromAPI(usuario.Identificacion);
                    var lstGrupos = vistas.OrderBy(d => d.Grupo).GroupBy(x => x.Grupo).ToList();

                    int counter = 0;
                    foreach (var group in lstGrupos)
                    {
                        if (group.Key != "")
                        {
                            string dnone = "";
                            if (group.Key.Equals("Dnone") || group.Key.Equals("Hotel"))
                            {
                                dnone = "d-none";
                            }
                            links +=
                                    "<a href='#lst"+ counter + "' class='items-list " + dnone + "' id='" + group.Key + "C' data-toggle='collapse' aria-expanded='false'>" +
                                    "<span><i class='fa fa-asterisk link-icon'></i></span>"+ group.Key +"<span><i class='fa fa-chevron-down arrow'></i></span></a>" +
                                    "<div class='collapse sub-menu text-danger' id='lst"+ counter++ + "'>";

                            foreach (var item in group)
                            {
                                links += "<a class='items-list' href=\"/Home/" + item.Id + "\">"+ item.Definicion + "</a>";
                            }

                            links += "</div>";
                        }
                        else
                        {
                            foreach (var item in group)
                            {
                                links += "<a href=\"/Home/" + item.Id + "\" class=\"items-list\" aria-expanded=\"false\">" +
                                    "<span><i class=\"fa fa-asterisk link-icon\"></i></span>" + item.Definicion + "<span><i class=\"fa fa-chevron-right arrow\"></i></span>" +
                                "</a>";
                            }
                        }
                    }
                }
                else
                {
                    var optionsName = Options.Split(',');
                    var optionsActionName = OptionsActionName.Split(',');
                    var o = optionsName.Zip(optionsActionName, (n, w) => new { Label = n, Action = w });
                    foreach (var nw in o)
                    {
                        links += "<a href=\"/Home/" + nw.Action + "\" class=\"items-list\" aria-expanded=\"false\">" +
                                    "<span><i class=\"fa fa-asterisk link-icon\"></i></span>" + nw.Label + "<span><i class=\"fa fa-chevron-right arrow\"></i></span>" +
                                "</a>";
                    }
                }

                return links;
            }
        }

        private List<Vista> GetVistasFromAPI(string idUsuario)
        {
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var response = client.DownloadString(URL_API_LISTs + idUsuario);
            var options = JsonConvert.DeserializeObject<List<Vista>>(response);
            return options;
        }
    }
}