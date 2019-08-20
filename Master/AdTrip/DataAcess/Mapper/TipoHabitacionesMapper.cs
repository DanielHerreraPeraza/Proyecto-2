using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class TipoHabitacionesMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_NUM_CAMAS = "NUM_CAMAS";
        private const string DB_COL_CUPO_MAX = "CUPO_MAX";
        private const string DB_COL_PRECIO = "PRECIO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_CHECK_IN = "HORA_CHECK_IN";
        private const string DB_COL_CHECK_OUT = "HORA_CHECK_OUT";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_NUM_HABITACIONES = "NUM_HABITACIONES";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";
        private const string DB_COL_FOTO = "FOTO";


        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_TIPO_HABITACIONES_PR" };


            var t = (TipoHabitaciones)entity;

            operation.AddVarcharParam(DB_COL_CODIGO, t.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, t.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, t.Descripcion);
            operation.AddIntParam(DB_COL_NUM_CAMAS, t.NumCamas);
            operation.AddIntParam(DB_COL_CUPO_MAX, t.CupoMax);
            operation.AddDecimalParam(DB_COL_PRECIO, t.Precio);
            operation.AddDateTimeParam(DB_COL_CHECK_IN, t.HoraCheckIn);
            operation.AddDateTimeParam(DB_COL_CHECK_OUT, t.HoraCheckOut);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, t.IdHotel);
            operation.AddIntParam(DB_COL_NUM_HABITACIONES, t.NumHabitaciones);
            operation.AddVarcharParam(DB_COL_FOTO, t.FotoPrincipal);

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_TIPO_HABITACIONES_PR" };

            var t = (TipoHabitaciones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, t.Codigo);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TIPO_HABITACIONES_PR" };
            return operation;
        }

        public SqlOperation GetRetriveAllByHotelStatement(string IdHotel)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_TIPO_HABITACIONES_BY_HOTEL_PR" };
            operation.AddVarcharParam(DB_COL_ID_HOTEL, IdHotel);
            return operation;

        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_TIPO_HABITACIONES_PR" };

            var t = (TipoHabitaciones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, t.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, t.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, t.Descripcion);
            operation.AddIntParam(DB_COL_NUM_CAMAS, t.NumCamas);
            operation.AddIntParam(DB_COL_CUPO_MAX, t.CupoMax);
            operation.AddDecimalParam(DB_COL_PRECIO, t.Precio);
            operation.AddVarcharParam(DB_COL_VALOR_ESTADO, t.ValorEstado);
            operation.AddDateTimeParam(DB_COL_CHECK_IN, t.HoraCheckIn);
            operation.AddDateTimeParam(DB_COL_CHECK_OUT, t.HoraCheckOut);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, t.IdHotel);
            operation.AddVarcharParam(DB_COL_FOTO, t.FotoPrincipal);
            

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_TIPO_HABITACION_PR" };

            var t = (TipoHabitaciones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, t.Codigo);
            return operation;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var tipoHabitaciones = BuildObject(row);
                lstResults.Add(tipoHabitaciones);
            }

            return lstResults;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var tipoHabitaciones = new TipoHabitaciones
            {
                Codigo = GetStringValue(row, DB_COL_CODIGO),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                NumCamas = GetIntValue(row, DB_COL_NUM_CAMAS),
                CupoMax = GetIntValue(row, DB_COL_CUPO_MAX),
                Precio = GetDecimalValue(row, DB_COL_PRECIO),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                HoraCheckIn = GetDateValue(row, DB_COL_CHECK_IN),
                HoraCheckOut = GetDateValue(row, DB_COL_CHECK_OUT),
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL),
                FotoPrincipal = GetStringValue(row, DB_COL_FOTO),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO)

            };

            return tipoHabitaciones;
        }




       
    }
}