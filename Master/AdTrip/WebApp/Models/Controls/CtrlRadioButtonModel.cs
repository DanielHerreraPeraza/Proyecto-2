using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlRadioButtonModel : CtrlBaseModel
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public string Checked { get; set; }
        public string ChangeFunction { get; set; }

        public CtrlRadioButtonModel()
        {
            ViewName = "";
        }
    }
}