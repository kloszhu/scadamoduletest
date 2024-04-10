using Microsoft.AspNetCore.Mvc;
using Websocket.BackGround;

namespace Websocket.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TickController:ControllerBase
    {

        private readonly BeatHeartServer beatHeart;

        public TickController(BeatHeartServer beatHeart)
        {
            this.beatHeart = beatHeart;
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
    }
}
