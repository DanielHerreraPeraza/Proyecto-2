using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Crud
{
    public class ProductoCrudFactory : CrudFactory
    {
        ProductoMapper mapper;

        public ProductoCrudFactory() : base()
        {
            mapper = new ProductoMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var producto = (Producto)entity;
            var sqlOperation = mapper.GetCreateStatement(producto);
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
            var lstProductos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllByHotelStatement(IdHotel));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstProductos.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstProductos;
        }

        public override List<T> RetrieveAll<T>()
        {
            var listaProductos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listaProductos.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listaProductos;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var listaProductos = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllByIdStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    listaProductos.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return listaProductos;
        }

        public override void Update(Entity entity)
        {
            var producto = (Producto)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(producto));
        }


        public override void Delete(Entity entity)
        {
            var producto = (Producto)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(producto));
        }
    }
}
