using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace API.WebSocketService
{
    public class WebSocketService
    {
        public ConcurrentDictionary<WebSocket, string> Clients { get; } = new ConcurrentDictionary<WebSocket, string>();
    }
}
