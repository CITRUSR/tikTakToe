using System.Net.WebSockets;

namespace server.Handlers.Sockets;

public interface ISocketHandler
{
    Task<string> ReceiveAsync(WebSocket socket);
    Task<T> ReceiveAsync<T>(WebSocket socket)
        where T : class;
    Task SendAsync(WebSocket socket, string message);
    Task CloseAsync(WebSocket socket);
}
