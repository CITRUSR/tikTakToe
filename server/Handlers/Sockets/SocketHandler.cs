using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace server.Handlers.Sockets;

public class SocketHandler : ISocketHandler
{
    public async Task CloseAsync(WebSocket socket)
    {
        if (socket.State != WebSocketState.Open)
            await socket.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Connection closed",
                CancellationToken.None
            );
    }

    public async Task<string> ReceiveAsync(WebSocket socket)
    {
        byte[] buffer = new byte[1024 * 4];

        var result = await socket.ReceiveAsync(
            new ArraySegment<byte>(buffer),
            CancellationToken.None
        );

        if (result.CloseStatus.HasValue)
        {
            await CloseAsync(socket);
        }

        return Encoding.UTF8.GetString(buffer, 0, result.Count);
    }

    public async Task<T> ReceiveAsync<T>(WebSocket socket)
        where T : class
    {
        byte[] buffer = new byte[1024 * 4];

        var result = await socket.ReceiveAsync(
            new ArraySegment<byte>(buffer),
            CancellationToken.None
        );

        if (result.CloseStatus.HasValue)
        {
            await CloseAsync(socket);
        }

        var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

        return JsonSerializer.Deserialize<T>(msg);
    }

    public async Task SendAsync(WebSocket socket, string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);

        await socket.SendAsync(
            new ArraySegment<byte>(buffer),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }
}
