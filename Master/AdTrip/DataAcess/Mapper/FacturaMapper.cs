using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class FacturaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_NUM_FACTURACION = "NUM_FACTURACION";
        private const string DB_COL_FECHA_FACTURA = "FECHA_FACTURA";
        private const string DB_COL_NOM_PLATAFORMA = "NOM_PLATAFORMA";
        private const string DB_COL_NOMBRE_CLIENTE = "NOMBRE_CLIENTE";
        private const string DB_COL_ID_CLIENTE = "ID_CLIENTE";
        private const string DB_COL_HOTEL = "HOTEL";
        private const string DB_COL_CED_JURIDICA = "CED_JURIDICA";
        private const string DB_COL_DIRECCION = "DIRECCION";
        private const string DB_COL_TOTAL_PAGAR = "TOTAL_PAGAR";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";




        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_FACTURA_PR" };

            var f = (Factura)entity;
            operation.AddIntParam(DB_COL_NUM_FACTURACION, f.NumFacturacion);
            operation.AddDateParam(DB_COL_FECHA_FACTURA, f.FechaFactura);
            operation.AddVarcharParam(DB_COL_NOM_PLATAFORMA, f.NomPlataforma);
            operation.AddVarcharParam(DB_COL_NOMBRE_CLIENTE, f.NombreCliente);
            operation.AddVarcharParam(DB_COL_ID_CLIENTE, f.IdCliente);
            operation.AddVarcharParam(DB_COL_HOTEL, f.Hotel);
            operation.AddVarcharParam(DB_COL_CED_JURIDICA, f.CedJuridica);
            operation.AddVarcharParam(DB_COL_DIRECCION, f.Direccion);
            operation.AddDecimalParam(DB_COL_TOTAL_PAGAR, f.TotalPagar);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, f.IdUsuario);


            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_FACTURA_PR" };

            var f = (Factura)entity;
            operation.AddIntParam(DB_COL_NUM_FACTURACION, f.NumFacturacion);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_FACTURA_PR" };
            return operation;
        }

        public SqlOperation GetRetriveByHotelStatement(string CedJuridica)
        {
            var operation = new SqlOperation { ProcedureName = "RET_FACTURA_BY_HOTEL_PR" };
            operation.AddVarcharParam(DB_COL_NUM_FACTURACION, CedJuridica);
            return operation;

        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_FACTURA_PR" };

            var f = (Factura)entity;
            operation.AddIntParam(DB_COL_NUM_FACTURACION, f.NumFacturacion);
            operation.AddVarcharParam(DB_COL_VALOR_ESTADO, f.ValorEstado);


            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_FACTURA_PR" };

            var f = (Factura)entity;
            operation.AddIntParam(DB_COL_NUM_FACTURACION, f.NumFacturacion);
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
            var factura = new Factura
            {
                NumFacturacion = GetIntValue(row, DB_COL_NUM_FACTURACION),
                FechaFactura = GetDateValue(row, DB_COL_FECHA_FACTURA),
                NomPlataforma = GetStringValue(row, DB_COL_NOM_PLATAFORMA),
                NombreCliente = GetStringValue(row, DB_COL_NOMBRE_CLIENTE),
                IdCliente = GetStringValue(row, DB_COL_ID_CLIENTE),
                Hotel = GetStringValue(row, DB_COL_HOTEL),
                CedJuridica = GetStringValue(row, DB_COL_CED_JURIDICA),
                Direccion = GetStringValue(row, DB_COL_DIRECCION),
                TotalPagar = GetDecimalValue(row, DB_COL_TOTAL_PAGAR),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO),
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO)

        };

            return factura;
        }

    }
}
