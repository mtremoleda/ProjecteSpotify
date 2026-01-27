using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ServidorMusica
{
    private const int PORT = 5081;
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

        Console.WriteLine($"Servidor de música escuchando en puerto {PORT}");

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
            Name = $"Cliente-{Guid.NewGuid()}",
            Song = "Ninguna"
        };

        lock (lockObj)
        {
            clients.Add(client);
        }

        Console.WriteLine($"Cliente conectado: {client.Name}");

        try
        {
            byte[] buffer = new byte[1024];
            while (client.Active)
            {
                int bytes = clientSocket.Receive(buffer);
                if (bytes <= 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

                if (message.StartsWith("NOMBRE:"))
                {
                    string name = message.Substring(7);
                    client.Name = name;
                    Console.WriteLine($"{client.Name} se ha identificado.");
                }
                else if (message.StartsWith("CANCION:"))
                {
                    string song = message.Substring(8);
                    client.Song = song;
                    Console.WriteLine($"{client.Name} está reproduciendo: {song}");
                }
                else if (message == "LISTAR")
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
            Console.WriteLine($"Cliente desconectado: {client.Name}");
        }
    }

    static void SendClientList(Socket socket)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--- CLIENTES CONECTADOS ---");
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