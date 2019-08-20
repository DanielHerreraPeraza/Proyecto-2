using Entities;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;

namespace CoreAPI
{
    public static class PdfManager
    {
        public static string DEST = HttpContext.Current.Server.MapPath("~/Pdfs/");
        public static string NEWLINE = "\n";

        public static async Task Create(string numeroFactura, string descripcion, ArrayList detalles, Hotel hotel, Usuario cliente)
        {
            var writer = new PdfWriter(DEST + numeroFactura + ".pdf");
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            var font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            var bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD, true);
            string numFactura = "Factura " + numeroFactura;

            // Header
            document.Add(
                new Paragraph()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetMultipliedLeading(1)
                    .Add(new Text(String.Format("{0} \n", numFactura))
                        .SetFont(bold).SetFontSize(14))
                    .Add(DateTime.Now.ToString("dd/MM/yyyy"))
            );

            var logo = new Image(ImageDataFactory.Create("https://res.cloudinary.com/datatek/image/upload/v1565550484/Imagenes%20default/WhatsApp_Image_2019-08-11_at_1.02.53_PM_s8xxeq.jpg"))
                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
            document.Add(logo);

            // Motivo de factura
            document.Add(new Paragraph(String.Format("Factura por {0}", descripcion))
                .SetFont(bold).SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER));

            // Partes
            document.Add(GetAddressTable(hotel, cliente, bold));

            // Detalles
            document.Add(GetLineItemTable(detalles, bold));

            // Total
            float total = 0;
            foreach(LineaDetalleFactura item in detalles)
            {
                total += item.Total;
            }
            document.Add(GetTotalsTable(total, bold));

            document.Close();


           await EnviarCorreoManager.GetInstance().ExecuteEnviarFactura(cliente.Correo, numeroFactura);

        }

        public static Table GetLineItemTable(ArrayList detalles, PdfFont bold)
        {


            NumberFormatInfo formatonumero = new NumberFormatInfo();

            formatonumero.CurrencySymbol = "$";

            Table table = new Table(
            new UnitValue[]{
                new UnitValue(UnitValue.PERCENT, 43.75f),
                new UnitValue(UnitValue.PERCENT, 12.5f),
                new UnitValue(UnitValue.PERCENT, 6.25f),
                new UnitValue(UnitValue.PERCENT, 12.5f),
                new UnitValue(UnitValue.PERCENT, 12.5f),
                new UnitValue(UnitValue.PERCENT, 12.5f)
            })
            .SetTextAlignment(TextAlignment.CENTER)
            .UseAllAvailableWidth()
            .SetMarginTop(10).SetMarginBottom(10);
            table.AddHeaderCell(CreateCell("Item:", bold).SetTextAlignment(TextAlignment.LEFT));
            table.AddHeaderCell(CreateCell("Precio:", bold).SetTextAlignment(TextAlignment.LEFT));
            table.AddHeaderCell(CreateCell("Cant:", bold).SetTextAlignment(TextAlignment.LEFT));
            table.AddHeaderCell(CreateCell("Subtotal:", bold).SetTextAlignment(TextAlignment.LEFT));
            table.AddHeaderCell(CreateCell("I.V:", bold).SetTextAlignment(TextAlignment.LEFT));
            table.AddHeaderCell(CreateCell("Total:", bold).SetTextAlignment(TextAlignment.LEFT));

            foreach (LineaDetalleFactura item in detalles)
            {
                table.AddCell(CreateCell(item.Nombre).SetTextAlignment(TextAlignment.LEFT));
                table.AddCell(CreateCell(item.Precio.ToString("C", formatonumero)))
                        .SetTextAlignment(TextAlignment.RIGHT);
                table.AddCell(CreateCell(item.Cantidad.ToString()))
                    .SetTextAlignment(TextAlignment.RIGHT);
                table.AddCell(CreateCell(item.SubTotal.ToString("C", formatonumero)))
                    .SetTextAlignment(TextAlignment.RIGHT);
                table.AddCell(CreateCell(item.Impuesto.ToString() +"%"))
                    .SetTextAlignment(TextAlignment.RIGHT);
                table.AddCell(CreateCell(item.Total.ToString("C", formatonumero)))
                    .SetTextAlignment(TextAlignment.RIGHT);
            }
            return table;
        }

        public static Cell CreateCell(string text)
        {
            return new Cell().SetPadding(0.8f)
                .Add(new Paragraph(text)
                    .SetMultipliedLeading(1));
        }

        public static Cell CreateCell(string text, PdfFont font)
        {
            return new Cell().SetPadding(0.8f)
                .Add(new Paragraph(text)
                    .SetFont(font).SetMultipliedLeading(1));
        }

        public static Table GetAddressTable(Hotel hotel, Usuario cliente, PdfFont bold)
        {
            Table table = new Table(new UnitValue[]{
            new UnitValue(UnitValue.PERCENT, 50),
            new UnitValue(UnitValue.PERCENT, 50)})
                    .UseAllAvailableWidth();
            table.AddCell(GetPartesInfo("Hotel:", hotel.Nombre, hotel.CedulaJuridica, hotel.Correo, hotel.Direccion,
                bold));

            string nombreCliente = cliente.PrimerNombre + " " + cliente.PrimerApellido + " " + cliente.SegundoApellido;
            table.AddCell(GetPartesInfo("Cliente:", nombreCliente, cliente.Identificacion, cliente.Correo, cliente.DireccionExacta,
                bold));
            return table;
        }

        public static Cell GetPartesInfo(String who, string nombre, string id, string correo, string direccion, PdfFont bold)
        {
            Paragraph p = new Paragraph()
                .SetMultipliedLeading(1.0f)
                .Add(new Text(who).SetFont(bold)).Add(NEWLINE)
                .Add(nombre).Add(NEWLINE)
                .Add(id).Add(NEWLINE)
                .Add(correo).Add(NEWLINE)
                .Add(direccion);
            Cell cell = new Cell()
                .SetBorder(Border.NO_BORDER)
                .Add(p);
            return cell;
        }

        public static Table GetTotalsTable(float total, PdfFont bold)
        {

            NumberFormatInfo formatonumero = new NumberFormatInfo();

            formatonumero.CurrencySymbol = "$";

            
            Table table = new Table(
                new UnitValue[]{
                    new UnitValue(UnitValue.PERCENT, 75f),
                    new UnitValue(UnitValue.PERCENT, 25f)
                })
                .UseAllAvailableWidth();
            table.AddCell(CreateCell(""));
            table.AddCell(CreateCell("Total", bold)
                .SetTextAlignment(TextAlignment.LEFT));

            table.AddCell(CreateCell(""));
            table.AddCell(CreateCell(total.ToString("C", formatonumero)))
                    .SetTextAlignment(TextAlignment.RIGHT);

            return table;


            
        }
    }

}
