using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class ParametrizablesHotelMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_COMISION = "COMISION";
        private const string DB_COL_PORCENTAJE_POLITICA = "PORCENTAJE_POLITICA";
        private const string DB_COL_DESCRIPCION_POLITICA = "DESCRIPCION_POLITICA";
        private const string DB_COL_CANT_DIAS_DEVOLUCION = "DIAS_DEVOLUCION";
        private const string DB_COL_MENSAJE_SMS = "MENSAJE_SMS";

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_PARAMETROS_PR" };

            var p = (ParametrizablesHotel)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);
            operation.AddDecimalParam(DB_COL_COMISION, p.Comision);
            operation.AddDecimalParam(DB_COL_PORCENTAJE_POLITICA, p.Porciento);
            operation.AddVarcharParam(DB_COL_DESCRIPCION_POLITICA, p.Politica);
            operation.AddIntParam(DB_COL_CANT_DIAS_DEVOLUCION, p.Dias);
            operation.AddVarcharParam(DB_COL_MENSAJE_SMS, p.Mensaje);


            return operation;
        }



        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ID_PARAMETROS_PR" };

            var p = (ParametrizablesHotel)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);


            return operation;
        }
        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_PARAMETROS_PR" };

            var p = (ParametrizablesHotel)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);

            return operation;
        }
        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PARAMETROS_PR" };

            return operation;
        }
        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_PARAMETROS_PR" };

            var p = (ParametrizablesHotel)entity;

            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);
            operation.AddDecimalParam(DB_COL_COMISION, p.Comision);
            operation.AddDecimalParam(DB_COL_PORCENTAJE_POLITICA, p.Porciento);
            operation.AddVarcharParam(DB_COL_DESCRIPCION_POLITICA, p.Politica);
            operation.AddIntParam(DB_COL_CANT_DIAS_DEVOLUCION, p.Dias);
            operation.AddVarcharParam(DB_COL_MENSAJE_SMS, p.Mensaje);
            return operation;
        }
        public Entity BuildObject(Dictionary<string, object> row)
        {
            var parametro = new ParametrizablesHotel
            {
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL),
                Comision = GetDecimalValue(row, DB_COL_COMISION),
                Porciento = GetDecimalValue(row, DB_COL_PORCENTAJE_POLITICA),
                Politica = GetStringValue(row, DB_COL_DESCRIPCION_POLITICA),
                Dias = GetIntValue(row, DB_COL_CANT_DIAS_DEVOLUCION),
                Mensaje = GetStringValue(row, DB_COL_MENSAJE_SMS)
            };

            return parametro;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var parametros = BuildObject(row);
                lstResults.Add(parametros);
            }

            return lstResults;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
