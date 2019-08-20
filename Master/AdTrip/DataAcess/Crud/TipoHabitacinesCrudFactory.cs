using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class TipoHabitacionesCrudFactory : CrudFactory
    {
        TipoHabitacionesMapper mapper;

        public TipoHabitacionesCrudFactory() : base()
        {
            mapper = new TipoHabitacionesMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var tipoHabitaciones = (TipoHabitaciones)entity;
            var sqlOperation = mapper.GetCreateStatement(tipoHabitaciones);
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
            var lstTipoHabitaciones = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllByHotelStatement(IdHotel));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstTipoHabitaciones.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstTipoHabitaciones;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstTipoHabitaciones = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstTipoHabitaciones.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstTipoHabitaciones;
        }

        public override void Update(Entity entity)
        {
            var tipoHabitaciones = (TipoHabitaciones)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(tipoHabitaciones));
        }

        public override void Delete(Entity entity)
        {
            var tipoHabitaciones = (TipoHabitaciones)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(tipoHabitaciones));
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
