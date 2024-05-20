using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;
using Google.Protobuf;

namespace Ys.AutoSwitch.socket.Core
{
    public class SocketServer
    {
        private string Uri;
        private readonly HttpListener _listener = new HttpListener();
        public CancellationToken CancellationToken;
        private ServerDataManager _DataManager;
        private PositionManager position;
        public SocketServer(ServerDataManager manager)
        {
            position = new PositionManager();
            _DataManager = manager;
            CancellationToken = CancellationToken.None;
        }

        public Task Start(string host, int port, string Prefixes)
        {
            _listener.Prefixes.Add($"http://{host}:{port}/{Prefixes}/");
            _listener.Start();
            return Task.Run(AcceptWebSocketConnections, CancellationToken);
        }


        private async Task AcceptWebSocketConnections()
        {

            while (!CancellationToken.IsCancellationRequested)
            {
                if (_listener.IsListening)
                {

                    var context = await _listener.GetContextAsync();
                    if (context.Request.IsWebSocketRequest)
                    {

                        await ProcessWebSocketConnection(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
            }


        }

        public async Task SendAll(SwitchMsg msg)
        {
            await position.SendAll(msg, false, WebSocketMessageType.Binary);
        }
        private async Task ProcessWebSocketConnection(HttpListenerContext context)
        {
            HttpListenerWebSocketContext ws = await context.AcceptWebSocketAsync(subProtocol: null);
            position.Data.TryAdd(ws.WebSocket, null);
            await EchoWebSocket(ws.WebSocket);
            position.Data.Remove(ws.WebSocket, out var data);
        }


        private async Task EchoWebSocket(WebSocket webSocket)
        {
            CancellationToken cancellationToken = new CancellationToken();
            WebSocketReceiveResult result;
            while (!cancellationToken.IsCancellationRequested && webSocket.State == WebSocketState.Open)
            {
                // 创建缓冲区来接收数据
                byte[] bytes = new byte[1024 * 8];
                var mem = new ArraySegment<byte>(bytes);
                result = await webSocket.ReceiveAsync(mem, cancellationToken);
                // 处理接收到的数据
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    // 对于文本消息，可以直接使用result.Count获取到有效负载长度

                    string message = Encoding.UTF8.GetString(mem.ToArray(), 0, result.Count);
                    Console.WriteLine("Received: " + message);
                    // 发送文本消息
                    var messageBuffer = Encoding.UTF8.GetBytes("Echo: " + message);
                    await webSocket.SendAsync(new ArraySegment<byte>(messageBuffer), WebSocketMessageType.Text, true, cancellationToken);
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {

                    SwitchMsg msg = new SwitchMsg();
                    msg = SwitchMsg.Parser.ParseFrom(mem.ToArray().ToDeserializeArray().Item2);
                    _DataManager.Msg.Enqueue(msg);
                    if (msg.MsgType == MsgType.PingType)
                    {
                        position.Data[webSocket] = new Models.SocketPosition
                        {
                            Uid = msg.Pingpack.UID, Host = msg.Pingpack.Host, Port = msg.Pingpack.Port
                        };
                        var pong=new SwitchMsg();
                        pong.MsgType = MsgType.PongType;
                        pong.Pongpack = new PongPackMSG
                        {
                            Host = msg.Pingpack.Host,
                            Port = msg.Pingpack.Port,
                            Ts = DateTime.Now.Ticks,
                            UID = msg.Pingpack.UID
                        };
                        await webSocket.SendAsync(pong.ToByteArray().ToSerializeArray(), WebSocketMessageType.Binary,false, CancellationToken.None);
                    }
                    
                }
            }
        }

        private byte[] resizeBuffer(byte[] originalArray)
        {
            //byte[] originalArray = { 1, 2, 3, 0, 0, 0 }; // 示例byte数组

            // 找到第一个非零元素的索引
            int firstNonZeroIndex = Array.LastIndexOf(originalArray, 0);

            // 如果数组中所有元素都是0，firstNonZeroIndex将是-1
            if (firstNonZeroIndex != -1)
            {
                // 调整数组大小
                Array.Resize(ref originalArray, firstNonZeroIndex + 1);
            }
            return originalArray;
        }

        public Task Stop()
        {
            CancellationToken = new CancellationToken(true);
            _listener.Stop();
            return Task.CompletedTask;
        }

       
    }
}
