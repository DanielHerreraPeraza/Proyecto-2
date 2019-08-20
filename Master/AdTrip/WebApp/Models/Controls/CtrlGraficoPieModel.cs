using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlGraficoPieModel : CtrlBaseModel
    {

        public string Title { get; set; }
        public string OnLoadFunction { get; set; }
        public string ChartType { get; set; }
        public string OnInitializeChart { get; set; }

        public CtrlGraficoPieModel()
        {

        }

    }
}