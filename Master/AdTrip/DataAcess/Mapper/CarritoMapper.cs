using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class CarritoMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_CARRITO = "ID_CARRITO";
        private const string DB_COL_CANTIDAD = "CANTIDAD";
        private const string DB_COL_ID_PRODUCTO = "ID_PRODUCTO";
        private const string DB_COL_ID_RESERVA = "ID_RESERVA";
        private const string DB_COL_ID_LLAVE_QR = "ID_LLAVE_QR";
        private const string DB_COL_CORREO = "CORREO";



        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CARRITO_PR" };


            var c = (Carrito)entity;

            operation.AddIntParam(DB_COL_CANTIDAD, c.Cantidad);
            operation.AddVarcharParam(DB_COL_ID_PRODUCTO, c.IdProducto);
            operation.AddIntParam(DB_COL_ID_RESERVA, c.IdReserva);
            operation.AddVarcharParam(DB_COL_ID_LLAVE_QR, c.IdLLaveQR);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CARRITO_PR" };

            var c = (Carrito)entity;
            operation.AddIntParam(DB_COL_ID_CARRITO, c.IdCarrito);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CARRITO_PR" };
            return operation;
        }

        public SqlOperation GetRetriveByReservaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CARRITO_BY_RESERVA_PR" };

            var c = (Carrito)entity;
            operation.AddVarcharParam(DB_COL_ID_LLAVE_QR, c.IdLLaveQR);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            return operation;

        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CARRITO_PR" };

            var c = (Carrito)entity;
            operation.AddIntParam(DB_COL_ID_CARRITO, c.IdCarrito);
            operation.AddIntParam(DB_COL_CANTIDAD, c.Cantidad);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CARRITO_PR" };

            var c = (Carrito)entity;
            operation.AddIntParam(DB_COL_ID_CARRITO, c.IdCarrito);
            return operation;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var Carrito = BuildObject(row);
                lstResults.Add(Carrito);
            }

            return lstResults;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var Carrito = new Carrito
            {
                IdCarrito = GetIntValue(row, DB_COL_ID_CARRITO),
                Cantidad = GetIntValue(row, DB_COL_CANTIDAD),
                IdProducto = GetStringValue(row, DB_COL_ID_PRODUCTO),
                IdReserva = GetIntValue(row, DB_COL_ID_RESERVA)
            };

            return Carrito;
        }

    }
}