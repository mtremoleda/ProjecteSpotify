using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ServidorMusica
{
    private const int PORT = 5083;
    private static Socket serverSocket;
    private static List<ClientInfo> clients = new List<ClientInfo>();
    private static object lockObj = new object();

    class ClientInfo
    {
        public Socket Socket;
        public string Name;
        public string Song;
        public bool Active = true;
    }

    static void Main()
    {
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
        serverSocket.Listen(10);

        Console.WriteLine($"Servidor de musica escoltant al port: {PORT}");

        while (true)
        {
            try
            {
                Socket clientSocket = serverSocket.Accept();
                Thread clientThread = new Thread(() => HandleClient(clientSocket));
                clientThread.Start();
            }
            catch { break; }
        }
    }

    static void HandleClient(Socket clientSocket)
    {
        var client = new ClientInfo
        {
            Socket = clientSocket,
            Name = $"Client-{Guid.NewGuid()}",
            Song = "Cap"
        };

        lock (lockObj)
        {
            clients.Add(client);
        }

        Console.WriteLine($"Client conectat: {client.Name}");

        try
        {
            byte[] buffer = new byte[1024];
            while (client.Active)
            {
                int bytes = clientSocket.Receive(buffer);
                if (bytes <= 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

                if (message.StartsWith("NOM:"))
                {
                    string name = message.Substring(7);
                    client.Name = name;
                    Console.WriteLine($"{client.Name} s'ha identificat.");
                }
                else if (message.StartsWith("CANCO:"))
                {
                    string song = message.Substring(8);
                    client.Song = song;
                    Console.WriteLine($"{client.Name} esta reproduint: {song}");
                }
                else if (message == "LLISTAR")
                {
                    SendClientList(clientSocket);
                }
            }
        }
        catch
        {
            client.Active = false;
        }
        finally
        {
            lock (lockObj)
            {
                clients.Remove(client);
            }
            clientSocket.Close();
            Console.WriteLine($"Client desconectat: {client.Name}");
        }
    }

    static void SendClientList(Socket socket)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- CLIENTS CONECTATS ---");
            lock (lockObj)
            {
                foreach (var c in clients)
                {
                    sb.AppendLine($"{c.Name}: {c.Song}");
                }
            }
            socket.Send(Encoding.UTF8.GetBytes(sb.ToString()));
        }
        catch { }
    }
}