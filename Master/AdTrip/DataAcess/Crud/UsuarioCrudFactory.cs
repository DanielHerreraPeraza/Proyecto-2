using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Crud
{
    public class UsuarioCrudFactory : CrudFactory
    {
        UsuarioMapper mapper;

        public UsuarioCrudFactory() : base()
        {
            mapper = new UsuarioMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(Entity entity)
        {
            var usuario = (Usuario)entity;
            var sqlOperation = mapper.GetCreateStatement(usuario);
            dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(Entity entity)
        {
            var usuario = (Usuario)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(usuario));
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

        public T ValidarUsuario<T>(Entity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetValidarUsuarioStatement(entity));
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
            var lstUsuarios = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstUsuarios.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsuarios;
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(Entity entity)
        {
            var usuario = (Usuario)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(usuario));
        }

        public void UpdateContrasenna(Entity entity)
        {
            var usuario = (Usuario)entity;
            dao.ExecuteProcedure(mapper.GetUpdateContrasennaStatement(usuario));
        }

        public void UpdateEstado(Entity entity)
        {
            var usuario = (Usuario)entity;
            dao.ExecuteProcedure(mapper.GetUpdateEstadoStatement(usuario));
        }

        public T ValidarUsuarioGoogle<T>(Entity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetValidarUsuarioGoogleStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public T Exists<T>(Entity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetExistsStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }
    }
}
