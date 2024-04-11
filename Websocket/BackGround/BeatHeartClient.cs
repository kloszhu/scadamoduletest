using Microsoft.Extensions.Options;
using System;
using Websocket.Cache;
using Websocket.Model;
using WebsocketClient;

namespace Websocket.BackGround
{
    public class BeatHeartClient:BackgroundService
    {
        public WebSocketClient Client;
        private CancellationToken cancellationToken;
        private CacheVote _cache;
        private IOptions<WebsocketModel> _options;

        private int index = 0;

        public void Start()
        {
           
            cancellationToken = CancellationToken.None;
            Excute();
        }
        public void Stop()
        {
            cancellationToken=new CancellationToken(true);
        }
        public BeatHeartClient(WebSocketClient client, CacheVote cache, IOptions<WebsocketModel> options)
        {
            Client = client;
            this._cache = cache;
            index = 0;
            cancellationToken=CancellationToken.None;
            _options = options;
        }

        public async Task BitHeart() {
          await  Client.BitHeart();
        }

        public Task Excute()
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (Client.GetMsgConnect() == 0)
                    {//客户端未开始
                       await Client.Start(_options.Value.Main);
                    }
                    if (Client.GetMsgConnect() == -1)
                    {
                        index++;
                        await Console.Out.WriteLineAsync(index.ToString());
                        await Client.Start(_options.Value.Main);
                        if (index > 2)
                        {
                            
                            if (_cache.GetVote() != null)
                            {
                                await Client.Start(_cache.GetVote());
                            }
                        }
                    }
                    Thread.Sleep(3000);
                }
            });
            return Task.CompletedTask;

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
               await BitHeart();
            }
        }
    }
}
