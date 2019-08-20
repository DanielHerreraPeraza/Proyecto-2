using Entities;
using System.Collections.Generic;

namespace DataAcess.Mapper
{
    interface IObjectMapper
    {
        List<Entity> BuildObjects(List<Dictionary<string,object>> lstRows);
        Entity BuildObject(Dictionary<string, object> row);
    }
}