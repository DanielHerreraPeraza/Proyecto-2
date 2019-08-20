using DataAcess.Dao;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Mapper
{

    public class ProductoMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_CODIGO = "CODIGO";
        private const string DB_COL_NOMBRE = "NOMBRE";
        private const string DB_COL_DESCRIPCION = "DESCRIPCION";
        private const string DB_COL_PRECIO = "PRECIO";
        private const string DB_COL_CANT_PRODUCTOS = "CANT_PRODUCTOS";
        private const string DB_COL_FOTO = "FOTO";
        private const string DB_COL_PROVEEDOR = "PROVEEDOR";
        private const string DB_COL_ID_ESTADO = "ID_ESTADO";
        private const string DB_COL_ESTADO = "ESTADO";
        private const string DB_COL_ID_CATEGORIA = "ID_CATEGORIA";
        private const string DB_COL_CATEGORIA = "CATEGORIA";
        private const string DB_COL_ID_TIPO_IMPUESTO = "ID_TIPO_IMPUESTO";
        private const string DB_COL_TIPO_IMPUESTO = "TIPO_IMPUESTO";
        private const string DB_COL_IMPUESTO = "IMPUESTO";
        private const string DB_COL_ID_SERVICIO = "ID_SERVICIO";
        private const string DB_COL_ID_HOTEL = "ID_HOTEL";

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_PRODUCTO_PR" };

            var p = (Producto)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, p.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, p.Descripcion);
            operation.AddDecimalParam(DB_COL_PRECIO, p.Precio); 
            operation.AddVarcharParam(DB_COL_FOTO, p.Foto);
            operation.AddVarcharParam(DB_COL_PROVEEDOR, p.Proveedor);
            operation.AddIntParam(DB_COL_CANT_PRODUCTOS, p.CantProductos);
            operation.AddIntParam(DB_COL_ID_CATEGORIA, p.IdCategoria);
            operation.AddIntParam(DB_COL_ID_TIPO_IMPUESTO, p.IdTipoImpuesto);
            operation.AddVarcharParam(DB_COL_ID_SERVICIO, p.IdServicio);

            return operation;
        }


        


        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PRODUCTOS_PR" };

            var p = (Producto)entity;
            operation.AddVarcharParam(DB_COL_ID_SERVICIO, p.IdServicio);


            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_PRODUCTO_PR" };

            var p = (Producto)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);

            return operation;
        }

        public SqlOperation GetRetriveAllByHotelStatement(string IdHotel)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_PRODUCTOS_BY_HOTEL_PR" };
            operation.AddVarcharParam(DB_COL_ID_HOTEL, IdHotel);
            return operation;

        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_PRODUCTO_PR" };

            var  p = (Producto)entity;

            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);
            operation.AddVarcharParam(DB_COL_NOMBRE, p.Nombre);
            operation.AddVarcharParam(DB_COL_DESCRIPCION, p.Descripcion);
            operation.AddDecimalParam(DB_COL_PRECIO, p.Precio);
            operation.AddIntParam(DB_COL_CANT_PRODUCTOS, p.CantProductos);
            operation.AddVarcharParam(DB_COL_FOTO, p.Foto);
            operation.AddVarcharParam(DB_COL_PROVEEDOR, p.Proveedor);
            operation.AddVarcharParam(DB_COL_ESTADO, p.Estado);
            operation.AddIntParam(DB_COL_ID_CATEGORIA, p.IdCategoria);
            operation.AddIntParam(DB_COL_ID_TIPO_IMPUESTO, p.IdTipoImpuesto);
            operation.AddVarcharParam(DB_COL_ID_SERVICIO, p.IdServicio);


            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_PRODUCTO_PR" };

            var p = (Producto)entity;
            operation.AddVarcharParam(DB_COL_CODIGO, p.Codigo);
            return operation;
        }


        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var producto = BuildObject(row);
                lstResults.Add(producto);
            }

            return lstResults;
        }



        public Entity BuildObject(Dictionary<string, object> row)
        {
            var producto = new Producto
            {
                Codigo = GetStringValue(row, DB_COL_CODIGO),
                Nombre = GetStringValue(row, DB_COL_NOMBRE),
                Descripcion = GetStringValue(row, DB_COL_DESCRIPCION),
                Proveedor = GetStringValue(row, DB_COL_PROVEEDOR),
                Precio = GetDecimalValue(row, DB_COL_PRECIO),
                CantProductos = GetIntValue(row, DB_COL_CANT_PRODUCTOS),
                Foto = GetStringValue(row, DB_COL_FOTO),
                IdEstado = GetStringValue(row, DB_COL_ID_ESTADO),
                Estado = GetStringValue(row, DB_COL_ESTADO),
                IdCategoria = GetIntValue(row, DB_COL_ID_CATEGORIA),
                Categoria = GetStringValue(row, DB_COL_CATEGORIA),
                IdTipoImpuesto = GetIntValue(row, DB_COL_ID_TIPO_IMPUESTO),
                TipoImpuesto = GetStringValue(row, DB_COL_TIPO_IMPUESTO),
                Impuesto = GetDecimalValue(row, DB_COL_IMPUESTO),
                IdServicio = GetStringValue(row, DB_COL_ID_SERVICIO)

            };

            return producto;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }
    }
}
