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
    public class BitacoraManager : BaseManager
    {
        private BitacoraCrudFactory crudBitacora;

        public BitacoraManager()
        {
            crudBitacora = new BitacoraCrudFactory();
        }

        public void Create(Bitacora bitacora)
        {
            try
            {
                var bit = crudBitacora.Retrieve<Bitacora>(bitacora);

                if (bit != null)
                {

                    throw new BussinessException(3);
                }
                else
                {
                    crudBitacora.Create(bitacora);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Bitacora> RetrieveAll()
        {
            return crudBitacora.RetrieveAll<Bitacora>();
        }

        public List<Bitacora> RetrieveAllBiHotel(string id)
        {
            List<Bitacora> allBitacoras = RetrieveAll();

            return allBitacoras.FindAll(FindBitacoraHotel);

            bool FindBitacoraHotel(Bitacora bitacora)
            {

                if (bitacora.IdHotel == id)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public Bitacora RetrieveById(Bitacora bitacora)
        {
            Bitacora bit = null;
            try
            {
                bit = crudBitacora.Retrieve<Bitacora>(bitacora);
                if (bit == null)
                {
                    throw new BussinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return bit;
        }
    }
}

