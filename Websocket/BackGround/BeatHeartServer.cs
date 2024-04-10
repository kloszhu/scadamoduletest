
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Sockets;
using Websocket.Manager;
using Websocket.Model;

namespace Websocket.BackGround
{
    public class BeatHeartServer
    {
        private WebSocketHandler handler;
        private IOptions<WebsocketModel> _options;
        private IServiceScopeFactory serviceScopeFactory;
        private static CancellationToken stoppingToken;
        private static bool Status = false;
        public BeatHeartServer(WebSocketHandler handler, IOptions<WebsocketModel> options, IServiceScopeFactory serviceScopeFactory)
        {
            this.handler = handler;
            this._options = options;
            this.serviceScopeFactory = serviceScopeFactory;
            stoppingToken = CancellationToken.None;
            Status = false;
        }

        public void Start()
        {
            stoppingToken = CancellationToken.None;
            if (!Status)
            {
                ExecuteAsync();
            }

        }
        public void Stop()
        {
            stoppingToken = new CancellationToken(true);
            ExecuteAsync();
        }

        int index = 0;

        public async Task ExecuteAsync()
        {
            var _ = Task.Run(async () =>
             {
                 using (var scope = serviceScopeFactory.CreateScope())
                 {

                     while (!stoppingToken.IsCancellationRequested)
                     {
                         if (Status == false)
                         {
                             Status = true;
                         }
                         //心跳
                         await handler.BroadcastMessageAsync(JsonConvert.SerializeObject(new BitHeart
                         {

                             Host = _options.Value.Main.Host,
                             Port = _options.Value.Main.Port,
                             Ts = DateTime.Now
                         }));
                         Thread.Sleep(_options.Value.Main.UpdateTime);

                         //推举下一个接班人
                         if (index > 5)
                         {
                             index = 0;
                         }
                         else
                         {
                             var main = _options.Value.Main.Host;
                             var tuijian = _options.Value.Backup.Where(a=>a.IsMain==false).First();
                             var pkmsg = new MsgPack { action = ActionEnum.Vote };
                             pkmsg.Msg = JsonConvert.SerializeObject(tuijian);
                             await handler.BroadcastMessageAsync(JsonConvert.SerializeObject(pkmsg));
                             index++;
                         }
                     }
                     Status = false;
                 }
             });
        }


    }
}
