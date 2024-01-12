using System.Text.Json.Serialization;
using Newtonsoft.Json;
using VelocityCs.Models;


namespace VelocityCs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Velocity Token: ");
            string Token = Console.ReadLine();
            AuthData data = new AuthData();
            data.token = Token;
            string json = JsonConvert.SerializeObject(data);
            string response = Helpers.Post("https://nont123.nl/api/messages", json, "application/json");
            Console.WriteLine(response);
            Console.ReadLine();
        }
    }
}