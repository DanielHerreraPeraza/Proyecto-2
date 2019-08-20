using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Producto : Entity
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        public string Proveedor { get; set; }
        public string IdEstado { get; set; }
        public string Estado { get; set; }
        public int CantProductos { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdTipoImpuesto { get; set; }
        public string TipoImpuesto { get; set; }
        public decimal Impuesto { get; set; }
        public string IdServicio { get; set; } 


        public Producto()
        {

        }


        public Producto(string[] infoArray)
        {
            if (infoArray != null && infoArray.Length >= 4)
            {
                Codigo = infoArray[0];
                Nombre = infoArray[1];
                Descripcion = infoArray[2];
                Foto = infoArray[3];
                Proveedor = infoArray[4];
                Estado = infoArray[5];
                IdServicio = infoArray[9];

                int cant;
                decimal imp;
                decimal p;
                int c;
                int ti;



                if (Decimal.TryParse(infoArray[3], out p))
                {
                    Precio = p;
                }
                else
                {
                    throw new Exception("El precio debe ser en decimal");
                }

                if (Int32.TryParse(infoArray[7], out cant))
                {
                    CantProductos = cant;
                } else
                {
                    throw new Exception("La cantidad de los productos debe ser un número");
                }

                if (Decimal.TryParse(infoArray[10], out imp))
                {
                    Impuesto = imp;
                }
                else
                {
                    throw new Exception("El impuesto debe ser en decimal");
                }


                if (Int32.TryParse(infoArray[7], out c))
                {
                    IdCategoria = c;
                }
                else
                {
                    throw new Exception("Debe tener una categoría");
                }


                if (Int32.TryParse(infoArray[8], out ti))
                {
                    IdTipoImpuesto = ti;
                }
                else
                {
                    throw new Exception("Debe tener un tipo de impuesto");
                }

            }



            else
            {
                throw new Exception("Los siguientes datos son requeridos para su registro: Código, Nombre, Descripción, Precio, " +
                    "Foto, Proveedor, Estado, CantProductos, Id de la categoría, Id Tipo de Impuesto, Id del Servicio");
            }

        }
    }


   
}
