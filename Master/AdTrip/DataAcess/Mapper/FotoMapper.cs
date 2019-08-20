using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{

    public class FotoMapper : EntityMapper, ISqlStaments, IObjectMapper
    {

        private const string DB_COL_ENTIDAD = "ENTIDAD";
        private const string DB_COL_ID_ENTIDAD = "ID_ENTIDAD";
        private const string DB_COL_FOTO = "FOTO";
        private const string DB_COL_TIPO_FOTO = "TIPO_FOTO";

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_FOTO_PR" };

            var f = (Foto)entity;
            operation.AddVarcharParam(DB_COL_ENTIDAD, f.Entidad);
            operation.AddVarcharParam(DB_COL_ID_ENTIDAD, f.IdEntidad);
            operation.AddVarcharParam(DB_COL_FOTO, f.UrlFoto);
            operation.AddVarcharParam(DB_COL_TIPO_FOTO, f.TipoFoto);

            return operation;
        }


        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_FOTO_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_FOTOS_PR" };

            var f = (Foto)entity;
            operation.AddVarcharParam(DB_COL_ENTIDAD, f.Entidad);
            operation.AddVarcharParam(DB_COL_ID_ENTIDAD, f.IdEntidad);

            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_FOTO_PR" };
            
            var f = (Foto)entity;
            operation.AddVarcharParam(DB_COL_ENTIDAD, f.Entidad);
            operation.AddVarcharParam(DB_COL_ID_ENTIDAD, f.IdEntidad);
            operation.AddVarcharParam(DB_COL_FOTO, f.UrlFoto);
            operation.AddVarcharParam(DB_COL_FOTO, f.TipoFoto);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_FOTO_PR" };

            var f = (Foto)entity;
            operation.AddVarcharParam(DB_COL_ENTIDAD, f.Entidad);
            operation.AddVarcharParam(DB_COL_ID_ENTIDAD, f.IdEntidad);
            operation.AddVarcharParam(DB_COL_FOTO, f.UrlFoto);
            return operation;
        }


        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var foto = BuildObject(row);
                lstResults.Add(foto);
            }

            return lstResults;
        }



        public Entity BuildObject(Dictionary<string, object> row)
        {
            var foto = new Foto
            {
                Entidad = GetStringValue(row, DB_COL_ENTIDAD),
                IdEntidad = GetStringValue(row, DB_COL_ID_ENTIDAD),
                UrlFoto = GetStringValue(row, DB_COL_FOTO),
                TipoFoto = GetStringValue(row, DB_COL_TIPO_FOTO)
            };

            return foto;
        }


    }
}
