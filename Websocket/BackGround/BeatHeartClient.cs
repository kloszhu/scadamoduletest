using Microsoft.Extensions.Options;
using System;
using Websocket.Cache;
using Websocket.Model;
using WebsocketClient;

namespace Websocket.BackGround
{
    public class BeatHeartClient
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

        public Task Excute()
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (Client.GetMsgConnect() == 0)
                    {//客户端未开始
                        Client.Start(_options.Value.Main);
                    }
                    if (Client.GetMsgConnect() == -1)
                    {
                        index++;
                        if (index > 10)
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
    }
}
