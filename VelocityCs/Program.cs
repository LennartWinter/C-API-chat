using System.Text.Json.Serialization;
using Newtonsoft.Json;
using VelocityCs.Models;


namespace VelocityCs
{
    internal class Program
    {
        public static void DeleteInput()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
        public static async Task ShowMessagesAsync(AuthData data)
        {
            string json = JsonConvert.SerializeObject(data);
            List<Message> oldmessages = new List<Message>();
            while (true)
            {
                string response = Helpers.Post("https://nont123.nl/api/messages", json, "application/json");
                List<Message> messages = JsonConvert.DeserializeObject<List<Message>>(response);
                messages.Reverse();
                foreach (Message message in messages)
                {
                    bool messageExists = oldmessages.Any(m => m.id == message.id);
                    if (!messageExists)
                    {
                        Console.WriteLine(message.username + ": " + message.message);
                        oldmessages.Add(message);
                    }
                }

                await Task.Delay(300);
            }
        }

        public static async Task AwaitMessageAsync(AuthData data)
        {
            while (true)
            {
                string msg = Console.ReadLine();
                DeleteInput();
                data.message = msg;
                string json = JsonConvert.SerializeObject(data);
                Helpers.Post("https://nont123.nl/api/messages/create", json, "application/json");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Velocity Token: ");
            string Token = Console.ReadLine();
            DeleteInput();
            AuthData data = new AuthData();
            data.token = Token;
            ShowMessagesAsync(data);
            AwaitMessageAsync(data);
        }
    }
}