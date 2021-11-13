using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace MainApp
{
    public static class Server
    {
        public static void RunServer()
        {
            using ResponseSocket server = new();
            server.Bind("tcp://*:5555");
            while (true)
            {
                string message = server.ReceiveFrameString();
                Console.WriteLine("Received {0}", message);
                // processing the request
                Thread.Sleep(100);
                Console.WriteLine("Sending World");
                server.SendFrame("World");
            }
        }
    }
}