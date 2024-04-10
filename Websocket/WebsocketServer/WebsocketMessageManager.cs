using System.Text;
using Websocket.Model;

namespace Websocket.Manager
{
    public class WebsocketMessageManager
    {
        private readonly ILogger<WebsocketMessageManager> _logger;
        private readonly WebSocketConnectionManager _websocketconnectManager;
        public WebsocketMessageManager(ILogger<WebsocketMessageManager> logger, WebSocketConnectionManager websocketManager)
        {
            _logger = logger;
            _websocketconnectManager = websocketManager;
        }
        public Guid[] GetClient()
        {
            return _websocketconnectManager.GetAllSockets().Keys.ToArray();
        }
        public KeyValuePair<Guid, HostModel>[] GetClientData()
        {
            return _websocketconnectManager.GetAllSockets().ToArray();
        }
        public async Task SendMessage(Guid SessionId,string Message)
        {
            var buffer = Encoding.UTF8.GetBytes(Message);
           var socket= _websocketconnectManager.GetSocket(SessionId);
           await socket.SendAsync(new ArraySegment<byte>(buffer, 0, buffer.Length),
               System.Net.WebSockets.WebSocketMessageType.Text , true, CancellationToken.None
                ) ;
        }
    }
}
