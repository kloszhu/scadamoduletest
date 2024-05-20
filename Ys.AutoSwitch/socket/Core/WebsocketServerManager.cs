using Ys.AutoSwitch.Proto;
using Ys.AutoSwitch.socket.Core;
using YS.Models.Auto.Config;

namespace Ys.AutoSwitch.socket
{
    public class WebsocketServerManager
    {
        private DateTime _lastUpdateTime = DateTime.MinValue;
        private int duration = 0;
        public string Prefix { get; set; } = "hub";
        SocketServer server;
        ActionHandler _ServerHandler;
        private AutoSwitchJsonConfig _JsonConfig;
        private ServerDataManager _dataManager;
        public WebsocketServerManager(ActionHandler handler, AutoSwitchJsonConfig config)
        {
            _ServerHandler = handler;
            handler.Callback1 = MsgCallBack;
            _JsonConfig = config;
            _dataManager = new ServerDataManager(_ServerHandler);
            server = new SocketServer(_dataManager);
        }



        private async void MsgCallBack(PingPackMSG callbackmsg)
        {
            _lastUpdateTime = new DateTime(callbackmsg.Ts);
            ///如果是主服务器，则发送pong，
            string uid = callbackmsg.UID;
            var msg = new SwitchMsg();
            msg.MsgType = MsgType.PongType;
            msg.Pongpack = new PongPackMSG
            {
                UID = uid,
                Host = _JsonConfig.Host,
                Port = _JsonConfig.Port,
                Ts = DateTime.Now.Ticks
            };
            await server.SendAll(msg);
        }


        public async Task InitServer(int Port)
        {
            var msgmanager = new ServerMessageHandler();
            var Data = new ServerDataManager(msgmanager);
            server = new SocketServer(Data);
            await server.Start("127.0.0.1", Port, Prefix);
        }

        public Task Stop()
        {
            return server.Stop();
        }

       
    }
}
