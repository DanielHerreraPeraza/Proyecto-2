using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class CarritoCrudFactory : CrudFactory
    {
        CarritoMapper mapper;

        public CarritoCrudFactory() : base()
        {
            mapper = new CarritoMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var Carrito = (Carrito)entity;
            var sqlOperation = mapper.GetCreateStatement(Carrito);
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

        public List<T> RetrieveByReservaId<T>(Entity entity)
        {
            var LstCarrito = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByReservaStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    LstCarrito.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return LstCarrito;
        }

        public override List<T> RetrieveAll<T>()
        {
            var LstCarrito = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    LstCarrito.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstCarrito;
        }

        public override void Update(Entity entity)
        {
            var carrito = (Carrito)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(carrito));
        }

        public override void Delete(Entity entity)
        {
            var carrito = (Carrito)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(carrito));
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
