
// version 1

using System.Net.Sockets;
using System.Text;

namespace ChatClient;

internal static class Program
{
    private static async Task SendMessageAsync(NetworkStream stream)
    {
        while (true)
        {
            Console.WriteLine("Enter message to send:");
            string message = Console.ReadLine();
            if (string.IsNullOrEmpty(message)) continue;

            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
            Console.WriteLine($"Sent: {message}");
        }
    }

    private static async Task ReceiveMessageAsync(NetworkStream stream)
    {
        try
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Server disconnected.");
                    break;
                }
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {response}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception while receiving: {e.Message}");
        }
    }

    public static async Task Main(string[] args)
    {
        const string serverAddress = "127.0.0.1";
        const int port = 5000;

        try
        {
            using TcpClient client = new(serverAddress, port);
            Console.WriteLine("Connected to server.");

            await using NetworkStream stream = client.GetStream();

            // Start sending and receiving messages on different threads
            Task sendTask = Task.Run(() => SendMessageAsync(stream));
            Task receiveTask = Task.Run(() => ReceiveMessageAsync(stream));

            await Task.WhenAny(sendTask, receiveTask);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
        }

        Console.WriteLine("Client stopped.");
    }
}


// version 0
/*using System.Net.Sockets;
using System.Text;

internal class Program
{
    public static async Task Main(string[] args)
    {
        const string serverAddress = "127.0.0.1";
        const int port = 5000;

        try
        {
            // Create a TCP client
            using TcpClient client = new(serverAddress, port);
            Console.WriteLine("Connected to server.");

            // Get the network stream
            await using NetworkStream stream = client.GetStream();
            // Send data to the server
            Console.WriteLine("Enter message to send:");
            string message = Console.ReadLine();
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);
            await stream.WriteAsync(messageBytes);
            Console.WriteLine($"Sent: {message}");

            // Receive response from the server
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received: {response}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
        }

        Console.WriteLine("Client stopped.");
    }
}*/