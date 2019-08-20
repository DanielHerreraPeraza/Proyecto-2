using System;
using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class RolHotelMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_ROL = "ID_ROL";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var rolHotel = new RolHotel
            {
                IdRol = GetStringValue(row, DB_COL_ID_ROL),
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL)
            };

            return rolHotel;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var rolHotel = BuildObject(row);
                lstResults.Add(rolHotel);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_ROL_HOTEL_PR" };

            var rh = (RolHotel)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, rh.IdRol);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, rh.IdHotel);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_ROL_HOTEL_PR" };

            var rh = (RolHotel)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, rh.IdRol);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, rh.IdHotel);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_HOTELES_ROL_PR" };
            return operation;
        }

        internal SqlOperation GetRetrieveAllRolesHotelStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_HOTEL_PR" };

            var rh = (RolHotel)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, rh.IdHotel);

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
