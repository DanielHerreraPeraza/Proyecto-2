using Entities;
using System;

namespace Exceptions
{
    public class BussinessException : Exception
    {
        public int ExceptionId;
        public string ExceptionMessage;
        public ApplicationMessage AppMessage { get; set; }
    
        public BussinessException()
        {

        }

        public BussinessException(int exceptionId)
        {
            ExceptionId = exceptionId;
        }

        public BussinessException(int exceptionId, Exception innerException)
        {
            ExceptionId = exceptionId; 
        }
    }
}
