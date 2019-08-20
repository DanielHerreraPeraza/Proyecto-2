using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    class LlaveQRMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO_QR = "CODIGO_QR";
        private const string DB_COL_IMAGEN_QR = "IMAGEN_QR";
        private const string DB_COL_ESTADO_QR = "ESTADO_QR";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_ID_RESERVA = "ID_RESERVA";
        

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_LLAVE_QR_PR" };
            var QR = (LlaveQR)entity;

            operation.AddVarcharParam(DB_COL_CODIGO_QR, QR.CodigoQR);
            operation.AddVarcharParam(DB_COL_IMAGEN_QR, QR.ImagenQR);
            operation.AddVarcharParam(DB_COL_ESTADO_QR, QR.EstadoQR);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, QR.IdUsuario);
            operation.AddIntParam(DB_COL_ID_RESERVA, QR.IdReserva);

            return operation;
        }
        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_LLAVES_QR_PR" };
            return operation;
        }
        public SqlOperation GetRetrieveAllByReservaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_LLAVES_QR_BY_RESERVA_PR" };
            var QR = (LlaveQR)entity;
            operation.AddIntParam(DB_COL_ID_RESERVA, QR.IdReserva);
            return operation;
        }
        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_LLAVE_QR_PR" };

            var QR = (LlaveQR)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_QR, QR.CodigoQR);

            return operation;
        }
        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_LLAVE_QR_PR" };
            var QR = (LlaveQR)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_QR, QR.CodigoQR);
            operation.AddVarcharParam(DB_COL_ESTADO_QR, QR.EstadoQR);

            return operation;
        }
        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_LLAVE_QR_PR" };

            var QR = (LlaveQR)entity;
            operation.AddVarcharParam(DB_COL_CODIGO_QR, QR.CodigoQR);
            return operation;
        }
        public SqlOperation GetDeleteALLByIdStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_LLAVES_QR_BY_RESERVA_PR" };

            var QR = (LlaveQR)entity;
            operation.AddIntParam(DB_COL_ID_RESERVA, QR.IdReserva);
            return operation;
        }
        public Entity BuildObject(Dictionary<string, object> row)
        {
            var QR = new LlaveQR
            {
                CodigoQR = GetStringValue(row, DB_COL_CODIGO_QR),
                ImagenQR = GetStringValue(row, DB_COL_IMAGEN_QR),
                EstadoQR = GetStringValue(row, DB_COL_ESTADO_QR),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO),
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO),
                IdReserva = GetIntValue(row, DB_COL_ID_RESERVA),
            };

            return QR;
        }
        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var llave = BuildObject(row);
                lstResults.Add(llave);
            }

            return lstResults;
        }

    }
}