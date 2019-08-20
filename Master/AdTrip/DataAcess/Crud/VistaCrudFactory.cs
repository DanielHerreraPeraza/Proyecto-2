using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Crud
{
    public class VistaCrudFactory : CrudFactory
    {
        VistaMapper mapper;

        public VistaCrudFactory() : base()
        {
            mapper = new VistaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var vista = (Vista)entity;
            var sqlOperation = mapper.GetCreateStatement(vista);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var vista = (Vista)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(vista));
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
            var lstVistas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstVistas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstVistas;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var lstVistas = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllVistasUsuarioStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstVistas.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstVistas;
        }

        public override void Update(Entity entity)
        {
            var vista = (Vista)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(vista));
        }
    }
}
