using System;
using DataAcess.Crud;
using System.Collections.Generic;
using Entities;
using Exceptions;

namespace CoreAPI
{
    public class ImpuestoManager : BaseManager
    {

        private ImpuestoCrudFactory crudImpuesto;

        public ImpuestoManager()
        {
            crudImpuesto = new ImpuestoCrudFactory();
        }

        public void Create(TipoImpuesto impuesto)
        {
            try
            {
                var imp = crudImpuesto.Retrieve<TipoImpuesto>(impuesto);

                if (imp != null)
                {

                    throw new BussinessException(1);
                }
                else
                {
                    crudImpuesto.Create(impuesto);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<TipoImpuesto> RetrieveAll()
        {
            return crudImpuesto.RetrieveAll<TipoImpuesto>();
        }

        public TipoImpuesto RetrieveById(TipoImpuesto impuesto)
        {
            TipoImpuesto imp = null;
            try
            {
                imp = crudImpuesto.Retrieve<TipoImpuesto>(impuesto);
                if (imp == null)
                {
                    throw new BussinessException(2);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return imp;
        }

        public void Update(TipoImpuesto impuesto)
        {
            crudImpuesto.Update(impuesto);
        }

        public void Delete(TipoImpuesto impuesto)
        {
            crudImpuesto.Delete(impuesto);
        }
    }
}
