using Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlNavBarModel : CtrlBaseModel
    {
        public string Options { get; set; }
        public string OptionsActionName { get; set; }
        private Usuario U { get; set; }

        private string URL_API_LISTs = "https://adtripapi.azurewebsites.net/api/vista/";


        public int OptionsCount => Options.Split(',').Length;

        public CtrlNavBarModel()
        {
            Options = "";
            ViewName = "";
            this.U = (Usuario)HttpContext.Current.Session["usuario"];
        }

        public string ActionLinks
        {
            get
            {
                U = (Usuario)HttpContext.Current.Session["usuario"];
                var vistas = (List<Vista>)HttpContext.Current.Session["permisos"];

                var links = "";
                if (U != null)
                {
                    //var vistas = GetVistasFromAPI(usuario.Identificacion);
                    var lstGrupos = vistas.OrderBy(d => d.Grupo).GroupBy(x => x.Grupo).ToList();

                    foreach (var group in lstGrupos)
                    {
                        if (group.Key != "")
                        {
                            string dnone = "";
                            if (group.Key.Equals("Dnone")|| group.Key.Equals("Hotel"))
                            {
                                dnone = "d-none";
                            }

                            links +=
                                 "<li class='nav-item dropdown'>" +
                                     "<a class='nav-link navbarDropdown " + dnone + "' id='" + group.Key + "' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>" + group.Key + "<span><i class='fa fa-angle-down '></i></span></a>" +
                                     "<ul class='dropdown-menu' aria-labelledby='navbarDropdown'>";

                            foreach (var item in group)
                            {
                                links += "<li>" +
                                "<a href=\"/Home/" + item.Id + "\" class=\"dropdown-item\">" + item.Definicion + "</a>" +
                                "</li>";
                            }

                            links += "</ul></li>";
                        }
                        else
                        {
                            foreach (var item in group)
                            {
                                links += "<li class=\"nav-item dropdown\">" +
                                "<a href=\"/Home/" + item.Id + "\" class=\"nav-link\">" + item.Definicion + "</a>" +
                                "</li>";
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
                        links += "<li class=\"nav-item dropdown\">" +
                            "<a href=\"/Home/" + nw.Action + "\" class=\"nav-link\">" + nw.Label + "</a>" +
                            "</li>";
                    }
                }

                return links;
            }
        }

        private List<Vista> GetVistasFromAPI(string idUsuario)
        {
            var client = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var response = client.DownloadString(URL_API_LISTs + idUsuario);
            var options = JsonConvert.DeserializeObject<List<Vista>>(response);
            return options;
        }
    }
}