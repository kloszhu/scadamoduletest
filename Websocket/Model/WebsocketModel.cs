namespace Websocket.Model
{
    public class WebsocketModel
    {
        public ConnectHost Main { get; set; }
        public List<ConnectHost> Backup { get; set; }
        public ConnectHost Local { get;  set; }
    }
}
