
// version 2
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer;

internal static class Program
{
    // Concurrent dictionary to store connected clients
    private static readonly ConcurrentDictionary<string, TcpClient> ConnectedClients = new();
    private const int Port = 5000;

    public static async Task Main(string[] args)
    {
        // Set the IP address and port number
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        // Create a TCP listener
        TcpListener listener = new(ipAddress, Port);

        try
        {
            // Start listening for incoming connections
            listener.Start();
            Console.WriteLine($"Server started. Listening on port {Port}");

            while (true)
            {
                // Accept a client connection
                TcpClient client = await listener.AcceptTcpClientAsync();
                string clientId = Guid.NewGuid().ToString(); // Generate a unique ID for the client
                ConnectedClients.TryAdd(clientId, client); // Add client to the dictionary

                Console.WriteLine($"Client connected with ID: {clientId}");

                // Handle the client in a separate task
                _ = Task.Run(() => HandleClientAsync(client, clientId));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
        }
        finally
        {
            // Stop listening
            listener.Stop();
        }

        Console.WriteLine("Server stopped.");
    }

    private static async Task HandleClientAsync(TcpClient client, string clientId)
    {
        try
        {
            // Get the network stream
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Read data from the client
            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                // Convert data to a string
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from client {clientId}: {data}");

                // Broadcast the message to all other clients
                await BroadcastMessageAsync(data, clientId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception for client {clientId}: {e}");
        }
        finally
        {
            // Remove the client from the dictionary and close the connection
            ConnectedClients.TryRemove(clientId, out _);
            client.Close();
            Console.WriteLine($"Client {clientId} disconnected.");
        }
    }

    private static async Task BroadcastMessageAsync(string message, string senderId)
    {
        byte[] msg = Encoding.ASCII.GetBytes(message);

        foreach (KeyValuePair<string, TcpClient> clientEntry in ConnectedClients)
        {
            string clientId = clientEntry.Key;
            TcpClient client = clientEntry.Value;

            // Skip the client that sent the message
            if (clientId == senderId) continue;

            try
            {
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(msg);
                Console.WriteLine($"Sent to client {clientId}: {message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception while sending to client {clientId}: {e}");
                // If sending fails, remove the client
                ConnectedClients.TryRemove(clientId, out _);
                client.Close();
            }
        }
    }
}









// version 1

/*
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer;

internal static class Program
{
    // Concurrent dictionary to store connected clients
    private static readonly ConcurrentDictionary<string, TcpClient> ConnectedClients = new();
    private const int Port = 5000;

    public static async Task Main(string[] args)
    {
        // Set the IP address and port number
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        // Create a TCP listener
        TcpListener listener = new(ipAddress, Port);

        try
        {
            // Start listening for incoming connections
            listener.Start();
            Console.WriteLine($"Server started. Listening on port {Port}");

            while (true)
            {
                // Accept a client connection
                TcpClient client = await listener.AcceptTcpClientAsync();
                string clientId = Guid.NewGuid().ToString(); // Generate a unique ID for the client
                ConnectedClients.TryAdd(clientId, client); // Add client to the dictionary

                Console.WriteLine($"Client connected with ID: {clientId}");

                // Handle the client in a separate task
                _ = HandleClientAsync(client, clientId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
        }
        finally
        {
            // Stop listening
            listener.Stop();
        }

        Console.WriteLine("Server stopped.");
    }

    private static async Task HandleClientAsync(TcpClient client, string clientId)
    {
        try
        {
            // Get the network stream
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Read data from the client
            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                // Convert data to a string
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received from client {clientId}: {data}");

                // Broadcast the message to all other clients
                await BroadcastMessageAsync(data, clientId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception for client {clientId}: {e}");
        }
        finally
        {
            // Remove the client from the dictionary and close the connection
            ConnectedClients.TryRemove(clientId, out _);
            client.Close();
            Console.WriteLine($"Client {clientId} disconnected.");
        }
    }

    private static async Task BroadcastMessageAsync(string message, string senderId)
    {
        byte[] msg = Encoding.ASCII.GetBytes(message);

        foreach (KeyValuePair<string, TcpClient> clientEntry in ConnectedClients)
        {
            string clientId = clientEntry.Key;
            TcpClient client = clientEntry.Value;

            // Skip the client that sent the message
            if (clientId == senderId) continue;

            try
            {
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(msg);
                Console.WriteLine($"Sent to client {clientId}: {message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception while sending to client {clientId}: {e}");
                // If sending fails, remove the client
                ConnectedClients.TryRemove(clientId, out _);
                client.Close();
            }
        }
    }
}
*/


// version 0
/*using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
public static async Task Main(string[] args)
    {
        // Set the IP address and port number
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        const int port = 5000;

        // Create a TCP listener
        TcpListener listener = new(ipAddress, port);

        try
        {
            // Start listening for incoming connections
            listener.Start();
            Console.WriteLine($"Server started. Listening on port {port}");

            while (true)
            {
                // Accept a client connection
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected.");

                // Handle the client in a separate task
                _ = HandleClientAsync(client);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
        }
        finally
        {
            // Stop listening
            listener.Stop();
        }

        Console.WriteLine("Server stopped.");
    }

    private static async Task HandleClientAsync(TcpClient client)
    {
        try
        {
            // Get the network stream
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Read data from the client
            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                // Convert data to a string
                string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {data}");

                // Echo the data back to the client
                /*byte[] msg = Encoding.ASCII.GetBytes(data);
                await stream.WriteAsync(msg);
                Console.WriteLine($"Sent: {data}");#1#
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}");
        }
        finally
        {
            // Close the client connection
            client.Close();
            Console.WriteLine("Client disconnected.");
        }
    }
}*/