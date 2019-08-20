using System;
using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class ListaValoresMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_LISTA = "ID_LISTA";
        private const string DB_COL_VALOR = "VALOR";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var optionList = new OptionList
            {
                ListId = GetStringValue(row, DB_COL_ID_LISTA),
                Codigo = GetStringValue(row, DB_COL_VALOR),
                Nombre = GetStringValue(row, DB_COL_DESCRIPCION)
            };

            return optionList;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var optionList = BuildObject(row);
                lstResults.Add(optionList);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_LISTA_VALORES_PR" };
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
    }
}
