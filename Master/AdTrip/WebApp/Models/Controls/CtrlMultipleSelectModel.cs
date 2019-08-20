using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlMultipleSelectModel : CtrlBaseModel
    {
        public string Label { get; set; }
        public string ListId { get; set; }
        public string ColumnDataName { get; set; }

        private string URL_API_LISTs = "https://adtripapi.azurewebsites.net/api/list/";

        public string ListOptions
        {
            get
            {
                var htmlOptions = "";
                var lst = GetOptionsFromAPI();

                foreach (var option in lst)
                {
                    htmlOptions += "<option value='" + option.Codigo + "' class='"+option.Extra+ "'>" + option.Nombre + "</option>";
                }
                return htmlOptions;
            }
            set
            {

            }
        }

        private List<OptionList> GetOptionsFromAPI()
        {
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var response = client.DownloadString(URL_API_LISTs + ListId);
            var options = JsonConvert.DeserializeObject<List<OptionList>>(response);
            return options;
        }

        public CtrlMultipleSelectModel()
        {
            ViewName = "";
        }
    }
}