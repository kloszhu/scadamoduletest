// See https://aka.ms/new-console-template for more information
using YSAI.Unility;
using YSAI.Core;
using YSAI.Model.data;
using Newtonsoft.Json;
using System.Collections.Generic;
Console.WriteLine("Hello, World!");
string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.log");

YSAI.RabbitMQ.RabbitMQOperate rmq = new YSAI.RabbitMQ.RabbitMQOperate(new YSAI.RabbitMQ.RabbitMQData.Basics { 
 ExChangeName= "exchang", HostName="192.168.2.221", UserName="ys", 
    Password="ys123456", Port=5672, RT=YSAI.Model.@enum.ResponseType.Content
});
//5Wtest
rmq.On();
rmq.OnDataEvent += Rmq_OnDataEvent;
rmq.Subscribe("abc");
void Rmq_OnDataEvent(object? sender, YSAI.Model.data.EventDataResult e)
{

    //Console.WriteLine(e.GetMessage());
    //JsonConvert.DeserializeObject<List<AddressValueSimplify>>(e.GetMessage());
    //Write(e.Message);
   // var simplifies = e.GetRData<IEnumerable<AddressValueSimplify>>();
    var simplifies = e.RData.ToString().ToJsonEntity<IEnumerable<AddressValueSimplify>>();
    //Console.WriteLine(simplifies.ToJson());
    Console.WriteLine(simplifies.ToJson().Length);
}

Console.ReadLine();