using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Twilio.Rest.Api.V2010.Account;

namespace TwilioBot
{
    public class Program
    {
        const string Sid = "AC668ffb74cd59a84de6bc10c8d80a372e";
        const string At = "06f7d80d8aa5380eaeab5f64adcbd957";
        const string TelefonoTwilio = "+12134442129";
        const string TelefonoCliente = "+50684042743";
        const string webURL = "https://adtripapi.azurewebsites.net/api/Reserva/GetReservasList";
        const string webURLP = "https://adtripapi.azurewebsites.net/api/ParametrizablesHotel/GetParametrizablesList";

        static void Main(string[] args)
        {
            bool accion = true;
            do
            {
                DateTime fechaHoy = DateTime.Now;

                if (fechaHoy.Hour == 0 && fechaHoy.Minute == 0 && fechaHoy.Second == 0)
                {
                    ObtenerTiempoReserva();
                }

            } while (accion == true);

        }

        public static void ObtenerTiempoReserva()
        {
            List<Reserva> reservas = GetReserva();
            foreach (Reserva r in reservas)
            {
                DateTime fechaActual = DateTime.Now;
                if (fechaActual.Year == r.FechaInicio.Year)
                {
                    if (fechaActual.Month == r.FechaInicio.Month)
                    {
                        int tiempoDiferencia = r.FechaInicio.Day - fechaActual.Day;

                        if (tiempoDiferencia == 1)
                        {
                            ParametrizablesHotel p = GetParametrizable(r.IdHotel);
                            EnviarMensaje(p.Mensaje);
                        }
                    }
                }
            }
        }

        public static void EnviarMensaje(string mensaje)
        {
            var message = MessageResource.Create(
                            body: mensaje,
                            from: new Twilio.Types.PhoneNumber(TelefonoTwilio),
                            to: new Twilio.Types.PhoneNumber(TelefonoCliente)
                            );
            Console.WriteLine(message.Sid);
        }

        private static List<Reserva> GetReserva()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            var response = client.DownloadString(webURL);
            var options = JsonConvert.DeserializeObject<List<Reserva>>(response);
            return options;
        }

        private static ParametrizablesHotel GetParametrizable(string id)
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            var response = client.DownloadString(webURLP + id);
            var options = JsonConvert.DeserializeObject<ParametrizablesHotel>(response);
            return options;
        }
    }
}
