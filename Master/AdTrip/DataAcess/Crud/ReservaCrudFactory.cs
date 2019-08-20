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
    public class ReservaCrudFactory : CrudFactory
    {

        ReservaMapper mapper;

        public ReservaCrudFactory() 
        {
            mapper = new ReservaMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var reserva = (Reserva)entity;
            var sqlOperation = mapper.GetCreateStatement(reserva);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var reserva = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(reserva));
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

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var lstReserva = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetAllByIdRetrieveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReserva.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstReserva;
        }

        public List<T> RetrieveAllHabsReserva<T>(Entity entity)
        {
            var lstReserva = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetAllHabitacionesReservaStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReserva.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstReserva;
        }


        



        public override List<T> RetrieveAll<T>()
        {
            var lstReserva = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReserva.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstReserva;
        }


        public override void Update(Entity entity)
        {
            var reserva = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(reserva));
        }



        public T VerificarCantHabXTipo<T>(Entity entity)
        {
            
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetVerCantHabXTipoStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);

        }


        public T VerificarDisponibilidadHabitaciones<T>(Entity entity)
        {
     
            var lstResult = dao.ExecuteQueryProcedure(mapper.GeVerDispHabStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public void AsignarHabitacionReserva(Entity entity)
        {
            var reserva = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetAsignarHabReservaStatement(reserva));
        }


        public List<T> ObtenerCodigoHabitacionesReserva<T>(Entity entity)
        {
            var lstReserva = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetCodigoHabitacionesReserva(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstReserva.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstReserva;

        }

        public void AsignarQRReservaReserva(Entity entity)
        {
            var reserva = (Reserva)entity;
            dao.ExecuteProcedure(mapper.GetAsignarQRReserva(reserva));
        }
    }
}

