using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class HotelCrudFactory : CrudFactory
    {
        HotelMapper mapper;

        public HotelCrudFactory() : base()
        {
            mapper = new HotelMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var Hotel = (Hotel)entity;
            var sqlOperation = mapper.GetCreateStatement(Hotel);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(Entity entity)
        {
            var lstHotel = dao.ExecuteQueryProcedure(mapper.GetRetrieveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstHotel.Count > 0)
            {
                dic = lstHotel[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstHoteles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHoteles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHoteles;
        }

        public List<T> RetrieveAllByRol<T>(Entity entity)
        {
            var Hotel = (Hotel)entity;
            var lstHoteles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllByIdStatement(Hotel));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHoteles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHoteles;
        }

        public override void Update(Entity entity)
        {
            var Hotel = (Hotel)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(Hotel));
        }

        public override void Delete(Entity entity)
        {
            var Hotel = (Hotel)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(Hotel));
        }
    }
}