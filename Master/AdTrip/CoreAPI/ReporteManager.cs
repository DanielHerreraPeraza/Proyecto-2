using DataAcess.Crud;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class ReporteManager
    {
        private ReporteCrudFactory crudReporte;

        public ReporteManager()
        {
            crudReporte = new ReporteCrudFactory();
        }


        public Reporte RetrieveGananciasTotalesAdmin()
        {
            Reporte r = null;

            r = crudReporte.RetrieveGananciasTotalesAdmin<Reporte>();

            return r;
        }


        public List<Reporte> RetrieveGananciasXMesAdmin()
        {
            return crudReporte.RetrieveGananciasXMesAdmin<Reporte>();
        }


        public List<Reporte> RetrieveGananciasComisionXDiaAdmin()
        {
            return crudReporte.RetrieveGananciasComisionXDiaAdmin<Reporte>();
        }

        public List<Reporte> RetrieveGananciaMembresiaXMesAdmin()
        {
            return crudReporte.RetrieveGananciasMembresiaXMesAdmin<Reporte>();
        }


        public List<Reporte> RetrieveGananciasTotalesGerente(Reporte reporte)
        {
            return crudReporte.RetrieveGananciasTotalesGerente<Reporte>(reporte);
        }


        public List<Reporte> RetrieveGananciaXDiaGerente(Reporte reporte)
        {
            
            return crudReporte.RetrieveGananciasXDiaGerente<Reporte>(reporte);
        }


        public List<Reporte> RetrieveCantHabHotel(Reporte reporte)
        {

            return crudReporte.RetrieveCantHabHoteles<Reporte>(reporte);
        }



        public List<Reporte> RetrieveGananciasTotalHotel(Reporte reporte)
        {

            return crudReporte.RetrieveGananciasTotalHotel<Reporte>(reporte);
        }

        public List<Reporte> RetrieveGananciaXMesHotel(Reporte reporte)
        {

            return crudReporte.RetrieveGananciaXMesHotel<Reporte>(reporte);
        }

        public List<Reporte> RetrieveCantHabTipo(Reporte reporte)
        {

            return crudReporte.RetrieveCantHabTipo<Reporte>(reporte);
        }

        public List<Reporte> RetrieveDisponibilidadHab(Reporte reporte)
        {

            return crudReporte.RetrieveDisponibilidadHab<Reporte>(reporte);
        }

    }
}
