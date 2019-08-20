using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class CustomerCrudFactory : CrudFactory
    {
        CustomerMapper mapper;

        public CustomerCrudFactory() : base()
        {
            mapper = new CustomerMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var customer=(Customer) entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
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
            var lstCustomers = new List<T>();
            
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
           
            return lstCustomers;
        }

        public override void Update(Entity entity)
        {
            var customer = (Customer)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(Entity entity)
        {
            var customer = (Customer)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

        //public override List<T> RetrieveAllById<T>(Entity entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
