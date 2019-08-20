using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlCardProductoModel : CtrlBaseModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Precio { get; set; }
        public string Categoria { get; set; }
        public string TipoImpuesto { get; set; }
        public string Impuesto { get; set; }
        public string Foto { get; set; }

        public CtrlCardProductoModel()
        {
            ViewName = "";
        }
    }
}