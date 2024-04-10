using System.Net.WebSockets;
namespace Websocket.Model
{

    public class HostModel
    {
        public ConnectHost That { get; set; }
        public Guid SessionId { get; set; }
        public DateTime Ts { get; set; }
        public bool IsRun { get; set; }
        public System.Net.WebSockets.WebSocket socket { get; set; }
    }
}
