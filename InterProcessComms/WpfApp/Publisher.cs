using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    internal class Publisher
    {
    }

    public class MyNetMQHost
    {

        private PublisherSocket _publishSocket;
        private SubscriberSocket _subscribeSocket;
        private NetMQPoller _poller;

        public MyNetMQHost( string subscribeAddress = "tcp://localhost:9000")
        {
            Task.Factory.StartNew(() => {

                Debug.WriteLine("Starting subscription");
                using (_subscribeSocket = new SubscriberSocket(subscribeAddress))
                using (_poller = new NetMQPoller { _subscribeSocket })
                {
                    _subscribeSocket.SubscribeToAnyTopic();
                    _subscribeSocket.ReceiveReady += SubscriberSocket_ReceiveReady;
                    _poller.RunAsync();
                }
            });
        }

        private async void SubscriberSocket_ReceiveReady(object sender, NetMQSocketEventArgs e)
        {
            var data = await e.Socket.ReceiveFrameStringAsync(); // This line is never reached
            Debug.WriteLine($"Got data {data}");
            
            //_publishSocket.SendMultipartBytes(data);
        }
    }

    public class MyNetMQClient
    {

        private readonly PublisherSocket _publishSocket;
        private readonly SubscriberSocket _subscribeSocket;

        public MyNetMQClient(string publishAddress = "tcp://localhost:9000")
        {
            _publishSocket = new PublisherSocket(publishAddress);
            
          
        }

        public void Publish(string data)
        {
            _publishSocket.SendMoreFrame("topicA").SendFrame(data);
        }
    }
}
