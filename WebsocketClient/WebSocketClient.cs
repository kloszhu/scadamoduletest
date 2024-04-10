using Microsoft.Extensions.Hosting;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace WebsocketClient
{
    public class WebSocketClient:BackgroundService
    {
        public Uri Uri;
        public ClientWebSocket clientWebSocket;

        public WebSocketClient()
        {
            Uri = new Uri("ws://localhost:5041/hub");
            clientWebSocket = new ClientWebSocket();
        }

        public async Task ConnectAndSendReceiveAsync(CancellationToken cancellationToken)
        {
            // Connect to the server
            await clientWebSocket.ConnectAsync(Uri, cancellationToken);
            Console.WriteLine("Connected to server");

    

            // Receive messages from server
            while (clientWebSocket.State == WebSocketState.Open)
            {
                var receiveBuffer = new ArraySegment<byte>(new byte[1024]);
                var result = await clientWebSocket.ReceiveAsync(receiveBuffer, cancellationToken);
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

        public async Task BitHeart(CancellationToken cancellationToken) {
            // Send message to server
            string sendMessage = "Hello, Server!";
            var sendBuffer = System.Text.Encoding.UTF8.GetBytes(sendMessage);
            await clientWebSocket.SendAsync(new ArraySegment<byte>(sendBuffer), WebSocketMessageType.Text, true, cancellationToken);
            Console.WriteLine("Message sent to server");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (clientWebSocket.State!= WebSocketState.Open)
            {
                await ConnectAndSendReceiveAsync(stoppingToken); 
            }else
            {
               await BitHeart(stoppingToken);
            }
            Thread.Sleep(1000);
        }
    }
}
