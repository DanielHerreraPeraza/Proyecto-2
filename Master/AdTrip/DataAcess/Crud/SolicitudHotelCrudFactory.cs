using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class SolicitudHotelCrudFactory : CrudFactory
    {
        SolicitudHotelMapper mapper;

        public SolicitudHotelCrudFactory() : base()
        {
            mapper = new SolicitudHotelMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var solicitudHotel = (SolicitudHotel)entity;
            var sqlOperation = mapper.GetCreateStatement(solicitudHotel);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(Entity entity)
        {
            var lstSolicitudHotel = dao.ExecuteQueryProcedure(mapper.GetRetrieveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstSolicitudHotel.Count > 0)
            {
                dic = lstSolicitudHotel[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstSolicitudesHoteles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstSolicitudesHoteles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstSolicitudesHoteles;
        }

        public override void Update(Entity entity)
        {
            var solicitudHotel = (SolicitudHotel)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(solicitudHotel));
        }

        public override void Delete(Entity entity)
        {
            var solicitudHotel = (SolicitudHotel)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(solicitudHotel));
        }
    }
}
