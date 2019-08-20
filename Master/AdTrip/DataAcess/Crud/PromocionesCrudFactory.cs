using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class PromocionesCrudFactory : CrudFactory
    {
        PromocionesMapper mapper;

        public PromocionesCrudFactory() : base()
        {
            mapper = new PromocionesMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var promociones = (Promociones)entity;
            var sqlOperation = mapper.GetCreateStatement(promociones);
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

        public List<T> RetrieveByHotelId<T>(string IdHotel)
        {
            var lstPromociones = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByHotelStatement(IdHotel));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstPromociones.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstPromociones;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstPromociones = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstPromociones.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstPromociones;
        }

        public override void Update(Entity entity)
        {
            var promociones = (Promociones)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(promociones));
        }

        public override void Delete(Entity entity)
        {
            var promociones = (Promociones)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(promociones));
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
