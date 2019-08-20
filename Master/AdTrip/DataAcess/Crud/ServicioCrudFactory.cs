using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;

namespace DataAcess.Crud
{
    public class ServicioCrudFactory : CrudFactory
    {

        ServicioMapper mapper;

        public ServicioCrudFactory() : base()
        {
            mapper = new ServicioMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var servicio = (Servicio)entity;
            var sqlOperation = mapper.GetCreateStatement(servicio);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(Entity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var listaServicios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listaServicios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listaServicios;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var listaServicios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllByIdStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listaServicios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listaServicios;
        }

        public override void Update(Entity entity)
        {
            var servicio = (Servicio)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(servicio));
        }


        public override void Delete(Entity entity)
        {
            var servicio = (Servicio)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(servicio));
        }
    }
}
