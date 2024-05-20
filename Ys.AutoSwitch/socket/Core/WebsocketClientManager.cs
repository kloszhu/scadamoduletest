using System.Net.WebSockets;
using Ys.AutoSwitch.Proto;
using Ys.AutoSwitch.socket.Core;
using Ys.AutoSwitch.socket.Models;

namespace Ys.AutoSwitch.socket
{
    public class WebsocketClientManager
    {
        ActionHandler _ClientHandler;
        public WebsocketClientManager(ActionHandler handler)
        {
            _ClientHandler = handler;
        }
        public string Prefix { get; set; } = "hub";
        SocketClient client;
        public async Task InitClient(DistHostInfo dist)
        {
            var clientsmgmanager = new ClientMessageHandler();
            var Datas = new ClientDataManager(clientsmgmanager);
            client = new SocketClient(Datas);
            await client.Start(dist.Host, dist.Port, Prefix);
        }

        public async Task Stop()
        {
            await client.Stop();
        }


        public async Task ClientPing(string Uid, string Host, int Port)
        {
            var msg = new SwitchMsg
            {
                MsgType = MsgType.PingType,
                Pingpack = new PingPackMSG
                {
                    UID = Uid,
                    Host = Host,
                    Port = Port,
                    Ts = DateTime.Now.Ticks
                }
            };
            await client.Send(msg, false, System.Net.WebSockets.WebSocketMessageType.Binary);
        }

        public async Task Tongbu(List<DistHostInfo> info)
        {
            var msg = new SwitchMsg
            {
                MsgType = MsgType.TongBuType,
                TongBu = new TongBu()
            };
            var list = info.Select(a => new ServerInfo
            {
                Code = a.Code,
                Host = a.Host,
                Port = a.Port,
                Uid = a.Uid,
                Name = a.Name,
                DeadBand = a.DeadBand,
                IsEnable = a.IsEnable,
                IsMain = a.IsMain,
                Status = a.Status,
                PingTime = a.PingTime
            });
            msg.TongBu.ServerInfo.AddRange(list);
            await client.Send(msg, false, System.Net.WebSockets.WebSocketMessageType.Binary);
            //await client.Stop();
        }


        public async Task ChangeClient(DistHostInfo dist)
        {
            await client.Stop();
            await client.Start(dist.Host, dist.Port, Prefix);
        }

    }
}
