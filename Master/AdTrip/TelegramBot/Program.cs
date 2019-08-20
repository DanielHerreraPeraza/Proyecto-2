using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("766532439:AAHIOhVEzv4GjYwXR1-2_tfBl2J8gcE-YM4");
        private static string URL_API_HOTELES = "https://adtripapi.azurewebsites.net/api/hotel/GetListaHoteles";
        private static string URL_API_RESERVAS = "https://adtripapi.azurewebsites.net/api/reserva/GetListaReservas";
        private static string URL_API_MAPQUEST = "http://www.mapquestapi.com/directions/v2/routematrix?key=bQsjr2FrPp9ISzBicAjdrdA2RPmlYfWm";

        static void Main(string[] args)
        {
            //Método que se ejecuta cuando se recibe un mensaje
            Bot.OnMessage += BotOnMessageReceived;

            //Método que se ejecuta cuando se recibe un callbackQuery
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;

            //Método que se ejecuta cuando se recibe un error
            Bot.OnReceiveError += BotOnReceiveError;

            //Inicia el bot
            Bot.StartReceiving();
            Console.WriteLine("Bot levantado!");
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message.Type == MessageType.Photo)
            {
                var test = Bot.GetFileAsync(message.Photo[message.Photo.Count() - 1].FileId);
                Console.Write(test.Result);
                var download_url = @"https://api.telegram.org/file/bot<token>/" + test.Result.FilePath;
            }

            if (message == null || message.Type != MessageType.Text) return;

            if (message.Text.Equals("1") || message.Text.Equals("2") || message.Text.Equals("3") || message.Text.Equals("4") || message.Text.Equals("5"))
            {
                await ListarHotelesAsync(message.Chat.Id, "Clasificación", message.Text);
                return;
            }

            if (message.Text.Equals("San José") || message.Text.Equals("Cartago") || message.Text.Equals("Alajuela") || message.Text.Equals("Heredia") ||
                message.Text.Equals("Limón") || message.Text.Equals("Puntarenas") || message.Text.Equals("Guanacaste"))
            {
                await ListarHotelesAsync(message.Chat.Id, "Provincia", message.Text);
                return;
            }

            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Equals("Para listar sus reservas, por favor ingrese su número de cédula"))
            {
                await ListarReservasAsync(message.Chat.Id, message.Text);
            }

            switch (message.Text.Split(' ').First())
            {
                case "/reservas":
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Mis reservas");
                    break;

                case "/ubicacion":
                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Ubicación");
                    break;

                case "Todos":
                    await ListarHotelesAsync(message.Chat.Id, "Todos", message.Text);
                    break;

                case "Estrellas":
                    List<string> estrellasHotel = new List<string>
                    {
                        "5", "4", "3", "2", "1"
                    };

                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "¿Cantidad de estrellas?",
                        replyMarkup: GetReplyKeyboard(estrellasHotel));
                    break;

                case "Provincia":
                    ReplyKeyboardMarkup ubicacionHotel = new[]
                    {
                        new[] { "San José", "Cartago" },
                        new[] { "Alajuela", "Heredia" },
                        new[] { "Limón", "Puntarenas" },
                        new[] { "Guanacaste" }
                    };

                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Seleccione una provincia",
                        replyMarkup: ubicacionHotel);
                    break;

                case "/hoteles":
                    List<string> filtroHoteles = new List<string>
                    {
                        "Todos",
                        "Estrellas",
                        "Ubicación",
                        "Calificación"
                    };

                    await Bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "¿Cómo desea listar los hoteles?",
                        replyMarkup: GetReplyKeyboard(filtroHoteles));
                    break;

                default:
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(5);

                    var keyboardEjemplo1 = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData(
                                text:"Hoteles",
                                callbackData: "hoteles"),
                            InlineKeyboardButton.WithCallbackData(
                                text:"Reservas",
                                callbackData: "reservas"),
                        }
                    });

                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Hola, " + message.Chat.FirstName + " ¿En qué podemos ayudarle?",
                        replyMarkup: keyboardEjemplo1);
                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;

            switch (callbackQuery.Data)
            {
                case "hoteles":
                    List<string> filtroHoteles = new List<string>
                    {
                        "Todos", "Estrellas", "Provincia"
                    };

                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "¿Cómo desea listar los hoteles?",
                        replyMarkup: GetReplyKeyboard(filtroHoteles));
                    break;

                case "reservas":
                    await Bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: "Para listar sus reservas, por favor ingrese su número de cédula",
                        replyMarkup: new ForceReplyMarkup());
                    break;

                default:
                    foreach (Hotel h in GetHotelesFromAPI())
                    {
                        if (callbackQuery.Data.Equals(h.CedulaJuridica))
                        {
                            float lat = float.Parse(h.UbicacionX.Replace('.',','));
                            float lon = float.Parse(h.UbicacionY.Replace('.', ','));

                            await Bot.SendVenueAsync(
                                chatId: callbackQuery.Message.Chat.Id,
                                latitude: lat,
                                longitude: lon,
                                title: h.Nombre,
                                address: h.Direccion
                                );
                        }
                    }
                    break;
            }
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}",
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }

        public static ReplyKeyboardMarkup GetReplyKeyboard(List<string> keys)
        {
            var rkm = new ReplyKeyboardMarkup();
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            foreach (var t in keys)
            {
                KeyboardButton kButton = new KeyboardButton(t);

                cols.Add(kButton);
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }
            rkm.Keyboard = rows.ToArray();
            rkm.OneTimeKeyboard = true;
            return rkm;
        }

        private static string PuntuacionAEstrellas(int clasificacion)
        {
            char star = Convert.ToChar(Convert.ToUInt32("0x2b50", 16));
            char blank = Convert.ToChar(Convert.ToUInt32("0x2b52", 16));

            string result = "";

            for (int i = 0; i < clasificacion; i++)
            {
                result += star;
            }

            int diferencia = 5 - clasificacion;
            for (int i = (5 - clasificacion); i > 0; i--)
            {
                result += blank;
            }
            return result;
        }

        private static async Task UbicacionHotelAsync(long chatId, string nombreHotel)
        {
            foreach(Hotel hotel in GetHotelesFromAPI())
            {
                if (hotel.Nombre.ToLowerInvariant().Contains(nombreHotel.ToLowerInvariant()))
                {
                    await Bot.SendVenueAsync(
                        chatId: chatId,
                        latitude: 9.932551f,
                        longitude: -84.031086f,
                        title: hotel.Nombre,
                        address: hotel.Direccion
                        );
                }
            }                    
        }

        private static async Task ListarHotelesAsync(long chatId, string filtro, string valor)
        {
            List<Hotel> hoteles = GetHotelesFromAPI();

            switch (filtro)
            {
                case "Clasificación":
                    hoteles = hoteles.Where(hotel => hotel.Clasificacion == Int32.Parse(valor)).ToList<Hotel>();
                    break;
                case "Provincia":
                    hoteles = hoteles.Where(hotel => hotel.vProvincia.Equals(valor)).ToList<Hotel>();
                    break;
            }

            if(hoteles.Count() == 0)
            {
                await Bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "No existen hoteles que cumplan con el filtro seleccionado"
                );
            }

            foreach (Hotel h in hoteles)
            {
                var ubicacion = new InlineKeyboardMarkup(new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData(
                            text:"Ubicación",
                            callbackData: h.CedulaJuridica),
                    }
                });

                await Bot.SendPhotoAsync(
                chatId: chatId,
                photo: h.FotoPerfil,
                caption:
                    "<b>Nombre: </b>" + h.Nombre + "\n" +
                    "<b>Puntuación: </b>" + PuntuacionAEstrellas(h.Clasificacion) + "\n" +
                    "<b>Descripción: </b>" + h.Descripcion + "\n" +
                    "<b>Ubicación: </b>" + h.vProvincia + ", " + h.vCanton + ", " + h.vDistrito + "\n" +
                    "<b>Teléfono: </b>" + h.Telefonos + "\n",
                parseMode: ParseMode.Html,
                replyMarkup: ubicacion
                );
            }
        }

        private static async Task ListarReservasAsync(long chatId, string id)
        {
            List<Reserva> reservas = GetReservasFromAPI(id);

            if (reservas.Count() == 0)
            {
                await Bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Vaya! Parece que aún no posee reservas!"
                );
            }

            foreach (Reserva r in reservas)
            {
                await Bot.SendPhotoAsync(
                chatId: chatId,
                photo: r.LlaveQR,
                caption:
                    "<b>Hotel: </b>" + r.IdHotel + "\n" +
                    "<b>Fecha de ingreso: </b>" + r.FechaInicio.ToString("dd/M/yyyy") + "\n" +
                    "<b>Fecha de salida: </b>" + r.FechaFin.ToString("dd/M/yyyy") + "\n" +
                    "<b>Código: </b>" + r.Codigo + "\n",
                parseMode: ParseMode.Html
                );
            }
        }

        private static List<Hotel> GetHotelesFromAPI()
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var response = client.DownloadString(URL_API_HOTELES);
            var options = JsonConvert.DeserializeObject<List<Hotel>>(response);
            return options;
        }

        private static List<Reserva> GetReservasFromAPI(string id)
        {
            var client = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            var response = client.DownloadString(URL_API_RESERVAS + "/" + id);
            var options = JsonConvert.DeserializeObject<List<Reserva>>(response);
            return options;
        }

        private static async Task<Ruta> GetRouteInfoFromAPIAsync(string lngIni, string latIni, string lngFin, string latFin)
        {
            string myJson = "{'locations': ['"+ lngIni +", " + latIni + "','" + lngFin + ", " + latFin + "'],'options': {'allToAll': false, 'unit': 'k'}}";
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    URL_API_MAPQUEST,
                    new StringContent(myJson, Encoding.UTF8, "application/json"));

                var contents = JsonConvert.DeserializeObject<Ruta>(await response.Content.ReadAsStringAsync());
                return contents;
            }
        }
    }

    internal class Ruta
    {
        public string[] distance { get; set; }
        public string[] time { get; set; }
    }
}
