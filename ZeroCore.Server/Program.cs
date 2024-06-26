﻿// See https://aka.ms/new-console-template for more information
using YSAI.NetMQ;
using YSAI.Unility;

var _netmq = new NetMQOperate(new NetMQData.Basics
{
    Address = "tcp://localhost:5578",
    RT = YSAI.Model.@enum.ResponseType.Content,
     UModel= NetMQData.UseModel.PubModel
});



while (true)
{
    var stat = _netmq.On();
    var abc= _netmq.GetStatus();
    if (abc.Status == true)
    {
        Console.WriteLine("Server 连接成功");
        _netmq.Produce("hello", "你好");
        Thread.Sleep(1000);
    }
}


Console.WriteLine("Hello, World!");
