using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Crud
{
    public class ReporteCrudFactory : CrudFactory
    {

        ReporteMapper mapper;

        public ReporteCrudFactory() : base()
        {
            mapper = new ReporteMapper();
            dao = SqlDao.GetInstance();
        }


        /*GET GANANCIAS TOTOALES*/

        public T RetrieveGananciasTotalesAdmin<T>()
        {

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasTotalesAdmin());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }


        /*GET GANANCIAS X MES*/
        public List<T> RetrieveGananciasXMesAdmin<T>()
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasXMesAdmin());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }

        /*GET COMISION X DIA*/
        public List<T> RetrieveGananciasComisionXDiaAdmin<T>() { 
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasComisionXDiaAdmin());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }


        /*GET COMISION X DIA*/
        public List<T> RetrieveGananciasMembresiaXMesAdmin<T>()
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasMembresiaXMesAdmin());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }


        /*GET TOTALES GERENTE*/
        public List<T> RetrieveGananciasTotalesGerente<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasTotalesGerente(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }



        /*GET GANANCIA X DIA GERENTE*/
        public List<T> RetrieveGananciasXDiaGerente<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasXDiaGerente(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }

        /*GET CANTIDAD DE HABITACIONES POR HOTEL DEL GERENTE*/
        public List<T> RetrieveCantHabHoteles<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveCantHabHoteles(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }



        /*GET TOTAL DE LAS GANANCIAS DE UN HOTEL*/
        public List<T> RetrieveGananciasTotalHotel<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasTotalesHotel(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }


        /*GET GANANCIAS POR MES DE UN HOTEL*/
        public List<T> RetrieveGananciaXMesHotel<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveGananciasXMesHotel(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }

        /*GET CANTIDAD DE HABITACIONES POR TIPO DE UN HOTEL*/
        public List<T> RetrieveCantHabTipo<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveCantHabXTipo(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }


        /*GET DISPONIBILIDAD DE HABITACIONES DE UN HOTEL*/
        public List<T> RetrieveDisponibilidadHab<T>(Entity entity)
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveDisponibilidadHab(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }



        public override void Create(Entity entity)
        {
            var reporte = (Reporte)entity;
            var sqlOperation = mapper.GetCreateStatement(reporte);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(Entity entity)
        {
            var lstReporte = dao.ExecuteQueryProcedure(mapper.GetRetrieveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstReporte.Count > 0)
            {
                dic = lstReporte[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstReportes = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReportes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstReportes;
        }

        public override void Update(Entity entity)
        {
            var reporte = (Reporte)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(reporte));
        }

        public override void Delete(Entity entity)
        {
            var reporte = (Reporte)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(reporte));
        }



    }
}
