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
    public class BitacoraCrudFactory : CrudFactory
    {
        BitacoraMapper mapper;

        public BitacoraCrudFactory() : base()
        {
            mapper = new BitacoraMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var bitacora = (Bitacora)entity;
            var sqlOperation = mapper.GetCreateStatement(bitacora);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var bitacora = (Bitacora)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(bitacora));
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
            var lstCategoria = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCategoria.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCategoria;
        }

        public override void Update(Entity entity)
        {
            var categoria = (Categoria)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(categoria));
        }
    }
}
