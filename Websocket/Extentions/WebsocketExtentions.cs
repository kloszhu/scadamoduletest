using Microsoft.AspNetCore.Builder;
using System.Net.WebSockets;
using Websocket.BackGround;
using Websocket.Cache;
using Websocket.Manager;
using WebsocketClient;

namespace Websocket.Extentions
{
    public static class WebsocketExtentions
    {


        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddSingleton<WebSocketHandler>();
            services.AddSingleton<WebSocketConnectionManager>();
            services.AddSingleton<WebsocketMessageManager>();
            services.AddSingleton<BeatHeartServer>();
            services.AddSingleton<WebSocketClient>();
            services.AddSingleton<CacheVote>();
            return services;
        }

        public static async void MapWebSocketManager(this WebApplication app, string ControllerName)
        {
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120)
            };
            app.UseWebSockets(webSocketOptions);
            var service = app.Services.GetService<WebSocketHandler>();
            app.Use((c, m) => service.HandleWebSocket(c, m, ControllerName));
        }


    }
}
