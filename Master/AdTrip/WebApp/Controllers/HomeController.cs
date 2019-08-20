using Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using WebApp.Security;

namespace WebApp.Controllers
{
    [SecurityFilter]
    public class HomeController : Controller
    {
        public void SetUser(Usuario u)
        {
            Session.Clear();
            Session.Add("usuario", u);
            Session.Add("permisos", GetVistasFromAPI(u.Identificacion));
        }

        public void RemoveUser()
        {
            Session.Clear();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "AdTrip";
            Session.Add("init", "prueba");

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult vCalificarHotel()
        {
            return View();
        }
        public ActionResult vDashboardAdministrador()
        {
            return View();
        }

        public ActionResult vDashboardGerente()
        {
            return View();
        }

        public ActionResult vDashboardHotel()
        {
            return View();
        }
        public ActionResult vServicios()
        {
            return View();
        }
        public ActionResult vEscanerQR()
        {
            return View();
        }
        public ActionResult vCustomers()
        {
            return View();
        }

        public ActionResult vRegistroUsuarios(string id)
        {
            if (id != null)
            {
                ViewBag.IdSolicitud = id;
            }
            else
            {
                ViewBag.IdSolicitud = "nulo";
            }
            return View();
        }

        public ActionResult vRegistroEmpleado()
        {
            return View();
        }

        public ActionResult vUsuarios()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vRoles()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vRegistroSolicitudHotel()
        {
            return View();
        }

        public ActionResult vListarSolicitudesHoteles()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vVerificarUsuario(string id)
        {
            if (id != null)
            {
                ViewBag.IdUsuario = id;
            }
            else
            {
                ViewBag.IdUsuario = "nulo";
            }

            return View();
        }

        public ActionResult vRegistroHotel(string id)
        {
            if (id != null)
            {
                ViewBag.IdSolicitud = id;
            }
            else
            {
                ViewBag.IdSolicitud = "nulo";
            }

            return View();
        }

        public ActionResult vPerfilHotel(string id)
        {
            if (id != null)
            {
                ViewBag.IdHotel = id;
            }
            else
            {
                ViewBag.IdHotel = "nulo";
            }

            return View();
        }

        public ActionResult vModificarHotel(string id)
        {
            if (id != null)
            {
                ViewBag.IdHotel = id;
            }
            else
            {
                ViewBag.IdHotel = "nulo";
            }

            return View();
        }

        public ActionResult vParametrosHotel(string id)
        {
            if (id != null)
            {
                ViewBag.IdHotel = id;
            }
            else
            {
                ViewBag.IdHotel = "nulo";
            }

            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }
        public ActionResult vBitacoraHotel(string id)
        {
            if (id != null)
            {
                ViewBag.IdHotel = id;
            }
            else
            {
                ViewBag.IdHotel = "nulo";
            }

            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }
        public ActionResult vHoteles()
        {
            return View();
        }

        public ActionResult vProductos()
        {
            //if (!TienePermiso())
              //  return View("vErrorAcceso");
            return View();
        }

        public ActionResult vPerfilServicio(string id)
        {
            if (id != null) {
                ViewBag.idServicio = id;
            } else {
                ViewBag.idServicio = "nulo";
            }
            return View();
        }

        public ActionResult vRestablecerContrasenna()
        {
            return View();
        }

        public ActionResult vCambiarContrasenna()
        {
            return View();
        }

        public ActionResult vNotificacion()
        {
            return View();
        }

        private ActionResult vErrorAcceso()
        {
            return View();
        }

        public ActionResult vComingSoon()
        {
            return View();
        }

        public ActionResult vLogin(string id)
        {
            if (id != null)
            {
                ViewBag.IdSolicitud = id;
            }
            else
            {
                ViewBag.IdSolicitud = "nulo";
            }
            if (UsuarioLogueado())
                return View("Index");
            return View();
        }

        private bool UsuarioLogueado()
        {
            Usuario usuario = (Usuario)HttpContext.Session["usuario"];
            if (usuario != null)
            {
                return true;
            }

            return false;
        }

        private bool TienePermiso()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            List<Vista> vistas = (List<Vista>)HttpContext.Session["permisos"];

            if (vistas != null)
            {
                foreach (Vista vista in vistas)
                {
                    if (vista.Id.Equals(actionName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<Vista> GetVistasFromAPI(string idUsuario)
        {
            var client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            var response = client.DownloadString("https://adtripapi.azurewebsites.net/api/vista/" + idUsuario);
            var options = JsonConvert.DeserializeObject<List<Vista>>(response);
            return options;
        }

        public ActionResult vTipoHabitaciones()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vHabitaciones()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vListadoHabitaciones()
        {
            return View();
        }

        public ActionResult vCreateHabitaciones()
        {
            return View();
        }

        public ActionResult vCategoria()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vImpuesto()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vBitacora()
        {
            if (!TienePermiso())
                return View("vErrorAcceso");
            return View();
        }

        public ActionResult vPromociones()
        {
            return View();
        }

        public ActionResult vPerfilUsuario()
        {
            return View();
        }
        public ActionResult vLlavesQR()
        {
            return View();
        }

        public ActionResult vPerfil()
        {
            return View("vComingSoon");
        }

         public ActionResult vPerfilReserva()
        {
            return View();
        }
        public ActionResult vCarrito()
        {
            return View();
        }
        public ActionResult vListadoProductos()
        {
            return View();
        }
    }
}