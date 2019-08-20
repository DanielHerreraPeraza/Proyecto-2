using System;
using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    class TipoImpuestoMapper : EntityMapper, IObjectMapper, ISqlStaments
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_PORCENTAJE = "PORCENTAJE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_ESTADO = "ID_ESTADO"; //Nuevo

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TIPO_IMPUESTO_PR" };

            var imp = (TipoImpuesto)entity;
            operation.AddIntParam(DB_COL_CODIGO, imp.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, imp.Nombre);
            operation.AddDecimalParam(DB_COL_PORCENTAJE, imp.Porcentaje);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, imp.Descripcion);
            

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TIPO_IMPUESTO_PR" };

            var imp = (TipoImpuesto)entity;
            operation.AddIntParam(DB_COL_CODIGO, imp.Codigo);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TIPO_IMPUESTO_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TIPO_IMPUESTO_PR" };

            var imp = (TipoImpuesto)entity;
            operation.AddIntParam(DB_COL_CODIGO, imp.Codigo);

            return operation;
        }
         public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            throw new NotImplementedException();
        }
        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TIPO_IMPUESTO_PR" };

            var imp = (TipoImpuesto)entity;
            operation.AddIntParam(DB_COL_CODIGO, imp.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, imp.Nombre);
            operation.AddDecimalParam(DB_COL_PORCENTAJE, imp.Porcentaje);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, imp.Descripcion);
            operation.AddVarcharParam(DB_COL_ESTADO, imp.Estado);

            return operation;
        }
        public Entity BuildObject(Dictionary<string, object> row)
        {
            var impuesto = new TipoImpuesto
            {
                Codigo = GetIntValue(row, DB_COL_CODIGO),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Porcentaje = GetDecimalValue(row, DB_COL_PORCENTAJE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdEstado = GetStringValue(row, DB_COL_ID_ESTADO)
            };

            return impuesto;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var impuesto = BuildObject(row);
                lstResults.Add(impuesto);
            }

            return lstResults;
        }

       
    }
}
