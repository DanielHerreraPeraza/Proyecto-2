using System;
using System.Collections.Generic;
using DataAcess.Mapper;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public class HabitacionesCrudFactory : CrudFactory
    {
        HabitacionesMapper mapper;

        public HabitacionesCrudFactory() : base()
        {
            mapper = new HabitacionesMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var habitaciones = (Habitaciones)entity;
            var sqlOperation = mapper.GetCreateStatement(habitaciones);
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
            var lstHabitaciones = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveByHotelStatement(IdHotel));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHabitaciones.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstHabitaciones;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstHabitaciones = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstHabitaciones.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstHabitaciones;
        }

        public override void Update(Entity entity)
        {
            var habitaciones = (Habitaciones)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(habitaciones));
        }

        public override void Delete(Entity entity)
        {
            var habitaciones = (Habitaciones)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(habitaciones));
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }



     

        
    }
}
