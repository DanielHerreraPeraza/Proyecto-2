using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Mapper
{
    public class CuentaMapper 
    {

        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_MONTO = "MONTO";
        private const string DB_COL_FECHA_REALIZADO = "FECHA_REALIZADO";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_TIPO_CUENTA = "TIPO_CUENTA";
        private const string DB_COL_TIPO_USUARIO = "TIPO_USUARIO";
        private const string DB_COL_TIPO_PAGO = "TIPO_PAGO";





        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CUENTA_PR" };


            var c = (Cuenta)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddVarcharParam(DB_COL_TIPO_CUENTA, c.TipoCuenta);

            return operation;
        }



        public SqlOperation GetCreateGananciaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_GANANCIA_DIA_PR" };


            var c = (Cuenta)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddDecimalParam(DB_COL_MONTO, c.Monto);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, c.IdHotel);
            operation.AddVarcharParam(DB_COL_TIPO_USUARIO, c.TipoUsuario);
            operation.AddVarcharParam(DB_COL_TIPO_PAGO, c.TipoPago);

            return operation;
        }



        public SqlOperation GetPagoMembresiaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "PAGO_MEMBRESIA_PR" };

            var c = (Cuenta)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddDecimalParam(DB_COL_MONTO, c.Monto);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, c.IdHotel);
            operation.AddVarcharParam(DB_COL_TIPO_PAGO, c.TipoPago);
            

            return operation;
        }


        public SqlOperation GetPagoReservaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "PAGO_RESERVA_PR" };

            var c = (Cuenta)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddDecimalParam(DB_COL_MONTO, c.Monto);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, c.IdHotel);
            operation.AddVarcharParam(DB_COL_TIPO_PAGO, c.TipoPago);
        


            return operation;
        }


        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_GANANCIA_DIA_PR" };

            var c = (Cuenta)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddDateTimeParam(DB_COL_FECHA_REALIZADO, c.FechaRealizado);
            operation.AddDecimalParam(DB_COL_MONTO, c.Monto);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, c.IdUsuario);
            operation.AddVarcharParam(DB_COL_TIPO_USUARIO, c.TipoUsuario);
            operation.AddVarcharParam(DB_COL_TIPO_PAGO, c.TipoPago);

            return operation;
        }
    }
}
