using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Websocket.BackGround;
using Websocket.Cache;
using Websocket.Model;
using WebsocketClient;

namespace Websocket.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ServerTickController:ControllerBase
    {
        private IOptions<WebsocketModel> _options;
        private readonly BeatHeartServer beatHeart;
        private readonly WebSocketClient client;
        private readonly CacheVote cache;

        public ServerTickController(IOptions<WebsocketModel> options, BeatHeartServer beatHeart, WebSocketClient client, CacheVote cache)
        {
            _options = options;
            this.beatHeart = beatHeart;
            this.client = client;
            this.cache = cache;
        }

        [HttpGet]
        public IActionResult Start()
        {
            beatHeart.Start();
            return Ok();
        }
        [HttpGet]
        public IActionResult Stop()
        {
            beatHeart.Stop();
            return Ok();
        }

        [HttpGet]
        public IActionResult StartClient() {
            client.Start(_options.Value.Main);
            return Ok();
        }
        [HttpGet]
        public IActionResult StopClient() {
            client.Stop();
            return Ok();
        }
        [HttpGet]
        public IActionResult ChangeClient()
        {
            client.Start(cache.GetVote());
            return Ok();
        }

        [HttpGet]
        public IActionResult GetChangeClient()
        {
            return Ok(cache.GetVote());
        }
    }
}
