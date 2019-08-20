using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess.Dao;
using DataAcess.Mapper;
using Entities;

namespace DataAcess.Crud
{
    public class FotoCrudFactory : CrudFactory
    {

        FotoMapper mapper;


        public FotoCrudFactory() : base()
        {
            mapper = new FotoMapper();
            dao = SqlDao.GetInstance();
        }


        public override void Create(Entity entity)
        {
            var foto = (Foto)entity;
            var sqlOperation = mapper.GetCreateStatement(foto);
            dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveAllById<T>(Entity entity)
        {
            var listaFotos = new List<T>();
            var listObjects = dao.ExecuteQueryProcedure(mapper.GetRetrieveStatement(entity));
            if (listObjects.Count > 0)
            {
                var objs = mapper.BuildObjects(listObjects);
                foreach (var p in objs)
                {
                    listaFotos.Add((T)Convert.ChangeType(p, typeof(T)));
                }
            }
            return listaFotos;
        }



        public override List<T> RetrieveAll<T>()
        {
            var listaFotos = new List<T>();
            var listObjects = dao.ExecuteQueryProcedure(mapper.GetRetrieveAllStatement());
            if (listObjects.Count > 0)
            {
                var objs = mapper.BuildObjects(listObjects);
                foreach (var p in objs)
                {
                    listaFotos.Add((T)Convert.ChangeType(p, typeof(T)));
                }
            }
            return listaFotos;
        }

        public override void Update(Entity entity)
        {
            var foto = (Foto)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(foto));
        }

        public override void Delete(Entity entity)
        {
            var foto = (Foto)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(foto));
        }

        public override T Retrieve<T>(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
