using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Entities;
using iText.Layout;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CoreAPI
{
    public class EnviarCorreoManager
    {
        private static EnviarCorreoManager instance;

        private EnviarCorreoManager() { }

        public static EnviarCorreoManager GetInstance()
        {

            if (instance == null)
            {
                instance = new EnviarCorreoManager();
            }
            return instance;
        }

        public async Task<Response> ExecuteVerificacionUsuario(Usuario usuario)
        {
            var client = new SendGridClient("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");

            var from = new EmailAddress("datatekcenfo@gmail.com", "Adtrip");
            var subject = "Verificación de su cuenta AdTrip";
            var to = new EmailAddress(usuario.Correo, usuario.PrimerNombre);
            var plainTextContent = "Gracias por registrarse en AdTrip, para continuar por favor valide su cuenta haciendo click en el siguiente link.";
            var htmlContent = @"<head>
            <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
        </head>
        <div style=""font-family: 'Montserrat', sans-serif; background-image: url('https://res.cloudinary.com/datatek/image/upload/v1563745950/correo_datatek_qpeykq.jpg'); 
        background-size: cover; "">
            <div style=""background:  rgba(0, 0, 0, 0.8); width:100%; height: 100%;"">
                <table style=""max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;"">
                    <tr>
                        <td>
                            <div style=""color: #fff; margin: 4% 10% 2%; font-family: sans-serif; text-shadow: 2px 2px 4px #000000"">
                                <div style=""width: 100%;margin:20px 0; display: inline-block;text-align: center"">
                                    <img style=""padding: 0; width: 150px; margin: 5px"" src=""https://res.cloudinary.com/datatek/image/upload/v1563746114/ADTRIP_LOGO_sfprzx.png"">
                                </div>
                                <h2 style=""color: #fff; margin: 0 0 7px; font-size: 34px; margin: 0 auto; text-align: center; padding-bottom: 40px"">¡Bienvenida/o  " + usuario.PrimerNombre + @"!</h2>
                                <p style=""margin: 2px; font-size: 22px; padding-left: 20px; color: #fff;text-align: center"">
                                    Gracias por registrarse en AdTrip, haga click en el siguiente enlace para activar su cuenta.</p>
                                <div style=""width: 100%; text-align: center; padding-top: 20px; padding-bottom: 42px"">
                                    <a style=""text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #00cccc""
                                        href=""https://adtripapi.azurewebsites.net/api/seguridad/ConfirmarUsuario/" + usuario.Identificacion + @""">Activar cuenta</a>                               
                                </div>
                                </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine(msg);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);

            return response;
        }

        public async Task<Response> ExecuteRestablecerContrasenna(Usuario usuario)
        {
            var client = new SendGridClient("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");

            var from = new EmailAddress("datatekcenfo@gmail.com", "Adtrip");
            var subject = "Restablecimiento de contraseña de cuenta AdTrip";
            var to = new EmailAddress(usuario.Correo, usuario.PrimerNombre);
            var plainTextContent = "Por favor siga los siguientes pasos para que pueda restablecer su contraseña.";
            var htmlContent = @"<head>
            <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
        </head>
        <div style=""font-family: 'Montserrat', sans-serif; background-image: url('https://res.cloudinary.com/datatek/image/upload/v1563745950/correo_datatek_qpeykq.jpg'); 
        background-size: cover; "">
            <div style=""background:  rgba(0, 0, 0, 0.8); width:100%; height: 100%;"">
                <table style=""max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;"">
                    <tr>
                        <td>
                            <div style=""color: #fff; margin: 4% 10% 2%; font-family: sans-serif; text-shadow: 2px 2px 4px #000000"">
                                <div style=""width: 100%;margin:20px 0; display: inline-block;text-align: center"">
                                    <img style=""padding: 0; width: 150px; margin: 5px"" src=""https://res.cloudinary.com/datatek/image/upload/v1563746114/ADTRIP_LOGO_sfprzx.png"">
                                </div>
                                <h2 style=""color: #fff; margin: 0 0 7px; font-size: 34px; margin: 0 auto; text-align: center; padding-bottom: 40px"">¡Hola " + usuario.PrimerNombre + @"!</h2>
                                <p style=""margin: 2px; font-size: 22px; padding-left: 20px; color: #fff;text-align: center"">
                                    Parece que ha olvidado su contraseña, así que hemos generado la siguiente contraseña temporal:</p>
                                <p style=""margin: 2px; font-size: 22px; padding-left: 20px; color: #fff;text-align: center"">
                                    ""Contraseña: " + usuario.Contrasenna + @"</p>
                                <div style=""width: 100%; text-align: center; padding-top: 20px; padding-bottom: 42px"">
                                    <a style=""text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #00cccc""
                                        href=""https://adtripapp.azurewebsites.net/Home/vLogin"">Iniciar sesión</a>                               
                                </div>
                                </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine(msg);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);

            return response;
        }

        public async Task<Response> ExecuteCorreoSolicitudAprobada(string correo, Entity entity)
        {
            var solicitud = (SolicitudHotel)entity;
            //correo = "datatekcenfo@gmail.com";
            var cedulaJ = solicitud.CedulaJuridica;
            var link = "";
            var datos = "";
            var usuario = new Usuario
            {
                Identificacion = solicitud.IdUsuario,
                Correo = correo
            };
            UsuarioManager mngU = new UsuarioManager();
            var c = mngU.Validar(usuario);
            if (c != null)
            {
                datos = ""+solicitud.CodigoSolicitud;
                link = "https://adtripapp.azurewebsites.net/Home/vLogin/";
            }
            else
            {
                datos = ""+solicitud.CodigoSolicitud;
                link = "https://adtripapp.azurewebsites.net/Home/vRegistroUsuarios/";
            }
            var membresia = solicitud.Membrecia;

            var client = new SendGridClient("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");

            var from = new EmailAddress("datatekcenfo@gmail.com", "Adtrip");//destinatario
            var subject = "Solicitud aprobada"; //asunto del correo
            var to = new EmailAddress(correo, "Usuario");
            var plainTextContent = "Solicitud de hotel aprobada, por favor continue con el registro";// texto plano         
            var htmlContent = @"<head>
            <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
        </head>
        <div style=""font-family: 'Montserrat', sans-serif; background-image: url('https://res.cloudinary.com/datatek/image/upload/v1563745950/correo_datatek_qpeykq.jpg'); 
        background-size: cover; "">
            <div style=""background:  rgba(0, 0, 0, 0.8); width:100%; height: 100%;"">
                <table style=""max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;"">
                    <tr>
                        <td>
                            <div style=""color: #fff; margin: 4% 10% 2%; font-family: sans-serif; text-shadow: 2px 2px 4px #000000"">
                                <div style=""width: 100%;margin:20px 0; display: inline-block;text-align: center"">
                                    <img style=""padding: 0; width: 150px; margin: 5px"" src=""https://res.cloudinary.com/datatek/image/upload/v1563746114/ADTRIP_LOGO_sfprzx.png"">
                                </div>
                                <h2 style=""color: #fff; margin: 0 0 7px; font-size: 34px; margin: 0 auto; text-align: center; padding-bottom: 40px"">Solicitud de hotel aprobada</h2>
                                <p style=""margin: 2px; font-size: 22px; padding-left: 20px; color: #fff;"">
                                    Por favor acepte la membresía en el registro:</p>
                                <ul style=""font-size: 18px; color: #fff; margin: 10px 0; padding-left: 50px;"">
                                    <li style=""padding: 10px;"">Membresía: $" + membresia + @"</li>
                                    <li style=""padding: 10px;"">Cédula jurídica del hotel: " + cedulaJ + @"</li>
                                </ul>
                                <div style=""width: 100%; text-align: center; padding-top: 20px; padding-bottom: 42px"">
                                    <a style=""text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #00cccc""
                                        href=""" + link + datos + @""">Ir a registro</a>                               
                                </div>
                                <p style=""color: #fff; font-size: 14px; text-align: center; margin: 30px 0 0; padding-bottom: 30px;"">Ir al registro para continuar</p>
                                </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>";
            //se crea el correo
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine(msg);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);

            return response;
        }

        public async Task<Response> ExecuteCorreoSolicitudRechazada(string correo, Entity entity)
        {

            var solicitud = (SolicitudHotel)entity;
            //correo = "datatekcenfo@gmail.com";
            var cedulaJ = solicitud.CedulaJuridica;

            var client = new SendGridClient("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");

            var from = new EmailAddress("datatekcenfo@gmail.com", "Adtrip");//destinatario
            var subject = "Solicitud rechazada"; //asunto del correo
            var to = new EmailAddress(correo, "Usuario");
            var plainTextContent = "Solicitud de hotel rechazada. El registro no cumple con las politicas de la aplicación";// texto plano         
            var htmlContent = @"<head>
            <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
        </head>
        <div style=""font-family: 'Montserrat', sans-serif; background-image: url('https://res.cloudinary.com/datatek/image/upload/v1563745950/correo_datatek_qpeykq.jpg'); 
        background-size: cover; "">
            <div style=""background:  rgba(0, 0, 0, 0.8); width:100%; height: 100%;"">
                <table style=""max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;"">
                    <tr>
                        <td>
                            <div style=""color: #fff; margin: 4% 10% 2%; font-family: sans-serif; text-shadow: 2px 2px 4px #000000"">
                                <div style=""width: 100%;margin:20px 0; display: inline-block;text-align: center"">
                                    <img style=""padding: 0; width: 150px; margin: 5px"" src=""https://res.cloudinary.com/datatek/image/upload/v1563746114/ADTRIP_LOGO_sfprzx.png"">
                                </div>
                                <h2 style=""color: #fff; margin: 0 0 7px; font-size: 34px; margin: 0 auto; text-align: center; padding-bottom: 40px"">Solicitud de hotel rechazada</h2>
                                <p style=""margin: 2px; font-size: 22px; padding-left: 20px; color: #fff;"">
                                    Por favor acepte la membresía en el registro:</p>
                                <ul style=""font-size: 18px; color: #fff; margin: 10px 0; padding-left: 50px;"">
                                    <li style=""padding: 10px;"">El administrador rechazó la solicitud.</li>
                                    <li style=""padding: 10px;"">Su hotel no cumple con las politicas de la aplicación</li>
                                    <li style=""padding: 10px;"">Cédula jurídica del hotel: " + cedulaJ + @"</li>
                                </ul>
                                <div style=""width: 100%; text-align: center; padding-top: 20px; padding-bottom: 42px"">
                                    <a style=""text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #00cccc""
                                        href=""https://adtripapp.azurewebsites.net/Home/vRegistroSolicitudHotel"">Ir a AdTrip</a>                               
                                </div>
                                <p style=""color: #fff; font-size: 14px; text-align: center; margin: 30px 0 0; padding-bottom: 30px;"">Inténtelo de nuevo</p>
                                </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>";
            //se crea el correo
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine(msg);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);

            return response;
        }

        public async Task<Response> ExecuteEnviarFactura(string correo, string pdf)
        {


            var client = new SendGridClient("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");

            var from = new EmailAddress("datatekcenfo@gmail.com", "Adtrip");
            var subject = "AdTrip - Comprobante de pago " + pdf;
            var to = new EmailAddress(correo, "Usuario");
            var plainTextContent = "AdTrip - Comprobante de pago " + pdf;
            var htmlContent = @"<head>
            <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
        </head>
        <div style=""font-family: 'Montserrat', sans-serif; background-image: url('https://res.cloudinary.com/datatek/image/upload/v1563745950/correo_datatek_qpeykq.jpg'); 
        background-size: cover; "">
            <div style=""background:  rgba(0, 0, 0, 0.8); width:100%; height: 100%;"">
                <table style=""max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;"">
                    <tr>
                        <td>
                            <div style=""color: #fff; margin: 4% 10% 2%; font-family: sans-serif; text-shadow: 2px 2px 4px #000000"">
                                <div style=""width: 100%;margin:20px 0; display: inline-block;text-align: center"">
                                    <img style=""padding: 0; width: 150px; margin: 5px"" src=""https://res.cloudinary.com/datatek/image/upload/v1563746114/ADTRIP_LOGO_sfprzx.png"">
                                </div>
                                <h2 style=""color: #fff; margin: 0 0 7px; font-size: 34px; margin: 0 auto; text-align: center; padding-bottom: 40px"">Comprobante de compra!</h2>
                                <p style=""margin: 2px; font-size: 22px; padding-left: 20px; color: #fff;text-align: center"">
                                    Gracias por utilizar AdTrip, a continuación se adjunta un archivo con su comprobante de compra.</p>
                                </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var path = HttpContext.Current.Server.MapPath("~/Pdfs/");

            var bytes = File.ReadAllBytes(path + pdf + ".pdf");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(filename: pdf + ".pdf", base64Content: file);

            Console.WriteLine(msg);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);

            return response;
        }

        public async Task<Response> ExecuteCorreoCodigoQR(string correo, Entity entity)
        {

            var llave = (LlaveQR)entity;
            //correo = "datatekcenfo@gmail.com";

            var client = new SendGridClient("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");

            var from = new EmailAddress("datatekcenfo@gmail.com", "Adtrip");//destinatario
            var subject = "Llave de habitación"; //asunto del correo
            var to = new EmailAddress(correo, "Usuario");
            var plainTextContent = "";// texto plano         
            var htmlContent = @"<head>
            <link href=""https://fonts.googleapis.com/css?family=Montserrat"" rel=""stylesheet"">
        </head>
        <div style=""font-family: 'Montserrat', sans-serif; background-image: url('https://res.cloudinary.com/datatek/image/upload/v1563745950/correo_datatek_qpeykq.jpg'); 
        background-size: cover; "">
            <div style=""background:  rgba(0, 0, 0, 0.8); width:100%; height: 100%;"">
                <table style=""max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;"">
                    <tr>
                        <td>
                            <div style=""color: #fff; margin: 4% 10% 2%; font-family: sans-serif; text-shadow: 2px 2px 4px #000000"">
                                <div style=""width: 100%;margin:20px 0; display: inline-block;text-align: center"">
                                    <img style=""padding: 0; width: 150px; margin: 5px"" src=""https://res.cloudinary.com/datatek/image/upload/v1563746114/ADTRIP_LOGO_sfprzx.png"">
                                </div>
                                <h2 style=""color: #fff; margin: 0 0 7px; font-size: 34px; margin: 0 auto; text-align: center; padding-bottom: 40px"">Su llave:</h2>
                                
                                <div style=""width: 100%; text-align: center; padding-top: 20px; padding-bottom: 42px"">
                                    <img style=""width: 250px; height: 250px;""
                                        src="""+ llave.ImagenQR + @""">                              
                                </div>
                                <p style=""color: #fff; font-size: 14px; text-align: center; margin: 30px 0 0; padding-bottom: 30px;"">La llave anterior le permitirá ingresar a
                             la habitación y comprar productos dentro del hotel. Es responsabilidad del usuario decidir a quien le compartirla esta llave.</p>
                           </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>";
            //se crea el correo
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            Console.WriteLine(msg);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response);

            return response;
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
