using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Crud;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class ServicioMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
    
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_ID_ESTADO = "ID_ESTADO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_FOTO_PERFIL = "FOTO";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_SERVICIO_PR" };

            var s = (Servicio)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, s.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, s.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, s.Descripcion);
            operation.AddVarcharParam(DB_COL_FOTO_PERFIL, s.FotoPerfil);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, s.IdHotel);

            return operation;
        }
       

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_SERVICIO_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_SERVICIO_PR" };

            var s = (Servicio)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, s.Codigo);
            return operation;
        }


        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_SERVICIOS_BY_HOTEL_PR" };

            var s = (Servicio)entity;
            operation.AddVarcharParam(DB_COL_ID_HOTEL, s.IdHotel);

            return operation;
        }


        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_SERVICIO_PR" };

            var s = (Servicio)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, s.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, s.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, s.Descripcion);
            operation.AddVarcharParam(DB_COL_ESTADO, s.Estado);
            operation.AddVarcharParam(DB_COL_FOTO_PERFIL, s.FotoPerfil);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, s.IdHotel);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_SERVICIO_PR" };

            var s = (Servicio)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, s.Codigo);
            operation.AddVarcharParam(DB_COL_ID_HOTEL, s.IdHotel);
            return operation;
        }


        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var servicio = BuildObject(row);
                lstResults.Add(servicio);
            }

            return lstResults;
        }



        public Entity BuildObject(Dictionary<string, object> row)
        {
            var servicio = new Servicio
            {
                Codigo = GetStringValue(row, DB_COL_CODIGO), 
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION), 
                IdEstado = GetStringValue(row, DB_COL_ID_ESTADO), 
                Estado = GetStringValue(row, DB_COL_ESTADO), 
                FotoPerfil = GetStringValue(row, DB_COL_FOTO_PERFIL), 
                IdHotel = GetStringValue(row, DB_COL_ID_HOTEL)
            };

            return servicio;
        }


    }
}
