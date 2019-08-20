using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class FacturaCrudFactory : CrudFactory
    {
        FacturaMapper mapper;

        public FacturaCrudFactory() : base()
        {
            mapper = new FacturaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var factura = (Factura)entity;
            var sqlOperation = mapper.GetCreateStatement(factura);
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

        public List<T> RetrieveByHotelId<T>(string CedJuridica)
        {
            var lstFactura = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByHotelStatement(CedJuridica));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstFactura.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstFactura;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstFactura = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstFactura.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstFactura;
        }

        public override void Update(Entity entity)
        {
            var factura = (Factura)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(factura));
        }

        public override void Delete(Entity entity)
        {
            var factura = (Factura)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(factura));
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
