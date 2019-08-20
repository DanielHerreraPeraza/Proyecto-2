using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Drawing.Imaging;
using System.Web;

namespace CoreAPI
{
    public class LlaveQRManager : BaseManager
    {
        private LlaveQRCrudFactory crudLlaveQR;
        public static Cloudinary cloudinary;
        public const string CLOUD_NAME = "datatek";
        public const string API_KEY = "678592257184526";
        public const string API_SECRET = "B2BSB98G-bfD7zU-m2VPj3y61ME";
        public static string PATH = HttpContext.Current.Server.MapPath("~/QRs/");

        public LlaveQRManager()
        {
            crudLlaveQR = new LlaveQRCrudFactory();
        }

        public async Task Create(LlaveQR llave)
        {
            try
            {
                var QR = new LlaveQR();
                do
                {
                    llave.CodigoQR = RandomString(8);
                    QR = crudLlaveQR.Retrieve<LlaveQR>(llave);

                } while (QR != null);

                llave.ImagenQR = getImagenQR(llave.CodigoQR);
                crudLlaveQR.Create(llave);
                await EnviarCorreoManager.GetInstance().ExecuteCorreoCodigoQR(llave.IdUsuario, llave);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public async Task CreateYEnviar(LlaveQR llave, Usuario usuario)
        {
            try
            {
                UsuarioManager mngU = new UsuarioManager();
                Usuario u = null;
                try
                {
                    u = mngU.RetrieveAll().FindAll(FindCorreo).First<Usuario>();


                }
                catch (Exception ex)
                {
                    throw new BussinessException(12);
                }
                //validar si existe
                if (u != null)
                {
                    //validar estado del usuario
                    switch (u.ValorEstado)
                    {
                        case "3":
                            //El usuario existe pero aún no ha verificado su cuenta
                            throw new BussinessException(8);
                        case "2":
                            //El usuario existe pero su contraseña ha expirado
                            throw new BussinessException(9);
                        case "0":
                            //El usuario existe pero se encuentra inactivo
                            throw new BussinessException(9);
                    }

                    var mngRoles = new Rol_UsuarioManager();
                    var rolUsuario = new Rol_Usuario
                    {
                        IdUsuario = u.Identificacion,
                        IdRol = "CLT"
                        
                    };
                    var roles = mngRoles.RetrieveAllById(rolUsuario);
                    //validar que el usuario sea un cliente

                    foreach (Rol_Usuario rol in roles)
                    {
                        if (rol.IdRol.Equals("CLT") == false)
                        {
                            throw new Exception("El usuario no es válido");
                        }

                    }


                    var QR = new LlaveQR();
                    do
                    {
                        llave.CodigoQR = RandomString(8);
                        QR = crudLlaveQR.Retrieve<LlaveQR>(llave);

                    } while (QR != null);

                    llave.ImagenQR = getImagenQR(llave.CodigoQR);

                    crudLlaveQR.Create(llave);
                    await EnviarCorreoManager.GetInstance().ExecuteCorreoCodigoQR(u.Correo, llave);

                }
                else
                {
                    //no existe ese usuario
                    throw new BussinessException(12);
                }

                bool FindCorreo(Usuario usu)
                {

                    if (usu.Correo == usuario.Correo)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<LlaveQR> RetrieveAll()
        {
            return crudLlaveQR.RetrieveAll<LlaveQR>();
        }

        public List<LlaveQR> RetrieveAllByReserva(LlaveQR llave)
        {
            return crudLlaveQR.RetrieveAllByReserva<LlaveQR>(llave);

        }

        public string CheckIn(Reserva res)
        {
            string salida = "";
            int idHabitacion = res.IdHabitacion;
            string idHotel = res.IdHotel;
            int codigoRes = res.Codigo;

            var crudReserva = new ReservaCrudFactory();

            var llave = new LlaveQR()
            {
                CodigoQR = res.idQR
            };

            try
            {
                llave = crudLlaveQR.Retrieve<LlaveQR>(llave);
                if (llave == null)
                {
                    // La llave QR no existe
                    throw new BussinessException(20);
                }

                var reserva = new Reserva()
                {
                    Codigo = llave.IdReserva
                };

                res = crudReserva.Retrieve<Reserva>(reserva);
                res.IdHabitacion = idHabitacion;

                if (res.IdHotel.Equals(idHotel) && llave.IdReserva == codigoRes)
                {
                    switch (llave.EstadoQR)
                    {
                        case "2":
                            // La llave QR se encuentra inactiva
                            throw new BussinessException(57);

                        case "3":
                            // La llave QR ya expiró
                            throw new BussinessException(58);
                    }

                    switch (res.Estado)
                    {
                        case "0":
                            // La reserva ya terminó!
                            throw new BussinessException(18);

                        case "1":
                            salida = "Adelante!";
                            break;

                        case "2":
                            if (res.FechaInicio.Date.CompareTo(DateTime.Now.Date) <= 0)
                            {
                                salida = "Check in exitoso!";
                                res.Estado = "1";
                                crudReserva.Update(res);
                            }
                            else
                            {
                                // La fecha no es válida!
                                throw new BussinessException(19);
                            }

                            break;

                        case "3":
                            // La reserva está cancelada!
                            throw new BussinessException(18);
                    }
                }
                else
                {
                    // El código no pertenece a esta reservación
                    throw new BussinessException(23);
                }
         
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return salida;
        }

        public string CheckOut(Reserva res)
        {
            string salida = "";

            var crudReserva = new ReservaCrudFactory();

            var llave = new LlaveQR()
            {
                CodigoQR = res.idQR
            };

            try
            {
                llave = crudLlaveQR.Retrieve<LlaveQR>(llave);
                if (llave == null)
                {
                    // La llave QR no existe
                    throw new BussinessException(20);
                }

                var reserva = new Reserva()
                {
                    Codigo = llave.IdReserva
                };

                res = crudReserva.Retrieve<Reserva>(reserva);

                llave.EstadoQR = "3";
                crudLlaveQR.Update(llave);

                res.Estado = "0";
                crudReserva.Update(res);

                salida = "Check out realizado con éxito";
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return salida;
        }

        public LlaveQR RetrieveById(LlaveQR llave)
        {
            LlaveQR p = null;
            try
            {
                p = crudLlaveQR.Retrieve<LlaveQR>(llave);
                if (p == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return p;
        }

        public void Update(LlaveQR llave)
        {
            crudLlaveQR.Update(llave);
        }

        public void Delete(LlaveQR llave)
        {
            crudLlaveQR.Delete(llave);
        }

        private string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        public string getImagenQR(string codigo)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250,
            };
            var writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;

            var qr = new ZXing.BarcodeWriter();
            qr.Options = options;
            qr.Format = ZXing.BarcodeFormat.QR_CODE;
            var bitm = new Bitmap(qr.Write(codigo));

            bitm.Save(PATH + codigo + ".jpg", ImageFormat.Jpeg);
            //Guarda la imagen en local para despues poderla subira cloudinary

            //aqui utilizo el path de nuevo para agarrar la imagen y luego subirla
            Account account = new Account(CLOUD_NAME, API_KEY, API_SECRET);
            cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(PATH + codigo + ".jpg")
            };
            var uploadResult = cloudinary.Upload(uploadParams);

            //y luego simplemente agarro el url y lo guardo en el atributo del objeto llave
            return uploadResult.SecureUri.ToString();
        }
    }
}