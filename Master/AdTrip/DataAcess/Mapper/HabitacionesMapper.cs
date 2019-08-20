using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class HabitacionesMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_TIPO_HAB = "ID_TIPO_HAB";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";



        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HABITACIONES_PR" };

            var h = (Habitaciones)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, h.IdHotel);
            operation.AddVarcharParam(DB_COL_ID_TIPO_HAB, h.IdTipoHab);
           

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HABITACIONES_PR" };

            var h = (Habitaciones)entity;
            operation.AddIntParam(DB_COL_CODIGO, h.Codigo);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_HABITACIONES_PR" };
            return operation;
        }

        public SqlOperation GetRetriveByHotelStatement(string IdHotel)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HABITACIONES_BY_HOTEL_PR" };
            operation.AddVarcharParam(DB_COL_ID_HOTEL, IdHotel);
            return operation;

        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_HABITACIONES_PR" };

            var h = (Habitaciones)entity;
            operation.AddIntParam(DB_COL_CODIGO, h.Codigo);
            operation.AddVarcharParam(DB_COL_VALOR_ESTADO, h.ValorEstado);


            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HABITACIONES_PR" };

            var h = (Habitaciones)entity;
            operation.AddIntParam(DB_COL_CODIGO, h.Codigo);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, h.IdHotel);
            return operation;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var habitaciones = BuildObject(row);
                lstResults.Add(habitaciones);
            }

            return lstResults;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var habitaciones = new Habitaciones
            {
                Codigo = GetIntValue(row, DB_COL_CODIGO),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdTipoHab = GetStringValue(row, DB_COL_ID_TIPO_HAB),
                NombreTipoHab = GetStringValue(row,DB_COL_NOMBRE),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO)
            };

            return habitaciones;
        }



    }
}
