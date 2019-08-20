using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public class DevolucionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
       

        public SqlOperation GetCreateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllByIdStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveStatement(Entity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(Entity entity)
        {
            throw new NotImplementedException();
        }
        public Entity BuildObject(Dictionary<string, object> row)
        {
            throw new NotImplementedException();
        }

        public List<Entity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            throw new NotImplementedException();
        }
    }
}
