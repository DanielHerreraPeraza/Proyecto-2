using System.Collections.Generic;
using DataAcess.Dao;
using Entities;

namespace DataAcess.Crud
{
    public abstract class CrudFactory
    {
        protected SqlDao dao;

        public abstract void Create(Entity entity);
        public abstract T Retrieve<T>(Entity entity);        
        public abstract List<T> RetrieveAll<T>();
        public abstract void Update(Entity entity);
        public abstract void Delete(Entity entity);
        //public abstract List<T> RetrieveAllById<T>(Entity entity);
    }
}
