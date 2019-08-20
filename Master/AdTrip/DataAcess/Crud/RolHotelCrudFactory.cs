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
    public class RolHotelCrudFactory : CrudFactory
    {
        RolHotelMapper mapper;

        public RolHotelCrudFactory() : base()
        {
            mapper = new RolHotelMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var rolHotel= (RolHotel)entity;
            var sqlOperation = mapper.GetCreateStatement(rolHotel);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var rolHotel = (RolHotel)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(rolHotel));
        }

        public override T Retrieve<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstRolesHotel = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRolesHotel.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRolesHotel;
        }

        public override void Update(Entity entity)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var lstRolesHotel = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllRolesHotelStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRolesHotel.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRolesHotel;
        }
    }
}
