using DataAcess.Dao;
using Entities;

namespace DataAcess.Mapper
{
    public interface ISqlStaments
    {
        SqlOperation GetCreateStatement(Entity entity);
        SqlOperation GetRetrieveStatement(Entity entity);
        SqlOperation GetRetrieveAllStatement();
        SqlOperation GetUpdateStatement(Entity entity);
        SqlOperation GetDeleteStatement(Entity entity);
    }
}