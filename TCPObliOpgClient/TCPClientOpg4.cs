using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class TCPClientOpg4
    {
        public static void Start()
        {
            string server = "localhost";
            int port = 7;

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
                    writer.WriteLine(command);

                    if (command == "close")
                    {
                        isRunning = false;
                    }

                    else if (command == "Random" || command == "Add" || command == "Subtract")
                    {
                        Console.WriteLine(reader.ReadLine());
                        
                        
                        string numbers = Console.ReadLine();
                        writer.WriteLine(numbers);
                        Console.WriteLine("Server response:" + reader.ReadLine());
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
