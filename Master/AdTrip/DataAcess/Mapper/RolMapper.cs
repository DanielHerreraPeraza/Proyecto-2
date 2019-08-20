using System;
using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class RolMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";
        private const string DB_COL_ID_GERENTE = "ID_GERENTE";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var rol = new Rol
            {
                Codigo = GetStringValue(row, DB_COL_CODIGO),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO)
            };

            return rol;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var rol = BuildObject(row);
                lstResults.Add(rol);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_ROL_PR" };

            var rol = (Rol)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, rol.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, rol.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, rol.Descripcion);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_ROL_PR" };

            var rol = (Rol)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, rol.Codigo);
            return operation;
        }

        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_PR" };
            return operation;
        }

        internal SqlOperation GetRetrieveAllByGerenteStatement(string idGerente)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_GERENTE_PR" };

            operation.AddVarcharParam(DB_COL_ID_GERENTE, idGerente);

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ROL_PR" };

            var rol = (Rol)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, rol.Codigo);

            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_ROL_PR" };

            var rol = (Rol)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, rol.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, rol.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, rol.Descripcion);
            operation.AddVarcharParam(DB_COL_ESTADO, rol.ValorEstado);

            return operation;
        }
    }
}
