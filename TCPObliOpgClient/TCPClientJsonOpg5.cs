using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCPServer
{
    public class TCPClientJsonOpg5
    {
        public static void Start()
        {
            string server = "localhost";
            int port = 8;

            try
            {
                using TcpClient client = new TcpClient(server, port);
                using NetworkStream ns = client.GetStream();
                using StreamReader reader = new StreamReader(ns);
                using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

                Console.WriteLine("Du er connected til serveren");

                bool isRunning = true;

                while (isRunning)
                {
                    Console.WriteLine("Skriv command: Random, Add Eller Subtract.");
                    string command = Console.ReadLine();

                    if (command == "close")
                    {
                        isRunning = false;
                    }

                    else if (command == "Random" || command == "Add" || command == "Subtract")
                    {
                        Console.WriteLine("Skrive to numbers. The numbers skal væle seperated by space");
                        string[] numbers = Console.ReadLine().Split(' ');
                        var request = new { Method = command, Tal1 = int.Parse(numbers[0]), Tal2 = int.Parse(numbers[1]) };
                        string jsonRequest = JsonSerializer.Serialize(request);
                        writer.WriteLine(jsonRequest);
                        string response = reader.ReadLine();
                        var responseObj = JsonSerializer.Deserialize<Request>(response);
                        Console.WriteLine("Server response: " + responseObj.Response);

                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public class Request
        {
            public string Method { get; set; }
            public int Tal1 { get; set; }
            public int Tal2 { get; set; }
            public string Response { get; set; }
        }
    }
}
