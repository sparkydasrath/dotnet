using System;
using NetMQ;
using NetMQ.Sockets;

namespace MainApp
{
    public static class Client
    {
        public static void RunClient()
        {
            using RequestSocket client = new();
            client.Connect("tcp://localhost:5555");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Sending Hello");
                client.SendFrame("Hello");
                // string message = client.ReceiveFrameString();
                if (client.TryReceiveFrameString(out string message))
                    Console.WriteLine("Received {0}", message);
                else
                    Console.WriteLine("No message received");
                Console.WriteLine("Received {0}", message);

            }
        }
    }
}