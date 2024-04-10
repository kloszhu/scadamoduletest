using System.Collections.Concurrent;
using System.Net.WebSockets;
using Websocket.Model;

namespace Websocket.Manager
{
    public class WebSocketConnectionManager
    {
        private readonly ConcurrentDictionary<Guid, HostModel> _sockets = new ConcurrentDictionary<Guid, HostModel>();



        public Guid AddSocket(WebSocket socket)
        {
            var socketId = Guid.NewGuid();
            var hostmodel = new HostModel
            {
                socket = socket,
                SessionId = socketId,
                Ts = DateTime.Now
            };
            _sockets.TryAdd(socketId, hostmodel);
            return socketId;
        }

        public bool IsOnline(string Host,int Port)
        {
          return _sockets.Values.Where(a=>a.That.Host==Host&&a.That.Port==Port).Any();
        }
        public WebSocket? GetSocket(Guid socketId)
        {
            _sockets.TryGetValue(socketId, out var socket);
            return socket.socket;
        }

        public ConcurrentDictionary<Guid, HostModel> GetAllSockets()
        {
            return _sockets;
        }

        public Guid? GetSocketId(WebSocket socket)
        {
            foreach (var (key, value) in _sockets)
            {
                if (value.socket == socket)
                {
                    return key;
                }
            }
            return null;
        }

        public void SetSocketId(Guid socketId, ConnectHost host,List<ConnectHost> list)
        {
            _sockets.TryGetValue(socketId, out var socket);
            if (socket != null)
            {
                if (socket.That==null)
                {
                   var first= list.FirstOrDefault(a => a.Host == host.Host && a.Port == host.Port);
                    if (first!=null)
                    {
                        socket.That=first;
                    }
                }
            }
        }

        public void RemoveSocket(Guid socketId)
        {
            _sockets.TryRemove(socketId, out _);
        }


    }
}