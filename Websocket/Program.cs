
using System.Net.WebSockets;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Websocket.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Websocket.BackGround;
using Websocket.Extentions;

namespace Websocket
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddWebSocketManager();
            builder.Services.Configure<WebsocketModel>(builder.Configuration.GetSection("WebsocketConfig"));
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            var app = builder.Build();
            var webmodel = builder.Configuration.GetSection("WebsocketConfig").Get<WebsocketModel>();
            builder.WebHost.UseUrls($"http://{webmodel.Local.Host}:{webmodel.Local.Port}");
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}
            app.UseAuthorization();
            app.MapWebSocketManager("/hub");
            app.MapControllers();
            app.Run();
        }

    }
}
