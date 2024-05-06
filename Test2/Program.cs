// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;
using OpcDaNetApi.Hda;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Test2;
using YSAI.Core.extendMethod;
using YSAI.Model.data;
using YSAI.Opc;
using YSAI.Unility;


Console.WriteLine("Hello, World!");




string result = String.Empty;
using (StreamReader reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "File.txt")))
{
    result = reader.ReadToEnd();
}
string[] array = result.Split("\r\n");
var listdata = array.Select(a => new AddressDetails
{
    AddressName = a,
    AddressDataType = YSAI.Model.@enum.DataType.Float,
    AddressType = YSAI.Model.@enum.AddressType.Reality
});
var ins = new YSAI.Opc.ua.client.OpcUaClientOperate(new YSAI.Opc.ua.client.OpcUaClientData.Basics
{
    AType = YSAI.Opc.core.Data.AuType.Anonymous,
    ServerUrl = "opc.tcp://192.168.2.78:49320"
});
RMQ.INIT();
ConcurrentQueue<IEnumerable<AddressValueSimplify>> queue = new    ConcurrentQueue<IEnumerable<AddressValueSimplify>>();
object locks = new object();
ins.On();
ins.OnDataEvent += Ins_OnDataEvent;

ins.Subscribe(new YSAI.Model.data.Address
{
    AddressArray = listdata.ToList(),

});
while (true) {

    
    while (queue.TryDequeue(out IEnumerable<AddressValueSimplify> data))
    {
        string json= data.ToJson();
        Console.WriteLine(json);
        RMQ.Send("abc", json);
    }

    Thread.Sleep(10);
}

void Ins_OnDataEvent(object? sender, YSAI.Model.data.EventDataResult e)
{
    //lock (locks)
    //{
    //    var simplifies = e.GetRData<ConcurrentDictionary<string, AddressValue>>()?.GetSimplifyArray();
    //RMQ.Send("abc", JsonConvert.SerializeObject(simplifies));
    //}
    IEnumerable<AddressValueSimplify> simplifies = e.GetRData<ConcurrentDictionary<string, AddressValue>>()?.GetSimplifyArray();
    queue.Enqueue(simplifies);
}

Console.ReadLine();