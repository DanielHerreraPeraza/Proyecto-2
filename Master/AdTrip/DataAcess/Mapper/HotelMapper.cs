using DataAcess.Dao;
using Entities;
using System.Collections.Generic;


namespace DataAcess.Mapper
{
    class HotelMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CEDULA_JURIDICA = "CEDULA_JURIDICA";
        private const string DB_COL_ID_GERENTE = "ID_GERENTE";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_CLASIFICACION = "CLASIFICACION";
        private const string DB_COL_UBICACION_X = "UBICACION_X";
        private const string DB_COL_UBICACION_Y = "UBICACION_Y";
        private const string DB_COL_PROVINCIA = "PROVINCIA";
        private const string DB_COL_CANTON = "CANTON";
        private const string DB_COL_DISTRITO = "DISTRITO";
        private const string DB_COL_V_PROVINCIA = "V_PROVINCIA";
        private const string DB_COL_V_CANTON = "V_CANTON";
        private const string DB_COL_V_DISTRITO = "V_DISTRITO";
        private const string DB_COL_DIRECCION = "DIRECCION";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_CADENA = "CADENA";
        private const string DB_COL_PROM_CALIFICACION = "PROM_CALIFICACION";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_CORREO = "CORREO_ELECTRONICO";
        private const string DB_COL_TELEFONOS = "TELEFONOS";
        private const string DB_COL_URL_SITIO = "URL_SITIO_WEB";
        private const string DB_COL_MEMBRECIA = "MEMBRECIA";
        private const string DB_COL_ESTADO_MEMBRECIA = "ESTADO_MEMBRECIA";
        private const string DB_COL_FECHA_FIN = "FECHA_FIN";
        private const string DB_COL_LOGO = "LOGO";
        private const string DB_COL_FOTO_PERFIL = "FOTO_PERFIL";
        private const string DB_COL_NUM_FACTURACION = "NUM_FACTURACION";

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HOTEL_PR" };

            var c = (Hotel)entity;

            operation.AddVarcharParam(DB_COL_CEDULA_JURIDICA, c.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_ID_GERENTE, c.IdGerente);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddIntParam(DB_COL_CLASIFICACION, c.Clasificacion);
            operation.AddVarcharParam(DB_COL_UBICACION_X, c.UbicacionX);
            operation.AddVarcharParam(DB_COL_UBICACION_Y, c.UbicacionY);
            operation.AddVarcharParam(DB_COL_PROVINCIA, c.Provincia);
            operation.AddVarcharParam(DB_COL_CANTON, c.Canton);
            operation.AddVarcharParam(DB_COL_DISTRITO, c.Distrito);
            operation.AddVarcharParam(DB_COL_DIRECCION, c.Direccion);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, c.Descripcion);
            operation.AddVarcharParam(DB_COL_CADENA, c.Cadena);
            operation.AddDoubleParam(DB_COL_PROM_CALIFICACION, c.PromCalificacion);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddVarcharParam(DB_COL_TELEFONOS, c.Telefonos);
            operation.AddVarcharParam(DB_COL_URL_SITIO, c.URLSitio);
            operation.AddDoubleParam(DB_COL_MEMBRECIA, c.Membrecia);
            operation.AddVarcharParam(DB_COL_ESTADO_MEMBRECIA, c.EstadoMembrecia);
            operation.AddDateTimeParam(DB_COL_FECHA_FIN, c.FechaFin);
            operation.AddVarcharParam(DB_COL_LOGO, c.Logo);
            operation.AddVarcharParam(DB_COL_FOTO_PERFIL, c.FotoPerfil);
            operation.AddIntParam(DB_COL_NUM_FACTURACION, c.NumFacturacion);

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HOTEL_PR" };

            var c = (Hotel)entity;
            operation.AddVarcharParam(DB_COL_CEDULA_JURIDICA, c.CedulaJuridica);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_HOTELES_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            var c = (Hotel)entity;         
            var operation = new SqlOperation { ProcedureName = "RET_HOTELES_ROL" };
            operation.AddVarcharParam(DB_COL_ID_GERENTE, c.IdGerente);
            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_HOTEL_PR" };

            var c = (Hotel)entity;
            operation.AddVarcharParam(DB_COL_CEDULA_JURIDICA, c.CedulaJuridica);
            operation.AddVarcharParam(DB_COL_NOMBRE, c.Nombre);
            operation.AddIntParam(DB_COL_CLASIFICACION, c.Clasificacion);
            operation.AddVarcharParam(DB_COL_UBICACION_X, c.UbicacionX);
            operation.AddVarcharParam(DB_COL_UBICACION_Y, c.UbicacionY);
            operation.AddVarcharParam(DB_COL_PROVINCIA, c.Provincia);
            operation.AddVarcharParam(DB_COL_CANTON, c.Canton);
            operation.AddVarcharParam(DB_COL_DISTRITO, c.Distrito);
            operation.AddVarcharParam(DB_COL_DIRECCION, c.Direccion);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, c.Descripcion);
            operation.AddVarcharParam(DB_COL_CADENA, c.Cadena);
            operation.AddVarcharParam(DB_COL_ESTADO, c.Estado);
            operation.AddVarcharParam(DB_COL_CORREO, c.Correo);
            operation.AddVarcharParam(DB_COL_TELEFONOS, c.Telefonos);
            operation.AddVarcharParam(DB_COL_URL_SITIO, c.URLSitio);
            operation.AddVarcharParam(DB_COL_ESTADO_MEMBRECIA, c.EstadoMembrecia);
            operation.AddDateTimeParam(DB_COL_FECHA_FIN, c.FechaFin);
            operation.AddVarcharParam(DB_COL_LOGO, c.Logo);
            operation.AddVarcharParam(DB_COL_FOTO_PERFIL, c.FotoPerfil);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HOTEL_PR" };

            var c = (Hotel)entity;
            operation.AddVarcharParam(DB_COL_CEDULA_JURIDICA, c.CedulaJuridica);

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
            var hotel = new Hotel
            {
                CedulaJuridica = GetStringValue(row, DB_COL_CEDULA_JURIDICA),
                IdGerente = GetStringValue(row, DB_COL_ID_GERENTE),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Clasificacion = GetIntValue(row, DB_COL_CLASIFICACION),
                UbicacionX = GetStringValue(row, DB_COL_UBICACION_X),
                UbicacionY = GetStringValue(row, DB_COL_UBICACION_Y),
                Provincia = GetStringValue(row, DB_COL_PROVINCIA),
                Canton = GetStringValue(row, DB_COL_CANTON),
                Distrito = GetStringValue(row, DB_COL_DISTRITO),
                vProvincia = GetStringValue(row, DB_COL_V_PROVINCIA),
                vCanton = GetStringValue(row, DB_COL_V_CANTON),
                vDistrito = GetStringValue(row, DB_COL_V_DISTRITO),
                Direccion = GetStringValue(row, DB_COL_DIRECCION),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Cadena = GetStringValue(row, DB_COL_CADENA),
                PromCalificacion = GetDoubleValue(row, DB_COL_PROM_CALIFICACION),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                Correo = GetStringValue(row, DB_COL_CORREO),
                Telefonos = GetStringValue(row, DB_COL_TELEFONOS),
                URLSitio = GetStringValue(row, DB_COL_URL_SITIO),
                Membrecia = GetDoubleValue(row, DB_COL_MEMBRECIA),
                EstadoMembrecia = GetStringValue(row, DB_COL_ESTADO_MEMBRECIA),
                FechaFin = GetDateValue(row, DB_COL_FECHA_FIN),
                Logo = GetStringValue(row, DB_COL_LOGO),
                FotoPerfil = GetStringValue(row, DB_COL_FOTO_PERFIL),
            };

            return hotel;
        }

    }
}
