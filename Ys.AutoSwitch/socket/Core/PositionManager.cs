using Google.Protobuf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;
using Ys.AutoSwitch.socket.Models;

namespace Ys.AutoSwitch.socket.Core
{

    public class PositionManager
    {
        public ConcurrentDictionary<WebSocket, SocketPosition> Data = new ConcurrentDictionary<WebSocket, SocketPosition>();

        public PositionManager()
        {
            Data = new ConcurrentDictionary<WebSocket, SocketPosition>();
        }

        public WebSocket FindByValue(string Uid)
        {
            return Data.ToDictionary(x => x.Value, y => y.Key).Where(a => a.Key.Uid == Uid).First().Value;
        }

        public async Task SendAll(SwitchMsg switchMsg, bool endofmsg, WebSocketMessageType type)
        {
            foreach (WebSocket ws in Data.Keys)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    switchMsg.WriteTo(stream);
                    // 序列化后的字节数组
                    byte[] bytes = stream.ToArray();
                    await ws.SendAsync(bytes.ToSerializeArray(), type, endofmsg, CancellationToken.None);
                }
            }
        }

    }


}
