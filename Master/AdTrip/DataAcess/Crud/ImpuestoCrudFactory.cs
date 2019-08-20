using System;
using System.Collections.Generic;
using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;

namespace DataAcess.Crud
{
    public class ImpuestoCrudFactory : CrudFactory
    {

        TipoImpuestoMapper mapper;

        public ImpuestoCrudFactory() : base()
        {
            mapper = new TipoImpuestoMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var impuesto = (TipoImpuesto)entity;
            var sqlOperation = mapper.GetCreateStatement(impuesto);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var impuesto = (TipoImpuesto)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(impuesto));
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
            var lstImpuesto = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstImpuesto.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstImpuesto;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Entity entity)
        {
            var impuesto = (TipoImpuesto)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(impuesto));
        }
    }
}
