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
    public class CategoriaManager : BaseManager
    {
        private CategoriaCrudFactory crudCategoria;

        public CategoriaManager()
        {
            crudCategoria = new CategoriaCrudFactory();
        }

        public void Create(Categoria categoria)
        {
            try
            {
                var cat = crudCategoria.Retrieve<Categoria>(categoria);

                if (cat != null)
                {

                    throw new BussinessException(1);
                }
                else
                {
                    crudCategoria.Create(categoria);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Categoria> RetrieveAll()
        {
            return crudCategoria.RetrieveAll<Categoria>();
        }

        public Categoria RetrieveById(Categoria categoria)
        {
            Categoria cat = null;
            try
            {
                cat = crudCategoria.Retrieve<Categoria>(categoria);
                if (cat == null)
                {
                    throw new BussinessException(2);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return cat;
        }

        public void Update(Categoria categoria)
        {
            crudCategoria.Update(categoria);
        }

        public void Delete(Categoria categoria)
        {
            crudCategoria.Delete(categoria);
        }
    }
}
