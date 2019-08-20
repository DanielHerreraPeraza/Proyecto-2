using DataAcess.Dao;
using Entities;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class CustomerMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_NAME = "NAME";
        private const string DB_COL_LAST_NAME= "LAST_NAME";
        private const string DB_COL_AGE = "AGE";


        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation {ProcedureName = "CRE_CUSTOMER_PR"};

            var c = (Customer) entity;            
            operation.AddVarcharParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_LAST_NAME, c.LastName);
            operation.AddIntParam(DB_COL_AGE, c.Age);

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation {ProcedureName = "RET_CUSTOMER_PR"};

            var c = (Customer)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
         
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CUSTOMER_PR" };            
            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CUSTOMER_PR" };

            var c = (Customer)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
            operation.AddVarcharParam(DB_COL_NAME, c.Name);
            operation.AddVarcharParam(DB_COL_LAST_NAME, c.LastName);
            operation.AddIntParam(DB_COL_AGE, c.Age);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CUSTOMER_PR" };

            var c = (Customer)entity;
            operation.AddVarcharParam(DB_COL_ID, c.Id);
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
            var customer = new Customer
            {
                Id = GetStringValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),
                LastName = GetStringValue(row, DB_COL_LAST_NAME),
                Age = GetIntValue(row, DB_COL_AGE)
            };

            return customer;
        }

        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
