using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;


namespace DataAcess.Mapper
{
    public class PromocionesMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_CANT_DISPONIBLE = "CANT_DISPONIBLE";
        private const string DB_COL_FECHA_INICIO = "FECHA_INICIO";
        private const string DB_COL_FECHA_FIN = "FECHA_FIN";
        private const string DB_COL_DESCUENTO = "DESCUENTO";
        private const string DB_COL_TIPO_PROMOCION = "TIPO_PROMOCION";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";
        private const string DB_COL_VALOR_TIPO_PROMOCION = "VALOR_TIPO_PROMOCION";


        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_PROMOCIONES_PR" };

            var p = (Promociones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);
            operation.AddIntParam(DB_COL_CANT_DISPONIBLE, p.CantDisponible);
            operation.AddDateParam(DB_COL_FECHA_INICIO, p.FechaFin);
            operation.AddDateParam(DB_COL_FECHA_FIN, p.FechaFin);
            operation.AddDecimalParam(DB_COL_DESCUENTO, p.Descuento);
            operation.AddVarcharParam(DB_COL_VALOR_TIPO_PROMOCION, p.ValorTipoPromocion);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);


            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_PROMOCIONES_PR" };

            var p = (Promociones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PROMOCIONES_PR" };
            return operation;
        }

        public SqlOperation GetRetriveByHotelStatement(string IdHotel)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PROMOCIONES_BY_HOTEL_PR" };
            operation.AddVarcharParam(DB_COL_ID_HOTEL, IdHotel);
            return operation;

        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_PROMOCIONES_PR" };

            var p= (Promociones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);
            operation.AddIntParam(DB_COL_CANT_DISPONIBLE, p.CantDisponible);
            operation.AddDateParam(DB_COL_FECHA_INICIO, p.FechaInicio);
            operation.AddDateParam(DB_COL_FECHA_FIN, p.FechaFin);
            operation.AddDecimalParam(DB_COL_DESCUENTO, p.Descuento);
            operation.AddVarcharParam(DB_COL_TIPO_PROMOCION, p.TipoPromocion);
            operation.AddVarcharParam(DB_COL_VALOR_ESTADO, p.ValorEstado);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);


            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_PROMOCIONES_PR" };

            var p = (Promociones)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, p.IdHotel);
            return operation;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var promociones = BuildObject(row);
                lstResults.Add(promociones);
            }

            return lstResults;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var promociones = new Promociones
            {
                Codigo = GetStringValue(row, DB_COL_CODIGO),
                CantDisponible = GetIntValue(row, DB_COL_CANT_DISPONIBLE),
                FechaInicio = GetDateValue(row, DB_COL_FECHA_INICIO),
                FechaFin = GetDateValue(row, DB_COL_FECHA_FIN),
                Descuento = GetDecimalValue(row, DB_COL_DESCUENTO),
                TipoPromocion = GetStringValue(row, DB_COL_TIPO_PROMOCION),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO),
                ValorTipoPromocion = GetStringValue(row, DB_COL_VALOR_TIPO_PROMOCION)
            };

            return promociones;
        }
    }
}
