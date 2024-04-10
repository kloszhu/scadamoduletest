using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Websocket.Manager;
using Websocket.Model;

namespace Websocket.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WebsoketController : ControllerBase
    {
        private readonly WebSocketHandler _webSocketHandler;
        private readonly WebsocketMessageManager _messageManager;
        private IConfiguration _options;
        public WebsoketController(WebSocketHandler webSocketHandler, WebsocketMessageManager messageManager, IConfiguration configuration)
        {
            _webSocketHandler = webSocketHandler;
            _messageManager= messageManager;
            _options= configuration;
        }

        [HttpGet]
        public Task<WebsocketModel> HostConfig()
        {
            return Task.FromResult(_options.GetSection("WebsocketConfig").Get<WebsocketModel>());
        }

        [HttpGet]
        public async Task<IActionResult> GetSocketSessions()
        {
            
            return Ok(_messageManager.GetClient());
        }
        [HttpGet]
        public async Task<IActionResult> GetSocketData()
        {

            return Ok(_messageManager.GetClientData());
        }
        [HttpGet]
        public async Task<IActionResult> SetMessage(string id,string message)
        {

            return Ok(_messageManager.SendMessage(Guid.Parse(id),message));
        }
    }
}
