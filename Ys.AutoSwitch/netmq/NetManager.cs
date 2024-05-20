using Google.Protobuf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using Ys.AutoSwitch.Proto;
using YS.Models.Auto.Config;
using YSAI.NetMQ;
using YSAI.Unility;

namespace Ys.AutoSwitch.netmq
{
    public class NetManager
    {
        //public ConcurrentDictionary<string,Simple>
        private AutoSwitchJsonConfig _config;
        public Action<SwitchMsg> MsgAction;
        //private IConfiguration _configuration;
        public ConcurrentQueue<SwitchMsg> Msgs { get; set; }
        ConcurrentDictionary<string, NetMQOperate> Data;
        public NetMQOperate _Sub;
        public NetManager(IOptions<AutoSwitchJsonConfig> options)
        {
            _config = options.Value;
            //_configuration = configuration;
            //_configuration.GetSection("AutoSwitch").Bind(_config);
            Data = new ConcurrentDictionary<string, NetMQOperate>();
            //Msgs = new ConcurrentQueue<SwitchMsg>();
        }


        public void CreateSub(string Uid)
        {
            if (_Sub == null)
            {
                _Sub = new NetMQOperate(new NetMQData.Basics
                {
                    Address = $"tcp://*:{_config.Port}",
                    RT = YSAI.Model.@enum.ResponseType.Bytes,
                    UModel = NetMQData.UseModel.SubModel
                });
            }
            else
            {
                _Sub?.Dispose();
                _Sub = new NetMQOperate(new NetMQData.Basics
                {
                    Address = $"tcp://*:{_config.Port}",
                    RT = YSAI.Model.@enum.ResponseType.Bytes,
                    UModel = NetMQData.UseModel.SubModel
                });
            }
            _Sub.On();
            _Sub.Subscribe(_config.Topic);
            _Sub.OnDataEvent += _Sub_OnDataEvent;
            Data.TryAdd(Uid, _Sub);
        }

        private void _Sub_OnDataEvent(object? sender, YSAI.Model.data.EventDataResult e)
        {
            if (e.RData is byte[] bytes)
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    SwitchMsg message = SwitchMsg.Parser.ParseFrom(stream);
                    MsgAction?.Invoke(message);
                }
            }
        }
        public void Send(string Uid, SwitchMsg switchMsg)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                switchMsg.WriteTo(stream);
                // 序列化后的字节数组
                byte[] bytes = stream.ToArray();
                Data[Uid].Produce(_config.Topic, switchMsg.ToByteArray());
            }
        }

        public void Dispose(string uid)
        {
            Data.Remove(uid, out var operate);
            operate.Dispose();
        }




        public void CreatePub(string Uid, NetConfig config)
        {
            var item = new NetMQOperate(new NetMQData.Basics
            {
                Address = $"tcp://{config.Host}:{config.Port}",
                RT = YSAI.Model.@enum.ResponseType.Bytes,
                UModel = NetMQData.UseModel.PubModel
            });
            item.On();
            Data.TryAdd(Uid, item);
        }



    }
}
