using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Crud
{
    public class Vista_RolCrudFactory : CrudFactory
    {

        Vista_RolMapper mapper;

        public Vista_RolCrudFactory() : base()
        {
            mapper = new Vista_RolMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var vistaRol = (Vista_Rol)entity;
            var sqlOperation = mapper.GetCreateStatement(vistaRol);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var vistaRol = (Vista_Rol)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(vistaRol));
        }

        public override T Retrieve<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstVistasRol = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstVistasRol.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstVistasRol;
        }

        public List<T> RetrieveAllVistasByRolId<T>(Entity entity)
        {
            var lstVistasRol = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllVistasRolStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstVistasRol.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstVistasRol;
        }

        public List<T> RetrieveAllRolesByVistaId<T>(Entity entity)
        {
            var lstVistasRol = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllRolesVistaStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstVistasRol.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstVistasRol;
        }
        public override void Update(Entity entity)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var lstVistasRol = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstVistasRol.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstVistasRol;
        }
    }
}
