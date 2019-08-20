using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class Vista_RolMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID_ROL = "ID_ROL";
        private const string DB_COL_ID_VISTA = "ID_VISTA";

        public Entity BuildObject(Dictionary<string, object> row)
        {
            var vistaRol = new Vista_Rol
            {
                IdVista = GetStringValue(row, DB_COL_ID_VISTA),
                IdRol = GetStringValue(row, DB_COL_ID_ROL)
            };

            return vistaRol;
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<Entity>();

            foreach (var row in lstRows)
            {
                var usuario = BuildObject(row);
                lstResults.Add(usuario);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_VISTA_ROL_PR" };

            var ru = (Vista_Rol)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdRol);
            operation.AddVarcharParam(DB_COL_ID_VISTA, ru.IdVista);

            return operation;
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_VISTA_ROL_PR" };

            var ru = (Vista_Rol)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdRol);
            operation.AddVarcharParam(DB_COL_ID_VISTA, ru.IdVista);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_VISTAS_ROL_PR" };
            return operation;
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_VISTA_ROL_PR" };

            var ru = (Vista_Rol)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdRol);
            operation.AddVarcharParam(DB_COL_ID_VISTA, ru.IdVista);

            return operation;
        }

        public SqlOperation GetRetrieveAllVistasRolStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_VISTAS_ROLID_PR" };

            var ru = (Vista_Rol)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdRol);

            return operation;
        }

        public SqlOperation GetRetrieveAllRolesVistaStatement(Entity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_ROLES_VISTAID_PR" };

            var ru = (Vista_Rol)entity;
            operation.AddVarcharParam(DB_COL_ID_ROL, ru.IdVista);

            return operation;
        }
    }
}
