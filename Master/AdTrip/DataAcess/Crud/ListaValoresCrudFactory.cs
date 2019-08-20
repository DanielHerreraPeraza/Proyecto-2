using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Crud
{
    public class ListaValoresCrudFactory : CrudFactory
    {
        ListaValoresMapper mapper;

        public ListaValoresCrudFactory() : base()
        {
            mapper = new ListaValoresMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstValores = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstValores.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstValores;
        }

        public  List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
