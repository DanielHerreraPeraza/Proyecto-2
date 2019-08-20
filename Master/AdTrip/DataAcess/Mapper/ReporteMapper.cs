using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class ReporteMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_FECHA_REALIZACION = "FECHA_REALIZADO";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_TIPO_USUARIO = "TIPO_USUARIO";
        private const string DB_COL_MONTO_GANANCIA = "MONTO_GANANCIA";
        private const string DB_COL_TIPO_PAGO = "TIPO_PAGO";
        private const string DB_COL_LUGAR_GANANCIA = "LUGAR_GANANCIA";
        private const string DB_COL_CANT_HAB = "CANT_HAB";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_NOM_HOTEL = "NOM_HOTEL";



        public SqlOperation GetRetrieveGananciasTotalesAdmin()
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_TOTALES_PR" };

            return operation;
        }


        public SqlOperation GetRetrieveGananciasXMesAdmin()
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_X_MES_PR" };

            return operation;
        }



        public SqlOperation GetRetrieveGananciasComisionXDiaAdmin()
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_COMISION_X_DIA_PR" };
            return operation;
        }


        public SqlOperation GetRetrieveGananciasMembresiaXMesAdmin()
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_MEMBRESIA_X_MES_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveGananciasTotalesGerente(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_TOTALES_GERENTE_PR" };

            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, r.IdUsuario);

            return operation;
        }


        public SqlOperation GetRetrieveGananciasXDiaGerente(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_X_DIA_GERENTE_PR" };

            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, r.IdUsuario);

            return operation;
        }


        public SqlOperation GetRetrieveCantHabHoteles(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CANT_HABITACIONES_X_HOTEL" };

            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, r.IdUsuario);

            return operation;
        }


        public SqlOperation GetRetrieveGananciasTotalesHotel(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_TOTALES_HOTEL_PR" };
            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);
            return operation;
        }


        public SqlOperation GetRetrieveGananciasXMesHotel(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_GANANCIAS_X_MES_HOTEL_PR" };
            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);
            return operation;
        }

        public SqlOperation GetRetrieveCantHabXTipo(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CANT_HAB_X_TIPO_PR" };
            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);
            return operation;
        }

        public SqlOperation GetRetrieveDisponibilidadHab(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_DISP_HABITACIONES_PR" };
            var r = (Reporte)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);
            return operation;
        }



        public SqlOperation GetCreateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }



        public Entity BuildObject(Dictionary<string, object> row)
        {
            var reporte = new Reporte
            {
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO),
                FechaRealizado = GetStringValue(row, DB_COL_FECHA_REALIZACION),
                TipoUsuario = GetStringValue(row, DB_COL_TIPO_USUARIO),
                MontoGanancia = GetDecimalValue(row, DB_COL_MONTO_GANANCIA),
                TipoPago = GetStringValue(row, DB_COL_TIPO_PAGO),
                LugarGanancia = GetStringValue(row, DB_COL_LUGAR_GANANCIA),
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL),
                CantHab = GetIntValue(row, DB_COL_CANT_HAB),
                NomHotel = GetStringValue(row, DB_COL_NOM_HOTEL)
            };


            return reporte;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var reporte = BuildObject(row);
                lstResults.Add(reporte);
            }

            return lstResults;
        }

    }

}
