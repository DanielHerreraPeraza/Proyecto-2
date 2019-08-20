using System;
using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class Rol_UsuarioMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_ROL = "ID_ROL";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var rol_Usuario = new Rol_Usuario
            {
                IdRol = GetStringValue(row, DB_COL_ID_ROL),
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO)
            };

            return rol_Usuario;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var usuario = BuildObject(row);
                lstResults.Add(usuario);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_ROL_USUARIO_PR" };

            var ru = (Rol_Usuario)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdRol);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, ru.IdUsuario);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_ROL_USUARIO_PR" };

            var ru = (Rol_Usuario)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdRol);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, ru.IdUsuario);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_USUARIOS_ROL_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllRolesUsuarioStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_USUARIO_PR" };

            var ru = (Rol_Usuario)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, ru.IdUsuario);

            return operation;
        }
    }
}
