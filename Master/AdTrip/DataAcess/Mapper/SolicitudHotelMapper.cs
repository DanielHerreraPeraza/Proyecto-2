using DataAcess.Dao;
using Entities;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    public class SolicitudHotelMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO_SOLICITUD = "CODIGO_SOLICITUD";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_CEDULA_JURIDICA = "CEDULA_JURIDICA";
        private const string DB_COL_EMPRESA_DUENNA = "EMPRESA_DUENNA";
        private const string DB_COL_DIRECCION = "DIRECCION";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_CADENA = "CADENA";
        private const string DB_COL_CLASIFICACION = "CLASIFICACION";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_USUARIO = "ID_USUARIO";
        private const string DB_COL_CORREO_USUARIO = "CORREO_USUARIO";
        private const string DB_COL_NOMBRE_USUARIO = "NOMBRE_USUARIO";
        private const string DB_COL_MEMBRECIA = "MEMBRECIA";



        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_SOLICITUD_HOTEL_PR" };

            var c = (SolicitudHotel)entity;
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddVarcharParam(DB_COL_CEDULA_JURIDICA, c.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_EMPRESA_DUENNA, c.EmpresaDuenna);
            operation.AddVarcharParam(DB_COL_DIRECCION, c.Direccion);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, c.Descripcion);
            operation.AddVarcharParam(DB_COL_CADENA, c.Cadena);
            operation.AddIntParam(DB_COL_CLASIFICACION, c.Clasificacion);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddVarcharParam(DB_COL_ID_USUARIO, c.IdUsuario);
            operation.AddVarcharParam(DB_COL_CORREO_USUARIO, c.CorreoUsuario);
            operation.AddVarcharParam(DB_COL_NOMBRE_USUARIO, c.NombreUsuario);

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_SOLICITUD_HOTEL_PR" };

            var c = (SolicitudHotel)entity;
            operation.AddIntParam(DB_COL_CODIGO_SOLICITUD, c.CodigoSolicitud);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_SOLICITUDES_HOTELES_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_SOLICITUD_HOTEL_PR" };

            var c = (SolicitudHotel)entity;
            operation.AddIntParam(DB_COL_CODIGO_SOLICITUD, c.CodigoSolicitud);          
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddDoubleParam(DB_COL_MEMBRECIA, c.Membrecia);


            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_SOLICITUD_HOTEL_PR" };

            var c = (SolicitudHotel)entity;
            operation.AddIntParam(DB_COL_CODIGO_SOLICITUD, c.CodigoSolicitud);

            return operation;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var solicitudHotel = BuildObject(row);
                lstResults.Add(solicitudHotel);
            }

            return lstResults;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var solicitudHotel = new SolicitudHotel
            {
                CodigoSolicitud = GetIntValue(row, DB_COL_CODIGO_SOLICITUD),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                CedulaJuridica = GetStringValue(row, DB_COL_CEDULA_JURIDICA),
                EmpresaDuenna = GetStringValue(row, DB_COL_EMPRESA_DUENNA),
                Direccion = GetStringValue(row, DB_COL_DIRECCION),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Cadena = GetStringValue(row, DB_COL_CADENA),
                Clasificacion = GetIntValue(row, DB_COL_CLASIFICACION),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdUsuario = GetStringValue(row, DB_COL_ID_USUARIO),
                CorreoUsuario = GetStringValue(row, DB_COL_CORREO_USUARIO),
                NombreUsuario = GetStringValue(row, DB_COL_NOMBRE_USUARIO),
                Membrecia = GetDoubleValue(row, DB_COL_MEMBRECIA)
            };
 
            return solicitudHotel;
        }

    }
}
