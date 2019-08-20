using DataAcess.Dao;
using Entities;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class ApplicationMessageMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_MENSAJE = "MENSAJE";



        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_EXCEPTION_PR" };

            var c = (ApplicationMessage)entity;
            operation.AddIntParam(DB_COL_CODIGO, c.Id);//CODIGO
            operation.AddVarcharParam(DB_COL_MENSAJE, c.Message);//MENSAJE


            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_EXCEPTION_PR" };

            var c = (ApplicationMessage)entity;
            operation.AddIntParam(DB_COL_CODIGO, c.Id);//CODIGO

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_EXCEPTIONES_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_EXCEPTION_PR" };

            var c = (ApplicationMessage)entity;
            operation.AddIntParam(DB_COL_CODIGO, c.Id);//CODIGO
            operation.AddVarcharParam(DB_COL_MENSAJE, c.Message);//MENSAJE


            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_EXCEPTION_PR" };

            var c = (ApplicationMessage)entity;
            operation.AddIntParam(DB_COL_CODIGO, c.Id);//CODIGO
            return operation;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var customer = BuildObject(row);
                lstResults.Add(customer);
            }

            return lstResults;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var mensaje = new ApplicationMessage
            {
                Id = GetIntValue(row, DB_COL_CODIGO),
                Message = GetStringValue(row, DB_COL_MENSAJE),

            };

            return mensaje;
        }

    }
}
