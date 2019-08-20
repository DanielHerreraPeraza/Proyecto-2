using System;
using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class UsuarioMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_IDENTIFICACION = "IDENTIFICACION";
        private const string DB_COL_PRIMER_NOMBRE = "PRIMER_NOMBRE";
        private const string DB_COL_SEGUNDO_NOMBRE = "SEGUNDO_NOMBRE";
        private const string DB_COL_PRIMER_APELLIDO = "PRIMER_APELLIDO";
        private const string DB_COL_SEGUNDO_APELLIDO = "SEGUNDO_APELLIDO";
        private const string DB_COL_PROVINCIA = "PROVINCIA";
        private const string DB_COL_CANTON = "CANTON";
        private const string DB_COL_DISTRITO = "DISTRITO";
        private const string DB_COL_DIRECCION_EXACTA = "DIRECCION_EXACTA";
        private const string DB_COL_TELEFONO = "TELEFONO";
        private const string DB_COL_CORREO = "CORREO";
        private const string DB_COL_PROM_CALIFICACIONES = "PROM_CALIFICACIONES";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_VALOR_ESTADO = "VALOR_ESTADO";
        private const string DB_COL_CONTRASENNA = "CONTRASENNA";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var usuario = new Usuario
            {
                Identificacion = GetStringValue(row, DB_COL_IDENTIFICACION),
                PrimerNombre = GetStringValue(row, DB_COL_PRIMER_NOMBRE),
                SegundoNombre = GetStringValue(row, DB_COL_SEGUNDO_NOMBRE),
                PrimerApellido = GetStringValue(row, DB_COL_PRIMER_APELLIDO),
                SegundoApellido = GetStringValue(row, DB_COL_SEGUNDO_APELLIDO),
                Contrasenna = GetStringValue(row, DB_COL_CONTRASENNA),
                Provincia = GetStringValue(row, DB_COL_PROVINCIA),
                Canton = GetStringValue(row, DB_COL_CANTON),
                Distrito = GetStringValue(row, DB_COL_DISTRITO),
                DireccionExacta = GetStringValue(row, DB_COL_DIRECCION_EXACTA),
                Telefono = GetStringValue(row, DB_COL_TELEFONO),
                Correo = GetStringValue(row, DB_COL_CORREO),
                Calificacion = GetDecimalValue(row, DB_COL_PROM_CALIFICACIONES),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                ValorEstado = GetStringValue(row, DB_COL_VALOR_ESTADO)
            };

            return usuario;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach(var row in lstRows)
            {
                var usuario = BuildObject(row);
                lstResults.Add(usuario);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            operation.AddVarcharParam(DB_COL_PRIMER_NOMBRE, u.PrimerNombre);
            operation.AddVarcharParam(DB_COL_SEGUNDO_NOMBRE, u.SegundoNombre);
            operation.AddVarcharParam(DB_COL_PRIMER_APELLIDO, u.PrimerApellido);
            operation.AddVarcharParam(DB_COL_SEGUNDO_APELLIDO, u.SegundoApellido);
            operation.AddVarcharParam(DB_COL_PROVINCIA, u.Provincia);
            operation.AddVarcharParam(DB_COL_CANTON, u.Canton);
            operation.AddVarcharParam(DB_COL_DISTRITO, u.Distrito);
            operation.AddVarcharParam(DB_COL_DIRECCION_EXACTA, u.DireccionExacta);
            operation.AddVarcharParam(DB_COL_TELEFONO, u.Telefono);
            operation.AddVarcharParam(DB_COL_CORREO, u.Correo);
            operation.AddVarcharParam(DB_COL_CONTRASENNA, u.Contrasenna);

            return operation;
        }

        internal SqlOperation GetValidarUsuarioGoogleStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "VAL_USUARIO_GOOGLE_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_CORREO, u.Correo);

            return operation;
        }

        public SqlOperation GetUpdateContrasennaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CONTRASENNA_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_CORREO, u.Correo);
            operation.AddVarcharParam(DB_COL_CONTRASENNA, u.Contrasenna);

            return operation;
        }

        public SqlOperation GetUpdateEstadoStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_ESTADO_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            operation.AddVarcharParam(DB_COL_ESTADO, u.ValorEstado);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_USUARIOS_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);

            return operation;
        }

        public SqlOperation GetExistsStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USUARIO_CORREO_ID_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            operation.AddVarcharParam(DB_COL_CORREO, u.Correo);

            return operation;
        }

        public SqlOperation GetValidarUsuarioStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "VAL_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_CORREO, u.Correo);
            operation.AddVarcharParam(DB_COL_CONTRASENNA, u.Contrasenna);

            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USUARIO_PR" };

            var u = (Usuario)entity;
            operation.AddVarcharParam(DB_COL_IDENTIFICACION, u.Identificacion);
            operation.AddVarcharParam(DB_COL_PRIMER_NOMBRE, u.PrimerNombre);
            operation.AddVarcharParam(DB_COL_SEGUNDO_NOMBRE, u.SegundoNombre);
            operation.AddVarcharParam(DB_COL_PRIMER_APELLIDO, u.PrimerApellido);
            operation.AddVarcharParam(DB_COL_SEGUNDO_APELLIDO, u.SegundoApellido);
            operation.AddVarcharParam(DB_COL_PROVINCIA, u.Provincia);
            operation.AddVarcharParam(DB_COL_CANTON, u.Canton);
            operation.AddVarcharParam(DB_COL_DISTRITO, u.Distrito);
            operation.AddVarcharParam(DB_COL_DIRECCION_EXACTA, u.DireccionExacta);
            operation.AddVarcharParam(DB_COL_TELEFONO, u.Telefono);
            operation.AddVarcharParam(DB_COL_CORREO, u.Correo);
            operation.AddDecimalParam(DB_COL_PROM_CALIFICACIONES, u.Calificacion);
            operation.AddVarcharParam(DB_COL_ESTADO, u.ValorEstado);

            return operation;
        }
    }
}
