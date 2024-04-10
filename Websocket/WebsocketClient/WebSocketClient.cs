using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Websocket.Cache;
using Websocket.Model;

namespace WebsocketClient
{
    public class WebSocketClient
    {
        public Uri Uri;
        public ClientWebSocket clientWebSocket;
        private WebsocketModel CurrentData;
        private IOptions<WebsocketModel> _options;
        private readonly CacheVote _cacheVote;
        private CancellationToken cancellationToken;
        public WebSocketClient(IOptions<WebsocketModel> options,CacheVote cacheVote)
        {
            _options = options;
            _cacheVote = cacheVote;
        }

        public async Task Start(ConnectHost connect)
        {
            if (clientWebSocket!=null)
            {
                clientWebSocket.Dispose();
            }
            clientWebSocket = new ClientWebSocket();
            Uri = new Uri($"ws://{connect.Host}:{connect.Port}/hub");
            cancellationToken =CancellationToken.None;
            await ConnectAndSendReceiveAsync();
        }
        public void Stop()
        {
            cancellationToken = new CancellationToken(true);
        }

        public async Task ConnectAndSendReceiveAsync()
        {
            // Connect to the server
            await clientWebSocket.ConnectAsync(Uri, cancellationToken);
            Console.WriteLine("Connected to Server");
            // Receive messages from server
            while (clientWebSocket.State == WebSocketState.Open)
            {

                var receiveBuffer = new ArraySegment<byte>(new byte[1024]);
                var result = await clientWebSocket.ReceiveAsync(receiveBuffer, cancellationToken);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = System.Text.Encoding.UTF8.GetString(receiveBuffer.Array, 0, result.Count);
                    var pkg= JsonConvert.DeserializeObject<MsgPack>(message);
                    switch (pkg.action)
                    {
                        case ActionEnum.ServerBitHeart:
                            var pack = new MsgPack { action= ActionEnum.ClintBitHeart };
                            pack.Msg = JsonConvert.SerializeObject(new BitHeart
                            { 
                                Host = _options.Value.Main.Host,
                                Port = _options.Value.Main.Port,
                                Ts = DateTime.Now
                            });
                            var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(pack));
                            clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, cancellationToken);
                            break;
                        case ActionEnum.ClintBitHeart:
                            var packs=new MsgPack { action=ActionEnum.ClintBitHeart };
                            packs.Msg = JsonConvert.SerializeObject(_options.Value.Local);
                            var buffers = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(packs));
                            clientWebSocket.SendAsync(new ArraySegment<byte>(buffers), WebSocketMessageType.Text, true, cancellationToken);
                            break;
                        case ActionEnum.ServerSelect:
                            break;
                        case ActionEnum.Vote:
                            _cacheVote.SetVote(vote: JsonConvert.DeserializeObject<ConnectHost>(pkg.Msg));
                            break;
                        case ActionEnum.ClientSelect:
                            break;
                        default:
                            break;
                    }
                }
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                }
                else
                {
                    var message = System.Text.Encoding.UTF8.GetString(receiveBuffer.Array, 0, result.Count);
                    Console.WriteLine("Received message: " + message);
                }

            }            
        }

        public async Task BitHeart(CancellationToken cancellationToken)
        {
            // Send message to server
            string sendMessage = "Hello, Server!";
            var sendBuffer = System.Text.Encoding.UTF8.GetBytes(sendMessage);
            await clientWebSocket.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, cancellationToken);
            Console.WriteLine("Message sent to server");
        }
        protected async Task ExecuteAsync()
        {
            if (clientWebSocket.State != WebSocketState.Open)
            {
                await ConnectAndSendReceiveAsync();
            }
            else
            {
                await BitHeart(cancellationToken);
            }
            Thread.Sleep(1000);
        }
    }
}
