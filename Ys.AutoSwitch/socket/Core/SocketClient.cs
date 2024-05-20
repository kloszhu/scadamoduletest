using Google.Protobuf;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;
using Ys.AutoSwitch.socket.Core;
using YSAI.Unility;

namespace Ys.AutoSwitch.socket
{
    public class SocketClient
    {
        private string Uri = "ws://localhost:8080";
        private ClientDataManager _DataManager;
        private CancellationToken CancellationToken;
        ClientWebSocket client;

        public SocketClient(ClientDataManager manager)
        {
            _DataManager= manager;
            CancellationToken = CancellationToken.None;
           
        }

        public async Task Start(string host,int port,string prifix)
        {
            client = new ClientWebSocket();
            Uri = $"ws://{host}:{port}/{prifix}";
            await Console.Out.WriteLineAsync(Uri);
            CancellationToken=CancellationToken.None;
            await client.ConnectAsync(new Uri(Uri), CancellationToken);
            Task.Run(AcceptWebSocketConnections, CancellationToken);
            Console.WriteLine("Connected to WebSocket server");
        }
        private async Task AcceptWebSocketConnections()
        {
            WebSocketReceiveResult result;
            while (!CancellationToken.IsCancellationRequested)
            {
                var bytes = new byte[1024 * 1024 * 8];
                ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
                result = await client.ReceiveAsync(buffer, CancellationToken.None);

                switch (result.MessageType) {
                case WebSocketMessageType.Close:
                        break;
                    case WebSocketMessageType.Binary:
                        SwitchMsg msg = new SwitchMsg();
                        msg = SwitchMsg.Parser.ParseFrom(buffer.ToArray().ToDeserializeArray().Item2);
                        _DataManager.Msg.Enqueue(msg);
                        break;
                }

            }


        }

      

        public async Task Send(SwitchMsg switchMsg, bool endofmsg, WebSocketMessageType type)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                switchMsg.WriteTo(stream);
                // 序列化后的字节数组
                byte[] bytes=stream.ToArray();
                //ArraySegment<byte> buffer=new ArraySegment<byte>(bytes,8,bytes.Length);
                //Data[Uid].Produce(_config.Topic, switchMsg.ToByteArray());
               
                await client.SendAsync(bytes.ToSerializeArray(), type, endofmsg, CancellationToken);
            }

           // ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
            
        }
        public async Task Stop()
        {
            CancellationToken = new CancellationToken(true);
            client?.CloseAsync( WebSocketCloseStatus.NormalClosure, "quit", CancellationToken.None);
            client.Dispose();
        }

       
    }
}
