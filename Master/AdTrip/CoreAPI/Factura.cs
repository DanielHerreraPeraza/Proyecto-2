using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class FacturaManager : BaseManager
    {
        private FacturaCrudFactory crudFactura;

        public FacturaManager()
        {
            crudFactura = new FacturaCrudFactory();
        }

        public void Create(Factura factura)
        {
            crudFactura.Create(factura);
            /*
            try
            {
                var f = crudFactura.Retrieve<Factura>(factura);
               
                if (f != null)
                {
                    throw new BussinessException(1);
                }
               
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }*/

        }

        public List<Factura> RetrieveAll()
        {
            return crudFactura.RetrieveAll<Factura>();
        }

        public List<Factura> RetrieveByHotelId(string CedJuridica)
        {
            return crudFactura.RetrieveByHotelId<Factura>(CedJuridica);
        }

        public Factura RetrieveById(Factura factura)
        {
            Factura f = null;
            try
            {
                f = crudFactura.Retrieve<Factura>(factura);
                if (f == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return f;
        }

        public void Update(Factura factura)
        {
            crudFactura.Update(factura);
        }

        public void Delete(Factura factura)
        {
            crudFactura.Delete(factura);
        }

        public async Task CreatePdfAsync()
        {
            var hotel = new Hotel()
            {
                Nombre = "Marriott los sueños",
                CedulaJuridica = "12345678945",
                Correo = "marriott@gmail.com",
                Direccion = "200 mtrs al norte de la escuela de San Pedro, posfhksf kjsadhfk sjh fksh dflsjdl"
            };

            var cliente = new Usuario()
            {
                PrimerNombre = "Daniel",
                PrimerApellido = "Herrera",
                SegundoApellido = "Peraza",
                Identificacion = "115800345",
                Correo = "danieljhp@gmail.com",
                DireccionExacta = "Frente a la municipalidad"
            };

            var detalles = new ArrayList()
                {
                    new LineaDetalleFactura()
                    {
                        Nombre = "Inscripción hotel",
                        Precio = 2500,
                        Cantidad = 1,
                        SubTotal = 2500,
                        Impuesto =  10,
                        Total = 2750
                    },
                    new LineaDetalleFactura()
                    {
                        Nombre = "Servicio al cuarto",
                        Precio = 500,
                        Cantidad = 2,
                        SubTotal = 1000,
                        Impuesto =  10,
                        Total = 1100
                    }
                };

            var numeroFactura = "17";
            var descripcionFactura = "Inscripción de hotel";

            await PdfManager.Create(numeroFactura, descripcionFactura, detalles, hotel, cliente);
            await EnviarCorreoManager.GetInstance().ExecuteEnviarFactura(cliente.Correo, numeroFactura);
        }
    }
}
