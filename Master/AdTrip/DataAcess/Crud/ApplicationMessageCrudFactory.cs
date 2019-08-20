using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;


namespace DataAcess.Crud
{
    public class ApplicationMessageCrudFactory : CrudFactory
    {
        ApplicationMessageMapper mapper;

        public ApplicationMessageCrudFactory() : base()
        {
            mapper = new ApplicationMessageMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var appM = (ApplicationMessage)entity;
            var sqlOperation = mapper.GetCreateStatement(appM);
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
            var lstAppM = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstAppM.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstAppM;
        }

        public override void Update(Entity entity)
        {
            var appM = (ApplicationMessage)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(appM));
        }

        public override void Delete(Entity entity)
        {
            var appM = (ApplicationMessage)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(appM));
        }
    }
}