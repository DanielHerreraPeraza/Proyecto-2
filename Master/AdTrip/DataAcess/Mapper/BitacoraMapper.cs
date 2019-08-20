using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class BitacoraMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_FECHA = "FECHA";
        private const string DB_COL_TIPO_ACTION = "TIPO_ACCION";
        private const string DB_COL_CONTROLLER = "CONTROLLER";
        private const string DB_COL_ROL_USUARIO = "ROL_USUARIO";
        private const string DB_COL_CORREO_USUARIO = "CORREO_USUARIO";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_BITACORA_PR" };
            var bit = (Bitacora)entity;

            operation.AddDateTimeParam(DB_COL_FECHA, bit.Fecha);
            operation.AddVarcharParam(DB_COL_TIPO_ACTION, bit.TipoAction);       
            operation.AddVarcharParam(DB_COL_CONTROLLER, bit.Controller);
            operation.AddVarcharParam(DB_COL_ROL_USUARIO, bit.RolUsuario);
            operation.AddVarcharParam(DB_COL_CORREO_USUARIO, bit.CorreoUsuario);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, bit.IdHotel);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_BITACORA_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_BITACORA_PR" };

            var bit = (Bitacora)entity;
            operation.AddIntParam(DB_COL_ID, bit.Id);

            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }
        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var bitacora = new Bitacora
            {


                Id = GetIntValue(row, DB_COL_ID),
                Fecha = GetDateValue(row, DB_COL_FECHA),
                TipoAction = GetStringValue(row, DB_COL_TIPO_ACTION),
                Controller = GetStringValue(row, DB_COL_CONTROLLER),
                RolUsuario = GetStringValue(row, DB_COL_ROL_USUARIO),
                CorreoUsuario = GetStringValue(row, DB_COL_CORREO_USUARIO),
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL),

            };

            return bitacora;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var bitacora = BuildObject(row);
                lstResults.Add(bitacora);
            }

            return lstResults;
        }
    }
}