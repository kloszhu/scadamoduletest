namespace Websocket.Model
{
     //"Host": "localhost",
     // "Port": 5011,
     // "Main": true
    public class ConnectHost
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsMain { get; set; }
        public bool IsOnLine { get; set; }
        public int UpdateTime { get;  set; }
    }
}