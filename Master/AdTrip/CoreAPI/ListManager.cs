using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;

namespace CoreAPI
{
    public class ListManager : BaseManager
    {
        private Dictionary<string, List<OptionList>> dicListOptions;
        private RolCrudFactory crudRol;
        private VistaCrudFactory crudVista;
        private ListaValoresCrudFactory crudListaValores;
        private TipoHabitacionesCrudFactory crudTipoHabitaciones;
        private HotelCrudFactory crudHoteles;

        public ListManager()
        {
            crudRol = new RolCrudFactory();
            crudVista = new VistaCrudFactory();
            crudListaValores = new ListaValoresCrudFactory();
            crudTipoHabitaciones = new TipoHabitacionesCrudFactory();
            crudHoteles = new HotelCrudFactory();
            LoadDictionary();
        }

        private void LoadDictionary()
        {
            dicListOptions = new Dictionary<string, List<OptionList>>();
            
            dicListOptions.Add("LST_TIPO_HABITACIONES", LoadTipoHabitaciones());

            dicListOptions.Add("LST_VISTA", LoadVistas());
            LoadListaValores();
        }

        public List<OptionList> RetrieveById(OptionList option)
        {

            try
            {
                if (option.ListId == "LST_TIPO_HABITACIONES")
                {
                    return LoadTipoHabitaciones();
                }
                if (option.ListId == "LST_HOTEL")
                {
                    return LoadHoteles();
                }
                if (option.ListId == "LST_ROL")
                {
                    return LoadRoles();
                }
                else if (dicListOptions.ContainsKey(option.ListId))
                {
                    return dicListOptions[option.ListId];
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return new List<OptionList>(); ;
        }

        private List<OptionList> LoadRoles()
        {
            var lst = new List<OptionList>();
            foreach (Rol rol in crudRol.RetrieveAll<Rol>())
            {
                var option = new OptionList
                {
                    ListId = "LST_ROL",
                    Codigo = rol.Codigo,
                    Nombre = rol.Nombre
                };
                lst.Add(option);
            }
            return lst;
        }

        private List<OptionList> LoadHoteles()
        {
            var lst = new List<OptionList>();
            foreach (Hotel hotel in crudHoteles.RetrieveAll<Hotel>())
            {
                var option = new OptionList
                {
                    ListId = "LST_HOTEL",
                    Codigo = hotel.CedulaJuridica,
                    Nombre = hotel.Nombre,
                    Extra = hotel.IdGerente
                };
                lst.Add(option);
            }
            return lst;
        }

        private List<OptionList> LoadVistas()
        {
            var lst = new List<OptionList>();
            foreach (Vista vista in crudVista.RetrieveAll<Vista>())
            {
                var option = new OptionList
                {
                    ListId = "LST_VISTA",
                    Codigo = vista.Id,
                    Nombre = vista.Definicion
                };
                lst.Add(option);
            }
            return lst;
        }

        private void LoadListaValores()
        {
            foreach (OptionList option in crudListaValores.RetrieveAll<OptionList>())
            {
                if (dicListOptions.ContainsKey(option.ListId))
                {
                    dicListOptions[option.ListId].Add(option);
                }
                else
                {
                    var lst = new List<OptionList>();
                    lst.Add(option);
                    dicListOptions.Add(option.ListId, lst);
                }
            }
        }

        public List<OptionList> LoadTipoHabitaciones()
        {
            var lst = new List<OptionList>();
            foreach (TipoHabitaciones tipoHabitaciones in crudTipoHabitaciones.RetrieveAll<TipoHabitaciones>())
            {
                var option = new OptionList
                {
                    ListId = "LST_TIPO_HABITACIONES",
                    Codigo = tipoHabitaciones.Codigo,
                    Nombre = tipoHabitaciones.Nombre
                };
                lst.Add(option);
            }
            return lst;
        }


    }
}
