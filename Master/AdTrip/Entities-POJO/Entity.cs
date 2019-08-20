using System;

namespace Entities
{
    public class Entity
    {
        public String GetEntityInformation()
        {
            var dump = ObjectDumper.Dump(this);
            return dump;
        }
    }
}
