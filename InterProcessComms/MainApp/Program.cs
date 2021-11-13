using System;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace MainApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World! - main app");
            /*Console.WriteLine("Running NetMQ server");
            
            Server.RunServer();
            
            Console.WriteLine("Running NetMQ client");
            Client.RunClient();*/

            /*using NetMQRuntime runtime = new();
            runtime.Run(ServerAsync(), ClientAsync());*/


            // REQUEST/RESPONSE
            /*using ResponseSocket responseSocket = new("@tcp://*:5555");
            using RequestSocket requestSocket = new(">tcp://localhost:5555");
            Console.WriteLine("requestSocket : Sending 'Hello'");
            requestSocket.SendFrame("Hello");
            string message = responseSocket.ReceiveFrameString();
            Console.WriteLine("responseSocket : Server Received '{0}'", message);
            Console.WriteLine("responseSocket Sending 'World'");
            responseSocket.SendFrame("World");
            message = requestSocket.ReceiveFrameString();
            Console.WriteLine("requestSocket : Received '{0}'", message);
            Console.ReadLine();*/


            // PUB/SUB

            /*Random rand = new(50);
            using PublisherSocket pubSocket = new();
            Console.WriteLine("Publisher socket binding...");
            pubSocket.Options.SendHighWatermark = 1000;
            pubSocket.Bind("tcp://*:12345");
            for (int i = 0; i < 100; i++)
            {
                double randomizedTopic = rand.NextDouble();
                if (randomizedTopic > 0.5)
                {
                    string msg = "TopicA msg-" + i;
                    Console.WriteLine("Sending message : {0}", msg);
                    pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                }
                else
                {
                    string msg = "TopicB msg-" + i;
                    Console.WriteLine("Sending message : {0}", msg);
                    pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                }

                Thread.Sleep(500);
            }*/
        }

        public static async Task ServerAsync()
        {
            using RouterSocket server = new("inproc://async");
            for (int i = 0; i < 1000; i++)
            {
                (RoutingKey routingKey, bool more) = await server.ReceiveRoutingKeyAsync();
                (string message, _) = await server.ReceiveFrameStringAsync();

                Console.WriteLine($"Message = {message}");
                // TODO: process message

                await Task.Delay(100);
                server.SendMoreFrame(routingKey);
                server.SendFrame("Welcome");
            }
        }

        public static async Task ClientAsync()
        {
            using DealerSocket client = new("inproc://async");
            for (int i = 0; i < 1000; i++)
            {
                client.SendFrame("Hello");
                (string message, bool more) = await client.ReceiveFrameStringAsync();

                // TODO: process reply

                await Task.Delay(100);
            }
        }
    }
}