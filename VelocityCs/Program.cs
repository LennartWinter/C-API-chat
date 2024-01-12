using System.Text.Json.Serialization;
using Newtonsoft.Json;
using VelocityCs.Models;


namespace VelocityCs
{
    internal class Program
    {
        public static async Task ShowMessagesAsync(string json)
        {
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
        static void Main(string[] args)
        {
            Console.WriteLine("Velocity Token: ");
            string Token = Console.ReadLine();
            AuthData data = new AuthData();
            data.token = Token;
            string json = JsonConvert.SerializeObject(data);
            ShowMessagesAsync(json);
            Console.ReadLine();
        }
    }
}