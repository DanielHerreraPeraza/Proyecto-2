using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;
using System.Collections.Generic;

namespace DataAcess
{
    public class VistaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_DEFINICION = "DEFINICION";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_GRUPO = "GRUPO";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var vista = new Vista
            {
                Id = GetStringValue(row, DB_COL_ID),
                Definicion = GetStringValue(row, DB_COL_DEFINICION),
                Grupo = GetStringValue(row, DB_COL_GRUPO)
            };

            return vista;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var vista = BuildObject(row);
                lstResults.Add(vista);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_VISTA_PR" };

            var vista = (Vista)entity;
            operation.AddVarcharParam(DB_COL_ID, vista.Id);
            operation.AddVarcharParam(DB_COL_DEFINICION, vista.Definicion);
            operation.AddVarcharParam(DB_COL_GRUPO, vista.Grupo);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_VISTA_PR" };

            var vista = (Vista)entity;
            operation.AddVarcharParam(DB_COL_ID, vista.Id);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_VISTAS_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_VISTA_PR" };

            var vista = (Vista)entity;
            operation.AddVarcharParam(DB_COL_ID, vista.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_VISTA_PR" };

            var vista = (Vista)entity;
            operation.AddVarcharParam(DB_COL_ID, vista.Id);
            operation.AddVarcharParam(DB_COL_DEFINICION, vista.Definicion);
            operation.AddVarcharParam(DB_COL_GRUPO, vista.Grupo);

            return operation;
        }

        public SqlOperation GetRetrieveAllVistasUsuarioStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_VISTAS_USUARIO_PR" };

            var us = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, us.Identificacion);

            return operation;
        }
    }
}
