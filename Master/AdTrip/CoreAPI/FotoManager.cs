using DataAcess.Crud;
using Entities;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI
{
    public class FotoManager : BaseManager
    {
               
        private FotoCrudFactory crudFoto;

        public FotoManager()
        {
            crudFoto = new FotoCrudFactory();
        }

        public void Create(Foto foto)
        {
            try
            {
                crudFoto.Create(foto);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Foto> RetrieveAll()
        {
            return crudFoto.RetrieveAll<Foto>();
        }

        public Foto RetrieveById(Foto foto)
        {
            Foto f = null;
            try
            {
                f = crudFoto.Retrieve<Foto>(foto);
                if (f == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return f;
        }


        public List<Foto> RetrieveAllById(Foto foto)
        {
            return crudFoto.RetrieveAllById<Foto>(foto);
        }


        public void Update(Foto foto)
        {
            crudFoto.Update(foto);
        }

        public void Delete(Foto foto)
        {
            crudFoto.Delete(foto);
        }
    }

}

