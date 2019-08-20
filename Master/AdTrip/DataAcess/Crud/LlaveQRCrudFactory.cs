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
    public class LlaveQRCrudFactory : CrudFactory
    {
        LlaveQRMapper mapper;

        public LlaveQRCrudFactory() : base()
        {
            mapper = new LlaveQRMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var QR = (LlaveQR)entity;
            var sqlOperation = mapper.GetCreateStatement(QR);
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

        public override List<T> RetrieveAll<T>()
        {
            var lstQR = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstQR.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstQR;
        }

        public List<T> RetrieveAllByReserva<T>(Entity entity)
        {
            var lstQR = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllByReservaStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstQR.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstQR;
        }

        public override void Update(Entity entity)
        {
            var QR = (LlaveQR)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(QR));
        }

        public override void Delete(Entity entity)
        {
            var QR = (LlaveQR)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(QR));
        }

        public void DeleteAllByReserva(Entity entity)
        {
            var QR = (LlaveQR)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(QR));
        }
    }
}