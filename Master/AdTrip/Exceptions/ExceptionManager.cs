using DataAcess.Crud;
using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Exceptions
{
    public class ExceptionManager
    {
    
        public string PATH = HttpContext.Current.Server.MapPath("~/Logs/");

        private static ExceptionManager instance;

        private static Dictionary<int, ApplicationMessage> messages = new Dictionary<int, ApplicationMessage>();

        private ExceptionManager()
        {
            LoadMessages();
        }

        public static ExceptionManager GetInstance()
        {
            if (instance == null)
                instance = new ExceptionManager();

            return instance;
        }

        public void Process(Exception ex)
        {
            var bex = new BussinessException();

            if (ex.GetType() == typeof(BussinessException))
            {
                bex = (BussinessException)ex;
            }
            else
            {
                bex = new BussinessException(0, ex);
            }

            ProcessBussinesException(bex);
        }

        private void ProcessBussinesException(BussinessException bex)
        {
            var today = DateTime.Now.ToString("yyyyMMdd_hh");
            var logName = PATH + today  + "_" + "log.txt";
            bex.AppMessage = GetMessage(bex);

            var message = bex.AppMessage.Message + "\n" + bex.StackTrace + "\n";

            if (bex.InnerException!=null)
                message += bex.InnerException.Message + "\n" + bex.InnerException.StackTrace;

            using (StreamWriter w = File.AppendText(logName))
            {
                Log(message, w);
            }

            throw bex;
    
        }

        public ApplicationMessage GetMessage(BussinessException bex)
        {

            var appMessage = new ApplicationMessage
            {
                Message = "Message not found!"
            };
            

            if (messages.ContainsKey(bex.ExceptionId))
                appMessage = messages[bex.ExceptionId];



            return appMessage;

        }

        private void LoadMessages()
        {

            var crudmessages = new ApplicationMessageCrudFactory();

            var lstmessages = crudmessages.RetrieveAll<ApplicationMessage>();

            foreach (var appmessage in lstmessages)//de los mensajes y llenelos
            {
                messages.Add(appmessage.Id, appmessage);
            }

        }

        private void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

    }
}
