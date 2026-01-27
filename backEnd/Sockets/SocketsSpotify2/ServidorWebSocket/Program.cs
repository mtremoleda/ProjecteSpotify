using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Collections.Concurrent; // Añadido para almacenar clientes

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
        string userName = $"Usuario-{Guid.NewGuid()}"; // Nombre por defecto
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

                    // Comando para cambiar nombre de usuario
                    if (message.StartsWith("USUARIO:"))
                    {
                        string newName = message.Substring(8);
                        clients[webSocket] = newName;
                        Console.WriteLine($"{userName} ahora se llama {newName}");
                        userName = newName;
                    }
                    // Comando para enviar canción
                    else if (message.StartsWith("CANCION:"))
                    {
                        string song = message.Substring(8);
                        Console.WriteLine($"✅ {userName} está reproduciendo: {song}");
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

app.Run("http://0.0.0.0:5085");