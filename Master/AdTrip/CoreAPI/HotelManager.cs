using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class HotelManager : BaseManager
    {
        private HotelCrudFactory crudHotel;
        private ParametrizablesHotelManager mngParametrizablesHotel;

        public HotelManager()
        {
            crudHotel = new HotelCrudFactory();
            mngParametrizablesHotel = new ParametrizablesHotelManager();
        }

        public async Task Create(Hotel hotel)
        {
            FacturaCrudFactory crudFactura = new FacturaCrudFactory();
            UsuarioManager mngUsuario = new UsuarioManager();
            Factura factura = new Factura();
            try
            {
                var resFactura = new Factura();
                do
                {
                    var guid = Guid.NewGuid();
                    var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());

                    factura.NumFacturacion = int.Parse(justNumbers.Substring(0, 9));
                    resFactura = crudFactura.Retrieve<Factura>(factura);
                } while (resFactura != null);

                if (hotel.Cadena == null)
                {
                    hotel.Cadena = "";
                }

                hotel.Estado = "Activo";

                hotel.EstadoMembrecia = "Pagada";//Pendiente, Pagada
                hotel.FechaFin = DateTime.Now;
                hotel.NumFacturacion = factura.NumFacturacion;

                var c = crudHotel.Retrieve<Hotel>(hotel);

                if (c != null)
                {
                    //El hotel ya fue registrado
                    throw new BussinessException(52);
                }
                else
                {
                    crudHotel.Create(hotel);
                    ParametrizablesHotel param = new ParametrizablesHotel
                    {
                        IdHotel = hotel.CedulaJuridica
                    };
                    mngParametrizablesHotel.Create(param);

                    Usuario usuarioEncontrado = null;

                    var u = new Usuario
                    {
                        Identificacion = hotel.IdGerente
                    };

                    usuarioEncontrado = mngUsuario.RetrieveById(u);

                    var detalleFactura = new LineaDetalleFactura
                    {
                        Nombre = "Membresía",
                        Precio = (float)hotel.Membrecia,
                        Cantidad = 1,
                        SubTotal = (float)hotel.Membrecia,
                        Impuesto = 0,
                        Total = (float)hotel.Membrecia
                    };

                    ArrayList detalle = new ArrayList();
                    detalle.Add(detalleFactura);

                    string numFactura = Convert.ToString(factura.NumFacturacion);

                    await PdfManager.Create(numFactura,
                        "el pago de una membresía en AdTrip", detalle, hotel,
                        (Usuario)usuarioEncontrado);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Hotel> RetrieveAll()
        {
            return crudHotel.RetrieveAll<Hotel>();
        }

        public List<Hotel> GetHotelesPorUsuario(string id)
        {
            var mngRoles = new Rol_UsuarioManager();
            var rolUsuario = new Rol_Usuario
            {
                IdUsuario = id
            };
            List<Hotel> allHoteles = RetrieveAll();
            var roles = mngRoles.RetrieveAllById(rolUsuario);

            if (roles==null)
            {
                return allHoteles.FindAll(FindHotelesActivos);
            }

            foreach (Rol_Usuario rol in roles)
            {
                switch (rol.IdRol)
                {
                    case "ADM":
                        return allHoteles;
                    case "GRT":
                        return allHoteles.FindAll(FindHotelesGerente);
                    case "CLT":
                        return allHoteles.FindAll(FindHotelesActivos);
                    default:
                        Hotel usuarioHotel = new Hotel()
                        {
                            IdGerente = id
                        };
                        return allHoteles = crudHotel.RetrieveAllByRol<Hotel>(usuarioHotel);
                }
                
            }
            return allHoteles.FindAll(FindHotelesActivos);

            bool FindHotelesGerente(Hotel hotel)
            {

                if (hotel.IdGerente == id)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            bool FindHotelesActivos(Hotel hotel)
            {

                if (hotel.Estado == "Activo")
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

        }

        public Hotel RetrieveById(Hotel hotel)
        {
            Hotel c = null;
            try
            {
                c = crudHotel.Retrieve<Hotel>(hotel);
                //sin exceptiones para recibir el nulo
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return c;
        }

        public void Update(Hotel hotel)
        {
            //valores a modificar
            if (hotel.Cadena == null)
            {
                hotel.Cadena = "";
            }
            Hotel c = null;
            c = crudHotel.Retrieve<Hotel>(hotel);
            if (c != null)//si el hotel del id existe quiero que le cambie el estado
            {
                hotel.Estado = c.Estado;
                hotel.EstadoMembrecia = c.EstadoMembrecia;
                hotel.FechaFin = c.FechaFin;

            }
            else
            {
                throw new BussinessException(53);
            }
            crudHotel.Update(hotel);
        }

        public void CambioEstado(Hotel hotel)
        {
            //valores a modificar
            Hotel c = null;
            c = crudHotel.Retrieve<Hotel>(hotel);

            if (c != null)//si la solicitud del id existe quiero que le cambie el estado
            {
                if (c.Estado.Equals("Activo"))
                {
                    c.Estado = "Inactivo";
                }
                else
                {
                    c.Estado = "Activo";
                }
                crudHotel.Update(c);
            }
            else
            {
                throw new BussinessException(53);
            }

        }

        public void Delete(Hotel hotel)
        {
            crudHotel.Delete(hotel);
        }
    }
}