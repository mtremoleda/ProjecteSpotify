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

// Emmagatzemar clients connectats: WebSocket -> Nom d'usuari
var clients = new ConcurrentDictionary<WebSocket, string>();

// Endpoint per a WebSocket
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        string userName = $"Usuari-{Guid.NewGuid()}";
        clients.TryAdd(webSocket, userName);

        Console.WriteLine($"Client connectat: {userName}");

        try
        {
            var buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                        if (message.StartsWith("USUARI:"))
                        {
                            string newName = message.Substring(7);
                            clients[webSocket] = newName;
                            Console.WriteLine($"{userName} ara es diu {newName}");
                            userName = newName;
                        }
                        else if (message.StartsWith("CANÇO:"))
                        {
                            string song = message.Substring(6);
                            Console.WriteLine($"{userName} està reproduint: {song}");

                            var broadcastMessage = $"{userName} està reproduint: {song}";
                            BroadcastMessage(broadcastMessage);
                        }
                    }
                }
                catch (WebSocketException ex) when (ex.ErrorCode == 10054 || ex.ErrorCode == 10058 || ex.ErrorCode == 10062)
                {
                    
                    Console.WriteLine($"Client desconectat: {userName} (ErrorCode: {ex.ErrorCode})");
                    break;
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepció no capturada en connexió: {ex.Message}");
        }
        finally
        {
            // Asegurar limpieza
            clients.TryRemove(webSocket, out _);
            try
            {
                if (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Tencat", CancellationToken.None);
                }
            }
            catch {}
            Console.WriteLine($"Client desconnectat: {userName}");
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

    
    var activeClients = clients.Keys.Where(c => c.State == WebSocketState.Open).ToList();

    foreach (var client in activeClients)
    {
        try
        {
            if (client.State == WebSocketState.Open)
            {
                client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
        catch (WebSocketException)
        {
            
            clients.TryRemove(client, out _);
        }
        catch (Exception)
        {
            
        }
    }
}

app.Run("http://0.0.0.0:5085");