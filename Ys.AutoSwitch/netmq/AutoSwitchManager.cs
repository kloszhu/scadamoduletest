using Microsoft.Extensions.Configuration;
using Ys.AutoSwitch.Proto;
using YS.Models.Auto.Config;
using YSAI.NetMQ;

namespace Ys.AutoSwitch.netmq
{
    public class AutoSwitchManager
    {
        private NetManager _manager;
        //private IConfiguration _config;
        public AutoSwitchManager(NetManager manager)
        {
            _manager = manager;
            _manager.MsgAction = MsgReceive;
            //_config = config;
            //_config.GetSection("AutoSwitch").Bind(_config);
        }

        public void InitSelf()
        {
            _manager.CreateSub("def");
            _manager.CreatePub("abc", new NetConfig { Host = "127.0.0.1", Port = 30802 });

        }
        public void Send()
        {
            _manager.Send("abc", new SwitchMsg
            {
                MsgType = MsgType.PingType,
                Pingpack = new PingPackMSG
                {
                    Ts = DateTime.Now.Ticks,
                    UID = "abc"
                }
            });
        }
        public void MsgReceive(SwitchMsg msg)
        {
            Console.WriteLine(msg.MsgType);
        }
    }
}
