using System;
using System.Collections.Generic;
using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;

namespace DataAcess.Crud
{
    public class RolCrudFactory : CrudFactory
    {
        RolMapper mapper;

        public RolCrudFactory() : base()
        {
            mapper = new RolMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var rol = (Rol)entity;
            var sqlOperation = mapper.GetCreateStatement(rol);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var rol = (Rol)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(rol));
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
            var lstRoles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRoles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRoles;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Entity entity)
        {
            var rol = (Rol)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(rol));
        }

        public List<T> RetrieveByGerente<T>(String idGerente)
        {
            var lstRoles = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllByGerenteStatement(idGerente));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRoles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRoles;
        }
    }
}
