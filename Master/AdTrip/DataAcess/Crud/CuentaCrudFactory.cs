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
    public class CuentaCrudFactory : CrudFactory
    {

        CuentaMapper mapper;

        public CuentaCrudFactory() : base()
        {
            mapper = new CuentaMapper();
            dao = SqlDao.GetInstance();
        }


        public override void Create(Entity entity)
        {
            var cuenta = (Cuenta)entity;
            var sqlOperation = mapper.GetCreateStatement(cuenta);
            dao.ExecuteProcedure(sqlOperation);
        }

        public void CreateGanancia(Entity entity)
        {
            var cuenta = (Cuenta)entity;
            var sqlOperation = mapper.GetCreateGananciaStatement(cuenta);
            dao.ExecuteProcedure(sqlOperation);
        }


        public void PagarMembresia(Entity entity)
        {
            var cuenta = (Cuenta)entity;
            var sqlOperation = mapper.GetPagoMembresiaStatement(cuenta);
            dao.ExecuteProcedure(sqlOperation);
        }

        public void PagarReserva(Entity entity)
        {
            var cuenta = (Cuenta)entity;
            var sqlOperation = mapper.GetPagoReservaStatement(cuenta);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override void Delete(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(Entity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(Entity entity)
        {
            var cuenta = (Cuenta)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(cuenta));
        }
    }
}
