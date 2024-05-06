// See https://aka.ms/new-console-template for more information
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansIGrain;
using System.Collections.Concurrent;
using System.ComponentModel;
using YSAI.Core.extendMethod;
using YSAI.Model.data;
using YSAI.Opc.ua.client;
using YSAI.OPCUA.KepServer;

public static class StaticVariable
{
    public static String SimpleMessageStream = "OrleansStream";
}
public class Program
{

    private static int i = 0;

    private static async Task<IClusterClient> OrleansRun()
    {
        var builder = new ClientBuilder()
                .UseConsulClustering(options =>
                {
                    options.KvRootFolder = "OrleansCluster";
                    options.AclClientToken = "abc";
                    options.Address = new Uri("http://127.0.0.1:8600");
                })
                .Configure<ClusterOptions>(option =>
                 {
                        option.ClusterId = "hello";
                        option.ServiceId = "hello";
                 })
            .AddSimpleMessageStreamProvider(StaticVariable.SimpleMessageStream)
           .Build();
        await builder.Connect();
        return builder;
    }
    private static IMutiBoard grain;
    private static IMultiReceiverGrain grainrcv;
    private static async Task Register()
    {
        var client = await OrleansRun();
        ///0.Server和link配置项管理。
        ///1.发送
        grain = client.GetGrain<IMutiBoard>(Guid.NewGuid());
        ///2.消费
        grainrcv = client.GetGrain<IMultiReceiverGrain>(Guid.NewGuid());
    }

    private static async Task Main(string[] args)
    {
        await Register();
        Console.WriteLine("Hello, Client!");
        while (true)
        {
            await Console.Out.WriteLineAsync($"进入发布时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            var list = new List<AddressValueSimplify>();
            for (int i = 0; i < 100000; i++)
            {
                var valur = Guid.NewGuid().ToString();
                list.Add(new AddressValueSimplify() { Id = valur, Address = "A" + valur, DataType = YSAI.Model.@enum.DataType.String, QL = 1, Ts = DateTime.Now, VL = Guid.NewGuid() });
            }
            await grain.CreateStream(list);
            await Console.Out.WriteLineAsync($"发布结束时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
        }

        //ConcurrentQueue<(IEnumerable<AddressValueSimplify> data, string index)> queueData = new ConcurrentQueue<(IEnumerable<AddressValueSimplify> data, string index)>();

        //var opc = new OpcUaClientOperate(new OpcUaClientData.Basics
        //{
        //    AType = YSAI.Opc.core.Data.AuType.Anonymous,
        //    ServerUrl = "opc.tcp://kloszhu:1233/zx",
        //    PublishingInterval = 100,
        //});


        //object locks = new object();
        //int j = 0;
        //opc.On();
        //var listdata = KepServerManager.Instance.GetAll();
        //opc.Subscribe(new Address { AddressArray = listdata });
        //opc.OnDataEvent += Opc_OnEvent;

        /////1.服务器接入测试。
        /////2.单机接入数据测试。
        //async void Opc_OnEvent(object? sender, EventDataResult e)
        //{
        //    Task.Run(() =>
        //    {
        //        IEnumerable<AddressValueSimplify>? pairs = e.GetRData<ConcurrentDictionary<string, AddressValue>>()?.GetSimplifyArray();
        //        queueData.Enqueue((pairs, Guid.NewGuid().ToString()));
        //    });
        //}

        //int AllSum = 0;
        //Task.Run(() =>
        //{
        //    while (true)
        //    {
        //        while (queueData.TryDequeue(out (IEnumerable<AddressValueSimplify> data, string index) qd))
        //        {
        //            grain.CreateStream(qd.data.ToList());
        //        }
        //    }
        //});


        //while (true)
        //{
        //    string a = Console.ReadLine();
        //    if (a.StartsWith("c"))
        //    {
        //        Console.WriteLine(opc.GetStatus());
        //    }
        //    if (a.StartsWith("a"))
        //    {
        //        Console.WriteLine("订阅总数：" + AllSum.ToString());
        //    }
        //    if (a.StartsWith("b"))
        //    {
        //        AllSum = 0;
        //        Console.WriteLine("订阅总数清零");
        //    }
        //}
    }
    Task DealData(EventDataResult e)
    {

        if (e.RData is ConcurrentDictionary<string, AddressValue>)
        {
            ///序列化
            ConcurrentDictionary<string, AddressValue>? pairs = e.GetRData<ConcurrentDictionary<string, AddressValue>>();
            //Thread.Sleep(10);
            //queue.Enqueue(pairs);
            i++;
            Console.WriteLine(i.ToString());

            //if (pairs != null)
            //{
            //    foreach (var item in pairs)
            //    {
            //        item.Value.AddressAnotherName=i.ToString();
            //    }
            //        /////批量Orleans数据消费
            //        grain1.CreateStream(pairs.Values.ToList());
            //    foreach (var itemc in pairs)
            //    {
            //        ///单条Orleans数据消费
            //        grain.CreateStream(itemc.Value);
            //    }
            //    Console.WriteLine("加锁处理业务次数"+i.ToString());
            //    i++;
            //}
        }


        return Task.CompletedTask;
    }

}