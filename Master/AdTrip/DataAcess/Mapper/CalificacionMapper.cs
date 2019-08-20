using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class CalificacionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {

        private const string DB_COL_VALOR = "VALOR";
        private const string DB_COL_USUARIO = "ID_USUARIO";
        private const string DB_COL_HOTEL = "ID_HOTEL";
        private const string DB_COL_RESERVA = "ID_RESERVA"; //Nuevo


        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CALIFICACIONES_HOTELES_PR" };

            var cal = (Calificacion)entity;
            operation.AddIntParam(DB_COL_VALOR, cal.Valor);
            operation.AddVarcharParam(DB_COL_USUARIO, cal.Usuario);
            operation.AddVarcharParam(DB_COL_HOTEL, cal.Hotel);
            operation.AddIntParam(DB_COL_RESERVA, cal.Reserva);

            return operation;
        }

        public SqlOperation GetCreateCalificacionStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CALIFICACIONES_USUARIO_PR" };

            var cal = (Calificacion)entity;
            operation.AddIntParam(DB_COL_VALOR, cal.Valor);
            operation.AddVarcharParam(DB_COL_USUARIO, cal.Usuario);
            operation.AddVarcharParam(DB_COL_HOTEL, cal.Hotel);
            operation.AddIntParam(DB_COL_RESERVA, cal.Reserva);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CALIFICACIONES_HOTELES_PR" };

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CALIFICACIONES_HOTELES_PR" };

            var cal = (Calificacion)entity;
            operation.AddIntParam(DB_COL_RESERVA, cal.Reserva);

            return operation;
        }

        public SqlOperation GetRetrieveAllCalificacionStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_CALIFICACIONES_USUARIOS_PR" };

            return operation;
        }

        public SqlOperation GetRetrieveCalificacionStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_CALIFICACIONES_USUARIOS_PR" };

            var cal = (Calificacion)entity;
            operation.AddIntParam(DB_COL_RESERVA, cal.Reserva);

            return operation;
        }


        public SqlOperation GetUpdateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }
        public Entity BuildObject(Dictionary<string, object> row)
        {
            var calificacion = new Calificacion
            {
                Valor = GetIntValue(row, DB_COL_VALOR),
                Usuario = GetStringValue(row, DB_COL_USUARIO),
                Hotel = GetStringValue(row, DB_COL_HOTEL),
                Reserva = GetIntValue(row, DB_COL_RESERVA)

            };

            return calificacion;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var calificacion = BuildObject(row);
                lstResults.Add(calificacion);
            }

            return lstResults;
        }
    }
}