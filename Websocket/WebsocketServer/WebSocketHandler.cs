using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using Websocket.BackGround;
using Websocket.Cache;
using Websocket.Model;

namespace Websocket.Manager
{
    public class WebSocketHandler
    {
        private readonly WebSocketConnectionManager _connectionManager;
        private readonly ILogger<WebSocketHandler> _logger;
        private readonly IOptions<WebsocketModel> _options;
        public WebSocketHandler(WebSocketConnectionManager connectionManager
            , ILogger<WebSocketHandler> logger
            , IOptions<WebsocketModel> options
            )
        {
            _connectionManager = connectionManager;
            _logger = logger;
            _options = options;
        }
        private int index = 0;
        public async Task HandleWebSocket(HttpContext context, Func<Task> TaskRun, string name)
        {
            context.Request.EnableBuffering();
            if (context.Request.Path == name)
            {
                if (!context.WebSockets.IsWebSocketRequest)
                {
                    return;
                }
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                var socketId = _connectionManager.AddSocket(webSocket);
                
                _logger.LogInformation($"WebSocket 已连接 with ID {socketId}");

                while (webSocket.State == WebSocketState.Open)
                {
                    
                    var message = await ReceiveMessageAsync(webSocket);
                    if (message != null)
                    {
                        _logger.LogInformation($"接受数据 from ID {socketId}: {message}");
                        //await BroadcastMessageAsync(message);
                        //更新我的列表状态。
                        var pkg = Newtonsoft.Json.JsonConvert.DeserializeObject<MsgPack>(message);
                        switch (pkg.action)
                        {
                            case ActionEnum.ServerBitHeart:
                                await Console.Out.WriteLineAsync("不应该进来");
                                break;
                            case ActionEnum.ClintBitHeart:
                                var package = JsonConvert.DeserializeObject<ConnectHost>(pkg.Msg);
                                _connectionManager.SetSocketId(socketId, package, _options.Value.Backup);
                                break;
                            case ActionEnum.ServerSelect:
                                break;
                            case ActionEnum.Vote:
                                
                                break;
                            case ActionEnum.ClientSelect:
                                break;
                            default:
                                break;
                        }
                    }
                }

                _connectionManager.RemoveSocket(socketId);
                _logger.LogInformation($"WebSocket 连接关闭 with ID {socketId}");
            }
            TaskRun();
        }

        public async Task<string?> ReceiveMessageAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.CloseStatus.HasValue)
            {
                return null;
            }

            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

        public async Task BroadcastMessageAsync(string message)
        {
            foreach (var socket in _connectionManager.GetAllSockets())
            {
                if (socket.Value.socket.State == WebSocketState.Open)
                {
                    await socket.Value.socket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }


    }
}
