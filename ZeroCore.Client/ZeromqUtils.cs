
using YSAI.Model.@interface;
using YSAI.NetMQ;
using YSAI.Unility;

public class ZeromqUtils
{
    NetMQOperate _netsub;
    NetMQOperate _netpub;
    public void Init()
    {
        _netsub = new NetMQOperate(new NetMQData.Basics
        {
            Address = "tcp://localhost:5578",
            RT = YSAI.Model.@enum.ResponseType.Content,
            UModel = NetMQData.UseModel.SubModel
        });
        _netpub = new NetMQOperate(new NetMQData.Basics
        {
            Address = "tcp://localhost:5578",
            RT = YSAI.Model.@enum.ResponseType.Content,
            UModel = NetMQData.UseModel.PubModel
        });
        _netsub.On();
        _netsub.Subscribe("hello");
        _netsub.OnDataEvent += _netsub_OnDataEvent;
        _netpub.On();
    }

    public void Produce()
    {
        _netpub.Produce("hello", "abc");
    }

    private void _netsub_OnDataEvent(object? sender, YSAI.Model.data.EventDataResult e)
    {
        Console.WriteLine(e.ToJson());
    }

}

