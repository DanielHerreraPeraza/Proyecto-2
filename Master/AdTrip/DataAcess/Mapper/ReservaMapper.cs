using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class ReservaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {

        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_FECHA_REALIZACION = "FECHA_REALIZACION";
        private const string DB_COL_FECHA_INICIO = "FECHA_INICIO";
        private const string DB_COL_FECHA_FIN = "FECHA_FIN";
        private const string DB_COL_PRECIO = "PRECIO";
        private const string DB_COL_PROMOCION = "PROMOCIONES";
        private const string DB_COL_ID_ESTADO = "ID_ESTADO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";
        private const string DB_COL_TIPO_HAB = "TIPO_HAB";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_CANT_HAB = "CANT_HAB";
        private const string DB_COL_RESPUESTA = "RESPUESTA";
        private const string DB_COL_ID_HABITACION = "ID_HABITACION";
        private const string DB_COL_CANT_PERSONAS = "CANT_PERSONAS";
        private const string DB_COL_LLAVE_QR = "LLAVE_QR";
        private const string DB_COL_NUM_FACTURACION = "NUM_FACTURACION";


        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_RESERVA_PR" };

            var r = (Reserva)entity;

            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);
            operation.AddDateTimeParam(DB_COL_FECHA_INICIO, r.FechaInicio);
            operation.AddDateTimeParam(DB_COL_FECHA_FIN, r.FechaFin);
            operation.AddDecimalParam(DB_COL_PRECIO, r.Precio);
            operation.AddVarcharParam(DB_COL_PROMOCION, r.Promocion);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, r.IdUsuario);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);
            operation.AddIntParam(DB_COL_CANT_PERSONAS, r.CantPersonas);
            operation.AddIntParam(DB_COL_NUM_FACTURACION, r.NumFacturacion);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_RESERVAS_PR" };

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_RESERVA_PR" };

            var r = (Reserva)entity;
            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);

            return operation;
        }

        public SqlOperation GetAllByIdRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_RESERVAS_BY_USUARIO_PR" };

            var r = (Reserva)entity;
            operation.AddVarcharParam(DB_COL_ID_USUARIO, r.IdUsuario);

            return operation;
        }


        public SqlOperation GetAllHabitacionesReservaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HABITACIONES_BY_RESERVA_PR" };

            var r = (Reserva)entity;
            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);

            return operation;
        }


        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_RESERVA_PR" };

            var r = (Reserva)entity;

            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);
            operation.AddVarcharParam(DB_COL_ESTADO, r.Estado);

            return operation;
        }


        public Entity BuildObject(Dictionary<string, object> row)
        {
            var reserva = new Reserva
            {
                Codigo = GetIntValue(row, DB_COL_CODIGO),
                FechaRealizacion = GetDateValue(row, DB_COL_FECHA_REALIZACION),
                FechaInicio = GetDateValue(row, DB_COL_FECHA_INICIO),
                FechaFin = GetDateValue(row, DB_COL_FECHA_FIN),
                Precio = GetDecimalValue(row, DB_COL_PRECIO),
                Promocion = GetStringValue(row, DB_COL_PROMOCION),
                IdEstado = GetStringValue(row, DB_COL_ID_ESTADO),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL),
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO),
                TipoHab = GetStringValue(row, DB_COL_TIPO_HAB),
                NombreTipoHab = GetStringValue(row, DB_COL_NOMBRE),
                CantHab = GetIntValue(row, DB_COL_CANT_HAB),
                Respuesta = GetIntValue(row, DB_COL_RESPUESTA),
                IdHabitacion = GetIntValue(row, DB_COL_ID_HABITACION),
                CantPersonas = GetIntValue(row, DB_COL_CANT_PERSONAS),
                LlaveQR = GetStringValue(row, DB_COL_LLAVE_QR)
            };

            return reserva;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var reserva = BuildObject(row);
                lstResults.Add(reserva);
            }

            return lstResults;
        }



        public SqlOperation GetVerCantHabXTipoStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "VAL_CANT_HAB_X_TIPO_PR" };

            var r = (Reserva)entity;

            operation.AddVarcharParam(DB_COL_TIPO_HAB, r.TipoHab);
            operation.AddIntParam(DB_COL_CANT_HAB, r.CantHab);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);

            return operation;
        }



        public SqlOperation GeVerDispHabStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "VAL_DISP_HAB_TIPO_PR" };

            var r = (Reserva)entity;

            operation.AddVarcharParam(DB_COL_TIPO_HAB, r.TipoHab);
            operation.AddIntParam(DB_COL_CANT_HAB, r.CantHab);
            operation.AddDateTimeParam(DB_COL_FECHA_INICIO, r.FechaInicio);
            operation.AddDateTimeParam(DB_COL_FECHA_FIN, r.FechaFin);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);

            return operation;
        }

        public SqlOperation GetAsignarHabReservaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "ASIGNAR_HAB_RESERVA_PR" };

            var r = (Reserva)entity;

            operation.AddVarcharParam(DB_COL_TIPO_HAB, r.TipoHab);
            operation.AddIntParam(DB_COL_CANT_HAB, r.CantHab);
            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);
            operation.AddDateTimeParam(DB_COL_FECHA_INICIO, r.FechaInicio);
            operation.AddDateTimeParam(DB_COL_FECHA_FIN, r.FechaFin);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, r.IdHotel);

            return operation;
        }


       public SqlOperation GetCodigoHabitacionesReserva(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_INFO_HABITACION_RESERVA_PR" };

            var r = (Reserva)entity;
            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);

            return operation;
        }

        public SqlOperation GetAsignarQRReserva(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "AGREGAR_LLAVE_RESERVA_PR" };

            var r = (Reserva)entity;
            operation.AddVarcharParam(DB_COL_LLAVE_QR, r.LlaveQR);
            operation.AddIntParam(DB_COL_CODIGO, r.Codigo);

            return operation;
        }

    }
}
