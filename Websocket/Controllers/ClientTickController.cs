using Microsoft.AspNetCore.Mvc;
using Websocket.BackGround;

namespace Websocket.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ClientTickController : ControllerBase
    {
        private readonly BeatHeartClient _client;

        public ClientTickController(BeatHeartClient client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Start()
        {
            _client.Start();
            return Ok();
        }
        [HttpGet]
        public IActionResult Stop()
        {
            _client.Stop();
            return Ok();
        }
    }
}
