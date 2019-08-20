using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    class CategoriaMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_ESTADO = "ID_ESTADO"; //Nuevo

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_CATEGORIA_PRODUCTO_PR" };

            var cat = (Categoria)entity;
            operation.AddIntParam(DB_COL_CODIGO, cat.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, cat.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, cat.Descripcion);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_CATEGORIA_PRODUCTO_PR" };

            var cat = (Categoria)entity;
            operation.AddIntParam(DB_COL_CODIGO, cat.Codigo);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {

            var operation = new SqlOperation { ProcedureName = "RET_ALL_CATEGORIA_PRODUCTO_PR" };

            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RET_CATEGORIA_PR" };

            var cat = (Categoria)entity;
            operation.AddIntParam(DB_COL_CODIGO, cat.Codigo);

            return operation;
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_CATEGORIA_PRODUCTO_PR" };

            var cat = (Categoria)entity;
            operation.AddIntParam(DB_COL_CODIGO, cat.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, cat.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, cat.Descripcion);
            operation.AddVarcharParam(DB_COL_ESTADO, cat.Estado);

            return operation;
        }

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var categoria = new Categoria
            {
                Codigo = GetIntValue(row, DB_COL_CODIGO),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdEstado = GetStringValue(row, DB_COL_ID_ESTADO)
            };

            return categoria;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var categoria = BuildObject(row);
                lstResults.Add(categoria);
            }

            return lstResults;
        }


        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            throw new NotImplementedException();
        }


    }
}
