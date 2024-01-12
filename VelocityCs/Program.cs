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
            int previousSenderId = -1;
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
                        if (previousSenderId != message.user_id)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n" + message.username);
                        }
                        previousSenderId = message.user_id;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(message.message);
                        oldmessages.Add(message);
                    }
                }
                Console.ResetColor();
                await Task.Delay(300);
            }
        }

        public static async Task AwaitMessageAsync(AuthData data)
        {
            while (true)
            {
                string msg = Console.ReadLine();
                DeleteInput();
                if (msg == ">help")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nCommands:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(">delete [id]");
                    Console.WriteLine(">edit [id] [message]");
                    Console.WriteLine(">search [query]");
                    Console.WriteLine(">online");
                    Console.WriteLine(">clear");
                    Console.WriteLine(">debug");
                    Console.WriteLine(">refresh");
                    Console.WriteLine(">getJson [url] [method]");
                    Console.WriteLine(">rename [name]");
                    Console.WriteLine(">setAvatar [url]");
                    Console.WriteLine(">profile");
                    Console.WriteLine(">help");
                    Console.ResetColor();
                }
                else
                {
                    data.message = msg;
                    string json = JsonConvert.SerializeObject(data);
                    Helpers.Post("https://nont123.nl/api/messages/create", json, "application/json");   
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Your token:");
            string Token = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("$$\\    $$\\ $$$$$$$$\\ $$\\       $$$$$$\\   $$$$$$\\  $$$$$$\\ $$$$$$$$\\ $$\\     $$\\ \n$$ |   $$ |$$  _____|$$ |     $$  __$$\\ $$  __$$\\ \\_$$  _|\\__$$  __|\\$$\\   $$  |\n$$ |   $$ |$$ |      $$ |     $$ /  $$ |$$ /  \\__|  $$ |     $$ |    \\$$\\ $$  / \n\\$$\\  $$  |$$$$$\\    $$ |     $$ |  $$ |$$ |        $$ |     $$ |     \\$$$$  /  \n \\$$\\$$  / $$  __|   $$ |     $$ |  $$ |$$ |        $$ |     $$ |      \\$$  /   \n  \\$$$  /  $$ |      $$ |     $$ |  $$ |$$ |  $$\\   $$ |     $$ |       $$ |    \n   \\$  /   $$$$$$$$\\ $$$$$$$$\\ $$$$$$  |\\$$$$$$  |$$$$$$\\    $$ |       $$ |    \n    \\_/    \\________|\\________|\\______/  \\______/ \\______|   \\__|       \\__|    ");
            Console.ResetColor();
            Console.Write("Type ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">help ");
            Console.ResetColor();
            Console.Write("to get started.\n");
            AuthData data = new AuthData();
            data.token = Token;
            ShowMessagesAsync(data);
            AwaitMessageAsync(data);
        }
    }
}