using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Crud
{
    public class Rol_UsuarioCrudFactory : CrudFactory
    {
        Rol_UsuarioMapper mapper;

        public Rol_UsuarioCrudFactory() : base()
        {
            mapper = new Rol_UsuarioMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var rolUsuario = (Rol_Usuario)entity;
            var sqlOperation = mapper.GetCreateStatement(rolUsuario);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var rolUsuario = (Rol_Usuario)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(rolUsuario));
        }

        public override T Retrieve<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstRolesUsuarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRolesUsuarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRolesUsuarios;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var lstRolesUsuario = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllRolesUsuarioStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRolesUsuario.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRolesUsuario;
        }

        public override void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
