using System;
using System.Collections.Generic;
using NetMQ;
using NetMQ.Sockets;

namespace App1
{
    public class Program
    {
        public static readonly IList<string> AllowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };

        private static void Main(string[] args)
        {
            Console.WriteLine("App1");

            if (args.Length != 1 || !AllowableCommandLineArgs.Contains(args[0]))
            {
                Console.WriteLine("Expected one argument, either " +
                                  "'TopicA', 'TopicB' or 'All'");
                Environment.Exit(-1);
            }

            string topic = args[0] == "All" ? "" : args[0];
            Console.WriteLine("Subscriber started for Topic : {0}", topic);
            using SubscriberSocket subSocket = new();
            subSocket.Options.ReceiveHighWatermark = 1000;
            subSocket.Connect("tcp://localhost:12345");
            subSocket.Subscribe(topic);
            Console.WriteLine("Subscriber socket connecting...");
            while (true)
            {
                string messageTopicReceived = subSocket.ReceiveFrameString();
                string messageReceived = subSocket.ReceiveFrameString();
                Console.WriteLine(messageReceived);
            }
        }
    }
}