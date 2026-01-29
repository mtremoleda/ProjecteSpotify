using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Habilitar WebSockets
app.UseWebSockets();

// Almacenar clientes conectados: WebSocket -> Nombre de usuario
var clients = new ConcurrentDictionary<WebSocket, string>();

// Endpoint para WebSocket
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        string userName = $"Usuario-{Guid.NewGuid()}";
        clients.TryAdd(webSocket, userName);

        Console.WriteLine($"Cliente conectado: {userName}");

        try
        {
            var buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    if (message.StartsWith("USUARIO:"))
                    {
                        string newName = message.Substring(8);
                        clients[webSocket] = newName;
                        Console.WriteLine($"{userName} ahora se llama {newName}");
                        userName = newName;
                    }
                    else if (message.StartsWith("CANCION:"))
                    {
                        string song = message.Substring(8);
                        Console.WriteLine($"✅ {userName} está reproduciendo: {song}");

                        // Enviar a todos los clientes conectados
                        var broadcastMessage = $"{userName} está reproduciendo: {song}";
                        BroadcastMessage(broadcastMessage);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            clients.TryRemove(webSocket, out _);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None);
            Console.WriteLine($"Cliente desconectado: {userName}");
        }
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

void BroadcastMessage(string message)
{
    var messageBytes = Encoding.UTF8.GetBytes(message);

    foreach (var client in clients.Keys.ToList())
    {
        if (client.State == WebSocketState.Open)
        {
            try
            {
                client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch
            {
                // Ignorar errores al enviar a clientes que ya se hayan desconectado
            }
        }
    }
}

app.Run("http://0.0.0.0:5085");