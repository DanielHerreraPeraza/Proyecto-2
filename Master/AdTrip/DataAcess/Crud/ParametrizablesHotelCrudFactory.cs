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
    public class ParametrizablesHotelCrudFactory : CrudFactory

    {

        ParametrizablesHotelMapper mapper;


        public ParametrizablesHotelCrudFactory() : base()
        {
            mapper = new ParametrizablesHotelMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var parametro = (ParametrizablesHotel)entity;
            var sqlOperation = mapper.GetCreateStatement(parametro);
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
            var lstParametros = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstParametros.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstParametros;
        }

        public override void Update(Entity entity)
        {
            var parametro = (ParametrizablesHotel)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(parametro));
        }

        public override void Delete(Entity entity)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var lstParametros = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllByIdStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstParametros.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstParametros;
        }
    }
}