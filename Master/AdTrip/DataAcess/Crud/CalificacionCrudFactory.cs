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
    public class CalificacionCrudFactory : CrudFactory
    {
        CalificacionMapper mapper;

        public CalificacionCrudFactory() : base()
        {
            mapper = new CalificacionMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var calificacion = (Calificacion)entity;
            var sqlOperation = mapper.GetCreateStatement(calificacion);
            dao.ExecuteProcedure(sqlOperation);
        }

        public void CreateCalificacion(Entity entity)
        {
            var calificacion = (Calificacion)entity;
            var sqlOperation = mapper.GetCreateCalificacionStatement(calificacion);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            throw new NotImplementedException();
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

        public T RetrieveCalificacion<T>(Entity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveCalificacionStatement(entity));
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
            var lstCategoria = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCategoria.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCategoria;
        }

        public List<T> RetrieveAllCalificacion<T>()
        {
            var lstCategoria = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllCalificacionStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCategoria.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCategoria;
        }

        public override void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
